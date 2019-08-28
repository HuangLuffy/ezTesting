using CMTest.Project.MasterPlus;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMTest
{
    public partial class TestIt
    {
        private readonly IDictionary<string, Func<dynamic>> _optionsMasterPlusTestsWithFuncs = new Dictionary<string, Func<dynamic>>();

        private void AssembleMasterPlusPlugInOutTests()
        {
            if (_optionsMasterPlusTestsWithFuncs.Any()) return;
            //Select Back that will trigger this function again, so try-catch.
            _optionsMasterPlusTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_LAUNCH_TEST, Flow_MasterPlus_LaunchTest);
            _optionsMasterPlusTestsWithFuncs.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN, _CMD.MenuShowAgain);
            _optionsMasterPlusTestsWithFuncs.Add(UtilCmd.OPTION_BACK, _CMD.MenuGoback);
        }
        private dynamic Flow_MasterPlus_LaunchTest()
        {
            _PortalTestFlows.Flow_LaunchTest();
            return FOUND_TEST;
        }
    }
}
