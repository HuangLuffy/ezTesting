﻿using System;
using ATLib;
using CMTest.Vm;
using CommonLib.Util;
using CommonLib.Util.OS;

namespace CMTest.Project.MasterPlusPer
{
    public class PortalTestActions : Portal
    {
        private readonly PortalObj _portalObj;
        private readonly VmOps _vmOps;
        public PortalTestActions()
        {
            _portalObj = (PortalObj)Obj;
            _vmOps = new VmOps();
        }

        public void PlugOutDeviceFromVm(string deviceNameVm, string waitTime = "5", string index = "0")
        {   //Using admin to run VM in win10 otherwise right-click would not work.
            var vmWindow = _vmOps.GetVm();
            vmWindow.DoWindowEvents().Normal(0.5);
            try
            {
                _vmOps.PlugOutInDeviceFromVm(deviceNameVm, Convert.ToInt16(index), vmWindow);
                //Timeout = Convert.ToInt16(waitTime);
                //WriteConsoleTitle(LaunchTimes, $"Waiting for connecting/disconnecting. ({Timeout}s)", Timeout);
            }
            catch (Exception)
            {
                // ignored
            }
            const int t = 10;
            if (_vmOps.PlugOutOrInDeviceCannotFindTimes == t)
            {
                throw new Exception($"Cannot find the device on the menu: VM > Removable Devices or wrong device vMname input in Conf.xml, please make sure this device is connected. Attempt times = {t}");
            }
        }
        public void ProfilesImExAimpadSwitch(long testTimes)
        {
            var TabItem_Profiles = SwMainWindow.GetElement(PortalObj.TabItem_PROFILES);
            TabItem_Profiles.DoClickPoint();
            UtilTime.WaitTime(2);
            for (var i = 1; i < testTimes; i++)
            {
                WriteConsoleTitle(i, $"Starting to Import Export switch (Support Aimpad Mode). ({Timeout}s)", Timeout);
                ProfilesImExSwitch(PortalObj.Profile_2, null, null, "EXPORT", true);
                ProfilesImExSwitch(PortalObj.Profile_1, null, null, "IMPORT", true);
                ProfilesImExSwitch(PortalObj.Profile_1, null, null, "", true);
                ProfilesImExSwitch(PortalObj.Profile_2, null, null, "", true);
                ProfilesImExSwitch(PortalObj.Profile_3, null, null, "", true);
                ProfilesImExSwitch(PortalObj.Profile_4, null, null, "", true);
            }
        }

