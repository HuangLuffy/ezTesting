using System;

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
        public class ResultTestInfo
        {
            public int AttrBlocks { get; set; }
            public string AttrBlocksPercent { get; set; }
            public int AttrErrors { get; set; }
            public string AttrErrorsPercent { get; set; }
            public int AttrFailures { get; set; }
            public string AttrFailuresPercent { get; set; }
            public int AttrPasses { get; set; }
            public string AttrPassesPercent { get; set; }
            public int AttrTbds { get; set; }
            public string AttrTbdsPercent { get; set; }
            public string AttrProject { get; set; }
            public string AttrTestName { get; set; }
            public string AttrOs { get; set; }
            public string AttrLanguage { get; set; }
            public long AttrTime { get; set; }
            public string AttrDeviceModel { get; set; }
            public string AttrDeviceName { get; set; }
            public string AttrRegion { get; set; }
            public int AttrTests { get; set; }
            public string AttrVersion { get; set; }
            public string AttrName { get; set; }
        }
        public class ResultTestCase
        {
            public int NodeStepNumber { get; set; }
            public string NodeDescription { get; set; }
            public string NodeExpectedResult { get; set; }
            public string NodeNeedToCheck { get; set; }
            public string NodeResult { get; set; }
            public string NodeFailure { get; set; }
            public string AttrMessage { get; set; }
            public string AttrType { get; set; }

            public long AttrTime { get; set; }
            public string AttrClassname { get; set; }
            public string AttrName { get; set; }
        }
        public const string NodeTestsuite = "testsuite";
        public const string AttrBlocks = "blocks";
        public const string AttrBlocksPercent = "blocksPercent";
        public const string AttrErrors = "errors";
        public const string AttrErrorsPercent = "errorsPercent";
        public const string AttrFailures = "failures";
        public const string AttrFailsPercent = "failsPercent";
        public const string AttrPasses = "passes";
        public const string AttrPassesPercent = "passesPercent";
        public const string AttrTbds = "tbds";
        public const string AttrTbdsPercent = "tbdsPercent";
        public const string AttrProject = "project";
        public const string AttrOs = "os";
        public const string AttrLanguage = "language";
        public const string AttrTime = "time";  // total time
        public const string AttrDeviceModel = "deviceModel";
        public const string AttrDeviceName = "deviceName";
        public const string AttrRegion = "region";
        public const string AttrTests = "tests"; // how many tests
        public const string AttrVersion = "version";
        public const string AttrName = "name"; // class name xxx.xxx.abc
        public const string AttrTestName = "testName";  //abc
        public const string NodeTestcase = "testcase";
        //public const string attr_time = "time";
        public const string AttrClassname = "classname";
        //public const string attr_name = "name"; // function name
        //public const string attr_time = "time"; // step time

        public const string NodeStep = "step";
        public const string NodeDescription = "description";
        public const string NodeExpectedResult = "expectedResult";
        public const string NodeNeedToCheck = "needToCheck";
        public const string NodeResult = "result";
        public const string NodeFailure = "failure";
        public const string AttrMessage = "message";
        public const string AttrType = "type";

        public string GetResultPercent(int result, int total, int keepPoint = 2)
        {
            return (Math.Round((double)result / total, keepPoint) * 100) + "%";
        }
    }
}
