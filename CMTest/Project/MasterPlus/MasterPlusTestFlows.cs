using CommonLib.Util;
using System.Threading;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
        }
        
        public static long TEST_TIMES = 99999999;

        public readonly MasterPlusTestActions MasterPlusTestActions = new MasterPlusTestActions();
        /// <summary>
        /// haha
        /// </summary>
        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TEST_TIMES; i++)
            {
                MasterPlusTestActions.LaunchTimes = i;
                MasterPlusTestActions.LaunchSw();
                MasterPlusTestActions.CloseSw();
            }
        }

        public void Flow_RestartSystem()
        {
            Thread t = UtilWait.WaitAnimationThread("Wait OS running. 30s", 1);
            t.Start();
            t.Join();
            MasterPlusTestActions.RestartFlow();
        }
    }
}
