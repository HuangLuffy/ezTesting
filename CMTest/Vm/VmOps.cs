using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;

namespace CMTest.Vm
{
    public class VmOps
    {
        public AT GetVm()
        {
            return new AT().GetElement(VMObj.Window_VM);
        }
        private void OpenRemovableDevices(AT windowVm)
        {
            var tabTestVm = windowVm.GetElement(VMObj.Tab_TestVM);
            tabTestVm.DoSetFocus();
            UtilTime.WaitTime(1);
            tabTestVm.DoClickPoint(10, 10, mk: HWSimulator.HWSend.MouseKeys.RIGHT);
            UtilTime.WaitTime(0.5);
            AT itemRemovableDevices = new AT().GetElement(VMObj.Item_RemovableDevices);
            itemRemovableDevices.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            UtilTime.WaitTime(0.5);
        }

        private AT GetTargetItem(string deviceNameVm, int targetIndex)
        {
            AT itemTarget;
            if (targetIndex > 0) // Show 2 identical devices in VM.
            {
                var itemTargets = new AT().GetElements(Name: deviceNameVm, TreeScope: ATElement.TreeScope.Descendants, ControlType: ATElement.ControlType.MenuItem);
                itemTarget = itemTargets.GetATCollection()[targetIndex];
            }
            else
            {
                itemTarget = new AT().GetElement(Name: deviceNameVm, TreeScope: ATElement.TreeScope.Descendants, ControlType: ATElement.ControlType.MenuItem);
            }
            return itemTarget;
        }

        private void PlugOutOrIn()
        {
            AT itemCon = null;
            UtilTime.WaitTime(1);
            try
            {
                itemCon = new AT().GetElement(VMObj.Item_Connect);
            }
            catch (Exception)
            {
                itemCon = new AT().GetElement(VMObj.Item_Disconnect);
            }
            itemCon.DoClickPoint(10, 10);
        }

        public void PlugOutInDeviceFromVm(string deviceNameVm, int itemIndex = 0, AT vmWindow = null)
        {   //Using admin to run VM in win10 otherwise right-click would no work.
            OpenRemovableDevices(vmWindow);
            var itemTarget = GetTargetItem(deviceNameVm, itemIndex);
            itemTarget.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            PlugOutOrIn();
        }
    }
}
