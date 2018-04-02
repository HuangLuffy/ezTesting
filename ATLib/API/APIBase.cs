using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ATLib.API
{
    public class APIBase
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Status
        {
            /// <summary>
            /// 
            /// </summary>
            public const int BM_GETCHECK = 0xf0;
            /// <summary>
            /// 
            /// </summary>
            public const int BST_CHECKED = 0x1;
            /// <summary>
            /// 
            /// </summary>
            public const int WM_GETTEXT = 0xD;
        }
        /// <summary>
        /// 
        /// </summary>
        public struct Style
        {
            const int WS_OVERLAPPED = 0;
            //const int WS_POPUP = 0x80000000;
            const int WS_CHILD = 0x40000000;
            const int WS_MINIMIZE = 0x20000000;
            const int WS_VISIBLE = 0x10000000;
            const int WS_DISABLED = 0x8000000;//这个就是是否灰色显示  
            const int WS_CLIPSIBLINGS = 0x4000000;
            const int WS_CLIPCHILDREN = 0x2000000;
            const int WS_MAXIMIZE = 0x1000000;
            const int WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME    
            const int WS_BORDER = 0x800000;
            const int WS_DLGFRAME = 0x400000;
            const int WS_VSCROLL = 0x200000;
            const int WS_HSCROLL = 0x100000;
            const int WS_SYSMENU = 0x80000;
            const int WS_THICKFRAME = 0x40000;
            const int WS_GROUP = 0x20000;
            const int WS_TABSTOP = 0x10000;
            const int WS_MINIMIZEBOX = 0x20000;
            const int WS_MAXIMIZEBOX = 0x10000;
            const int WS_TILED = WS_OVERLAPPED;
            const int WS_ICONIC = WS_MINIMIZE;
            const int WS_SIZEBOX = WS_THICKFRAME;

            // Extended Window Styles   
            const int WS_EX_DLGMODALFRAME = 0x0001;
            const int WS_EX_NOPARENTNOTIFY = 0x0004;
            const int WS_EX_TOPMOST = 0x0008;
            const int WS_EX_ACCEPTFILES = 0x0010;
            const int WS_EX_TRANSPARENT = 0x0020;
            const int WS_EX_MDICHILD = 0x0040;
            const int WS_EX_TOOLWINDOW = 0x0080;
            const int WS_EX_WINDOWEDGE = 0x0100;
            const int WS_EX_CLIENTEDGE = 0x0200;
            const int WS_EX_CONTEXTHELP = 0x0400;
            const int WS_EX_RIGHT = 0x1000;
            const int WS_EX_LEFT = 0x0000;
            const int WS_EX_RTLREADING = 0x2000;
            const int WS_EX_LTRREADING = 0x0000;
            const int WS_EX_LEFTSCROLLBAR = 0x4000;
            const int WS_EX_RIGHTSCROLLBAR = 0x0000;
            const int WS_EX_CONTROLPARENT = 0x10000;
            const int WS_EX_STATICEDGE = 0x20000;
            const int WS_EX_APPWINDOW = 0x40000;
            const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
            const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
            const int WS_EX_LAYERED = 0x00080000;
            const int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children  
            const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring  
            const int WS_EX_COMPOSITED = 0x02000000;
            const int WS_EX_NOACTIVATE = 0x08000000;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentHandle"></param>
        /// <param name="childAfter"></param>
        /// <param name="lpsz1"></param>
        /// <param name="lpsz2"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lpsz1, string lpsz2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        /// <summary>
        /// 
        /// </summary>
        private void button2_Click()
        {
            //if (pcalc == null || pcalc.HasExited) return;
            //IntPtr hEdit = FindWindowEx(pcalc.MainWindowHandle, IntPtr.Zero, "Edit", null);
            //string w = " ";
            //IntPtr ptr = Marshal.StringToHGlobalAnsi(w);
            //if (SendMessage(hEdit, WM_GETTEXT, 100, ptr))
            //{
            //    Console.WriteLine(Marshal.PtrToStringAnsi(ptr));
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public extern static IntPtr GetForegroundWindow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpString"></param>
        /// <param name="cch"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(
        IntPtr hwnd,
        StringBuilder lpString,
        int cch
        );
        //GetWindowText(GetForegroundWindow(), str, str.Capacity);
        //Console.WriteLine(str.ToString());
        //Console.ReadKey();
        //int i = GetWindowText(hWnd, s, s.Capacity);
        //MessageBox.Show(s.ToString());
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wFlag"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindow", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wFlag);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndCtl"></param>
        /// <returns></returns>
        [DllImport("user32.Dll")]
        public static extern int GetDlgCtrlID(IntPtr hWndCtl);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDlg"></param>
        /// <param name="nControlID"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName,
        int nMaxCount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out APIPoint point);
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct APIPoint
        {
            /// <summary>
            /// 
            /// </summary>
            public int X;
            /// <summary>
            /// 
            /// </summary>
            public int Y;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public APIPoint(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static APIPoint ScreenCursorPosition
        {
            get
            {
                APIPoint point = new APIPoint();
                GetCursorPos(out point);
                return point;
            }
        }
    }
}
