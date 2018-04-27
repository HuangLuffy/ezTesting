using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLib
{
    public class Reporter
    {
        public struct Result
        {
            public const string PASS = "Pass";
            public const string FAIL = "Fail";
            public const string BLOCK = "Block";
            public const string TBD = "Tbd";
        }
        public class Result_TestInfo
        {
            public int Attribute_blocks { get; set; }
            public string Attribute_blocksPercent { get; set; }
            public int Attribute_errors { get; set; }
            public string Attribute_errorsPercent { get; set; }
            public int Attribute_failures { get; set; }
            public string Attribute_failuresPercent { get; set; }
            public int Attribute_passes { get; set; }
            public string Attribute_passesPercent { get; set; }
            public int Attribute_tbds { get; set; }
            public string Attribute_tbdsPercent { get; set; }
            public string Attribute_project { get; set; }
            public string Attribute_testName { get; set; }
            public string Attribute_os { get; set; }
            public string Attribute_language { get; set; }
            public long Attribute_time { get; set; }
            public string Attribute_deviceModel { get; set; }
            public string Attribute_deviceName { get; set; }
            public string Attribute_region { get; set; }
            public int Attribute_tests { get; set; }
            public string Attribute_version { get; set; }
            public string Attribute_name { get; set; }
        }
        public class Result_TestCase
        {
            public int Node_stepNumber { get; set; }
            public string Node_description { get; set; }
            public string Node_expectedResult { get; set; }
            public string Node_needToCheck { get; set; }
            public string Node_result { get; set; }
            public string Node_failure { get; set; }
            public string Attribute_message { get; set; }
            public string Attribute_type { get; set; }

            public long Attribute_time { get; set; }
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
        public const string Attribute_os = "os";
        public const string Attribute_language = "language";
        public const string Attribute_time = "time";  // total time
        public const string Attribute_deviceModel = "deviceModel";
        public const string Attribute_deviceName = "deviceName";
        public const string Attribute_region = "region";
        public const string Attribute_tests = "tests"; // how many tests
        public const string Attribute_version = "version";
        public const string Attribute_name = "name"; // class name xxx.xxx.abc
        public const string Attribute_testName = "testName";  //abc
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

        public string GetResultPercent(int result, int total, int keepPoint = 2)
        {
            return (Math.Round((double)result / total, keepPoint) * 100) .ToString() + "%";
        }
    }
}
