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
    }
}
