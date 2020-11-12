using System;
using CommonLib.Util.IO;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ReportLib
{
    public class ReporterXsl : Reporter, IReporter
    {
        private XDocument XDoc { get; set; }
        private string PathReportXml { get; }
        public ReporterXsl(string pathReportXml, string xslPath = "")
        {
            PathReportXml = pathReportXml;
            if (!File.Exists(PathReportXml))
            {
                CreateResultXml(xslPath: xslPath);
            }
        }
        public void SetEnvBlockMsg(string envBlockMsg)
        {
            
        }
        public string SetAsLink(string link)
        {
            return $"#$#{link}#$#{SetNewLine("")}";
        }
        public string SetNewLine(string line)
        {
            return $"{line}~!~";
        }
        public string SetManualCheck(string comment, string link)
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
                    new XElement(NodeExpectedResult, resultTestCase.NodeExpectedResult ?? Reporter.DefaultContent),
                    new XElement(NodeNeedToCheck, resultTestCase.NodeNeedToCheck ?? Reporter.DefaultContent),
                    new XElement(NodeResult, resultTestCase.NodeResult ?? Reporter.DefaultContent),
                    new XElement(
                        NodeFailure,
                        new XAttribute(AttrMessage, Reporter.DefaultContent),
                        new XAttribute(AttrType, Reporter.DefaultContent)
                    )
            );
        }
        public void AddTestStep(ResultTestCase resultTestCase)
        {
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
        }

        public void ModifyTestInfo(ResultTestInfo resultTestInfo)
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
            rootElement.Attribute(AttrTests).Value = resultTestInfo.AttrTests.ToString() ?? Reporter.DefaultContent;
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
    }
}
