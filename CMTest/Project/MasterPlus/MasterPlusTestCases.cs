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

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestCases
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
        public MasterPlusTestCases()
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
                , $"{deviceName} can be found. (For now, cannot verify if the device selection was successful.)"
                , "Failed to find the device."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_SelectTab(ATElementStruct whichTab , bool reset = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.SelectTab(whichTab);
                    if (reset)
                    {
                        MpActions.ClickResetButton();
                    }
                }
                , Ireporter.SetAsLines($"Click {whichTab.Name} tab.", reset ? "Click Reset button." : $"Go to {whichTab.Name} tab.")
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
                , $"NA."
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        private string GetStringDescriptionStringOpenReassignmentDialog(string gridKey)
        {
            return $"Open Reassignment Dialog for a single Keyboard Key {gridKey}.";
        }
        private string GetStringExpectedVerifyKey(string Key, bool blVerifyKeyWork = true)
        {
            return blVerifyKeyWork ? $"Verify that the key works as {Key}." : "";
        }
        private string GetStringDescription(string pressedKey, string gridKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            return Ireporter.SetAsLines(GetStringDescriptionStringOpenReassignmentDialog(gridKey),
                    blAssignKey ? $"Assign {pressedKey}." : $"Check the assigned Value is {pressedKey} on the Reassignment Dialog.");
        }
        private string GetStringExpected(string pressedKey, string gridKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            return Ireporter.SetAsLines(blAssignKey ? $"Assign successfully." : $"The assigned Value is still {pressedKey} on the Reassignment Dialog.",
                pressedKey.Equals(gridKey) ? "The Grid would be green." : "The Grid would be purple.", GetStringExpectedVerifyKey(pressedKey, blVerifyKeyWork));
        }
        public void Case_AssignKeyOnReassignDialog(KeyPros pressedKey, KeyPros assignWhichKeyGrid, bool blScanCode = false, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyOnReassignDialog(pressedKey, assignWhichKeyGrid, blScanCode, blAssignKey, blVerifyKeyWork);
                }
                , GetStringDescription(pressedKey.UiaName, assignWhichKeyGrid.UiaName, blAssignKey, blVerifyKeyWork)
                , GetStringExpected(pressedKey.UiaName, assignWhichKeyGrid.UiaName, blAssignKey, blVerifyKeyWork)
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_SelectItemFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKeyGrid, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyFromReassignMenu(whichMenuItem, whichMenuItemSubItem, assignWhichKeyGrid, blAssignKey, blVerifyKeyWork);
                }
                , GetStringDescription($"{whichMenuItem} > {whichMenuItemSubItem}", assignWhichKeyGrid, blAssignKey, blVerifyKeyWork)
                , GetStringExpected($"{whichMenuItem} > {whichMenuItemSubItem}", assignWhichKeyGrid, blAssignKey, blVerifyKeyWork)
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_DisableKey(string disableWhichKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.DisableKey(disableWhichKey, blAssignKey);
                }
                , Ireporter.SetAsLines(GetStringDescriptionStringOpenReassignmentDialog(disableWhichKey),
                    blAssignKey ? $"Set it disabled." : $"Check the {disableWhichKey} is disabled.")
                , Ireporter.SetAsLines(blAssignKey ? $"Disable successfully." : $"The {disableWhichKey} is still disabled.", "The Grid would be red.", GetStringExpectedVerifyKey("disabled", blVerifyKeyWork))
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_EnableKey(string enableWhichKey, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.EnableKey(enableWhichKey, blAssignKey);
                }
                , Ireporter.SetAsLines(GetStringDescriptionStringOpenReassignmentDialog(enableWhichKey),
                    blAssignKey ? $"Set it enabled." : $"Check the {enableWhichKey} is enabled.")
                , Ireporter.SetAsLines(blAssignKey ? $"Enable successfully and the key value is default value {enableWhichKey} on the Reassignment Dialog." : $"The {enableWhichKey} is still enabled and the key value is still the default value {enableWhichKey} on the Reassignment Dialog.", "The Grid would be green.", GetStringExpectedVerifyKey("enabled", blVerifyKeyWork))
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_VerifyKeysValueAndColor(string pressedKey, string assignWhichKeyGrid, Action<AT> assignAction, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.CommonAssignKeyAndVerify(pressedKey, assignWhichKeyGrid, assignAction, blAssignKey, blVerifyKeyWork);
                }
                , GetStringDescriptionStringOpenReassignmentDialog(assignWhichKeyGrid)
                , Ireporter.SetAsLines($"The key value is still the default value {assignWhichKeyGrid} on the Reassignment Dialog.", pressedKey.Equals(assignWhichKeyGrid) ? "The Grid would be green." : "The Grid would be purple.", GetStringExpectedVerifyKey(assignWhichKeyGrid, blVerifyKeyWork))
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }

        //or ma
        public void Case_AssignInLoop(bool blAssignKey = true, bool blVerifyKeyWork = true, bool blScanCode = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignInLoop(blAssignKey, blVerifyKeyWork, blScanCode);
                }
                , Ireporter.SetAsLines($"Assign all keys on the Keyboard with all keys in Reassignment menu.",
                    blAssignKey ? $"Check the all the assigned key work." : $"Check the all the assigned key work!")
                , Ireporter.SetAsLines(blVerifyKeyWork ? $"Check that all the assigned key work as expected." : $"Assigned successfully.", "All assigned Grids would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }

        public void Case_CheckAllKeysOnRelayController()
        {
            typeof(Hw.KbKeys).GetFields().ToList().ForEach((x) => {
                var v = (KeyPros)(x.GetValue(""));
                if (!v.Port.Equals(""))
                {
                    try
                    {
                        TestIt.SendUsbKeyAndCheck(v, v.KeyCode);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }           
                }
            });
        }
    }
}