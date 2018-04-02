using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.os
{
    class UtilOS
    {
        //public static void GetSystemIEVersion()
        //{
        //    try
        //    {
        //        Global.name_OS_IE = Convert.ToString("IE" + Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Internet Explorer", "svcVersion", "NA"));
        //        Global.name_OS_IE = Global.name_OS_IE.Split('.')[0];
        //        if (Global.name_OS_IE.Equals("IENA"))
        //        {
        //            Global.name_OS_IE = Convert.ToString("IE" + Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Internet Explorer", "Version", "NA"));
        //            Global.name_OS_IE = Global.name_OS_IE.Split('.')[0];
        //        }
        //    }
        //    catch (Exception ex) { MessageBox.Show("{GetVmwareExeInstallPath}" + "\r\n" + "- Failed to get IE Version. " + "\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //}
        //public static void GetSystemInfo()
        //{
        //    try
        //    {
        //        SelectQuery query = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        //        ManagementObjectCollection moCollection = searcher.Get();
        //        foreach (ManagementObject mo in moCollection)
        //        {
        //            // Console.WriteLine("asas:{0}", mo["Caption"]);
        //            try { Global.name_OS_Caption = Convert.ToString(mo["Caption"]); }
        //            catch (Exception) { Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN7; }
        //            if (Global.name_OS_Caption.IndexOf(" 7") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN7;
        //            else if (Global.name_OS_Caption.IndexOf("XP") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_XP;
        //            else if (Global.name_OS_Caption.IndexOf(" 8 ") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN8;
        //            else if (Global.name_OS_Caption.IndexOf(" 8.1 ") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN81;
        //            else if (Global.name_OS_Caption.IndexOf("2003") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN2003;
        //            else if (Global.name_OS_Caption.IndexOf("2008") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN2008;
        //            else if (Global.name_OS_Caption.IndexOf("2012") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_WIN2012;
        //            else if (Global.name_OS_Caption.IndexOf("Vista") != -1) Global.name_OS_Caption = Const.NAME_OS_CAPTION_VISTA;
        //            else Global.name_OS_Caption = Const.NAME_OS_CAPTION_UNKNOWNOS;
        //            try
        //            {
        //                if (Convert.ToString(mo["OSArchitecture"]).IndexOf("64") != -1)
        //                {
        //                    Global.name_OS_Architecture = Const.NAME_OS_ARCHITECTURE_64BIT;
        //                }
        //                else
        //                {
        //                    Global.name_OS_Architecture = Const.NAME_OS_ARCHITECTURE_32BIT;
        //                }
        //            }
        //            catch (Exception) { Global.name_OS_Architecture = Const.NAME_OS_ARCHITECTURE_64BIT; }
        //        }
        //    }
        //    catch (Exception ex) { MessageBox.Show("{GetSystemInfo}" + "\r\n" + "- Failed to get system info. " + "\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex; }
        //}
    }
}
