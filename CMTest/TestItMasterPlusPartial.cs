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

            AssembleDevices(fromConf);
            _optionsMasterPlusTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_TEST, () => _cmd.ShowCmdMenu(_optionsXmlPlugInOutDeviceNames, _optionsPortalTestsWithFuncs));

            //_optionsMasterPlusTestsWithFuncs.Add(UtilCmd.Result.SHOW_MENU_AGAIN, _cmd.MenuShowAgain);
            _optionsMasterPlusTestsWithFuncs.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }
        private void AssembleDevices(bool fromConf = true)
        {
            if (_optionsXmlPlugInOutDeviceNames.Any()) return;
            //foreach (var deviceName in _xmlOps.GetDeviceNameList())
            foreach (var deviceName in new List<string>(){"SK622W", "SK622B" })
            {
                _optionsXmlPlugInOutDeviceNames.Add(deviceName, () => {
                    _masterPlusTestFlows.Flow_KeymappingTest(deviceName);
                    return MARK_FOUND_RESULT;
                });       
            }
            _optionsXmlPlugInOutDeviceNames.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
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

        private dynamic Flow_KeymappingTest(string deviceName)
        {
            _masterPlusTestFlows.Flow_KeymappingTest(deviceName);
            return MARK_FOUND_RESULT;
        }
    }
}
