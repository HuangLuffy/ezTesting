using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalTestActions : Portal
    {
        PortalObj _PortalObj;
        public PortalTestActions()
        {
            this.Initialize();
            this._PortalObj = (PortalObj)this.Obj;
        }

        public void VMPlugOutDevice(string deviceNameVM)
        {
            try
            {
                AT Window_VM = new AT().GetElement(VMObj.Window_VM);
                Window_VM.DoSetFocus();
                UtilTime.WaitTime(1);
                AT Tab_TestVM = Window_VM.GetElement(VMObj.Tab_TestVM);
                Tab_TestVM.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.RIGHT);
                UtilTime.WaitTime(0.5);
                //AT Item_MenuContext = new AT().GetElement(VM.Menu_Context);
                //AT Item_RemovableDevices = Item_MenuContext.GetElement(VM.Item_RemovableDevices);
                AT Item_RemovableDevices = new AT().GetElement(VMObj.Item_RemovableDevices);
                Item_RemovableDevices.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
                UtilTime.WaitTime(0.5);
                AT Item_Target = new AT().GetElement(Name: deviceNameVM, TreeScope: AT.TreeScope.Descendants, ControlType: AT.ControlType.MenuItem);
                Item_Target.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
                UtilTime.WaitTime(1);
                AT Item_Con = null;
                try
                {
                    Item_Con = new AT().GetElement(VMObj.Item_Connect);
                }
                catch (Exception)
                {
                    try
                    {
                        Item_Con = new AT().GetElement(VMObj.Item_Disconnect);
                    }
                    catch (Exception)
                    {

                    }
                }
                if (Item_Con != null)
                {
                    Item_Con.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.LEFT);
                    Timeout = 20;
                    UtilTime.WaitTime(20);
                    this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for connecting/disconnecting. ({Timeout}s)", this.Timeout);
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void ProfilesComplexSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(2);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            this.WriteConsoleTitle(this.LaunchTimes, $"Starting to complex switch. ({Timeout}s)", Timeout);
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.ProfilesComplexSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT");
                this.ProfilesComplexSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT");
                this.ProfilesComplexSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "");
                this.ProfilesComplexSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "");
                this.ProfilesComplexSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "");
                this.ProfilesComplexSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "");
            }
        }

        private void ProfilesComplexSwitch(ATElementStruct WhichProfile, AT Profile, AT Loading, string WhereToClick)
        {
            Profile = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            if (WhereToClick.Equals("EXPORT"))
            {
                Profile.GetElement(ATElementStruct: PortalObj.Profile_EXPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
                AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout: 5);
                //dialog.DoWindowEvents().Close();
                AT Button_Save = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
                Button_Save.DoClickPoint();
                dialog.GetElement(ATElementStruct: PortalObj.Button_Exist_Yes, TreeScope: AT.TreeScope.Descendants, Timeout: 2).DoClickPoint();
            }
            else if(WhereToClick.Equals("IMPORT"))
            {
                Profile.GetElement(ATElementStruct: PortalObj.Profile_IMPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
                AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout:5);
                //dialog.DoWindowEvents().Close();
                dialog = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
                dialog.DoClickPoint();
            }
            Profile.DoClickPoint();
            UtilTime.WaitTime(3);
            try
            {
                while (true)
                {
                    Loading = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void ProfilesSimpleSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(1);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            this.WriteConsoleTitle(this.LaunchTimes, $"Starting to simple switch. ({Timeout}s)", Timeout);
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.ProfilesSimpleSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp);
            }
        }
        private void ProfilesSimpleSwitch(ATElementStruct WhichProfile, AT Profile, AT Loading)
        {
            Profile = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            Profile.DoClickPoint();
            UtilTime.WaitTime(3);
            try
            {
                while (true)
                {
                    Loading = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
