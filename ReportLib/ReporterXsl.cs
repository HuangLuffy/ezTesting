using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                this.CreateResultXml();
            }
        }
        public void SetEnvBlockMsg(string envBlockMsg)
        {
            
        }
        public string SetAsLink(string link)
        {
            return $"#$#{link}#$#{this.SetNewLine("")}";
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
                    Node_testsuite,
                    new XAttribute(Attribute_project, "project"),
                    new XAttribute(Attribute_testName, "testName"),
                    new XAttribute(Attribute_os, "os"),
                    new XAttribute(Attribute_language, "language"),
                    new XAttribute(Attribute_time, "time"),
                    new XAttribute(Attribute_deviceModel, "deviceModel"),
                    new XAttribute(Attribute_deviceName, "deviceName"),
                    new XAttribute(Attribute_region, "region"),
                    new XAttribute(Attribute_tests, "tests"),
                    new XAttribute(Attribute_version, "version"),
                    new XAttribute(Attribute_name, "name"),

                    new XAttribute(Attribute_blocks, "blocks"),
                    new XAttribute(Attribute_blocksPercent, "blocksPercent"),
                    new XAttribute(Attribute_errors, "errors"),
                    new XAttribute(Attribute_errorsPercent, "errorsPercent"),
                    new XAttribute(Attribute_failures, "failures"),
                    new XAttribute(Attribute_failsPercent, "failsPercent"),
                    new XAttribute(Attribute_passes, "passes"),
                    new XAttribute(Attribute_passesPercent, "passesPercent"),
                    new XAttribute(Attribute_tbds, "tbds"),
                    new XAttribute(Attribute_tbdsPercent, "tbdsPercent")
                )
            );
            this.xDoc = xDoc;
            //xDoc.Save(this.pathReportXml);
        }
        private XElement AssembleElement(Result_TestCase _Result_TestCase)
            //(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result)
        {
            return 
                new XElement(
                    Node_testcase,
                    new XAttribute(Attribute_classname, _Result_TestCase.Attribute_classname),
                    new XAttribute(Attribute_time, _Result_TestCase.Attribute_time),
                    new XAttribute(Attribute_name, _Result_TestCase.Attribute_name),
                    new XElement(Node_step, _Result_TestCase.Node_stepNumber),
                    new XElement(Node_description, _Result_TestCase.Node_description),
                    new XElement(Node_expectedResult, _Result_TestCase.Node_expectedResult),
                    new XElement(Node_needToCheck, _Result_TestCase.Node_needToCheck),
                    new XElement(Node_result, _Result_TestCase.Node_result),
                    new XElement(
                        Node_failure,
                        new XAttribute(Attribute_message, "na"),
                        new XAttribute(Attribute_type, "na")
                )
            );
        }
        public void AddTestStep(Result_TestCase _Result_TestCase)
        {
            XDocument xDoc = this.xDoc == null ? XDocument.Load(this.pathReportXml) : this.xDoc;
            var testcases = xDoc.Root.Elements(Node_testcase);
            XElement xElement = AssembleElement(_Result_TestCase);
            if (testcases.Count() == 0)
            {
                xDoc.Root.Add(xElement);
            }
            else
            {
                testcases.Last().AddAfterSelf(xElement);
            }
            xDoc.Save(this.pathReportXml);
        }

        public void ModifTestInfo(Result_TestInfo _Result_TestInfo)
            //(string project, string os, string language, string region, string time, string deviceModel, string deviceName, string testTotalNumber, string version, string name, string testName
            //, string testName, string testName, string testName, string testName, string testName)
        {
            XDocument xDoc = this.xDoc == null ? XDocument.Load(this.pathReportXml) : this.xDoc;
            XElement rootElement = this.xDoc.Root;
            rootElement.Attribute(Attribute_project).Value = _Result_TestInfo.Attribute_project;
            rootElement.Attribute(Attribute_testName).Value = _Result_TestInfo.Attribute_testName;
            rootElement.Attribute(Attribute_os).Value = _Result_TestInfo.Attribute_os;
            rootElement.Attribute(Attribute_language).Value = _Result_TestInfo.Attribute_language;
            rootElement.Attribute(Attribute_time).Value = _Result_TestInfo.Attribute_time;
            rootElement.Attribute(Attribute_deviceModel).Value = _Result_TestInfo.Attribute_deviceModel;
            rootElement.Attribute(Attribute_deviceName).Value = _Result_TestInfo.Attribute_deviceName;
            rootElement.Attribute(Attribute_region).Value = _Result_TestInfo.Attribute_region;
            rootElement.Attribute(Attribute_tests).Value = _Result_TestInfo.Attribute_tests;
            rootElement.Attribute(Attribute_version).Value = _Result_TestInfo.Attribute_version;
            rootElement.Attribute(Attribute_name).Value = _Result_TestInfo.Attribute_name;

            rootElement.Attribute(Attribute_blocks).Value = _Result_TestInfo.Attribute_blocks;
            rootElement.Attribute(Attribute_blocksPercent).Value = _Result_TestInfo.Attribute_blocks;
            rootElement.Attribute(Attribute_errors).Value = _Result_TestInfo.Attribute_errors;
            rootElement.Attribute(Attribute_errorsPercent).Value = _Result_TestInfo.Attribute_errorsPercent;
            rootElement.Attribute(Attribute_failures).Value = _Result_TestInfo.Attribute_failures;
            rootElement.Attribute(Attribute_failsPercent).Value = _Result_TestInfo.Attribute_failsPercent;
            rootElement.Attribute(Attribute_passes).Value = _Result_TestInfo.Attribute_passes;
            rootElement.Attribute(Attribute_passesPercent).Value = _Result_TestInfo.Attribute_passesPercent;
            rootElement.Attribute(Attribute_tbds).Value = _Result_TestInfo.Attribute_tbds;
            rootElement.Attribute(Attribute_tbdsPercent).Value = _Result_TestInfo.Attribute_tbdsPercent);
        }
    }
}
