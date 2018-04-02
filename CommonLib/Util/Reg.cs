using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class Reg
    {
        private bool IsRegeditKeyExit()
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test");
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test", true);
            subkeyNames = software.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)
            {
                if (keyName == "test") //判断键值的名称
                {
                    hkml.Close();
                    return true;
                }

            }
            hkml.Close();
            return false;
        }

        public static string getValue(string path, string name)
        {
            RegistryKey Key = Registry.LocalMachine;
            Key = Key.OpenSubKey(path);
            return Key.GetValue(name).ToString();
        }
    }
}
