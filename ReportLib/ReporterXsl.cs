using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ReportLib
{
    public class ReporterXsl : Reporter, IReporter
    {
        public XDocument xDoc { get; set; }
        public string pathReportXml { get; set; }
        public ReporterXsl(string pathReportXml)
        {
            this.pathReportXml = pathReportXml;
            if (!File.Exists(this.pathReportXml))
            {
                CreateResultXml();
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
        public string setManualCheck(string comment, string link)
        {
            return $"{comment}@@@{link}";
        }
        
        public void CreateResultXml(string xslName = "xmlReport.xsl")
        {

            XDocument xDoc = new XDocument(
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
            this.xDoc = xDoc;
            xDoc.Save(pathReportXml);
        }
        private XElement AssembleElement(ResultTestCase resultTestCase)
            //(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result)
        {
            return 
                new XElement(
                    NodeTestcase,
                    new XAttribute(AttrClassname, resultTestCase.AttrClassname),
                    new XAttribute(AttrTime, resultTestCase.AttrTime),
                    new XAttribute(AttrName, resultTestCase.AttrName),
                    new XElement(NodeStep, resultTestCase.NodeStepNumber),
                    new XElement(NodeDescription, resultTestCase.NodeDescription),
                    new XElement(NodeExpectedResult, resultTestCase.NodeExpectedResult),
                    new XElement(NodeNeedToCheck, resultTestCase.NodeNeedToCheck),
                    new XElement(NodeResult, resultTestCase.NodeResult),
                    new XElement(
                        NodeFailure,
                        new XAttribute(AttrMessage, "na"),
                        new XAttribute(AttrType, "na")
                    )
            );
        }
        public void AddTestStep(ResultTestCase resultTestCase)
        {
            var thisDoc = xDoc ?? XDocument.Load(pathReportXml);
            var testCases = xDoc.Root.Elements(NodeTestcase);
            XElement xElement = AssembleElement(resultTestCase);
            if (!testCases.Any())
            {
                xDoc.Root.Add(xElement);
            }
            else
            {
                testCases.Last().AddAfterSelf(xElement);
            }
            xDoc.Save(pathReportXml);
        }

        public void ModifyTestInfo(ResultTestInfo resultTestInfo)
            //(string project, string os, string language, string region, string time, string deviceModel, string deviceName, string testTotalNumber, string version, string name, string testName
            //, string testName, string testName, string testName, string testName, string testName)
        {
            var thisDoc = xDoc ?? XDocument.Load(pathReportXml);
            var rootElement = xDoc.Root;
            rootElement.Attribute(AttrProject).Value = resultTestInfo.AttrProject;
            rootElement.Attribute(AttrTestName).Value = resultTestInfo.AttrTestName;
            rootElement.Attribute(AttrOs).Value = resultTestInfo.AttrOs;
            rootElement.Attribute(AttrLanguage).Value = resultTestInfo.AttrLanguage;
            rootElement.Attribute(AttrTime).Value = resultTestInfo.AttrTime.ToString();
            rootElement.Attribute(AttrDeviceModel).Value = resultTestInfo.AttrDeviceModel;
            rootElement.Attribute(AttrDeviceName).Value = resultTestInfo.AttrDeviceName;
            rootElement.Attribute(AttrRegion).Value = resultTestInfo.AttrRegion;
            rootElement.Attribute(AttrTests).Value = resultTestInfo.AttrTests.ToString();
            rootElement.Attribute(AttrVersion).Value = resultTestInfo.AttrVersion;
            rootElement.Attribute(AttrName).Value = resultTestInfo.AttrName;

            rootElement.Attribute(AttrBlocks).Value = resultTestInfo.AttrBlocks.ToString();
            rootElement.Attribute(AttrBlocksPercent).Value = resultTestInfo.AttrBlocksPercent;
            rootElement.Attribute(AttrErrors).Value = resultTestInfo.AttrErrors.ToString();
            rootElement.Attribute(AttrErrorsPercent).Value = resultTestInfo.AttrErrorsPercent;
            rootElement.Attribute(AttrFailures).Value = resultTestInfo.AttrFailures.ToString();
            rootElement.Attribute(AttrFailsPercent).Value = resultTestInfo.AttrFailuresPercent;
            rootElement.Attribute(AttrPasses).Value = resultTestInfo.AttrPasses.ToString();
            rootElement.Attribute(AttrPassesPercent).Value = resultTestInfo.AttrPassesPercent;
            rootElement.Attribute(AttrTbds).Value = resultTestInfo.AttrTbds.ToString();
            rootElement.Attribute(AttrTbdsPercent).Value = resultTestInfo.AttrTbdsPercent;
            thisDoc.Save(pathReportXml);
        }
    }
}
