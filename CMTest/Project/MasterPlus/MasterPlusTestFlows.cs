using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public readonly IReporter IReporter;
        public readonly MasterPlusTestActions MpActions;

        public MasterPlusTestFlows()
        {
            MpActions = new MasterPlusTestActions();
            MpActions.Initialize();
            IReporter = new ReporterXsl(Path.Combine(MpActions.GetResultTimePath(), new StackTrace().GetFrame(0).GetMethod().ReflectedType?.Name + ".xml"),
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
            MpActions.SetReport(IReporter);
        }

        public void LaunchTestReport()
        {
            Process.Start("IExplore.exe", IReporter.GetResultFullPath());
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
            IReporter.Exec(() =>
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
            IReporter.Exec(() =>
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
            IReporter.Exec(() =>
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
            IReporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.SelectTab(deviceName);
                    if (reset)
                    {
                        MpActions.ClickResetButton(deviceName);
                    }
                }
                , IReporter.SetAsLines($"Select KeyMapping tab.", "Click Reset button.")
                , $"Select successfully."
                , "Failed to select KeyMapping tab."
                , ReportLib.Reporter.WhenCaseFailed.BlockAllLeftCases);
        }
        public void Case_AssignKeyOnReassignDialog(ScanCode scanCode, string assignWhichKey, bool onlyVerify = false)
        {
            IReporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.AssignKeyOnReassignDialog(scanCode, assignWhichKey, onlyVerify);
                }
                , IReporter.SetAsLines($"Open Reassignment dialog for Single Keyboard Key {assignWhichKey}.",
                    onlyVerify ? $"Check the assigned Value is {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)}." : $"Push {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)}.")
                , IReporter.SetAsLines(onlyVerify ? $"The assigned Value is still {UtilEnum.GetEnumNameByValue<ScanCode>(scanCode)}." : $"Assign successfully.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKey, bool onlyVerify = false)
        {
            IReporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.AssignKeyFromReassignMenu(whichMenuItem, whichMenuItemSubItem, assignWhichKey, onlyVerify);
                }
                , IReporter.SetAsLines($"Open Reassignment dialog for Single Keyboard Key {assignWhichKey}.",
                    $"Open Reassignment menu.",
                    onlyVerify ? $"Check the assigned Value is {whichMenuItemSubItem}." : $"Choose {whichMenuItem} > {whichMenuItemSubItem}.")
                , IReporter.SetAsLines(onlyVerify ? $"The assigned Value is still {whichMenuItemSubItem}." : $"Assign successfully.", "The Grid would be purple.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_DisableKey(string disableWhichKey, bool onlyVerify = false)
        {
            IReporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.DisableKey(disableWhichKey, onlyVerify);
                }
                , IReporter.SetAsLines($"Open Reassignment dialog for Single Keyboard Key {disableWhichKey}.",
                    onlyVerify ? $"Check the assigned Value is disabled." : $"Set it disabled.")
                , IReporter.SetAsLines(onlyVerify ? $"The assigned Value is still disabled." : $"Disable successfully.", "The Grid would be red.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
        public void Case_EnableKey(string enableWhichKey, bool onlyVerify = false)
        {
            IReporter.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    MpActions.EnableKey(enableWhichKey, onlyVerify);
                }
                , IReporter.SetAsLines($"Open Reassignment dialog for Single Keyboard Key {enableWhichKey}.",
                    onlyVerify ? $"Check the assigned Value is enabled." : $"Set it enabled.")
                , IReporter.SetAsLines(onlyVerify ? $"The assigned Value is still enabled." : $"enable successfully and the key value is default value.", "The Grid would be green.")
                , "Failed."
                , ReportLib.Reporter.WhenCaseFailed.StillRunThisCase);
        }
    }
}