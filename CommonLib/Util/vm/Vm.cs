using System;

namespace CommonLib.Util.vm
{
    public class Vm
    {
        protected static string vmwareInstallFolderPath = @"C:\Program Files (x86)\VMware\VMware Workstation";
        protected static string vmwareInstallFullPath = vmwareInstallFolderPath + @"\vmware.exe";
        protected static string vmrunInstallFullPath = vmwareInstallFolderPath + @"\vmrun.exe";
        public static void getVmwareInstalledPaths()
        {
            try
            {
                vmwareInstallFolderPath = UtilRegistry.getValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "Path");
                vmwareInstallFolderPath = vmwareInstallFolderPath.Remove(vmwareInstallFolderPath.Length - 1, 1);
                vmwareInstallFullPath = UtilRegistry.getValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "");
            }
            catch (Exception)
            {

            }
        }
    }
}
