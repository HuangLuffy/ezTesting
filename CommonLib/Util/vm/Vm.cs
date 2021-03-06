﻿using System;

namespace CommonLib.Util.VM
{
    public class Vm
    {
        private static string _vmwareInstallFolderPath = @"C:\Program Files (x86)\VMware\VMware Workstation";
        private static string _vmwareInstallFullPath = _vmwareInstallFolderPath + @"\vmware.exe";
        protected static readonly string VmrunInstallFullPath = _vmwareInstallFolderPath + @"\vmrun.exe";
        public static void GetVmwareInstalledPaths()
        {
            try
            {
                _vmwareInstallFolderPath = UtilRegistry.GetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "Path");
                _vmwareInstallFolderPath = _vmwareInstallFolderPath.Remove(_vmwareInstallFolderPath.Length - 1, 1);
                _vmwareInstallFullPath = UtilRegistry.GetValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "");
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
