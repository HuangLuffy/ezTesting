﻿namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
        }
        
        public static long TEST_TIMES = 99999999;

        public MasterPlusTestActions _MasterPlusTestActions = new MasterPlusTestActions();
        /// <summary>
        /// haha
        /// </summary>
        public void Flow_LaunchTest()
        {
            for (int i = 1; i < TEST_TIMES; i++)
            {
                _MasterPlusTestActions.LaunchTimes = i;
                _MasterPlusTestActions.LaunchSw();
                _MasterPlusTestActions.CloseSw();
            }
        }
    }
}
