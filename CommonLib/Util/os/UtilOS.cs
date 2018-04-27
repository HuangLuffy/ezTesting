using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.os
{
    public class UtilOS
    {
        public static string GetOsIeVersion()
        {
            string name = "";
            //try
            //{
            name = Convert.ToString("ie" + Registry.GetValue("hkey_local_machine\\software\\microsoft\\internet explorer", "svcversion", "na"));
            name = name.Split('.')[0];
                if (name.Equals("iena"))
                {
                   name = Convert.ToString("ie" + Registry.GetValue("hkey_local_machine\\software\\microsoft\\internet explorer", "version", "na"));
                   name = name.Split('.')[0];
                }
            //}
            //catch (exception ex) { messagebox.show("{getvmwareexeinstallpath}" + "\r\n" + "- failed to get ie version. " + "\r\n" + ex.message, "error", messageboxbuttons.ok, messageboxicon.error); }
            return name;
        }
        public static string GetOsVersion()
        {
            string name = "";
            try
            {
                SelectQuery query = new SelectQuery("select * from Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection mocollection = searcher.Get();
                foreach (ManagementObject mo in mocollection)
                {
                    // console.writeline("asas:{0}", mo["caption"]);
                    //try { name = Convert.ToString(mo["caption"]); }
                    //catch (exception) { name = const.name_os_caption_win7; }
                    name = Convert.ToString(mo["caption"]);
                    if (name.IndexOf(" 7") != -1) name = "Win7";
                    else if (name.IndexOf("xp") != -1) name = "XP";
                    else if (name.IndexOf(" 8 ") != -1) name = "Win8";
                    else if (name.IndexOf(" 8.1 ") != -1) name = "Win81";
                    else if (name.IndexOf(" 10") != -1) name = "Win10";
                    else if (name.IndexOf("2003") != -1) name = "Win2003";
                    else if (name.IndexOf("2008") != -1) name = "Win2008";
                    else if (name.IndexOf("2012") != -1) name = "Win2012";
                    else if (name.IndexOf("2016") != -1) name = "Win2016";
                    else if (name.IndexOf("2019") != -1) name = "Win2019";
                    else if (name.IndexOf("vista") != -1) name = "Vista";
                    else name = "UnknownOS";
                    try
                    {
                        if (Convert.ToString(mo["OSArchitecture"]).IndexOf("64") != -1)
                        {
                            name += "x64";
                        }
                        else
                        {
                            name += "x86"; ;
                        }
                    }
                    catch (Exception) { return name; }
                }
                return name;
            }
            catch (Exception) { return name; }
            finally
            {
                //return name;
            }
        }
    }
}
