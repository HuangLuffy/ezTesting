﻿using ATLib;
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
    public partial class MasterPlusTestActions
    {
        #region KeyMapping
        public void CommonAssignKeyAndVerify(string keyValueNeedToInput, string assignWhichKeyGrid, Action<AT> assignAction, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);
            var keyGridNeedToBeAssigned = assignContainer.GetElementFromChild(new ATElementStruct() { Name = assignWhichKeyGrid });
            keyGridNeedToBeAssigned.DoClickPoint(1);
            var reassignDialog = GetMasterPlusMainWindow().GetElementFromChild(MPObj.ReassignDialog);
            //For old //var reassignDialog = GetMasterPlusMainWindow().GetElementFromDescendants(MPObj.ReassignDialog);
            if (blAssignKey)
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
            if (blVerifyKeyWork)
            {
                VerifyKeyWork(keyGridNeedToBeAssigned, keyValueNeedToInput);
            }
        }

        private void VerifyKeyWork(AT keyGridNeedToBeAssigned, string keyValueNeedToInput)
        {

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
                    reassignCollapseButton.DoClickPoint(1);
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
        public void AssignKeyOnReassignDialog(ScanCode keyValueNeedToInputScanCode, string assignWhichKeyGrid, bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            var keyValueNeedToInput = UtilEnum.GetEnumNameByValue<ScanCode>(keyValueNeedToInputScanCode);
            CommonAssignKeyAndVerify(keyValueNeedToInput, assignWhichKeyGrid,
                (reassignDialog) =>
                {
                    KbEvent.Press(keyValueNeedToInputScanCode);
                    UtilTime.WaitTime(1);
                }, blAssignKey, blVerifyKeyWork);
        }
        private bool _blBreak = false;
        //for ma
        public void AssignInLoop(bool blAssignKey = true, bool blVerifyKeyWork = true)
        {
            var assignContainer = GetMasterPlusMainWindow().GetElementFromChild(MPObj.AssignContainer);

            assignContainer.Spy();

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
                        AssignKeyFromReassignMenu(reassignMenuSubItems.MenuOption, subItem.Value,
                            validKeys[i].GetElementInfo().Name(), blAssignKey);
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
                        _blBreak = true;
                        break;
                    }
                }
            }
        }

        #endregion
    }
}