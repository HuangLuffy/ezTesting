using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ATLib;
using ATLib.Input;
using CMTest.Tool;
using CMTest.Xml;
using CommonLib;
using CommonLib.Util;
using CommonLib.Util.ComBus;
using CommonLib.Util.OS;
using CommonLib.Util.Project;
using Nancy;
using ReportLib;
using static ATLib.Input.Hw;
using static ATLib.Input.KbEvent;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
            public const string OPTION_LAUNCH_CHECK_CRASH = "Launch and check crash";
            public const string OPTION_TEST = "Test";
        }
        private static readonly long TEST_TIMES = 99999999;
        public readonly IReporter Ireporter;
        public readonly MasterPlusTestActions MpActions;
        public static UtilSerialRelayController Usrc = new UtilSerialRelayController();
        public MasterPlusTestFlows()
        {
            MpActions = new MasterPlusTestActions();
            MpActions.Initialize();
            Ireporter = new ReporterXsl(Path.Combine(MpActions.GetResultTimePath(), new StackTrace().GetFrame(0).GetMethod().ReflectedType?.Name + ".xml"),
                ProjectPath.GetProjectFullPath(), MpActions.GetScreenshotsRelativePath(),
                new Reporter.ResultTestInfo
                {
                    AttrProject = "Cooler Master",
                    AttrOs = UtilOs.GetOsVersion(),
                    AttrLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name.Replace("-", "_"),
                    AttrRegion = System.Globalization.CultureInfo.InstalledUICulture.Name.Split('-')[1],
                    AttrDeviceModel = ReportLib.Reporter.DefaultContent,
                    AttrDeviceName = ReportLib.Reporter.DefaultContent,
                    AttrVersion = UtilOs.GetOsProperty(),
                    AttrTotalCases = 0,
                    AttrPasses = 0,
                    AttrFailures = 0,
                    AttrTbds = 0,
                    AttrBlocks = 0
                });
            MpActions.SetIReport(Ireporter);
            Usrc.Load();
        }

        public void LaunchTestReport()
        {
            Process.Start("IExplore.exe", Ireporter.GetResultFullPath());
        }

        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TEST_TIMES; i++)
            {
                MpActions.LaunchTimes = i;
                MpActions.LaunchMasterPlus("", 11);
                MpActions.CloseMasterPlus();
            }
        }

        public void Flow_RestartSystemAndCheckDeviceRecognition(XmlOps xmlOps)
        {
            MpActions.RestartSystemAndCheckDeviceRecognitionFlow(xmlOps);
        }

        public void Flow_LaunchAndCheckCrash()
        {
            MpActions.LaunchAndCheckCrash(TEST_TIMES);
        }

        //Common cases

        public void Case_LaunchMasterPlus(int timeout)
        {
            Ireporter.Exec(() =>
                {
                    var swMainWindow = MpActions.LaunchMasterPlus(MpActions.SwLnkPath, timeout);
                }
                , $"Launch MasterPlus from {MpActions.SwLnkPath}. Timeout = {timeout}s."
                , "MasterPlus+ launched successfully."
                , "Failed to launch MP+."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_CloseMasterPlus(int timeout)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.CloseMasterPlus(timeout);
                }
                , $"Close MasterPlus by clicking X button. Timeout = {timeout}s."
                , "MasterPlus+ closed successfully."
                , "Failed to close MP+."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_SelectDeviceFromList(string deviceName)
        {
            Ireporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.SelectTestDevice(deviceName, swMainWindow);
                }
                , $"Select {deviceName} from MasterPlus."
                , $"{deviceName} can be found."
                , "Failed to find the device."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_SelectKeyMappingTab(string deviceName , bool reset = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.SelectTab(deviceName);
                    if (reset)
                    {
                        MpActions.ClickResetButton(MPObj.KeyMappingResetButton);
                    }
                }
                , Ireporter.SetAsLines($"Click KeyMapping tab.", reset ? "Click Reset button." : "Go to KeyMapping tab.")
                , $"Select successfully."
                , "Failed to select KeyMapping tab."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_Reset(ATElementStruct whichResetButton)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.ClickResetButton(whichResetButton);
                }
                , $"Click Reset button."
                , $"Reset button can be clicked."
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_AssignKeyOnReassignDialog(KeyPros pressedKey, KeyPros assignWhichKeyGrid, bool blScanCode = false, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyOnReassignDialog(pressedKey, assignWhichKeyGrid, blScanCode, blAssignKey, blVerifyKeyWork);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for a single Keyboard Key {assignWhichKeyGrid.UiaName}.",
                    blAssignKey ? $"Push {pressedKey.UiaName}." : $"Check the assigned Value is {pressedKey.UiaName} on the Reassignment Dialog.")
                , Ireporter.SetAsLines(blAssignKey ? $"Assign successfully." : $"The assigned Value is still {pressedKey.UiaName} on the Reassignment Dialog.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyFromReassignMenu(whichMenuItem, whichMenuItemSubItem, assignWhichKey, blAssignKey);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {assignWhichKey}.",
                    $"Open Reassignment Menu.",
                    blAssignKey ? $"Check the assigned Value is {whichMenuItemSubItem} on the Reassignment Dialog." : $"Choose {whichMenuItem} > {whichMenuItemSubItem}.")
                , Ireporter.SetAsLines(blAssignKey ? $"The assigned Value is still {whichMenuItemSubItem} on the Reassignment Dialog." : $"Assign successfully.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_DisableKey(string disableWhichKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.DisableKey(disableWhichKey, blAssignKey);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {disableWhichKey}.",
                    blAssignKey ? $"Check the {disableWhichKey} is disabled." : $"Set it disabled.")
                , Ireporter.SetAsLines(blAssignKey ? $"The {disableWhichKey} is still disabled." : $"Disable successfully.", "The Grid would be red.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_EnableKey(string enableWhichKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.EnableKey(enableWhichKey, blAssignKey);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {enableWhichKey}.",
                    blAssignKey ? $"Check the {enableWhichKey} is enabled." : $"Set it enabled.")
                , Ireporter.SetAsLines(blAssignKey ? $"The {enableWhichKey} is still enabled and the key value is still the default value {enableWhichKey} on the Reassignment Dialog." : $"Enable successfully and the key value is default value {enableWhichKey} on the Reassignment Dialog.", "The Grid would be green.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_VerifyKeysValueAndColor(string assignWhichKey)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.CommonAssignKeyAndVerify("", assignWhichKey, null, true);
                }
                , $"Open Reassignment Dialog for Single Keyboard Key {assignWhichKey}."
                , Ireporter.SetAsLines($"The key value is still the default value {assignWhichKey} on the Reassignment Dialog.", "The Grid would be green.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }

        //or ma
        public void Case_AssignInLoop(bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignInLoop();
                }
                , Ireporter.SetAsLines($"Assign all keys on the Keyboard with all keys in Reassignment menu.",
                    blAssignKey ? $"Check the all the assigned key work." : $"Check the all the assigned key work!")
                , Ireporter.SetAsLines(blAssignKey ? $"Check the all the assigned key work." : $"All the assigned key work.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }

        public void Case_CheckAllKeysOnRelayController()
        {
            KeysSpyOp _KeysSpyOp = new KeysSpyOp(this.MpActions.KeySpyRelativePath);
            UtilSerialRelayController _Usb = new UtilSerialRelayController();
            _Usb.Load();
            _KeysSpyOp.ClickClear();
            //var s = _KeysSpyOp.GetContentList();
            //var c = typeof(Hw.KbKeys).GetFields().ToList().FindAll((x) => !((KeyPros)x.GetValue("")).Port.Equals(""));
            typeof(Hw.KbKeys).GetFields().ToList().ForEach((x) => {
                var v = (KeyPros)(x.GetValue(""));
                if (!v.Port.Equals(""))
                {
                    _KeysSpyOp.ClickClear();
                    _Usb.SendToPort(v.Port, 0.1);
                    UtilTime.WaitTime(1);
                    if (_KeysSpyOp.GetContentList() != null)
                    {
                        if (!_KeysSpyOp.GetContentList().ElementAt(0).Equals(v.KeyCode))
                        {
                            //throw new Exception("");
                            Console.WriteLine($"Inconsistent keys - Actual: [{_KeysSpyOp.GetContentList().ElementAt(0)}] - Expected:[{v.KeyCode}] Port:[{v.Port}] Ui:[{v.UiaName}]");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No key captured - Expected:[{v.KeyCode}] Port:[{v.Port}] Ui:[{v.UiaName}]");
                    }
                }
            });
        }
    }
}