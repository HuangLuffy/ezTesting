using ATLib;
using ATLib.Input;
using CommonLib.Util;
using CommonLib.Util.os;
using CMTest.Vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.Portal
{
    public class PortalTestActions : Portal
    {
        PortalObj _PortalObj;
        VmOps _VmOps;
        public PortalTestActions()
        {
            this.Initialize();
            this._PortalObj = (PortalObj)this.Obj;
            this._VmOps = new VmOps();
        }

        public void PlugOutDeviceFromVM(string deviceNameVM)
        {   //Using admin to run VM in win10 otherwise rightclick would not work.
            try
            {
                if (deviceNameVM.Equals(VMObj.DeviceItem.Item_MH650.Name)) // Show 2 identical devices in VM.
                {
                    this._VmOps.PlugOutInDeviceFromVM(deviceNameVM, 1);
                    this.Timeout = 5;
                }
                else
                {
                    this._VmOps.PlugOutInDeviceFromVM(deviceNameVM);
                    this.Timeout = 20;
                }
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for connecting/disconnecting. ({this.Timeout}s)", this.Timeout);
            }
            catch (Exception)
            {
                
            }
        }
        public void ProfilesImExAimpadSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();
            UtilTime.WaitTime(2);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.WriteConsoleTitle(i, $"Starting to Import Export switch (Support Aimpad Mode). ({Timeout}s)", Timeout);
                this.ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT", true);
                this.ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT", true);
                this.ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "", true);
                this.ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "", true);
                this.ProfilesImExSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "", true);
                this.ProfilesImExSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "", true);
            }
        }

        public void ProfilesImExSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(2);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.WriteConsoleTitle(i, $"Starting to Import Export switch. ({Timeout}s)", Timeout);
                this.ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT");
                this.ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT");
                this.ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "");
                this.ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "");
                this.ProfilesImExSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "");
                this.ProfilesImExSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "");
            }
        }
        private void WaitForEvent(AT Profile_tmp, AT Loading_tmp, bool IsAimpad = false)
        {
            if (IsAimpad)
            {
                this.WaitForLoadingOrReconnecting(Profile_tmp, Loading_tmp);
            }
            else
            {
                this.WaitForLoading(Loading_tmp);
            }
        }
        private void WaitForLoadingOrReconnecting(AT Profile_tmp, AT Loading_tmp)
        {
            if (UtilOS.GetOsVersion().Contains(UtilOS.Name.Win10))
            {
                UtilTime.WaitTime(3);
            }
            else
            {
                UtilTime.WaitTime(3);
            }
            try
            {
                Profile_tmp = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Profile_1, TreeScope: AT.TreeScope.Descendants, ReturnNullWhenException: true);
                if (Profile_tmp == null)
                {
                    this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
                    return;
                }
                do
                {
                    Loading_tmp = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants, ReturnNullWhenException: true);
                } while (Loading_tmp != null);
            }
            catch (Exception)
            {

            }
        }
        private void WaitForLoading(AT Loading_tmp)
        {
            UtilTime.WaitTime(2);
            try
            {
                while (true)
                {
                    Loading_tmp = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {

            }
        }
        private void ProfilesImExSwitch(ATElementStruct WhichProfile, AT Profile_tmp, AT Loading_tmp, string WhereToClick, bool IsAimpad = false)
        {
            Profile_tmp = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            Profile_tmp.DoClickPoint();
            this.WaitForEvent(Profile_tmp, Loading_tmp, IsAimpad);
            Profile_tmp = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            if (WhereToClick.Equals("EXPORT"))
            {
                Profile_tmp.GetElement(ATElementStruct: PortalObj.Profile_EXPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
                AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout: 5);
                AT Button_Save = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
                Button_Save.DoClickPoint();
                try
                {
                    dialog.GetElement(ATElementStruct: PortalObj.Button_Exist_Yes, TreeScope: AT.TreeScope.Descendants, Timeout: 1).DoClickPoint();
                }
                catch (Exception)
                {

                }
                this.WaitForEvent(Profile_tmp, Loading_tmp, IsAimpad);
            }
            else if (WhereToClick.Equals("IMPORT"))
            {
                Profile_tmp.GetElement(ATElementStruct: PortalObj.Profile_IMPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
                AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout: 5);
                dialog = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
                dialog.DoClickPoint();
                this.WaitForEvent(Profile_tmp, Loading_tmp, IsAimpad);
            }
        }
        //public void ProfilesImExSwitch(long TEST_TIMES)
        //{
        //    AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
        //    TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
        //    UtilTime.WaitTime(2);
        //    AT Profile_tmp = null;
        //    AT Loading_tmp = null;
        //    this.WriteConsoleTitle(this.LaunchTimes, $"Starting to Import Export switch. ({Timeout}s)", Timeout);
        //    for (int i = 1; i < TEST_TIMES; i++)
        //    {
        //        this.ProfilesComplexSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT");
        //        this.ProfilesComplexSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT");
        //        this.ProfilesComplexSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "");
        //        this.ProfilesComplexSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "");
        //        this.ProfilesComplexSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "");
        //        this.ProfilesComplexSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "");
        //    }
        //}

        //private void ProfilesComplexSwitch(ATElementStruct WhichProfile, AT Profile, AT Loading, string WhereToClick)
        //{
        //    Profile = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
        //    if (WhereToClick.Equals("EXPORT"))
        //    {
        //        Profile.GetElement(ATElementStruct: PortalObj.Profile_EXPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
        //        AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout: 5);
        //        AT Button_Save = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
        //        Button_Save.DoClickPoint();
        //        dialog.GetElement(ATElementStruct: PortalObj.Button_Exist_Yes, TreeScope: AT.TreeScope.Descendants, Timeout: 2).DoClickPoint();
        //    }
        //    else if(WhereToClick.Equals("IMPORT"))
        //    {
        //        Profile.GetElement(ATElementStruct: PortalObj.Profile_IMPORT, TreeScope: AT.TreeScope.Descendants).DoClickPoint();
        //        AT dialog = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: AT.TreeScope.Children, Timeout:5);
        //        dialog = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: AT.TreeScope.Children, Timeout: 1);
        //        dialog.DoClickPoint();
        //    }
        //    Profile.DoClickPoint();
        //    UtilTime.WaitTime(3);
        //    try
        //    {
        //        while (true)
        //        {
        //            Loading = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //}

        public void ProfilesSimpleSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(1);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.WriteConsoleTitle(i, $"Starting to simple switch. ({Timeout}s)", Timeout);
                this.ProfilesSimpleSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp);
            }
        }
        private void ProfilesSimpleSwitch(ATElementStruct WhichProfile, AT Profile_tmp, AT Loading_tmp)
        {
            Profile_tmp = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            Profile_tmp.DoClickPoint();
            this.WaitForLoading(Loading_tmp);
        }
    }
}
