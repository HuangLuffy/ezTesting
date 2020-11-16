using System;
using CommonLib.Util.IO;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommonLib.Util;

namespace ReportLib
{
    public class ReporterXsl : Reporter, IReporter
    {
        private XDocument XDoc { get; set; }
        private string PathReportXml { get; }
        private string CaptureRelativePath { set; get; }
        private Reporter.ResultTestInfo ResultTestInfo { get; }
        public ReporterXsl(string pathReportXml, string xslPath = "", string captureRelativePath = "", Reporter.ResultTestInfo resultTestInfo = null)
        {
            PathReportXml = pathReportXml;
            if (!File.Exists(PathReportXml))
            {
                CreateResultXml(xslPath: xslPath);
            }
            ResultTestInfo = resultTestInfo;
            CaptureRelativePath = captureRelativePath;
        }
        public string GetResultFullPath()
        {
            return PathReportXml;
        }
        public void SetCaptureRelativePath(string captureRelativePath)
        {
            CaptureRelativePath = captureRelativePath;
        }
        public string GetCaptureRelativePath()
        {
            return CaptureRelativePath;
        }
        public ResultTestInfo GetResultTestInfo()
        {
            return ResultTestInfo;
        }
        public string SetAsLink(string link)
        {
            return $"#$#{link}#$#{SetNewLine("")}";
        }
        public string SetNewLine(string line)
        {
            return $"{line}~!~";
        }
        public string SetAsLines(params string[] lines)
        {
            var addNumber = !lines[0].StartsWith("1.");
            var wholeLine = "";
            for (var i = 0; i < lines.Length; i++)
            {
                if (addNumber)
                {
                    wholeLine += SetNewLine($"{i + 1} {lines[i]}");
                }
            }
            return wholeLine;
        }
        public string SetNeedToCheck(string comment, string link)
        {
            return $"{comment}@@@{link}";
        }

        private void CreateResultXml(string xslName = "xmlReport.xsl", string xslPath = "")
        {
            if (!xslPath.Equals(""))
            {
                if (!xslPath.Contains("."))
                {
                    xslPath = Path.Combine(xslPath, xslName);
                }
                File.Copy(xslPath, Path.Combine(Path.GetDirectoryName(PathReportXml), xslName), false);
            }
            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XProcessingInstruction("xml-stylesheet", $"type='text/xsl' href='{xslName}'"),
                new XElement(
                    NodeTestsuite,
                    new XAttribute(AttrProject, "project"),
                    new XAttribute(AttrTestName, "testName"),
                    new XAttribute(AttrOs, "os"),
                    new XAttribute(AttrLanguage, "language"),
                    new XAttribute(AttrTime, "time"),
                    new XAttribute(AttrDeviceModel, "deviceModel"),
                    new XAttribute(AttrDeviceName, "deviceName"),
                    new XAttribute(AttrRegion, "region"),
                    new XAttribute(AttrTests, "tests"),
                    new XAttribute(AttrVersion, "version"),
                    new XAttribute(AttrName, "name"),

                    new XAttribute(AttrBlocks, "blocks"),
                    new XAttribute(AttrBlocksPercent, "blocksPercent"),
                    new XAttribute(AttrErrors, "errors"),
                    new XAttribute(AttrErrorsPercent, "errorsPercent"),
                    new XAttribute(AttrFailures, "failures"),
                    new XAttribute(AttrFailsPercent, "failsPercent"),
                    new XAttribute(AttrPasses, "passes"),
                    new XAttribute(AttrPassesPercent, "passesPercent"),
                    new XAttribute(AttrTbds, "tbds"),
                    new XAttribute(AttrTbdsPercent, "tbdsPercent")
                )
            );
            XDoc = xDoc;
            xDoc.Save(PathReportXml);
        }
        private XElement AssembleElement(ResultTestCase resultTestCase)
            //(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result)
        {
            return 
                new XElement(
                    NodeTestcase,
                    new XAttribute(AttrClassname, resultTestCase.AttrClassname??Reporter.DefaultContent),
                    new XAttribute(AttrTime, resultTestCase.AttrTime),
                    new XAttribute(AttrName, resultTestCase.AttrName ?? Reporter.DefaultContent),
                    new XElement(NodeStep, resultTestCase.NodeStepNumber),
                    new XElement(NodeDescription, resultTestCase.NodeDescription ?? Reporter.DefaultContent),
                   // new XElement(NodeErrorMessage, resultTestCase.NodeErrorMessage ?? Reporter.DefaultContent),
                    new XElement(NodeExpectedResult, resultTestCase.NodeExpectedResult ?? Reporter.DefaultContent),
                    new XElement(NodeNeedToCheck, resultTestCase.NodeNeedToCheck ?? ""),
                    new XElement(NodeResult, resultTestCase.NodeResult ?? Reporter.DefaultContent),
                    new XElement(
                        NodeFailure,
                        new XAttribute(AttrMessage, resultTestCase.AttrMessage ?? Reporter.DefaultContent),
                        new XAttribute(AttrType, Reporter.DefaultContent)
                    )
            );
        }
        public void AddTestStep(ResultTestCase resultTestCase, ResultTestInfo resultTestInfo = null)
        {
            resultTestInfo.AttrTotalCases += 1;
            resultTestCase.NodeStepNumber += 1;//new object every time, so it would always be 1 and do the next step.
            resultTestCase.NodeStepNumber = resultTestInfo.AttrTotalCases;
            var thisDoc = XDoc ?? XDocument.Load(PathReportXml);
            if (thisDoc?.Root != null)
            {
                var testCases = thisDoc.Root.Elements(NodeTestcase);
                var xElement = AssembleElement(resultTestCase);
                if (!testCases.Any())
                {
                    thisDoc.Root.Add(xElement);
                }
                else
                {
                    testCases.Last().AddAfterSelf(xElement);
                }
            }
            thisDoc?.Save(PathReportXml);
            if (resultTestInfo != null)
            {
                if (resultTestCase.NodeResult.Equals(Reporter.Result.FAIL))
                {
                    resultTestInfo.AttrFailures += 1;
                }
                else if (resultTestCase.NodeResult.Equals(Reporter.Result.BLOCK))
                {
                    resultTestInfo.AttrBlocks += 1;
                }
                else if (resultTestCase.NodeResult.Equals(Reporter.Result.PASS))
                {
                    resultTestInfo.AttrPasses += 1;
                }
                else if (resultTestCase.NodeResult.Equals(Reporter.Result.TBD))
                {
                    resultTestInfo.AttrTbds += 1;
                }
                resultTestInfo.AttrTime += resultTestCase.AttrTime;
                resultTestInfo.AttrPassesPercent = GetResultPercent(resultTestInfo.AttrPasses, resultTestInfo.AttrTotalCases, 1);
                resultTestInfo.AttrFailuresPercent = GetResultPercent(resultTestInfo.AttrFailures, resultTestInfo.AttrTotalCases, 1);
                resultTestInfo.AttrErrorsPercent = GetResultPercent(resultTestInfo.AttrErrors, resultTestInfo.AttrTotalCases, 1);
                resultTestInfo.AttrBlocksPercent = GetResultPercent(resultTestInfo.AttrBlocks, resultTestInfo.AttrTotalCases, 1);
                resultTestInfo.AttrTbdsPercent = GetResultPercent(resultTestInfo.AttrTbds, resultTestInfo.AttrTotalCases, 1);
                ModifyResultTestInfo(resultTestInfo);
            }
        }

