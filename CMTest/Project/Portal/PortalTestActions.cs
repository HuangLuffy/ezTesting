using ATLib;
using CommonLib.Util;
using CommonLib.Util.os;
using CMTest.Vm;
using System;


namespace CMTest.Project.Portal
{
    public class PortalTestActions : Portal
    {
        private readonly PortalObj _PortalObj;
        readonly VmOps _VmOps;
        public PortalTestActions()
        {
            Initialize();
            _PortalObj = (PortalObj)Obj;
            _VmOps = new VmOps();
        }

        public void PlugOutDeviceFromVM(string deviceNameVM, string waitTime = "5", string index = "0")
        {   //Using admin to run VM in win10 otherwise rightclick would not work.
            AT VM_Window = _VmOps.GetVM();
            VM_Window.DoWindowEvents().Normal(0.5);
            try
            {
                _VmOps.PlugOutInDeviceFromVM(deviceNameVM, Convert.ToInt16(index), VM_Window);
                Timeout = Convert.ToInt16(waitTime);
                WriteConsoleTitle(LaunchTimes, $"Waiting for connecting/disconnecting. ({Timeout}s)", Timeout);
            }
            catch (Exception)
            {
                
            }
        }
        public void ProfilesImExAimpadSwitch(long testTimes)
        {
            AT TabItem_Profiles = SwMainWindow.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: ATElement.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();
            UtilTime.WaitTime(2);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < testTimes; i++)
            {
                WriteConsoleTitle(i, $"Starting to Import Export switch (Support Aimpad Mode). ({Timeout}s)", Timeout);
                ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT", true);
                ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT", true);
                ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "", true);
                ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "", true);
                ProfilesImExSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "", true);
                ProfilesImExSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "", true);
            }
        }

        public void ProfilesImExSwitch(long TEST_TIMES)
        {
            AT tabItemProfiles = SwMainWindow.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: ATElement.TreeScope.Descendants);
            tabItemProfiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(2);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                WriteConsoleTitle(i, $"Starting to Import Export switch. ({Timeout}s)", Timeout);
                ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "EXPORT");
                ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "IMPORT");
                ProfilesImExSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp, "");
                ProfilesImExSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp, "");
                ProfilesImExSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp, "");
                ProfilesImExSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp, "");
            }
        }
        private void WaitForEvent(AT profileTmp, AT loadingTmp, bool isAimpad = false)
        {
            if (isAimpad)
            {
                WaitForLoadingOrReconnecting(profileTmp, loadingTmp);
            }
            else
            {
                WaitForLoading(loadingTmp);
            }
        }
        private void WaitForLoadingOrReconnecting(AT profileTmp, AT loadingTmp)
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
                profileTmp = SwMainWindow.GetElement(ATElementStruct: PortalObj.Profile_1, TreeScope: ATElement.TreeScope.Descendants, ReturnNullWhenException: true);
                if (profileTmp == null)
                {
                    SwMainWindow.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: ATElement.TreeScope.Descendants).DoClickPoint();
                    return;
                }
                do
                {
                    loadingTmp = SwMainWindow.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: ATElement.TreeScope.Descendants, ReturnNullWhenException: true);
                } while (loadingTmp != null);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private void WaitForLoading(AT loadingTmp)
        {
            UtilTime.WaitTime(2);
            try
            {
                while (true)
                {
                    loadingTmp = SwMainWindow.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: ATElement.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private void ProfilesImExSwitch(ATElementStruct whichProfile, AT profileTmp, AT loadingTmp, string whereToClick, bool isAimpad = false)
        {
            profileTmp = SwMainWindow.GetElement(ATElementStruct: whichProfile, TreeScope: ATElement.TreeScope.Descendants);
            profileTmp.DoClickPoint();
            WaitForEvent(profileTmp, loadingTmp, isAimpad);
            profileTmp = SwMainWindow.GetElement(ATElementStruct: whichProfile, TreeScope: ATElement.TreeScope.Descendants);
            if (whereToClick.Equals("EXPORT"))
            {
                profileTmp.GetElement(ATElementStruct: PortalObj.Profile_EXPORT, TreeScope: ATElement.TreeScope.Descendants).DoClickPoint();
                AT dialog = SwMainWindow.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: ATElement.TreeScope.Children, Timeout: 5);
                AT buttonSave = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: ATElement.TreeScope.Children, Timeout: 1);
                buttonSave.DoClickPoint();
                try
                {
                    dialog.GetElement(ATElementStruct: PortalObj.Button_Exist_Yes, TreeScope: ATElement.TreeScope.Descendants, Timeout: 1).DoClickPoint();
                }
                catch (Exception)
                {
                    // ignored
                }

                WaitForEvent(profileTmp, loadingTmp, isAimpad);
            }
            else if (whereToClick.Equals("IMPORT"))
            {
                profileTmp.GetElement(ATElementStruct: PortalObj.Profile_IMPORT, TreeScope: ATElement.TreeScope.Descendants).DoClickPoint();
                AT dialog = SwMainWindow.GetElement(ATElementStruct: PortalObj.Window_OpenFIle, TreeScope: ATElement.TreeScope.Children, Timeout: 5);
                dialog = dialog.GetElement(ATElementStruct: PortalObj.Button_Save, TreeScope: ATElement.TreeScope.Children, Timeout: 1);
                dialog.DoClickPoint();
                WaitForEvent(profileTmp, loadingTmp, isAimpad);
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
            AT TabItem_Profiles = SwMainWindow.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: ATElement.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(1);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                WriteConsoleTitle(i, $"Starting to simple switch. ({Timeout}s)", Timeout);
                ProfilesSimpleSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp);
                ProfilesSimpleSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp);
                ProfilesSimpleSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp);
                ProfilesSimpleSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp);
            }
        }
        private void ProfilesSimpleSwitch(ATElementStruct WhichProfile, AT Profile_tmp, AT Loading_tmp)
        {
            Profile_tmp = SwMainWindow.GetElement(ATElementStruct: WhichProfile, TreeScope: ATElement.TreeScope.Descendants);
            Profile_tmp.DoClickPoint();
            WaitForLoading(Loading_tmp);
        }
    }
}
