using System.Diagnostics;
using System.IO;
using System.Linq;
using ATLib;
using CMTest.Xml;
using CommonLib;
using CommonLib.Util;
using CommonLib.Util.OS;
using CommonLib.Util.Project;
using Nancy;
using ReportLib;
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
        public void Case_AssignKeyOnReassignDialog(ScanCode scanCode, string assignWhichKey, bool onlyVerify = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyOnReassignDialog(scanCode, assignWhichKey, onlyVerify);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {assignWhichKey}.",
                    onlyVerify ? $"Check the assigned Value is {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)} on the Reassignment Dialog." : $"Push {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)}.")
                , Ireporter.SetAsLines(onlyVerify ? $"The assigned Value is still {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)} on the Reassignment Dialog." : $"Assign successfully.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKey, bool onlyVerify = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignKeyFromReassignMenu(whichMenuItem, whichMenuItemSubItem, assignWhichKey, onlyVerify);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {assignWhichKey}.",
                    $"Open Reassignment Menu.",
                    onlyVerify ? $"Check the assigned Value is {whichMenuItemSubItem} on the Reassignment Dialog." : $"Choose {whichMenuItem} > {whichMenuItemSubItem}.")
                , Ireporter.SetAsLines(onlyVerify ? $"The assigned Value is still {whichMenuItemSubItem} on the Reassignment Dialog." : $"Assign successfully.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_DisableKey(string disableWhichKey, bool onlyVerify = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.DisableKey(disableWhichKey, onlyVerify);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {disableWhichKey}.",
                    onlyVerify ? $"Check the {disableWhichKey} is disabled." : $"Set it disabled.")
                , Ireporter.SetAsLines(onlyVerify ? $"The {disableWhichKey} is still disabled." : $"Disable successfully.", "The Grid would be red.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_EnableKey(string enableWhichKey, bool onlyVerify = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.EnableKey(enableWhichKey, onlyVerify);
                }
                , Ireporter.SetAsLines($"Open Reassignment Dialog for Single Keyboard Key {enableWhichKey}.",
                    onlyVerify ? $"Check the {enableWhichKey} is enabled." : $"Set it enabled.")
                , Ireporter.SetAsLines(onlyVerify ? $"The {enableWhichKey} is still enabled and the key value is still the default value {enableWhichKey} on the Reassignment Dialog." : $"Enable successfully and the key value is default value {enableWhichKey} on the Reassignment Dialog.", "The Grid would be green.")
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
        public void Case_AssignInLoop(bool onlyVerify = false)
        {
            Ireporter.Exec(() =>
                {
                    MpActions.AssignInLoop();
                }
                , Ireporter.SetAsLines($"Assign all keys on the Keyboard with all keys in Reassignment menu.",
                    onlyVerify ? $"Check the all the assigned key work." : $"Check the all the assigned key work!")
                , Ireporter.SetAsLines(onlyVerify ? $"Check the all the assigned key work." : $"All the assigned key work.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
    }
}