        public void ModifyResultTestInfo(ResultTestInfo resultTestInfo)
            //(string project, string os, string language, string region, string time, string deviceModel, string deviceName, string testTotalNumber, string version, string name, string testName
            //, string testName, string testName, string testName, string testName, string testName)
        {
            var thisDoc = XDoc ?? XDocument.Load(PathReportXml);
            var rootElement = XDoc.Root;
            rootElement.Attribute(AttrProject).Value = resultTestInfo.AttrProject ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrTestName).Value = resultTestInfo.AttrTestName ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrOs).Value = resultTestInfo.AttrOs ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrLanguage).Value = resultTestInfo.AttrLanguage ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrTime).Value = resultTestInfo.AttrTime.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrDeviceModel).Value = resultTestInfo.AttrDeviceModel ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrDeviceName).Value = resultTestInfo.AttrDeviceName ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrRegion).Value = resultTestInfo.AttrRegion ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrTests).Value = resultTestInfo.AttrTotalCases.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrVersion).Value = resultTestInfo.AttrVersion ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrName).Value = resultTestInfo.AttrName ?? Reporter.DefaultContent;

            rootElement.Attribute(AttrBlocks).Value = resultTestInfo.AttrBlocks.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrBlocksPercent).Value = resultTestInfo.AttrBlocksPercent ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrErrors).Value = resultTestInfo.AttrErrors.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrErrorsPercent).Value = resultTestInfo.AttrErrorsPercent ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrFailures).Value = resultTestInfo.AttrFailures.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrFailsPercent).Value = resultTestInfo.AttrFailuresPercent ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrPasses).Value = resultTestInfo.AttrPasses.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrPassesPercent).Value = resultTestInfo.AttrPassesPercent ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrTbds).Value = resultTestInfo.AttrTbds.ToString() ?? Reporter.DefaultContent;
            rootElement.Attribute(AttrTbdsPercent).Value = resultTestInfo.AttrTbdsPercent ?? Reporter.DefaultContent;
            thisDoc.Save(PathReportXml);
        }
        public void Capture(Reporter.ResultTestCase r = null, string commentOnWeb = "Step_End", string imageName = "", string capturesRelativePath = "",
            UtilCapturer.ImageType imageType = UtilCapturer.ImageType.PNG)
        {
            if (imageName.Equals(""))
            {
                imageName = UtilTime.GetLongTimeString();
            }

            if (capturesRelativePath.Equals(""))
            {
                capturesRelativePath = this.GetCaptureRelativePath();
;            }
            var t = Path.Combine(capturesRelativePath, imageName);
            var manualCheckLink = SetNeedToCheck(commentOnWeb,
                Path.Combine(capturesRelativePath.Split('\\').Last(), imageName + "." + imageType));
            manualCheckLink = SetAsLink(manualCheckLink);
            UtilCapturer.Capture(t, imageType);
            if (r != null)
            {
                r.NodeNeedToCheck += manualCheckLink;
            }
        }
        public void Exec(Action action, string nodeDescription, string nodeExpectedResult, string nodeErrorMessage)
        {
            var r = new Reporter.ResultTestCase()
            {
                NodeDescription = nodeDescription,
                NodeExpectedResult = nodeExpectedResult,
            };
            if (Reporter.IsLastCaseFailed)
            {
                r.NodeResult = Reporter.Result.BLOCK;
            }
            else
            {
                try
                {
                    //var actualResult = action.Invoke();
                    action.Invoke();
                }
                catch (Exception)
                {
                    Reporter.IsLastCaseFailed = true;
                    r.NodeResult = Reporter.Result.FAIL;
                    r.AttrMessage = nodeErrorMessage;
                }
                Capture(r);
            }
            AddTestStep(r, ResultTestInfo);
        }
    }
}
