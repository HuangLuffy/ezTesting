﻿using CMTest.Project.MasterPlus;
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
        public readonly IDictionary<string, IEnumerable<string>> CMKeys = new Dictionary<string, IEnumerable<string>>();
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
            var keyboardKeyLines = UtilFile.GetListByLine(_mpTestFlows.MpActions.ResourcesKeysRelativePath);
            foreach (var line in keyboardKeyLines)
            {
                if (line.Contains("SC_KEY_"))
                {
                    var keys = UtilRegex.GetStringsFromDoubleQuo(line);
                    CMKeys.Add(line.Split(',')[0], keys);
                }
            }

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

        private dynamic Flow_KeymappingTest(string deviceName)
        {
            _mpTestFlows.IReporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _mpTestFlows.IReporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            _mpTestFlows.Case_LaunchMasterPlus(60);
            _mpTestFlows.Case_SelectDeviceFromList(deviceName);
            _mpTestFlows.Case_SelectKeyMappingTab(deviceName);
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.A, "B");
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.B, "C");
            _mpTestFlows.Case_AssignKeyOnReassignDialog(KbEvent.ScanCode.C, "A");

            _mpTestFlows.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_KeyMappingBaseTest(string deviceName)
        {
            _mpTestFlows.IReporter.GetResultTestInfo().AttrDeviceModel = deviceName;
            _mpTestFlows.IReporter.GetResultTestInfo().AttrTestName = new StackTrace().GetFrame(0).GetMethod().Name;
            //_mpTestFlows.Case_LaunchMasterPlus(120);
            //_mpTestFlows.Case_SelectDeviceFromList(deviceName);
            //_mpTestFlows.Case_SelectKeyMappingTab(deviceName);
            //var forTest = new List<List<string>>();
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.EMail, "A"});
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.Calculator, "B" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.PlayPause, "C" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.Stop, "D" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.PreviousTrack, "E" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.NextTrack, "F" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.Mute, "G" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.VolumeDown, "H" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.VolumeUP, "I" });
            //forTest.Add(new List<string> { MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.WebBrowser, "J" });
            //foreach (var item in forTest)
            //{
            //    _mpTestFlows.Case_AssignKeyFromReassignMenu(item[0], item[1], item[2]);
            //}
            //_mpTestFlows.Case_DisableKey("K");
            //_mpTestFlows.Case_EnableKey("K");
            //_mpTestFlows.Case_DisableKey("L");
            //_mpTestFlows.Case_CloseMasterPlus(10);
            //_mpTestFlows.Case_LaunchMasterPlus(120);
            //_mpTestFlows.Case_SelectDeviceFromList(deviceName);
            //_mpTestFlows.Case_SelectKeyMappingTab(deviceName, false);
            //foreach (var item in forTest)
            //{
            //    _mpTestFlows.Case_AssignKeyFromReassignMenu(item[0], item[1], item[2], true);
            //}
            //_mpTestFlows.Case_AssignKeyFromReassignMenu(MasterPlus.ReassignMenuItems.MediaKeys, MasterPlus.ReassignMenuItems.MediaKeysItems.EMail, "A");
            _mpTestFlows.Case_EnableKey("K", true);
            _mpTestFlows.Case_DisableKey("L",true);


            _mpTestFlows.LaunchTestReport();
            return MARK_FOUND_RESULT;
        }
    }
}
