using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public const string OPTION_LAUNCH_TEST = "Launch Test";
        public static long TEST_TIMES = 99999999;

        public List<string> Options_Cmd = new List<string> { OPTION_LAUNCH_TEST };

        MasterPlusTestActions _MasterPlusTestActions = new MasterPlusTestActions();
        /// <summary>
        /// haha
        /// </summary>
        public void Flow_LaunchTest()
        {
            for (int i = 1; i < TEST_TIMES; i++)
            {
                _MasterPlusTestActions.LaunchTimes = i;
                _MasterPlusTestActions.LaunchSW();
                _MasterPlusTestActions.CloseSW();
            }
        }
    }
}
