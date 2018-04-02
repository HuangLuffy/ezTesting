using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                vmwareInstallFolderPath = Reg.getValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "Path");
                vmwareInstallFolderPath = vmwareInstallFolderPath.Remove(vmwareInstallFolderPath.Length - 1, 1);
                vmwareInstallFullPath = Reg.getValue("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\vmware.exe", "");
            }
            catch (Exception)
            {

            }
        }
    }
}