        public void ProfilesImExSwitch(long testTimes)
        {
            var tabItemProfiles = SwMainWindow.GetElement(PortalObj.TabItem_PROFILES);
            tabItemProfiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(2);
            for (var i = 1; i < testTimes; i++)
            {
                WriteConsoleTitle(i, $"Starting to Import Export switch. ({Timeout}s)", Timeout);
                ProfilesImExSwitch(PortalObj.Profile_2, null, null, "EXPORT");
                ProfilesImExSwitch(PortalObj.Profile_1, null, null, "IMPORT");
                ProfilesImExSwitch(PortalObj.Profile_1, null, null, "");
                ProfilesImExSwitch(PortalObj.Profile_2, null, null, "");
                ProfilesImExSwitch(PortalObj.Profile_3, null, null, "");
                ProfilesImExSwitch(PortalObj.Profile_4, null, null, "");
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
            if (UtilOs.GetOsVersion().Contains(UtilOs.Name.Win10))
            {
                UtilTime.WaitTime(3);
            }
            else
            {
                UtilTime.WaitTime(3);
            }
            try
            {
                profileTmp = SwMainWindow.GetElement(aTElementStruct: PortalObj.Profile_1, treeScope: ATElement.TreeScope.Descendants, returnNullWhenException: true);
                if (profileTmp == null)
                {
                    SwMainWindow.GetElement(aTElementStruct: PortalObj.TabItem_PROFILES, treeScope: ATElement.TreeScope.Descendants).DoClickPoint();
                    return;
                }
                do
                {
                    loadingTmp = SwMainWindow.GetElement(aTElementStruct: PortalObj.Lable_LOADING, treeScope: ATElement.TreeScope.Descendants, returnNullWhenException: true);
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
                    loadingTmp = SwMainWindow.GetElement(aTElementStruct: PortalObj.Lable_LOADING, treeScope: ATElement.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private void ProfilesImExSwitch(ATElementStruct whichProfile, AT profileTmp, AT loadingTmp, string whereToClick, bool isAimpad = false)
        {
            profileTmp = SwMainWindow.GetElement(whichProfile);
            profileTmp.DoClickPoint();
            WaitForEvent(profileTmp, loadingTmp, isAimpad);
            profileTmp = SwMainWindow.GetElement(whichProfile);
            if (whereToClick.Equals("EXPORT"))
            {
                profileTmp.GetElement(PortalObj.Profile_EXPORT).DoClickPoint();
                var dialog = SwMainWindow.GetElement(PortalObj.Window_OpenFIle, treeScope: ATElement.TreeScope.Children, timeout: 5);
                var buttonSave = dialog.GetElement(PortalObj.Button_Save, treeScope: ATElement.TreeScope.Children, timeout: 1);
                buttonSave.DoClickPoint();
                try
                {
                    dialog.GetElement(aTElementStruct: PortalObj.Button_Exist_Yes, treeScope: ATElement.TreeScope.Descendants, timeout: 1).DoClickPoint();
                }
                catch (Exception)
                {
                    // ignored
                }
                WaitForEvent(profileTmp, loadingTmp, isAimpad);
            }
            else if (whereToClick.Equals("IMPORT"))
            {
                profileTmp.GetElement(PortalObj.Profile_IMPORT).DoClickPoint();
                var dialog = SwMainWindow.GetElement(PortalObj.Window_OpenFIle, treeScope: ATElement.TreeScope.Children, timeout: 5);
                dialog = dialog.GetElement(PortalObj.Button_Save, treeScope: ATElement.TreeScope.Children, timeout: 1);
                dialog.DoClickPoint();
                WaitForEvent(profileTmp, loadingTmp, isAimpad);
            }
        }

        public void ProfilesSimpleSwitch(long testTimes)
        {
            var TabItem_Profiles = SwMainWindow.GetElement(aTElementStruct: PortalObj.TabItem_PROFILES, treeScope: ATElement.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint(1);//"DoClick();" does not work
            for (int i = 1; i < testTimes; i++)
            {
                WriteConsoleTitle(i, $"Starting to simple switch. ({Timeout}s)", Timeout);
                ProfilesSimpleSwitch(PortalObj.Profile_1, null, null);
                ProfilesSimpleSwitch(PortalObj.Profile_2, null, null);
                ProfilesSimpleSwitch(PortalObj.Profile_3, null, null);
                ProfilesSimpleSwitch(PortalObj.Profile_4, null, null);
            }
        }
        private void ProfilesSimpleSwitch(ATElementStruct whichProfile, AT profileTmp, AT loadingTmp)
        {
            profileTmp = SwMainWindow.GetElement(whichProfile);
            profileTmp.DoClickPoint();
            WaitForLoading(loadingTmp);
        }
        public void Flow_Installation(string buildPath, bool checkRemove = true, string language = "English")
        {
            UtilProcess.KillProcessByFuzzyName("MasterPlus");
            UtilTime.WaitTime(1);
            var uninstallerPath = UtilRegistry.GetProductInfo("MasterPlus(PER. Only).*", UtilRegistry.ProductInfo.UninstallString);
            if (!uninstallerPath.Equals(string.Empty))
            {
                WriteConsoleTitle(LaunchTimes, $"Waiting for silent uninstalling. ({2}s)", 2);
                UtilProcess.StartProcessGetString(uninstallerPath, "/silent");
                UtilTime.WaitTime(1);
            }
            UtilProcess.StartProcess(buildPath);
            AT Window_SelectLauguage = new AT().GetElement(PortalObj.Window_SelectLauguage, Timeout = 5, treeScope: AT.TreeScope.Children);

            AT ComboBox_SelectLauguage = Window_SelectLauguage.GetElement(PortalObj.ComboBox_SelectLauguage);
            ComboBox_SelectLauguage.DoExpand();
            Window_SelectLauguage = new AT().GetElement(PortalObj.Window_SelectLauguage, Timeout = 5, treeScope: AT.TreeScope.Children); //refresh
            Window_SelectLauguage.GetElement(name: language, treeScope: AT.TreeScope.Descendants).DoSelect(1);
            AT Button_OK = Window_SelectLauguage.GetElement(PortalObj.Button_Install_OK, Timeout = 2);
            Button_OK.DoClick(1);
            AT Window_InstallWizard = new AT().GetElement(PortalObj.Window_InstallWizard, Timeout = 5, treeScope: AT.TreeScope.Children);
            UtilTime.WaitTime(1);
            AT Button_RemoveCache = Window_InstallWizard.GetElement(PortalObj.CheckBox_RemoveCache, Timeout = 2);
            Button_RemoveCache.DoClickPoint(waitTime:1);
            AT Button_Next = Window_InstallWizard.GetElement(PortalObj.Button_Next);
            Button_Next.DoClick(1);
            AT Button_Location_Next = Window_InstallWizard.GetElement(PortalObj.Button_Location_Next);
            Button_Location_Next.DoClick(2);
            try
            {
                AT Dialog_FolderExists = Window_InstallWizard.GetElement(PortalObj.Dialog_FolderExists);
                AT Button_DialogFolderExists_Yes = Dialog_FolderExists.GetElement(PortalObj.Button_DialogFolderExists_Yes);
                Button_DialogFolderExists_Yes.DoClick(1);
            }
            catch (Exception)
            {

            }
            AT Button_SelectStartMemuFolder_Next = Window_InstallWizard.GetElement(PortalObj.Button_SelectStartMemuFolder_Next);
            Button_SelectStartMemuFolder_Next.DoClick(1);
            AT Button_AddtionalTask_Next = Window_InstallWizard.GetElement(PortalObj.Button_AddtionalTask_Next);
            Button_AddtionalTask_Next.DoClick(1);
            AT Button_ReadyToInstall_Install = Window_InstallWizard.GetElement(PortalObj.Button_ReadyToInstall_Install);
            Button_ReadyToInstall_Install.DoClick(1);
            AT CheckBox_LaunchPortal = Window_InstallWizard.GetElement(PortalObj.CheckBox_LaunchPortal, Timeout = 20);
            AT Button_Finish = Window_InstallWizard.GetElement(PortalObj.Button_Finish);
            Button_Finish.DoClick(1);
            AT Window_MasterPlusPer = new AT().GetElement(PortalObj.Window_MasterPlusPer, timeout:10);
        }
    }
}
