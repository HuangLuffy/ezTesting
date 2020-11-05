using CMTest.Xml;
using CommonLib.Util;
using System.Threading;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
            public const string OPTION_LAUNCH_CHECK_CRASH = "Launch and check crash";
            public const string OPTION_TEST = "Test";
        }
        
        private static readonly long TEST_TIMES = 99999999;

        public readonly MasterPlusTestActions MasterPlusTestActions = new MasterPlusTestActions();
        /// <summary>
        /// haha
        /// </summary>
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
            SW.WriteConsoleTitle(1, $"Waiting for launching. (1s)", 1);
            var SwMainWindow = MasterPlusTestActions.GetMasterPlusMainWindow(11);
            var dut = MasterPlusTestActions.GetTestDevice(deviceName, SwMainWindow);
            dut.DoClickPoint();
            //MasterPlusTestActions.KeyMappingTest(deviceName);
        }
    }
}
