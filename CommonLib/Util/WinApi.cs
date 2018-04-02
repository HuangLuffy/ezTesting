using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CommonLib.Util
{
    public class WinApi
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hwnd, int nCmdShow);
        public const int SW_RESTORE = 9;
    }
}
