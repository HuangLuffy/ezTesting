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

        private void AssembleMasterPlusPlugInOutTests(bool fromConf = true)
        {
            if (_optionsMasterPlusTestsWithFuncs.Any()) return;
            _optionsMasterPlusTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_LAUNCH_TEST, Flow_MasterPlus_LaunchTest);
            _optionsMasterPlusTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_LAUNCH_CHECK_CRASH, Flow_MasterPlus_LaunchAndCheckCrash);

            AssemblePortalPortalTestLanguages();
            AssemblePortalPlugInOutDevices(fromConf);
            _optionsMasterPlusTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_TEST, () => _cmd.ShowCmdMenu(_optionsXmlPlugInOutDeviceNames, _optionsPortalTestsWithFuncs));

            //_optionsMasterPlusTestsWithFuncs.Add(UtilCmd.Result.SHOW_MENU_AGAIN, _cmd.MenuShowAgain);
            _optionsMasterPlusTestsWithFuncs.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }

        private dynamic Flow_MasterPlus_LaunchAndCheckCrash()
        {
            _masterPlusTestFlows.Flow_LaunchAndCheckCrash();
            return MARK_FOUND_RESULT;
        }

        private dynamic Flow_MasterPlus_LaunchTest()
        {
            _masterPlusTestFlows.Flow_LaunchTest();
            return MARK_FOUND_RESULT;
        }

        public void RestartSystemAndCheckDeviceRecognition()
        {
            _masterPlusTestFlows.Flow_RestartSystemAndCheckDeviceRecognition(_xmlOps);
        }

        private dynamic Flow_KeymappingTest()
        {
            _masterPlusTestFlows.Flow_KeymappingTest(_xmlOps);
            return MARK_FOUND_RESULT;
        }
    }
}
