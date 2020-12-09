﻿
using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
using System;
using System.Collections.Generic;
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
            dut.DoClickPoint(waitTime: 1);
        }
        public void SelectTab(string deviceName)
        {
            var currentTab = GetMasterPlusMainWindow().GetElement(new ATElementStruct() { ControlType = ATElement.ControlType.Tab });
            var tabs = currentTab.GetElementsAllChild();
            tabs.GetATCollection()[GetTabIndexByTabCount(tabs.GetATCollection().Length)].DoClickPoint(1);
        }

        #region KeyMapping

        public void ClickResetButton(ATElementStruct whichResetButton)
        {
            var keyMappingResetButton = GetMasterPlusMainWindow().GetElementFromChild(whichResetButton);
            keyMappingResetButton.DoClickPoint(1);
            ClickCommonDialog();
        }
        public void CommonAssignKeyAndVerify(string keyValueNeedToInput, string assignWhichKeyGrid, Action<AT> assignAction, bool onlyVerify = false)
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            var keyGridNeedToBeAssigned = assignContainer.GetElementFromChild(new ATElementStruct() { Name = assignWhichKeyGrid });
            keyGridNeedToBeAssigned.DoClickPoint(1);
            var reassignDialog = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDialog);
            //For old //var reassignDialog = GetMasterPlusMainWindow().GetElementFromDescendants(MPObj.ReassignDialog);
            if (!onlyVerify)
            {
                try
                {
                    assignAction.Invoke(reassignDialog);
                }
                catch (Exception e)
                {
                    _r.SetStepFailed($"{e.Message}", $"{e.Message}", blContinueTest: true);
                    try
                    {
                        var closeButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCloseButton);
                        closeButton.DoClickPoint(1);
                        closeButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCloseButton);// if dropdown
                        closeButton.DoClickPoint(1);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    return; // do not verify when failed
                }
            }
            VerifyAssignedKeyValueAndGridColor(keyGridNeedToBeAssigned, reassignDialog, keyValueNeedToInput);
        }
        private void VerifyAssignedKeyValueAndGridColor(AT keyGridNeedToBeAssigned, AT reassignDialog, string keyValueNeedToInput)
        {
            var gridColorValue = KeyMappingGridColor.Purple;
            try
            {
                var assignedValue = reassignDialog.GetElementFromDescendants(MPObj.AssignedValue, returnNullWhenException: true);
                if (keyValueNeedToInput.Equals(MPObj.DisableKeyCheckbox.Name))
                {
                    gridColorValue = KeyMappingGridColor.Red;
                    if (assignedValue != null && !assignedValue.GetElementInfo().IsOffscreen())
                    {
                        _r.SetStepFailed($"Reassign textbox is still there.", "ReassignTextboxStillThere");
                    }
                }
                else if (keyValueNeedToInput.Equals(MPObj.EnableKeyCheckbox.Name) || keyValueNeedToInput.Equals(""))
                {
                    gridColorValue = KeyMappingGridColor.Green;
                    var reassignTitleValue = reassignDialog.GetElementFromDescendants(MPObj.ReassignTitleValue);
                    if (assignedValue.GetElementInfo().FullDescription() != reassignTitleValue.GetElementInfo().FullDescription())
                    {
                        _r.SetStepFailed($"The assigned key is not restored to {reassignTitleValue.GetElementInfo().FullDescription()} when enabling it.", "assignedValueNotRestored");
                    }
                }
                else
                {
                    gridColorValue = KeyMappingGridColor.Purple;
                    var value = assignedValue.GetElementInfo().FullDescription();
                    if (!value.Equals(keyValueNeedToInput))
                    {
                        _r.SetStepFailed($"Input {keyValueNeedToInput}, but get {value}.", "assignedValueWrong");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                var saveButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignSaveButton);
                saveButton.DoClickPoint(1);
            }
            if (!keyGridNeedToBeAssigned.GetElementInfo().FullDescription().Equals(gridColorValue))
            {
                _r.SetStepFailed($"Key {keyValueNeedToInput} in not in {KeyMappingGridColor.GetVarName(gridColorValue)} color.", "colorWrong");
            }
        }
        public void AssignKeyOnReassignDialog(ScanCode keyValueNeedToInputScanCode, string assignWhichKeyGrid, bool onlyVerify = false)
        {
            var keyValueNeedToInput = UtilEnum.GetEnumNameByValue<ScanCode>(keyValueNeedToInputScanCode);
            CommonAssignKeyAndVerify(keyValueNeedToInput, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    KbEvent.Press(keyValueNeedToInputScanCode);
                    UtilTime.WaitTime(1);
                }, onlyVerify);
        }
        private string _theLastMenuItem = string.Empty;
        private int _scrollNum = 0;
        private void _CollapseReassignMenus(AT reassignDropdown)
        {
            try
            {
                while (true)
                {
                    var all = reassignDropdown.GetElementsAllChild(returnNullWhenException: true).GetATCollection();
                    if (!all.Any((x) => x.GetElementInfo().Name().Equals(MPObj.ReassignCatalogListItem.Name)))
                    {
                        break;
                    }
                    for (var i = 0; i < all.Length; i++)
                    {
                        if (!all[i].GetElementInfo().Name().Equals(MPObj.ReassignCatalogListItem.Name)) continue;
                        all[i - 1].DoClickPoint(0.5);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to _CollapseReassignMenus. " + e.Message);
            }
        }
        private void _ScrollMenuItemSubItem(AT reassignDropdown)
        {
            try
            {
                var left = (int)reassignDropdown.GetElementInfo().RectangleLeft() + 10;
                var top = (int)reassignDropdown.GetElementInfo().RectangleTop() + 50;
                switch (_scrollNum)
                {
                    case 0:
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELDOWN);
                        UtilTime.WaitTime(1);
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELDOWN);
                        UtilTime.WaitTime(1);
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELDOWN);
                        UtilTime.WaitTime(1);
                        _scrollNum = 1;
                        break;
                    case 1:
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELUP);
                        UtilTime.WaitTime(1);
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELUP);
                        UtilTime.WaitTime(1);
                        HWSimulator.HWSend.MoveCursorAndDo(left, top, HWSimulator.HWSend.MouseKeys.WHEELUP);
                        UtilTime.WaitTime(1);
                        _scrollNum = 0;
                        break;
                }
            }
            catch (Exception e)
            {

            }
        }
        public void AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKeyGrid, bool onlyVerify = false)
        {
            var localWhichMenuItem = whichMenuItem;
            var localWhichMenuItemSubItem = whichMenuItemSubItem;
            CommonAssignKeyAndVerify(whichMenuItemSubItem, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    var reassignCollapseButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCollapseButton);
                    reassignCollapseButton.DoClickPoint(1);
                    var reassignDropdown = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDropdown);
                    ATS allReassignCatalogListItems = null;
                    if (_theLastMenuItem.Equals(string.Empty) || !_theLastMenuItem.Equals(whichMenuItem))
                    {
                        _CollapseReassignMenus(reassignDropdown);
                    }
                    _theLastMenuItem = whichMenuItem;
                    allReassignCatalogListItems = reassignDropdown.GetElementsFromChild(MPObj.ReassignCatalogListItem, returnNullWhenException: true);
                    if (allReassignCatalogListItems == null)
                    {
                        var whichCatalog = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItem });
                        whichCatalog.DoClickPoint(1);
                    }
                    var subItem = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItemSubItem }, returnNullWhenException: true);
                    if (subItem == null && whichMenuItem.Equals(MasterPlus.ReassignMenuItems.LettersNumbers))
                    {
                        _ScrollMenuItemSubItem(reassignDropdown);
                        reassignDropdown = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDropdown); // refresh
                    }
                    if (subItem == null) // this extra if that just for the exception triggers by GetElementFromChild
                    {
                        subItem = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItemSubItem });
                    }
                    subItem.GetIAccessible().DoDefaultAction();
                }, onlyVerify);
        }

        public void DisableKey(string disableWhichKeyGrid, bool onlyVerify = false)
        {
            CommonAssignKeyAndVerify(MPObj.DisableKeyCheckbox.Name, disableWhichKeyGrid,
                (reassignDialog) =>
                {
                    var disableKeyCheckbox = reassignDialog.GetElementFromDescendants(MPObj.DisableKeyCheckbox);
                    disableKeyCheckbox.DoClickPoint(1);
                }, onlyVerify);
        }
        public void EnableKey(string disableWhichKeyGrid, bool onlyVerify = false)
        {
            CommonAssignKeyAndVerify(MPObj.EnableKeyCheckbox.Name, disableWhichKeyGrid,
                (reassignDialog) =>
                {
                    var enableKeyCheckbox = reassignDialog.GetElementFromDescendants(MPObj.EnableKeyCheckbox);
                    enableKeyCheckbox.DoClickPoint(1);
                }, onlyVerify);
        }


        private bool boolBreak = false;
        //for ma
        public void AssignInLoop(bool onlyVerify = false)
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            var keys = assignContainer.GetElementsAllChild().GetATCollection().ToList();
            //var keyCount = keys.Count;
            //for (var i = keyCount - 1; i >= 4; i--)
            //{
            //    keys.Remove(keys[i]);
            //}
            var reassignMenuItemsList = MasterPlus.ReassignMenuItems.GetReassignMenuItemsList().ToList();
            var validKeys = keys.Where(t => !t.GetElementInfo().IsOffscreen()).ToList();
            ReassignMenuOptionAndSubItems recordReassignMenuSubItems = null;
            for (var i = 0; i < validKeys.Count(); i++) 
            {
                if (recordReassignMenuSubItems != null)
                {
                    reassignMenuItemsList.Remove(recordReassignMenuSubItems);
                    recordReassignMenuSubItems = null;
                }
                if (!reassignMenuItemsList.Any()) // reassign items are less than keys
                {
                    return;
                }
                if (boolBreak)// add this here for when reassignMenuItemsList's count is 1
                {
                    boolBreak = false;
                }
                foreach (var reassignMenuSubItems in reassignMenuItemsList)
                {
                    if (boolBreak)
                    {
                        boolBreak = false;
                        break;
                    }
                    foreach (var subItem in reassignMenuSubItems.MenuSubItems)
                    {
                        AssignKeyFromReassignMenu(reassignMenuSubItems.MenuOption, subItem.Value,
                            validKeys[i].GetElementInfo().Name(), onlyVerify);
                        reassignMenuSubItems.MenuSubItems.Remove(subItem);
                        if (!reassignMenuSubItems.MenuSubItems.Any())
                        {
                            recordReassignMenuSubItems = reassignMenuSubItems;
                        }
                        if (i == (validKeys.Count() - 1))
                        {
                            if (reassignMenuItemsList.Any() || reassignMenuSubItems.MenuSubItems.Any())
                            {
                                i = -1;
                            }
                        }
                        boolBreak = true;
                        break;
                    }
                }
            }
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
                if (UtilProcess.IsProcessExistedByName("RENEW"))
                {
                    UtilProcess.KillProcessByName("RENEW");
                    UtilTime.WaitTime(1);
                }
                var mainWindow = GetMasterPlusMainWindow();
                mainWindow.GetElement(MPObj.DeviceList);
                return mainWindow;
            }, timeout);
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
