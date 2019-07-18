using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.Portal.VmOerations
{
    public class VmOps
    {
        private void OpenRemovableDevices()
        {
            AT Window_VM = new AT().GetElement(VMObj.Window_VM);
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
                ATS Item_Targets = new AT().GetElements(Name: deviceNameVM, TreeScope: AT.TreeScope.Descendants, ControlType: AT.ControlType.MenuItem);
                itemTarget = Item_Targets.GetATCollection()[targetIndex];
            }
            else
            {
                itemTarget = new AT().GetElement(Name: deviceNameVM, TreeScope: AT.TreeScope.Descendants, ControlType: AT.ControlType.MenuItem);
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

        public void PlugOutInDeviceFromVM(string deviceNameVM, int itemIndex = 0)
        {   //Using admin to run VM in win10 otherwise rightclick would no work.
            this.OpenRemovableDevices();
            AT Item_Target = this.GetTargetItem(deviceNameVM, itemIndex);
            Item_Target.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
            this.PlugoutOrIn();
        }
    }
}
