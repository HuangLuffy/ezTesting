using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ezTesting
{
    public class ReportXsl
    {
        public XDocument xDoc { get; set; }
        public string pathXml { get; set; }
        public ReportXsl(string pathXml)
        {
            this.pathXml = pathXml;
        }
        public class Result_TestSuite
        {
            public string Attribute_blocks { get; set; }
            public string Attribute_blocksPercent { get; set; }
            public string Attribute_errors { get; set; }
            public string Attribute_errorsPercent { get; set; }
            public string Attribute_failures { get; set; }
            public string Attribute_failsPercent { get; set; }
            public string Attribute_passes { get; set; }
            public string Attribute_passesPercent { get; set; }
            public string Attribute_tbds { get; set; }
            public string Attribute_tbdsPercent { get; set; }
            public string Attribute_project { get; set; }
            public string Attribute_testName { get; set; }
            public string Attribute_os { get; set; }
            public string Attribute_language { get; set; }
            public string Attribute_time { get; set; }
            public string Attribute_deviceModel { get; set; }
            public string Attribute_deviceName { get; set; }
            public string Attribute_region { get; set; }
            public string Attribute_tests { get; set; }
            public string Attribute_version { get; set; }
            public string Attribute_name { get; set; }
        }
        public class Result_TestCase
        {
            public string Node_step { get; set; }
            public string Node_description { get; set; }
            public string Node_expectedResult { get; set; }
            public string Node_needToCheck { get; set; }
            public string Node_result { get; set; }
            public string Node_failure { get; set; }
            public string Attribute_message { get; set; }
            public string Attribute_type { get; set; }

            public string Attribute_time { get; set; }
            public string Attribute_classname { get; set; }
            public string Attribute_name { get; set; }
        }
        public const string Node_testsuite = "testsuite";
        public const string Attribute_blocks = "blocks";
        public const string Attribute_blocksPercent = "blocksPercent";
        public const string Attribute_errors = "errors";
        public const string Attribute_errorsPercent = "errorsPercent";
        public const string Attribute_failures = "failures";
        public const string Attribute_failsPercent = "failsPercent";
        public const string Attribute_passes = "passes";
        public const string Attribute_passesPercent = "passesPercent";
        public const string Attribute_tbds = "tbds";
        public const string Attribute_tbdsPercent = "tbdsPercent";
        public const string Attribute_project = "project";
        public const string Attribute_testName = "testName";  //abc
        public const string Attribute_os = "os";
        public const string Attribute_language = "language";
        public const string Attribute_time = "time";  // total time
        public const string Attribute_deviceModel = "deviceModel";
        public const string Attribute_deviceName = "deviceName";
        public const string Attribute_region = "region";
        public const string Attribute_tests = "tests"; // how many tests
        public const string Attribute_version = "version";
        public const string Attribute_name = "name"; // class name xxx.xxx.abc
        public const string Node_testcase = "testcase";
        //public const string Attribute_time = "time";
        public const string Attribute_classname = "classname";
        //public const string Attribute_name = "name"; // function name
        //public const string Attribute_time = "time"; // step time

        public const string Node_step = "step";
        public const string Node_description = "description";
        public const string Node_expectedResult = "expectedResult";
        public const string Node_needToCheck = "needToCheck";
        public const string Node_result = "result";
        public const string Node_failure = "failure";
        public const string Attribute_message = "message";
        public const string Attribute_type = "type";

        public void CreateResultXml(string xslName = "xmlReport.xsl")
        {
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XProcessingInstruction("xml-stylesheet", $"type='text/xsl' href='{xslName}'"),
                new XElement(
                    Node_testsuite,
                    new XAttribute(Attribute_project, "1"),
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

                    //new XElement(
                    //    Node_testcase,
                    //    new XAttribute(Attribute_classname, "classname"),
                    //    new XAttribute(Attribute_time, "time"),
                    //    new XAttribute(Attribute_name, "name"),
                    //    new XElement(Node_step, "2"),
                    //    new XElement(Node_description, "2"),
                    //    new XElement(Node_expectedResult, "2"),
                    //    new XElement(Node_needToCheck, "2"),
                    //    new XElement(Node_result, "2"),
                    //    new XElement(Node_result, "2"),
                    //    new XElement(
                    //        Node_failure,
                    //        new XAttribute(Attribute_message, "message"),
                    //        new XAttribute(Attribute_type, "type")
                    //    )
                    //)
                )
            );
            this.xDoc = xDoc;
            xDoc.Save(this.pathXml);
        }
        public XElement AssembleElement()
        {
            return 
                new XElement(
                Node_testcase,
                new XAttribute(Attribute_classname, "classname"),
                new XAttribute(Attribute_time, "time"),
                new XAttribute(Attribute_name, "name"),
                new XElement(Node_step, "3"),
                new XElement(Node_description, "3"),
                new XElement(Node_expectedResult, "2"),
                new XElement(Node_needToCheck, "2"),
                new XElement(Node_result, "2"),
                new XElement(
                    Node_failure,
                    new XAttribute(Attribute_message, "message"),
                    new XAttribute(Attribute_type, "type")
                )
            );
        }
        public void AddTestCase()
        {
            XDocument xDoc = this.xDoc == null ? XDocument.Load(this.pathXml) : this.xDoc;
            var testcases = xDoc.Root.Elements(Node_testcase);
            XElement xElement = new XElement(
                Node_testcase,
                new XAttribute(Attribute_classname, "classname"),
                new XAttribute(Attribute_time, "time"),
                new XAttribute(Attribute_name, "name"),
                new XElement(Node_step, "3"),
                new XElement(Node_description, "3"),
                new XElement(Node_expectedResult, "2"),
                new XElement(Node_needToCheck, "2"),
                new XElement(Node_result, "2"),
                new XElement(
                    Node_failure,
                    new XAttribute(Attribute_message, "message"),
                    new XAttribute(Attribute_type, "type")
                )
            );
            if (testcases.Count() == 0)
            {
                xDoc.Root.Add(xElement);
            }
            else
            {
                testcases.Last().AddAfterSelf(xElement);
            }
            xDoc.Save(this.pathXml);
        }
    }
}
