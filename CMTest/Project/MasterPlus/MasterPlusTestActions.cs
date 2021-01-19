
using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
using System;
using System.IO;
using ReportLib;

namespace CMTest.Project.MasterPlus
{
    public partial class MasterPlusTestActions : MasterPlus
    {
        private IReporter _r;
        public void SetIReport(IReporter r)
        {
            _r = r;
        }
        private new Tuple<int, string, string, string, string> GetLanguageFromUi()
        {
            var currentTab = GetMasterPlusMainWindow().GetElement(new ATElementStruct() { ControlType = ATElement.ControlType.Tab });
            var tabs = currentTab.GetElementsAllChild();
            var name = tabs.GetATCollection()[0].GetElementInfo().Name();
            return null;
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
            if (!UtilFile.Exists(appFullPath))
            {
                throw new Exception($"{appFullPath} does not exist.");
            }
            UtilProcess.StartProcess(appFullPath);
            return GetMasterPlusMainWindowForLaunching(timeout);
        }
        public void CloseMasterPlus(int timeout = 10)
        {
            var buttonClose = GetMasterPlusMainWindow().GetElementFromChild(MPObj.CloseMasterPlusButton);
            buttonClose.DoClickPoint(1);
            UtilWait.ForTrue(() => !UtilProcess.IsProcessExistedByName(this.SwProcessName), timeout);
        }
        public void SelectTestDevice(string deviceName, AT swMainWindow)
        {
            var deviceList = swMainWindow.GetElement(MPObj.DeviceList);
            //var devices = deviceList.GetElementsAllChild();
            //return devices.GetElementByIA(new ATElementStruct() { IADescription = deviceName });
            var dut = deviceList.GetElementFromChild(new ATElementStruct() { Name = deviceName });
            //dut.DoClickPoint(waitTime: 1);
            dut.DoClick(2);
        }
        public void SelectTab(ATElementStruct whichTab)
        {
            var currentTabbar = GetMasterPlusMainWindow().GetElement(new ATElementStruct() { ControlType = ATElement.ControlType.Tab });
            //var tabs = currentTab.GetElementsAllChild();
            //tabs.GetATCollection()[GetTabIndexByTabCount(tabs.GetATCollection().Length)].DoClickPoint(1);
            var targetTab = currentTabbar.GetElementFromChild(whichTab);
            if (targetTab.GetElementInfo().ToggleState().Equals("Off"))
            {
                targetTab.DoClickPoint(1);
                UtilWait.ForTrue(() => targetTab.GetElementInfo().ToggleState().Equals("On"), 3, 1);
            }
        }
        public void ClickResetButton(ATElementStruct whichResetButton = null)
        {
            var resetButton = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ResetButton);
            resetButton.DoClickPoint(1);
            ClickCommonDialog();
        }
        public AT GetMasterPlusMainWindow(int timeout = 0)
        {
            return new AT().GetElementFromChild(MPObj.MainWindow, timeout);
        }
        public AT GetMasterPlusMainWindowForLaunching(int timeout = 0)
        {
            return UtilWait.ForNonNull(() =>
            {
                if (UtilProcess.IsProcessExistedByName("RENEW"))
                {
                    UtilProcess.KillProcessByName("RENEW");
                }
                var mainWindow = GetMasterPlusMainWindow();
                mainWindow.GetElement(MPObj.DeviceList);
                return mainWindow;
            }, timeout, 2);
        }
        public enum CommonDialogButtons
        {
            XButton = 0,
            OKButton = 1,
            CancelButton = 2
        }
        public void ClickCommonDialog(CommonDialogButtons whichButton = CommonDialogButtons.OKButton)
        {
            var commonDialogParent = GetMasterPlusMainWindow().GetElementFromChild(MPObj.CommonDialogParent, 3);
            var commonDialog = commonDialogParent.GetElementFromChild(MPObj.CommonDialog);
            var commonDialogButtons = commonDialog.GetElementsFromChild(new ATElementStruct() { ControlType = ATElement.ControlType.Button });
            commonDialogButtons.GetATCollection()[(int)whichButton].DoClickPoint(2);
        }










        public void LaunchAndCheckCrash(long testTimes)
        {
            var crashTimes = 0;
            UtilCmd.Clear();
            for (var i = 1; i < testTimes; i++)
            {
                var titleTotal = $"Reopen Times: {i} - Crash Times: {crashTimes}";
                var logLines = UtilFile.ReadFileByLine(LogPathLaunch);
                logLines.ForEach(UtilCmd.WriteLine);
                //logLines.ForEach(line => UtilCmd.WriteLine(line));
                var launchLogTime = GetRestartLogTime();
                var screenshotPath = Path.Combine(ScreenshotsPath, crashTimes.ToString());
                UtilProcess.StartProcess(SwLnkPath);
                Timeout = 1;
                UtilCmd.WriteTitle($"{titleTotal} - Searching MP+ UI.");
                var startTime = DateTime.Now;
                var dialogWarning = UtilWait.ForAnyResultCatch(() =>
                {
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
            var dialogWarning = UtilWait.ForAnyResultCatch(() =>
            {
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
    }
}
