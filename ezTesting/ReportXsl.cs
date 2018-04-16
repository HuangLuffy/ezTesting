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

        public void CreateElement()
        {
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XProcessingInstruction("xml-stylesheet", "type='text/xsl' href = 'xmlReport.xsl'"),
                new XElement(Node_testsuite,
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
                            new XElement(Node_testcase,
                                          new XElement(Node_step, "2"),
                                          new XElement(Node_description, "2"),
                                          new XElement(Node_expectedResult, "2"),
                                          new XElement(Node_needToCheck, "2"),
                                          new XElement(Node_result, "2"),
                                          new XElement(Node_failure, "2")
                             )
                )
            );
            xDoc.Save(@"D:\Dev\DevicePass\results\1.xml");
        }
    }
}
