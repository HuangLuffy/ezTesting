
using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using ATLib.Input;
using CommonLib;
using static ATLib.Input.KbEvent;
using ReportLib;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestActions : MasterPlus
    {
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

            UtilTime.WaitTime(4);
            UtilProcess.KillProcessByName("RENEW");
            UtilTime.WaitTime(1);
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
            devices.GetATCollection()[GetTabIndexByTabCount(devices.GetATCollection().Length)].DoClickPoint(1);
        }

        #region KeyMapping

        public void ClickResetButton(string deviceName)
        {
            var keyMappingResetButton = GetMasterPlusMainWindow().GetElementFromChild(MPObj.KeyMappingResetButton);
            keyMappingResetButton.DoClickPoint(1);
            ClickCommonDialog();
        }

        private void CommonAssignKey(string keyValueNeedToInput, string assignWhichKeyGrid, Action<AT> action)
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            var keyGridNeedToBeAssigned = assignContainer.GetElementFromChild(new ATElementStruct() { Name = assignWhichKeyGrid });
            keyGridNeedToBeAssigned.DoClickPoint(1);
            var reassignDialog = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDialog);
            action.Invoke(reassignDialog);
            VerifyAssignedKeyValueAndGridColor(keyGridNeedToBeAssigned, reassignDialog, keyValueNeedToInput);
        }
        private void VerifyAssignedKeyValueAndGridColor(AT keyGridNeedToBeAssigned, AT reassignDialog, string keyValueNeedToInput)
        {
            var assignedValue = reassignDialog.GetElementFromDescendants(MPObj.AssignedValue);
            var value = assignedValue.GetElementInfo().FullDescription();
            if (!value.Equals(keyValueNeedToInput))
            {
                throw new Exception($"Input {keyValueNeedToInput}, but get {value}.");
            }
            var saveButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignSaveButton);
            saveButton.DoClickPoint(1);
            if (!keyGridNeedToBeAssigned.GetElementInfo().FullDescription().Equals(KeyMappingGridColor.Purple))
            {
                throw new Exception($"Key {keyValueNeedToInput} in not {nameof(KeyMappingGridColor.Purple)}.");
            }
        }
        public void AssignKeyOnReassignDialog(ScanCode keyValueNeedToInputScanCode, string assignWhichKeyGrid)
        {
            var keyValueNeedToInput = UtilEnum.GetEnumNameByValue<ScanCode>(keyValueNeedToInputScanCode);
            CommonAssignKey(keyValueNeedToInput, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    KbEvent.Press(keyValueNeedToInputScanCode);
                    UtilTime.WaitTime(1);
                });
        }
        public void AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKeyGrid)
        {
            CommonAssignKey(whichMenuItemSubItem, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    var reassignCollapseButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCollapseButton);
                    reassignCollapseButton.DoClickPoint(1);
                    var reassignDropdown = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDropdown);
                    var whichCatalog = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItem });
                    whichCatalog.DoClickPoint(1);
                    var subItem = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItemSubItem });
                    subItem.GetIAccessible().DoDefaultAction();
                });
        }

        #endregion

        public AT GetMasterPlusMainWindow(int timeout = 0)
        {
            return new AT().GetElementFromChild(MPObj.MainWindow, timeout);
        }
        public AT GetMasterPlusMainWindowForLaunching(int timeout = 0)
        {
            return UtilWait.ForNonNull(() =>
            {
                var mainWindow = GetMasterPlusMainWindow();
                mainWindow.GetElement(MPObj.DeviceList);
                return mainWindow;
            }, timeout);
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
    }
}
