using CMTest.Project.MasterPlus;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest
{
    public partial class TestIt
    {
        IDictionary<string, Func<dynamic>> Options_MasterPlus_Tests_With_Funcs = new Dictionary<string, Func<dynamic>>();

        private void AssembleMasterPlusPlugInOutTests()
        {
            if (Options_MasterPlus_Tests_With_Funcs.Count() == 0)
            {
                //Select Back that will trigger this function again, so try-catch.
                Options_MasterPlus_Tests_With_Funcs.Add(MasterPlusTestFlows.Test_Names.OPTION_LAUNCH_TEST, Flow_MasterPlus_LaunchTest);
                Options_MasterPlus_Tests_With_Funcs.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN, _CMD.MenuShowAgain);
                Options_MasterPlus_Tests_With_Funcs.Add(UtilCmd.OPTION_BACK, _CMD.MenuGoback);
            }
        }
        private dynamic Flow_MasterPlus_LaunchTest()
        {
            _PortalTestFlows.Flow_LaunchTest();
            return FOUND_TEST;
        }
    }
}
