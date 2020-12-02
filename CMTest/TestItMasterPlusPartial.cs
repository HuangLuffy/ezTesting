using CMTest.Project.MasterPlus;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ATLib.Input;
using CommonLib.Util.IO;
using System.Text.RegularExpressions;

namespace CMTest
{
    public partial class TestIt
    {
        private readonly IDictionary<string, Func<dynamic>> _optionsTestsWithFuncs = new Dictionary<string, Func<dynamic>>();
        //IEnumerable<string> IReadOnlyList<string>
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
                    this.Flow_KeyMappingBaseTest(deviceName);
                    return MARK_FOUND_RESULT;
                });       
            }
            _optionsXmlPlugInOutDeviceNames.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }

        public void GetKeyboardKeys()
        {
            Hw.GetKeyboardKeys(_mpTestFlows.MpActions.ResourcesKeysRelativePath);
        }

        private dynamic Flow_MasterPlus_LaunchAndCheckCrash()
        {
            _mpTestFlows.Flow_LaunchAndCheckCrash();
            return MARK_FOUND_RESULT;
        }

        private dynamic Flow_MasterPlus_LaunchTest()
        {
            _mpTestFlows.Flow_LaunchTest();
            return MARK_FOUND_RESULT;
        }

        public void RestartSystemAndCheckDeviceRecognition()
        {
            _mpTestFlows.Flow_RestartSystemAndCheckDeviceRecognition(_xmlOps);
        }

        #region MyRegion
        private dynamic Flow_KeyMappingTest(string deviceName)
        {
            _mpTestFlows.IReporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _mpTestFlows.IReporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _mpTestFlows.Case_LaunchMasterPlus(60);
            _mpTestFlows.Case_SelectDeviceFromList(deviceName);
            _mpTestFlows.Case_SelectKeyMappingTab(deviceName);
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.SC_KEY_A, "B");
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.SC_KEY_B, "C");
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.SC_KEY_C, "A");

            _mpTestFlows.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }

        private void _AssignLoopVerifyLogic(IReadOnlyList<List<string>> loop, bool onlyVerify = false)
        {
            for (var i = 0; i < loop.Count(); i++)
            {
                if ((i + 1) <= (loop.Count() - 1) && loop.ElementAt(i)[2].Equals(loop.ElementAt(i + 1)[2]) &&
                    onlyVerify) continue;
                if (loop.ElementAt(i)[0].Equals(MPObj.DisableKeyCheckbox.Name))
                {
                    _mpTestFlows.Case_DisableKey(loop.ElementAt(i)[2], onlyVerify);
                }
                else if (loop.ElementAt(i)[0].Equals(MPObj.EnableKeyCheckbox.Name))
                {
                    _mpTestFlows.Case_EnableKey(loop.ElementAt(i)[2], onlyVerify);
                }
                else
                {
                    _mpTestFlows.Case_AssignKeyFromReassignMenu(loop.ElementAt(i)[0], loop.ElementAt(i)[1], loop.ElementAt(i)[2], onlyVerify);
                }
            }
        }
        private void _ResetLoopVerifyLogic(IReadOnlyList<List<string>> loop)
        {
            var t = new List<string>();
            for (var i = 0; i < loop.Count(); i++)
            {
                t.Add(loop.ElementAt(i)[2]);
            }
            var b = t.Distinct();
            foreach (var item in b)
            {
                _mpTestFlows.Case_VerifyKeysValueAndColor(item);
            }
        }
        private dynamic Flow_KeyMappingBaseTest(string deviceName)
        {
            var keysNeedToAssignList = new List<List<string>>
            {
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.EMail, "A" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.Calculator, "B" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.PlayPause, "C" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.Stop, "D" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.PreviousTrack, "E" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.NextTrack, "F" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.Mute, "G" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.VolumeDown, "H" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.VolumeUP, "I" },
                new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeys.WebBrowser, "J" },
                new List<string> { MPObj.DisableKeyCheckbox.Name, "", "K" },
                new List<string> { MPObj.EnableKeyCheckbox.Name, "", "K" },
                new List<string> { MPObj.DisableKeyCheckbox.Name, "", "L" }
            };
            _mpTestFlows.IReporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _mpTestFlows.IReporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _mpTestFlows.Case_LaunchMasterPlus(120);
            _mpTestFlows.Case_SelectDeviceFromList(deviceName);
            _mpTestFlows.Case_SelectKeyMappingTab(deviceName);
            _AssignLoopVerifyLogic(keysNeedToAssignList);
            _mpTestFlows.Case_CloseMasterPlus(10);
            _mpTestFlows.Case_LaunchMasterPlus(120);
            _mpTestFlows.Case_SelectDeviceFromList(deviceName);
            _mpTestFlows.Case_SelectKeyMappingTab(deviceName, false);
            _AssignLoopVerifyLogic(keysNeedToAssignList, true);
            _mpTestFlows.Case_Reset(MPObj.KeyMappingResetButton);
            _ResetLoopVerifyLogic(keysNeedToAssignList);
            _mpTestFlows.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
        #endregion

    }
}
