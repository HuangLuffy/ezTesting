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
    }
}
