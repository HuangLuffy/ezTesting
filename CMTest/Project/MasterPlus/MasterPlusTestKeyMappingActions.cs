﻿using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ATLib.Input;
using static ATLib.Input.Hw;

namespace CMTest.Project.MasterPlus
{
    public partial class MasterPlusTestActions
    {
        #region KeyMapping
        public void CommonAssignKeyAndVerify(string pressedKey, string assignWhichKeyGrid, Action<AT> assignAction, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            var masterPlusMainWindow = GetMasterPlusMainWindow();
            UtilWinApi.SetForegroundWindow(masterPlusMainWindow.GetElementInfo().GetHwnd());
            //var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            //var keyGridNeedToBeAssigned = assignContainer.GetElementFromChild(new ATElementStruct() { Name = assignWhichKeyGrid });
            var keyGridNeedToBeAssigned = GetAllKbGridKeys().First(x => x.GetElementInfo().Name().Equals(assignWhichKeyGrid));

            keyGridNeedToBeAssigned.DoClickPoint(0.6);
            var reassignDialog = masterPlusMainWindow.GetElementFromChild(MPObj.ReassignDialog);
            if (blAssignKey)
            {
                try
                {
                    assignAction.Invoke(reassignDialog);
                }
                catch (Exception e)
                {
                    _r.RecordActionFailedDuringCaseRunning($"{e.Message}", $"{e.Message}", blContinueTest: true);
                    try
                    {
                        var closeButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCloseButton);
                        closeButton.DoClickPoint(0.6);
                        closeButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCloseButton);// if dropdown
                        closeButton.DoClickPoint(0.6);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    return; // do not verify when failed
                }
            }
            VerifyAssignedKeyValueAndGridColor(keyGridNeedToBeAssigned, reassignDialog, pressedKey, blAssignKey);
            if (blVerifyKeyWork)
            {
                VerifyKeyWork(keyGridNeedToBeAssigned, pressedKey);
            }
        }
        public void DifferentFlowForDifferentPressedKey(string pressedKey, Action disableKeyCheckboxAction, Action enableKeyCheckboxAction, Action assignedAction)
        {
            if (pressedKey.Equals(MPObj.DisableKeyCheckbox.Name))
            {
                disableKeyCheckboxAction.Invoke();
            }
            else if (pressedKey.Equals(MPObj.EnableKeyCheckbox.Name))
            {
                enableKeyCheckboxAction.Invoke();
            }
            else
            {
                assignedAction.Invoke();
            }
        }
        private void VerifyKeyWork(AT keyGridNeedToBeAssigned, string pressedKey)
        {
            var key = Hw.KbKeys.GetScKeyByUiaName(keyGridNeedToBeAssigned.GetElementInfo().Name());
            DifferentFlowForDifferentPressedKey(pressedKey,
                () => {
                    TestIt.SendUsbKeyAndCheck(key, null);
                },
                () => {
                    TestIt.SendUsbKeyAndCheck(key, key.KeyCode);
                },
                () => {
                    var f = typeof(MasterPlus.ReassignMenuItems.MediaKeysItems).GetFields().FirstOrDefault(x => x.GetValue("").Equals(pressedKey));
                    if (f != null)
                    {
                        var key1 = Hw.KbKeys.GetScKeyByVarName(f.Name);
                        TestIt.SendUsbKeyAndCheck(key, key1.KeyCode);
                    }
                    else
                    {
                        TestIt.SendUsbKeyAndCheck(key, Hw.KbKeys.GetScKeyByUiaName(pressedKey).KeyCode);
                    }
                });
        }
        private AT LaunchAndGetWmplayer()
        {
            UtilProcess.KillAllProcessesByName("wmplayer");
            UtilTime.WaitTime(1);
            var p = UtilWmp.StartWmpWithMedias(Path.Combine(this.MediaFolderPath, "1.mp3"), Path.Combine(this.MediaFolderPath, "2.mp3"), Path.Combine(this.MediaFolderPath, "3.mp3"));
            var wmpWindow = new AT().GetElementFromHwndAndWaitAppears(p);
            HWSimulator.HWSend.MoveMouseTo((int)(wmpWindow.GetElementInfo().RectangleRight() - wmpWindow.GetElementInfo().RectangleLeft())/2, (int)(wmpWindow.GetElementInfo().RectangleBottom() - wmpWindow.GetElementInfo().RectangleTop()) / 2);
            return wmpWindow;
        }
        private void VerifyAssignedKeyValueAndGridColor(AT keyGridNeedToBeAssigned, AT reassignDialog, string pressedKey, bool blAssignKey)
        {
            var gridColorValue = KeyMappingGridColor.Purple;
            try
            {
                var assignedValue = reassignDialog.GetElementFromDescendants(MPObj.AssignedValue, returnNullWhenException: true);
                DifferentFlowForDifferentPressedKey(pressedKey, 
                    () => {
                        gridColorValue = KeyMappingGridColor.Red;
                        if (assignedValue != null && !assignedValue.GetElementInfo().IsOffscreen())
                        {
                            _r.RecordActionFailedDuringCaseRunning($"The Key is not disabled, the reassignable textbox is still there.", "ReassignTextboxStillThere");
                        }
                    }, 
                    () => {
                        gridColorValue = KeyMappingGridColor.Green;
                        var reassignTitleValue = reassignDialog.GetElementFromDescendants(MPObj.ReassignTitleValue);
                        if (assignedValue.GetElementInfo().FullDescription() != reassignTitleValue.GetElementInfo().FullDescription())
                        {
                            _r.RecordActionFailedDuringCaseRunning($"The assigned key is not restored to {reassignTitleValue.GetElementInfo().FullDescription()} when enabling it.", "assignedValueNotRestored");
                        }
                    }, 
                    () => {
                        gridColorValue = KeyMappingGridColor.Purple;
                        var value = assignedValue.GetElementInfo().FullDescription();
                        if (!value.Equals(pressedKey))
                        {
                            _r.RecordActionFailedDuringCaseRunning($"Input {pressedKey}, but got {value}.", "assignedValueWrong");
                        }
                        if (keyGridNeedToBeAssigned.GetElementInfo().Name().Equals(pressedKey))
                        {
                            gridColorValue = KeyMappingGridColor.Green;
                        }
                    });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                var saveOrCloseButton = reassignDialog.GetElementFromDescendants(blAssignKey ? MPObj.ReassignSaveButton : MPObj.ReassignCloseButton);
                saveOrCloseButton.DoClickPoint(0.4);
            }
            if (!keyGridNeedToBeAssigned.GetElementInfo().FullDescription().Equals(gridColorValue))
            {
                _r.RecordActionFailedDuringCaseRunning($"Key {pressedKey} in not in {KeyMappingGridColor.GetVarName(gridColorValue)} color.", "WrongColor");
            }
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
                throw new Exception("Failed to _ScrollMenuItemSubItem. " + e.Message);
            }
        }
        public void AssignKeyFromReassignMenu(string whichMenuItem, string whichMenuItemSubItem, string assignWhichKeyGrid, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            var localWhichMenuItem = whichMenuItem;
            var localWhichMenuItemSubItem = whichMenuItemSubItem;
            ATS allReassignCatalogListItems;
            CommonAssignKeyAndVerify(whichMenuItemSubItem, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    var reassignCollapseButton = reassignDialog.GetElementFromDescendants(MPObj.ReassignCollapseButton);
                    reassignCollapseButton.DoClickPoint(0.6);
                    var reassignDropdown = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDropdown);
                    if (_theLastMenuItem.Equals(string.Empty) || !_theLastMenuItem.Equals(whichMenuItem))
                    {
                        _CollapseReassignMenus(reassignDropdown);
                    }
                    _theLastMenuItem = whichMenuItem;
                    allReassignCatalogListItems = reassignDropdown.GetElementsFromChild(MPObj.ReassignCatalogListItem, returnNullWhenException: true);
                    if (allReassignCatalogListItems == null)
                    {
                        var whichCatalog = reassignDropdown.GetElementFromChild(new ATElementStruct() { FullDescriton = whichMenuItem });
                        whichCatalog.DoClickPoint(0.6);
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
                }, blAssignKey, blVerifyKeyWork);
        }

        public void DisableKey(string disableWhichKeyGrid, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            CommonAssignKeyAndVerify(MPObj.DisableKeyCheckbox.Name, disableWhichKeyGrid,
                (reassignDialog) =>
                {
                    var disableKeyCheckbox = reassignDialog.GetElementFromDescendants(MPObj.DisableKeyCheckbox);
                    disableKeyCheckbox.DoClickPoint(1);
                }, blAssignKey, blVerifyKeyWork);
        }
        public void EnableKey(string disableWhichKeyGrid, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            CommonAssignKeyAndVerify(MPObj.EnableKeyCheckbox.Name, disableWhichKeyGrid,
                (reassignDialog) =>
                {
                    var enableKeyCheckbox = reassignDialog.GetElementFromDescendants(MPObj.EnableKeyCheckbox);
                    enableKeyCheckbox.DoClickPoint(1);
                }, blAssignKey, blVerifyKeyWork);
        }
        public void AssignKeyOnReassignDialog(KeyPros pressedKey, KeyPros assignWhichKeyGrid, bool blScanCodeInput = false, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            CommonAssignKeyAndVerify(pressedKey.UiaName, assignWhichKeyGrid.UiaName,
                (reassignDialog) =>
                {
                    if (blScanCodeInput)
                    {
                        UtilKeys.PressByScanCode(pressedKey.ScanCode);
                    }
                    else
                    {
                        TestIt.USBController.SendToPort(pressedKey.Port);
                    }
                }, blAssignKey, blVerifyKeyWork);
        }
        private IEnumerable<AT> GetAllKbGridKeys()
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer, returnNullWhenException: true);
            var r = assignContainer != null ? assignContainer.GetElementsAllChild().GetATCollection().ToList() : GetMasterPlusMainWindow().GetElementsAllChild().GetATList().Where(x => x.GetElementInfo().FullDescription().StartsWith("#"));
            if (r.Count() == 0)
            {
                throw new Exception("Can not find any virtual keys on the UI.");
            }

            //IEnumerable<AT> t = new List<AT>
            //{
            //    r.ElementAt(0),
            //    r.ElementAt(1),
            //    r.ElementAt(2)
            //};
            //return t;
            return r;
        }
        private bool _blBreak = false;
        //for ma
        private string GetDupKeys(IEnumerable<AT> validKeys)
        {
            var keysNameList = new List<string>();
            foreach (var item in validKeys)
            {
                keysNameList.Add(item.GetElementInfo().Name());
            }
            var u = keysNameList.GroupBy(o => o).Where(g => g.Count() > 1).Select(g => g.ElementAt(0));
            string t1 = "";
            foreach (var item in u)
            {
                t1 += $"[{item}]";
            }
            return t1;
        }
        public void AssignInLoop(bool blAssignKey = true, bool blVerifyKeyWork = true, bool blScanCodeInput = false)
        {
            var keys = GetAllKbGridKeys();
            var reassignMenuItemsList = MasterPlus.ReassignMenuItems.GetReassignMenuItemsListOneToOne();
            var validKeys = keys.Where(t => !t.GetElementInfo().IsOffscreen());
            var dupString = GetDupKeys(validKeys);
            if (!dupString.Equals(""))
            {
                _r.CurrentTestCase.ErrorMessages.Add($"There are duplicated keys exist. {dupString}");
            }
            KeyPros assignWhichKeyGrid = null;

            UtilLoop.OneParentItemOneChildItemLoopController(validKeys, reassignMenuItemsList, (x, y) =>
            {
                try
                {
                    if (x.GetElementInfo().Name().Equals("PAUSE"))
                    {
                        ;
                    }
                    if (blScanCodeInput)
                    {
                        try
                        {
                            assignWhichKeyGrid = Hw.KbKeys.GetScKeyByUiaName(x.GetElementInfo().Name());
                        }
                        catch (Exception)
                        {
                            assignWhichKeyGrid = new KeyPros() { UiaName = x.GetElementInfo().Name() };
                        }
                        AssignKeyOnReassignDialog(Hw.KbKeys.SC_KEY_A, Hw.KbKeys.GetScKeyByUiaName(x.GetElementInfo().Name()), blScanCodeInput, blAssignKey, blVerifyKeyWork);
                    }
                    else
                    {
                        AssignKeyFromReassignMenu(y.MenuOption, y.MenuSubItems, x.GetElementInfo().Name(), blAssignKey, blVerifyKeyWork);
                    }
                }
                catch (Exception e)
                {
                    _r.CurrentTestCase.ErrorMessages.Add(e.Message);
                }
            }, (x) =>
            {
                try
                {
                    try
                    {
                        assignWhichKeyGrid = Hw.KbKeys.GetScKeyByUiaName(x.GetElementInfo().Name());
                    }
                    catch (Exception)
                    {
                        assignWhichKeyGrid = new KeyPros() { UiaName = x.GetElementInfo().Name() };
                    }
                    AssignKeyOnReassignDialog(Hw.KbKeys.SC_KEY_A, Hw.KbKeys.GetScKeyByUiaName(x.GetElementInfo().Name()), blScanCodeInput, blAssignKey, blVerifyKeyWork);
                }
                catch (Exception e)
                {
                    _r.CurrentTestCase.ErrorMessages.Add(e.Message);
                }
            }, false);
        }
        public void AssignInLoop1(bool blAssignKey = true, bool blVerifyKeyWork = true, bool blScanCodeInput = false)
        {
            var keys = GetAllKbGridKeys();

            var reassignMenuItemsList = MasterPlus.ReassignMenuItems.GetReassignMenuItemsList().ToList();
            var validKeys = keys.Where(t => !t.GetElementInfo().IsOffscreen()).ToList();
            ReassignMenuOptionAndSubItems recordReassignMenuSubItems = null;
            for (var i = 0; i < validKeys.Count(); i++)
            {
                if (recordReassignMenuSubItems != null)
                {
                    reassignMenuItemsList.Remove(recordReassignMenuSubItems); //remove null list  
                    recordReassignMenuSubItems = null;
                }
                if (!reassignMenuItemsList.Any()) // reassign items are less than keys then assign A for rest of keys
                {
                    try
                    {
                        AssignKeyOnReassignDialog(Hw.KbKeys.SC_KEY_A, Hw.KbKeys.GetScKeyByUiaName(validKeys[i].GetElementInfo().Name()), blScanCodeInput, blAssignKey, blVerifyKeyWork);
                    }
                    catch (Exception e)
                    {
                        _r.CurrentTestCase.ErrorMessages.Add(e.Message);
                    }
                    continue;
                }
                if (_blBreak)// add this here for when reassignMenuItemsList's count is 1
                {
                    _blBreak = false;
                }
                foreach (var reassignMenuSubItems in reassignMenuItemsList)
                {
                    if (_blBreak)
                    {
                        _blBreak = false;
                        break;
                    }
                    foreach (var subItem in reassignMenuSubItems.MenuSubItems)
                    {
                        try
                        {
                            AssignKeyFromReassignMenu(reassignMenuSubItems.MenuOption, subItem.Value, validKeys[i].GetElementInfo().Name(), blAssignKey, blVerifyKeyWork);
                        }
                        catch (Exception e)
                        {
                            _r.CurrentTestCase.ErrorMessages.Add(e.Message);
                        }
                        reassignMenuSubItems.MenuSubItems.Remove(subItem);
                        if (!reassignMenuSubItems.MenuSubItems.Any())
                        {
                            recordReassignMenuSubItems = reassignMenuSubItems; //record null reassignMenuSubItems for row 316 to remove  [reassignMenuItemsList.Remove(recordReassignMenuSubItems); //remove null list  ]
                        }
                        if (i == (validKeys.Count() - 1))
                        {
                            if (reassignMenuItemsList.Any() || reassignMenuSubItems.MenuSubItems.Any())
                            {
                                i = -1; // reassign items are more than keys, to reassign from Esc
                                if (!blVerifyKeyWork) //for manual when all keys are assigned
                                {
                                    return;
                                }
                            }
                        }
                        _blBreak = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
