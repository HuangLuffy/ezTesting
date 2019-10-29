using System.Linq;
using Microsoft.Win32;

namespace CommonLib.Util
{
    public static class UtilRegistry
    {
        private static bool IsRegeditKeyExisting()
        {
            var hkml = Registry.LocalMachine;
            var software = hkml.OpenSubKey("SOFTWARE\\test");
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test", true);
            var subkeyNames = software.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            if (subkeyNames.Any(keyName => keyName == "test"))
            {
                hkml.Close();
                return true;
            }
            hkml.Close();
            return false;
        }
        public static string GetValue(string path, string name)
        {
            var key = Registry.LocalMachine;
            key = key.OpenSubKey(path);
            return key.GetValue(name).ToString();
        }
        public struct ProductInfo
        {
            public const string displayName = "displayName";
            public const string InstallDate = "InstallDate";
            public const string UninstallString = "UninstallString";
            public const string DisplayVersion = "DisplayVersion";
            public const string ProductGuid = "ProductGuid";
        }
        public static string GetProductInfo(string displayName, string option = ProductInfo.UninstallString)
        {
            string productGuid = string.Empty;
            string bit32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey unistall = localMachine.OpenSubKey(bit32, true);
            var subNames = unistall.GetSubKeyNames();
            foreach (string subkey in subNames)
            {
                RegistryKey product = unistall.OpenSubKey(subkey);
                try
                {
                    if (product.GetValueNames().Any(n => n == "DisplayName") == true)
                    {
                        string tempDisplayName = product.GetValue("DisplayName").ToString();
                        if (displayName.Contains(".*") && tempDisplayName.ToUpper().Contains(displayName.Replace(".*", "").ToUpper())  || !displayName.Contains(".*") && tempDisplayName.ToUpper().Equals(displayName.ToUpper()))
                        {
                            if (!option.Equals(ProductInfo.ProductGuid))
                            {
                                return product.GetValue(option).ToString().ToString().Split(new char[2] { '\"', '\"' })[1];
                            }
                            else
                            {
                                if (product.GetValueNames().Any(n => n == "UninstallString") == true)
                                {
                                    var unitstallStr = product.GetValue("UninstallString").ToString();
                                    if (unitstallStr.Contains("MsiExec.exe") && option.Equals(ProductInfo.ProductGuid))
                                    {
                                        string[] strs = unitstallStr.Split(new char[2] { '{', '}' });
                                        productGuid = strs[1];
                                        return productGuid;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
            //从注册表中我们找到UninstallString这个键值: MsiExec.exe / X{ C56BBAC8 - 0DD2 - 4CE4 - 86E0 - F2BDEABDD0CF}, 那么ProductCode就是{ C56BBAC8 - 0DD2 - 4CE4 - 86E0 - F2BDEABDD0CF}
            //我们可以通过 MsiExec.exe / X{ ProductCode}
            //命令来卸载程序.
            //那么卸载的命令应该为 MsiExec.exe / X{ C56BBAC8 - 0DD2 - 4CE4 - 86E0 - F2BDEABDD0CF}
        }
    }
}
