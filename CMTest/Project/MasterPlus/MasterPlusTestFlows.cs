using System.IO;
using CMTest.Xml;
using ReportLib;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows :SW
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
            public const string OPTION_LAUNCH_CHECK_CRASH = "Launch and check crash";
            public const string OPTION_TEST = "Test";
        }
        
        private static readonly long TEST_TIMES = 99999999;
        private IReporter _iReporter;
        public readonly MasterPlusTestActions MasterPlusTestActions = new MasterPlusTestActions();

        public MasterPlusTestFlows()
        {
            _iReporter = new ReporterXsl(Path.Combine(ResultPath, "MasterPlusTestFlows.xml"));
        }
        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TEST_TIMES; i++)
            {
                MasterPlusTestActions.LaunchTimes = i;
                MasterPlusTestActions.LaunchMasterPlus();
                MasterPlusTestActions.CloseMasterPlus();
            }
        }

        public void Flow_RestartSystemAndCheckDeviceRecognition(XmlOps xmlOps)
        {
            MasterPlusTestActions.RestartSystemAndCheckDeviceRecognitionFlow(xmlOps);
        }

        public void Flow_LaunchAndCheckCrash()
        {
            MasterPlusTestActions.LaunchAndCheckCrash(TEST_TIMES);
        }

        public void Flow_KeymappingTest(string deviceName)
        {
            SW.WriteConsoleTitle($"Waiting for launching. (1s)");
            var swMainWindow = MasterPlusTestActions.GetMasterPlusMainWindow(11);
            var dut = MasterPlusTestActions.GetTestDevice(deviceName, swMainWindow);
            dut.DoClickPoint();
            _iReporter.AddTestStep(new Reporter.ResultTestCase()
            {
                NodeStepNumber = 1,
                NodeDescription = "123123",
                NodeExpectedResult = "NodeExpectedResult",
                NodeResult = SW.Result.Fail
            });
            //MasterPlusTestActions.KeyMappingTest(deviceName);
        }
    }
}
