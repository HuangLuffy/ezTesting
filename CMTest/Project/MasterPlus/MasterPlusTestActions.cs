
using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
using System;
using System.IO;
using System.Threading;
using ATLib.Input;
using CommonLib;
using static ATLib.Input.KbEvent;
using ReportLib;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestActions : MasterPlus
    {
        public MasterPlusTestActions()
        {

        }
        public AT LaunchMasterPlus(string appFullPath, int timeout, bool killCurrentOne = true)
        {
            if (killCurrentOne)
            {
                UtilProcess.KillProcessByName(SwProcessName);
                UtilTime.WaitTime(1);
            }
            if (appFullPath.Equals(""))
            {
                appFullPath = SwLnkPath;
            }
            UtilProcess.StartProcess(appFullPath);
            //WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(4);
            UtilProcess.KillProcessByName("RENEW");
            return GetMasterPlusMainWindowForLaunching(timeout);
        }
        public void CloseMasterPlus()
        {
            var buttonClose = SwMainWindow.GetElement(MPObj.ButtonCloseMainWindow);
            buttonClose.DoClick();
            Timeout = 6;
            //WriteConsoleTitle(LaunchTimes, $"Waiting for closing. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(Timeout);
        }

        public void LaunchAndCheckCrash(long testTimes)
        {
            var crashTimes = 0;
            UtilCmd.Clear();
            for (var i = 1; i < testTimes; i++)
            {
                var titleTotal = $"Reopen Times: {i} - Crash Times: {crashTimes}";
                var logLines = UtilFile.ReadFileByLine(LogPathLaunch);
                logLines.ForEach(line => UtilCmd.WriteLine(line));
                var launchLogTime = GetRestartLogTime();
                var screenshotPath = Path.Combine(ScreenshotsPath, crashTimes.ToString());
                UtilProcess.StartProcess(SwLnkPath);
                Timeout = 1;
                UtilCmd.WriteTitle($"{titleTotal} - Searching MP+ UI.");
                var startTime = DateTime.Now;
                var dialogWarning = UtilWait.ForAnyResultCatch(() => {
                    UtilCmd.WriteTitle($"{titleTotal} - Searching Warning dialog of the MP+ in 60s. Time elapsed: {(DateTime.Now - startTime).Seconds}s.");
                    SwMainWindow = new AT().GetElement(MPObj.MainWindow, Timeout);  // The MP+ will change after a while.
                    return SwMainWindow.GetElement(MPObj.DialogWarning, Timeout);
                }, 60, 2);
                if (SwMainWindow == null)
                {
                    UtilCapturer.Capture(i.ToString());
                    UtilFile.WriteFile(LogPathLaunch, $"{launchLogTime}: Reopen Times: {i} - Could not open MasterPlus.");
                    crashTimes++;
                }
                UtilTime.WaitTime(1);
                UtilProcess.KillProcessByName(SwProcessName);
                UtilTime.WaitTime(1);
            }
        }
        public void RestartSystemAndCheckDeviceRecognitionFlow(XmlOps xmlOps)
        {
            UtilFolder.CreateDirectory(Path.Combine(ScreenshotsPath, "Restart"));
            var restartLogTime = GetRestartLogTime();
            var screenshotPath = Path.Combine(RestartScreenshotPath, restartLogTime);
            var logLines = UtilFile.ReadFileByLine(LogPathRestart);
            logLines.ForEach(line => UtilCmd.WriteLine(line));
            var titleLaunchTimes = xmlOps.GetRestartTimes();
            var titleTotal = $"Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count}";
            var t = UtilWait.WaitTimeElapseThread($"{titleTotal} - Waiting 35s.", 35);
            t.Start();
            t.Join();
            if (!File.Exists(SwLnkPath))
            {
                UtilCmd.WriteTitle($"{titleTotal} - Could not find {SwLnkPath}.");
                UtilCmd.PressAnyContinue();
            }
            UtilProcess.StartProcess(SwLnkPath);
            Timeout = 1;
            UtilCmd.WriteTitle($"{titleTotal} - Searching MP+ UI.");
            var startTime = DateTime.Now;
            var dialogWarning = UtilWait.ForAnyResultCatch(() => {
                UtilCmd.WriteTitle($"{titleTotal} - Searching Warning dialog of the MP+ in 60s. Time elapsed: {(DateTime.Now - startTime).Seconds}s.");
                SwMainWindow = new AT().GetElement(MPObj.MainWindow, Timeout);  // The MP+ will change after a while.
                return SwMainWindow.GetElement(MPObj.DialogWarning, Timeout);
            }, 60, 3);
            if (SwMainWindow == null)
            {
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
                UtilCapturer.Capture(screenshotPath);
                UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
            }     
            else if (dialogWarning != null)
            {
                UtilCapturer.Capture(screenshotPath);
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - The device was not displayed");
                UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - The device was not displayed.");
            }
            xmlOps.SetRestartTimes(Convert.ToInt16(titleLaunchTimes) + 1);
            UtilTime.WaitTime(1);
            UtilProcess.KillProcessByName(this.SwProcessName);
            //UtilProcess.ExecuteCmd();// sometimes it does not work somehow.
            UtilOS.Reboot();
        }

        public void SelectTestDevice(string deviceName, AT swMainWindow)
        {
            var deviceList = swMainWindow.GetElement(MPObj.DeviceList);
            //var devices = deviceList.GetElementsAllChild();
            //return devices.GetElementByIA(new ATElementStruct() { IADescription = deviceName });
            var dut = deviceList.GetElementFromChild(new ATElementStruct() {Name = deviceName});
            dut.DoClickPoint(waitTime: 1);
        }
        public void SelectTab(string deviceName)
        {
            var currentTab = GetMasterPlusMainWindow().GetElement(new ATElementStruct() { ControlType = ATElement.ControlType.Tab });
            var devices = currentTab.GetElementsAllChild();
            devices.GetATCollection()[2].DoClickPoint(1);
        }
        public void ClickResetButton(string deviceName)
        {
            var keyMappingResetButton = GetMasterPlusMainWindow().GetElementFromChild(MPObj.KeyMappingResetButton);
            keyMappingResetButton.DoClickPoint(1);
            ClickCommonDialog();
        }
        public void AssignKeyOnReassignDialog(ScanCode keyNeedToInput, string assignWhichKey)
        {
            var keyName = UtilEnum.GetEnumNameByValue<ScanCode>(keyNeedToInput);
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            var keyA = assignContainer.GetElementFromChild(new ATElementStruct() { Name = assignWhichKey });
            keyA.DoClickPoint(1);
            var reassignDialog = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDialog);
            KbEvent.Press(keyNeedToInput);
            UtilTime.WaitTime(1);
            var assignedValue = reassignDialog.GetElementFromDescendants(MPObj.AssignedValue);
            var value = assignedValue.GetElementInfo().FullDescription();
            if (!value.Equals(keyName))
            {
                throw new Exception($"Input {UtilEnum.GetEnumNameByValue<ScanCode>(keyNeedToInput)}, but get {value}.");
            }
            var saveButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignSaveButton);
            saveButton.DoClickPoint(1);
            if (!keyA.GetElementInfo().FullDescription().Equals(KeyMappingGridColor.Purple))
            {
                throw new Exception($"Key {UtilEnum.GetEnumNameByValue<ScanCode>(keyNeedToInput)} in not {nameof(KeyMappingGridColor.Purple)}.");
            }
        }

        public AT GetMasterPlusMainWindow(int timeout = 0)
        {
            return new AT().GetElementFromChild(MPObj.MainWindow, timeout);
        }

        public AT GetMasterPlusMainWindowForLaunching(int timeout = 0)
        {
            AT mainWindow = null;
            UtilWait.ForTrueCatch(() =>
            {
                mainWindow = GetMasterPlusMainWindow(timeout);
                mainWindow.GetElement(MPObj.DeviceList);
                return true;
            }, timeout);
            return mainWindow;
        }
        public enum CommonDialogButtons
        {
            XButton = 0,
            OKButton = 1,
            CancelButton =2
        }
        public void ClickCommonDialog(CommonDialogButtons whichButton = CommonDialogButtons.OKButton)
        {
            var commonDialogParent = GetMasterPlusMainWindow().GetElementFromChild(MPObj.CommonDialogParent, 3);
            var commonDialog = commonDialogParent.GetElementFromChild(MPObj.CommonDialog);
            var commonDialogButtons = commonDialog.GetElementsFromChild(new ATElementStruct() {ControlType = ATElement.ControlType.Button});
            commonDialogButtons.GetATCollection()[(int)whichButton].DoClickPoint(2);
        }
    }
}
