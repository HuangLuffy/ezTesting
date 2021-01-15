using CMTest.Project.MasterPlus;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ATLib.Input;
using CommonLib.Util.IO;
using System.Text.RegularExpressions;
using CommonLib.Util.ComBus;
using CMTest.Tool;
using static ATLib.Input.Hw;

namespace CMTest
{
    public partial class TestIt
    {
        private readonly IDictionary<string, Func<dynamic>> _optionsTestsWithFuncs = new Dictionary<string, Func<dynamic>>();
        private readonly int MasterPlusLaunchTime = 120;
        private void AssembleMasterPlusPlugInOutTests(bool fromConf = true)
        {
            if (_optionsTestsWithFuncs.Any()) return;
            _optionsTestsWithFuncs.Add(MasterPlusTestCases.TestNames.OPTION_LAUNCH_TEST, Flow_MasterPlus_LaunchTest);
            _optionsTestsWithFuncs.Add(MasterPlusTestCases.TestNames.OPTION_LAUNCH_CHECK_CRASH, Flow_MasterPlus_LaunchAndCheckCrash);
            _KeyMappingTestWithFuncs(fromConf);
            _optionsTestsWithFuncs.Add(MasterPlusTestCases.TestNames.OPTION_TEST, () => _cmd.ShowCmdMenu(_optionsXmlPlugInOutDeviceNames, _optionsTestsWithFuncs));
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
                    this.Suit_KeyMappingBaseTest(deviceName);
                    return MARK_FOUND_RESULT;
                });       
            }
            _optionsXmlPlugInOutDeviceNames.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }
        public void GetMatrixFromFile()
        {
            Hw.GetMatrixFromRelayControllerAndAssembleToKeys(_MpCases.MpActions.MatrixRelativePath);
        }
        public void GetKeyboardKeysFromKeyMapTabFile()
        {
            Hw.GetKeyboardKeys(_MpCases.MpActions.ResourcesKeysRelativePath);
        }
        private dynamic Flow_MasterPlus_LaunchAndCheckCrash()
        {
            _MpCases.Flow_LaunchAndCheckCrash();
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_MasterPlus_LaunchTest()
        {
            _MpCases.Flow_LaunchTest();
            return MARK_FOUND_RESULT;
        }
        public void RestartSystemAndCheckDeviceRecognition()
        {
            _MpCases.Flow_RestartSystemAndCheckDeviceRecognition(_xmlOps);
        }
        #region MyRegion
        private dynamic Flow_KeyMappingTest(string deviceName)
        {
            _MpCases.Ireporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _MpCases.Ireporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _MpCases.Case_LaunchMasterPlus(MasterPlusLaunchTime);
            _MpCases.Case_SelectDeviceFromList(deviceName);
            _MpCases.Case_SelectTab(MPObj.KeyMappingTab);
            _MpCases.Case_AssignKeyOnReassignDialog(KbKeys.SC_KEY_A, KbKeys.SC_KEY_B);
            _MpCases.Case_AssignKeyOnReassignDialog(KbKeys.SC_KEY_B, KbKeys.SC_KEY_C);
            _MpCases.Case_AssignKeyOnReassignDialog(KbKeys.SC_KEY_C, KbKeys.SC_KEY_A);

            _MpCases.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
        private void _AssignLoopVerifyLogic(IReadOnlyList<List<string>> loop, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            for (var i = 0; i < loop.Count(); i++)
            {
                //if ((i + 1) <= (loop.Count() - 1) && loop.ElementAt(i)[2].Equals(loop.ElementAt(i + 1)[2]) &&
                //    blAssignKey) continue;
                _MpCases.MpActions.DifferentFlowForDifferentPressedKey(loop.ElementAt(i)[0],
                () => {
                    _MpCases.Case_DisableKey(loop.ElementAt(i)[2], blAssignKey, blVerifyKeyWork);
                },
                () => {
                    _MpCases.Case_EnableKey(loop.ElementAt(i)[2], blAssignKey, blVerifyKeyWork);
                },
                () => {
                    if (loop.ElementAt(i)[0].Equals(""))
                    {
                        _MpCases.Case_AssignKeyOnReassignDialog(KbKeys.GetScKeyByUiaName(loop.ElementAt(i)[1]), KbKeys.GetScKeyByUiaName(loop.ElementAt(i)[2]), false, blAssignKey, blVerifyKeyWork);
                    }
                    else
                    {
                        _MpCases.Case_SelectItemFromReassignMenu(loop.ElementAt(i)[0], loop.ElementAt(i)[1], loop.ElementAt(i)[2], blAssignKey, blVerifyKeyWork);
                    }  
                });
            }
        }
        private void _ResetLoopVerifyLogic(IReadOnlyList<List<string>> loop, bool blAssignKey = false, bool blVerifyKeyWork = true)
        {
            var t = new List<string>();
            for (var i = 0; i < loop.Count(); i++)
            {
                t.Add(loop.ElementAt(i)[2]);
            }
            var b = t.Distinct();
            foreach (var item in b)
            {
                _MpCases.Case_VerifyKeysValueAndColor(item, item, null, blAssignKey, blVerifyKeyWork);
            }
        }
        private dynamic Suit_KeyMappingBaseTest(string deviceName)
        {
            //_MpCases.Case_AssignInLoop();
            var keysNeedToAssignList = new List<List<string>>
            {
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_MAIL, KbKeys.SC_KEY_A.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_CALCULATOR, KbKeys.SC_KEY_B.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_PLAY_PAUSE, KbKeys.SC_KEY_C.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_STOP, KbKeys.SC_KEY_D.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_PRE_TRACK, KbKeys.SC_KEY_E.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_NEXT_TRACK, KbKeys.SC_KEY_F.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_MUTE, KbKeys.SC_KEY_G.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_VOL_DEC, KbKeys.SC_KEY_H.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_VOL_INC, KbKeys.SC_KEY_I.UiaName },
                //new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.SC_KEY_W3HOME, KbKeys.SC_KEY_J.UiaName },
                //new List<string> { MPObj.DisableKeyCheckbox.Name, "", KbKeys.SC_KEY_K.UiaName },
                //new List<string> { MPObj.EnableKeyCheckbox.Name, "", KbKeys.SC_KEY_K.UiaName },
                new List<string> { "", KbKeys.SC_KEY_D.UiaName, KbKeys.SC_KEY_L.UiaName },
            };
            _MpCases.Ireporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _MpCases.Ireporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _MpCases.Case_LaunchMasterPlus(MasterPlusLaunchTime);
            _MpCases.Case_SelectDeviceFromList(deviceName);
            _MpCases.Case_SelectTab(MPObj.KeyMappingTab);
            _AssignLoopVerifyLogic(keysNeedToAssignList);
            _MpCases.Case_CloseMasterPlus(10);
            _MpCases.Case_LaunchMasterPlus(MasterPlusLaunchTime);
            _MpCases.Case_SelectDeviceFromList(deviceName);
            _MpCases.Case_SelectTab(MPObj.KeyMappingTab, false);
            _AssignLoopVerifyLogic(keysNeedToAssignList, false);
            _MpCases.Case_Reset(MPObj.KeyMappingResetButton);
            _ResetLoopVerifyLogic(keysNeedToAssignList);
            _MpCases.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
        #endregion
    }
}
