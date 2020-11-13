using CMTest.Project.MasterPlus;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CMTest
{
    public partial class TestIt
    {
        private readonly IDictionary<string, Func<dynamic>> _optionsTestsWithFuncs = new Dictionary<string, Func<dynamic>>();

        private void AssembleMasterPlusPlugInOutTests(bool fromConf = true)
        {
            if (_optionsTestsWithFuncs.Any()) return;
            _optionsTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_LAUNCH_TEST, Flow_MasterPlus_LaunchTest);
            _optionsTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_LAUNCH_CHECK_CRASH, Flow_MasterPlus_LaunchAndCheckCrash);

            _KeyMappingTestWithFuncs(fromConf);
            _optionsTestsWithFuncs.Add(MasterPlusTestFlows.TestNames.OPTION_TEST, () => _cmd.ShowCmdMenu(_optionsXmlPlugInOutDeviceNames, _optionsTestsWithFuncs));
            //_optionsMasterPlusTestsWithFuncs.Add(UtilCmd.Result.SHOW_MENU_AGAIN, _cmd.MenuShowAgain);
            _optionsTestsWithFuncs.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }
        private void _KeyMappingTestWithFuncs(bool fromConf = true)
        {
            if (_optionsXmlPlugInOutDeviceNames.Any()) return;
            //foreach (var deviceName in _xmlOps.GetDeviceNameList())
            foreach (var deviceName in new List<string>(){"SK622W", "SK622B" })
            {
                _optionsXmlPlugInOutDeviceNames.Add(deviceName, () => {
                    this.Flow_KeymappingTest(deviceName);
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
            _masterPlusTestFlows.resultTestInfo.AttrDeviceModel = deviceName;
            _masterPlusTestFlows.resultTestInfo.AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _masterPlusTestFlows.Case_LaunchMasterPlus();
            _masterPlusTestFlows.Case_SelectDevice(deviceName);
            _masterPlusTestFlows.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
    }
}
