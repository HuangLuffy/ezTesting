using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Util
{
    public class UtilKeys
    {
        public enum Status
        {
            Off,
            On
        }
        private static void PressByScanCode(int scanCode, bool up)
        {
            //var a = Convert.ToInt32("1E", 16);
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                //UtilWinApi.keybd_event((byte)key, 30, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                UtilWinApi.keybd_event((byte)scanCode, (byte)(scanCode), KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                UtilWinApi.keybd_event((byte)scanCode, (byte)(scanCode), 0, (UIntPtr)0);
            }
        }
        public static void PressByScanCode(int scanCode)
        {
            PressByScanCode(scanCode, false);
            PressByScanCode(scanCode, true);
        }
        public static void PressByKeyValue(int KeyValue)
        {
            PressByKeyValue(KeyValue, false);
            PressByKeyValue(KeyValue, true);
        }
        private static void PressByKeyValue(int KeyValue, bool up)
        {
            //var a = Convert.ToInt32("1E", 16);
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                //UtilWinApi.keybd_event((byte)key, 30, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                UtilWinApi.keybd_event((byte)KeyValue, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                UtilWinApi.keybd_event((byte)KeyValue, 0, 0, (UIntPtr)0);
            }
        }
        //PressKey(Keys.A, false);
        //PressKey(Keys.A, true);
        private static void PressByKeys(Keys key, bool up)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                //UtilWinApi.keybd_event((byte)key, bScan, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                UtilWinApi.keybd_event((byte)key, (byte)(key), KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                UtilWinApi.keybd_event((byte)key, 0x45, 0, (UIntPtr)0);
            }
        }
        public static void SetPhysicalKeyStatus(int KeyValue, Status s)
        {
            if (UtilWinApi.GetKeyState(KeyValue) != (int)s)
            {
                PressByKeyValue(KeyValue);
            }
        }
    }
}
