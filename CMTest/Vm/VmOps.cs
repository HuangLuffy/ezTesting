using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;

namespace CMTest.Vm
{
    public class VmOps
    {
        public AT GetVM()
        {
            return new AT().GetElement(VMObj.Window_VM);
        }
        private void OpenRemovableDevices(AT Window_VM)
        {
            AT Tab_TestVM = Window_VM.GetElement(VMObj.Tab_TestVM);
            Tab_TestVM.DoSetFocus();
            UtilTime.WaitTime(1);
            Tab_TestVM.DoClickPoint(10, 10, mk: HWSimulator.HWSend.MouseKeys.RIGHT);
            UtilTime.WaitTime(0.5);
            AT Item_RemovableDevices = new AT().GetElement(VMObj.Item_RemovableDevices);
            Item_RemovableDevices.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            UtilTime.WaitTime(0.5);
        }

        private AT GetTargetItem(string deviceNameVM, int targetIndex)
        {
            AT itemTarget = null;
            if (targetIndex > 0) // Show 2 identical devices in VM.
            {
                ATS Item_Targets = new AT().GetElements(Name: deviceNameVM, TreeScope: ATElement.TreeScope.Descendants, ControlType: ATElement.ControlType.MenuItem);
                itemTarget = Item_Targets.GetATCollection()[targetIndex];
            }
            else
            {
                itemTarget = new AT().GetElement(Name: deviceNameVM, TreeScope: ATElement.TreeScope.Descendants, ControlType: ATElement.ControlType.MenuItem);
            }
            return itemTarget;
        }

        private void PlugoutOrIn()
        {
            AT itemCon = null;
            UtilTime.WaitTime(1);
            try
            {
                itemCon = new AT().GetElement(VMObj.Item_Connect);
            }
            catch (Exception)
            {
                try
                {
                    itemCon = new AT().GetElement(VMObj.Item_Disconnect);
                }
                catch (Exception)
                {

                }
            }
            itemCon.DoClickPoint(10, 10, mk: HWSimulator.HWSend.MouseKeys.LEFT);
        }

        public void PlugOutInDeviceFromVM(string deviceNameVM, int itemIndex = 0, AT VM_Window = null)
        {   //Using admin to run VM in win10 otherwise rightclick would no work.
            OpenRemovableDevices(VM_Window);
            AT Item_Target = GetTargetItem(deviceNameVM, itemIndex);
            Item_Target.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            PlugoutOrIn();
        }
    }
}
