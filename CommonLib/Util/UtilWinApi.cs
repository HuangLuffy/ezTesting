using System;
using System.Runtime.InteropServices;

namespace CommonLib.Util
{
    public class UtilWinApi
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hwnd, int nCmdShow);
        public const int SwRestore = 9;
        [DllImport("user32", EntryPoint = "GetKeyState")]
        public static extern int GetKeyState(int KeyValue);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    }
}
