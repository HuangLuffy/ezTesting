using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace CommonLib.Util.OS
{
    public static class UtilOs
    {
        public struct OsProperty
        {
            public const string Caption = "caption";
            public const string Version = "version";
            public const string OsAndVersion = "osAndVersion";
            public const string OsArchitecture = "OSArchitecture";
        }
        public static List<string> GetDevices()
        {
            var list = new List<string>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            foreach (var mgt in searcher.Get())
            {
                list.Add(Convert.ToString(mgt["Name"]));
            }
            return list;
        }

        public struct Name {
            public const string Win7 = "Win7";
            public const string Win10 = "Win10";
        }

        public static string GetOsIeVersion()
        {
            var name = Convert.ToString("ie" + Registry.GetValue("hkey_local_machine\\software\\microsoft\\internet explorer", "svcversion", "na"));
            name = name.Split('.')[0];
            if (!name.Equals("iena")) return name;
            name = Convert.ToString("ie" + Registry.GetValue("hkey_local_machine\\software\\microsoft\\internet explorer", "version", "na"));
                name = name.Split('.')[0];
                return name;
        }

        public static string GetOsProperty(string whichOne = OsProperty.Version)
        {
            var name = "";
            try
            {
                var query = new SelectQuery("select * from Win32_OperatingSystem");
                var searcher = new ManagementObjectSearcher(query);
                var moCollection = searcher.Get();
                foreach (var mo in moCollection)
                {
                    name = Convert.ToString(mo[whichOne]);
                }
                return name;
            }
            catch (Exception) { return name; }
        }

        public static string GetOsVersion()
        {
            var name = "";
            try
            {
                var query = new SelectQuery("select * from Win32_OperatingSystem");
                var searcher = new ManagementObjectSearcher(query);
                var moCollection = searcher.Get();
                foreach (var mo in moCollection)
                {
                    // console.writeline("asas:{0}", mo["caption"]);
                    //try { name = Convert.ToString(mo["caption"]); }
                    //catch (exception) { name = const.name_os_caption_win7; }
                    name = Convert.ToString(mo["caption"]);
                    if (name.IndexOf(" 7", StringComparison.Ordinal) != -1) name = Name.Win7;
                    else if (name.IndexOf("xp", StringComparison.Ordinal) != -1) name = "XP";
                    else if (name.IndexOf(" 8 ", StringComparison.Ordinal) != -1) name = "Win8";
                    else if (name.IndexOf(" 8.1 ", StringComparison.Ordinal) != -1) name = "Win81";
                    else if (name.IndexOf(" 10", StringComparison.Ordinal) != -1) name = Name.Win10;
                    else if (name.IndexOf("2003", StringComparison.Ordinal) != -1) name = "Win2003";
                    else if (name.IndexOf("2008", StringComparison.Ordinal) != -1) name = "Win2008";
                    else if (name.IndexOf("2012", StringComparison.Ordinal) != -1) name = "Win2012";
                    else if (name.IndexOf("2016", StringComparison.Ordinal) != -1) name = "Win2016";
                    else if (name.IndexOf("2019", StringComparison.Ordinal) != -1) name = "Win2019";
                    else if (name.IndexOf("vista", StringComparison.Ordinal) != -1) name = "Vista";
                    else name = "UnknownOS";
                    try
                    {
                        if (Convert.ToString(mo["OSArchitecture"]).IndexOf("64", StringComparison.Ordinal) != -1)
                        {
                            name += "x64";
                        }
                        else
                        {
                            name += "x86";
                        }
                    }
                    catch (Exception) { return name; }
                }
                return name;
            }
            catch (Exception) { return name; }
        }
    }
}
