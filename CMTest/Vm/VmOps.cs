using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;

namespace CMTest.Vm
{
    public class VmOps
    {
        public int PlugOutOrInDeviceCannotFindTimes = 0;

        public AT GetVm()
        {
            return new AT().GetElement(VmObj.Window_VM);
        }
        private void OpenRemovableDevices(AT windowVm)
        {
            var tabTestVm = windowVm.GetElement(VmObj.Tab_TestVM);
            tabTestVm.DoSetFocus();
            UtilTime.WaitTime(1);
            tabTestVm.DoClickPoint(10, 10, mk: HWSimulator.HWSend.MouseKeys.RIGHT);
            UtilTime.WaitTime(0.5);
            var itemRemovableDevices = new AT().GetElement(VmObj.Item_RemovableDevices);
            itemRemovableDevices.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            UtilTime.WaitTime(0.5);
        }

        private AT GetTargetItem(string deviceNameVm, int targetIndex)
        {
            AT itemTarget;
            if (targetIndex > 0) // Show 2 identical devices in VM.
            {
                var itemTargets = new AT().GetElements(name: deviceNameVm, treeScope: ATElement.TreeScope.Descendants, controlType: ATElement.ControlType.MenuItem);
                itemTarget = itemTargets.GetATCollection()[targetIndex];
            }
            else
            {
                itemTarget = new AT().GetElement(name: deviceNameVm, treeScope: ATElement.TreeScope.Descendants, controlType: ATElement.ControlType.MenuItem);
            }
            return itemTarget;
        }

        private void PlugOutOrIn()
        {
            AT itemCon;
            UtilTime.WaitTime(1);
            try
            {
                itemCon = new AT().GetElement(VmObj.Item_Connect);
            }
            catch (Exception)
            {
                itemCon = new AT().GetElement(VmObj.Item_Disconnect);
            }
            itemCon.DoClickPoint(10, 10);
        }

        public void PlugOutInDeviceFromVm(string deviceNameVm, int itemIndex = 0, AT vmWindow = null)
        {   //Using admin to run VM in win10 otherwise right-click would no work.
            OpenRemovableDevices(vmWindow);
            AT itemTarget;
            try
            {
                itemTarget = GetTargetItem(deviceNameVm, itemIndex);
            }
            catch (Exception)
            {
                PlugOutOrInDeviceCannotFindTimes += 1;
                throw;
            }
            itemTarget.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            PlugOutOrIn();
        }
    }
}
