namespace ATLib.Input
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    /// <summary>
    /// 
    /// </summary>
    public class HWSimulator
    {
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags()]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 1,
            MOUSEEVENTF_LEFTDOWN = 2,
            MOUSEEVENTF_LEFTUP = 4,
            MOUSEEVENTF_RIGHTDOWN = 8,
            MOUSEEVENTF_RIGHTUP = 16,
            MOUSEEVENTF_MIDDLEDOWN = 32,
            MOUSEEVENTF_MIDDLEUP = 64,
            MOUSEEVENTF_XDOWN = 128,
            MOUSEEVENTF_XUP = 256,
            MOUSEEVENTF_WHEEL = 2048,
            MOUSEEVENTF_VIRTUALDESK = 16384,
            MOUSEEVENTF_ABSOLUTE = 32768
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }
        [Flags()]
        public enum KeyboardEventFlags : uint
        {
            KEYEVENTF_EXTENDEDKEY = 1,
            KEYEVENTF_KEYUP = 2
        }
        [Flags()]
        public enum HookStructFlags : uint
        {
            KF_EXTENDED = 256,
            KF_DLGMODE = 2048,
            KF_MENUMODE = 4096,
            KF_ALTDOWN = 8192,
            KF_REPEAT = 16384,
            KF_UP = 32768
        }
        [Flags()]
        public enum WParamFlags : uint
        {
            WM_KEYDOWN = 256,
            WM_KEYUP = 257,
            WM_SYSKEYDOWN = 260,
            WM_SYSKEYUP = 261,
            WM_MOUSEMOVE = 512,
            WM_LBUTTONDOWN = 513,
            WM_LBUTTONUP = 514,
            WM_LBUTTONDBLCLK = 515,
            WM_RBUTTONDOWN = 516,
            WM_RBUTTONUP = 517,
            WM_RBUTTONDBLCLK = 518,
            WM_MBUTTONDOWN = 519,
            WM_MBUTTONUP = 520,
            WM_MBUTTONDBLCLK = 521,
            WM_MOUSEWHEEL = 522
        }
        [Flags()]
        public enum LParamFlags : uint
        {
            WM_KEYDOWN = 0,
            WM_KEYUP = 128,
            WM_SYSKEYDOWN = 32,
            WM_SYSKEYUP = 160
        }
        [Flags()]
        public enum VirtualKeys : ushort
        {
            VK_LBUTTON = 1,
            VK_RBUTTON = 2,
            VK_CANCEL = 3,
            VK_MBUTTON = 4,
            VK_BACK = 8,
            VK_TAB = 9,
            VK_CLEAR = 12,
            VK_RETURN = 13,
            VK_SHIFT = 16,
            VK_CONTROL = 17,
            VK_MENU = 18,
            VK_PAUSE = 19,
            VK_CAPITAL = 20,
            VK_KANA = 21,
            VK_HANGEUL = 21,
            VK_HANGUL = 21,
            VK_JUNJA = 23,
            VK_FINAL = 24,
            VK_HANJA = 25,
            VK_KANJI = 25,
            VK_ESCAPE = 27,
            VK_CONVERT = 28,
            VK_NONCONVERT = 29,
            VK_ACCEPT = 30,
            VK_MODECHANGE = 31,
            VK_SPACE = 32,
            VK_PRIOR = 33,
            VK_NEXT = 34,
            VK_END = 35,
            VK_HOME = 36,
            VK_LEFT = 37,
            VK_UP = 38,
            VK_RIGHT = 39,
            VK_DOWN = 40,
            VK_SELECT = 41,
            VK_PRINT = 42,
            VK_EXECUTE = 43,
            VK_SNAPSHOT = 44,
            VK_INSERT = 45,
            VK_DELETE = 46,
            VK_HELP = 47,
            // VK_0 thru VK_9 are the same as ASCII '0' thru '9' (= 0x30 - = 0x39)   
            VK_0 = 48,
            VK_1 = 49,
            VK_2 = 50,
            VK_3 = 51,
            VK_4 = 52,
            VK_5 = 53,
            VK_6 = 54,
            VK_7 = 55,
            VK_8 = 56,
            VK_9 = 57,
            // VK_A thru VK_Z are the same as ASCII 'A' thru 'Z' (= 0x41 - = 0x5A)    
            VK_A = 65,
            VK_B = 66,
            VK_C = 67,
            VK_D = 68,
            VK_E = 69,
            VK_F = 70,
            VK_G = 71,
            VK_H = 72,
            VK_I = 73,
            VK_J = 74,
            VK_K = 75,
            VK_L = 76,
            VK_M = 77,
            VK_N = 78,
            VK_O = 79,
            VK_P = 80,
            VK_Q = 81,
            VK_R = 82,
            VK_S = 83,
            VK_T = 84,
            VK_U = 85,
            VK_V = 86,
            VK_W = 87,
            VK_X = 88,
            VK_Y = 89,
            VK_Z = 90,
            // SPECIAL KEY   
            VK_LWIN = 91,
            VK_RWIN = 92,
            VK_APPS = 93,
            // VK_NUMPAD0 thru VK_NUMPAD9 (= 0x60 - = 0x69)  
            VK_NUMPAD0 = 96,
            VK_NUMPAD1 = 97,
            VK_NUMPAD2 = 98,
            VK_NUMPAD3 = 99,
            VK_NUMPAD4 = 100,
            VK_NUMPAD5 = 101,
            VK_NUMPAD6 = 102,
            VK_NUMPAD7 = 103,
            VK_NUMPAD8 = 104,
            VK_NUMPAD9 = 105,
            VK_MULTIPLY = 106,
            VK_ADD = 107,
            VK_SEPARATOR = 108,
            VK_SUBTRACT = 109,
            VK_DECIMAL = 110,
            VK_DIVIDE = 111,
            // VK_F1 thru VK_F24 (= 0x70 - = 0x87)   
            VK_F1 = 112,
            VK_F2 = 113,
            VK_F3 = 114,
            VK_F4 = 115,
            VK_F5 = 116,
            VK_F6 = 117,
            VK_F7 = 118,
            VK_F8 = 119,
            VK_F9 = 120,
            VK_F10 = 121,
            VK_F11 = 122,
            VK_F12 = 123,
            VK_F13 = 124,
            VK_F14 = 125,
            VK_F15 = 126,
            VK_F16 = 127,
            VK_F17 = 128,
            VK_F18 = 129,
            VK_F19 = 130,
            VK_F20 = 131,
            VK_F21 = 132,
            VK_F22 = 133,
            VK_F23 = 134,
            VK_F24 = 135,
            VK_NUMLOCK = 144,
            VK_SCROLL = 145,
            VK_LSHIFT = 160,
            VK_RSHIFT = 161,
            VK_LCONTROL = 162,
            VK_RCONTROL = 163,
            VK_LMENU = 164,
            VK_RMENU = 165,
            VK_PROCESSKEY = 229,
            VK_ATTN = 246,
            VK_CRSEL = 247,
            VK_EXSEL = 248,
            VK_EREOF = 249,
            VK_PLAY = 250,
            VK_ZOOM = 251,
            VK_NONAME = 252,
            VK_PA1 = 253,
            VK_OEM_CLEAR = 254
        }
        public class HWSend
        {
            [DllImport("user32.dll", SetLastError = true)]
            private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
            [DllImport("user32.dll")]
            private static extern int GetSystemMetrics(SystemMetric smIndex);

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, UIntPtr dwExtraInfo);
            enum SystemMetric
            {
                SM_CXSCREEN = 0,
                SM_CYSCREEN = 1
            }
            /// <summary>
            /// Click left mouse button
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public static void ClickLeftMouseButton(int x, int y)
            {
                INPUT mouseInput = new INPUT();
                mouseInput.type = SendInputEventType.InputMouse;
                mouseInput.mkhi.mi.dx = (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseInput.mkhi.mi.dy = (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            /// <summary>
            /// Click left mouse button
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public static void MoveMouseTo(int x, int y)
            {
                INPUT mouseInput = new INPUT
                {
                    type = SendInputEventType.InputMouse
                };
                mouseInput.mkhi.mi.dx = (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseInput.mkhi.mi.dy = (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            /// <summary>
            /// Click Start Button
            /// </summary>
            public static void ClickStart()
            {
                INPUT mouseInput = new INPUT();
                object x = 0;
                object y = 0;
                mouseInput.type = SendInputEventType.InputMouse;
                if (System.Globalization.CultureInfo.CurrentUICulture.Parent.ThreeLetterWindowsLanguageName.ToLower() == "ARA".ToLower() | System.Globalization.CultureInfo.CurrentUICulture.Parent.ThreeLetterWindowsLanguageName.ToLower() == "HEB".ToLower())
                {
                    mouseInput.mkhi.mi.dx = ((Screen.PrimaryScreen.Bounds.Width - 1) * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                }
                else
                {
                    mouseInput.mkhi.mi.dx = (1 * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                }
                mouseInput.mkhi.mi.dy = ((Screen.PrimaryScreen.Bounds.Height - 1) * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            public enum MouseKeys
            {
                NOTCLICK = 0,
                LEFT = 1,
                RIGHT = 2,
                WHEELUP = 3,
                WHEELDOWN = 4
            }

            /// <summary>
            /// Move Mouse And Click Left
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="mk"></param>
            /// 
            public static void MoveCursorAndDo(int x, int y, MouseKeys mk = MouseKeys.LEFT)
            {
                INPUT mouseInput = new INPUT();
                mouseInput.type = SendInputEventType.InputMouse;
                mouseInput.mkhi.mi.dx = x * 65536 / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseInput.mkhi.mi.dy = y * 65536 / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                if (mk == MouseKeys.NOTCLICK)
                {
                    return;
                }
                if (mk == MouseKeys.RIGHT)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
                }
                else if(mk == MouseKeys.LEFT)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                }
                else if (mk == MouseKeys.WHEELUP)
                {
                    mouse_event(2048, 0, 0, 120, (UIntPtr)0);
                    return;
                }
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                if (mk == MouseKeys.RIGHT)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
                }
                else if (mk == MouseKeys.LEFT)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                }
                else if (mk == MouseKeys.WHEELDOWN)
                {
                    mouse_event(2048, 0, 0, -120, (UIntPtr)0);
                    return;
                }
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            /// <summary>
            /// Drag to bottom to close Metro App
            /// </summary>
            public static void DragPointToPoint(int oX, int oY, int dX, int dY)
            {
                var mouseInput = new INPUT();
                mouseInput.type = SendInputEventType.InputMouse;
                mouseInput.mkhi.mi.dx = (oX * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseInput.mkhi.mi.dy = (oY * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

                    mouseInput.mkhi.mi.dx = (dX * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                    mouseInput.mkhi.mi.dy = (dY * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                    mouseInput.mkhi.mi.mouseData = 0;
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                    SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            public static void DragToClose()
            {
                INPUT mouseInput = new INPUT();
                mouseInput.type = SendInputEventType.InputMouse;
                mouseInput.mkhi.mi.dx = (Screen.PrimaryScreen.Bounds.Width / 2 * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseInput.mkhi.mi.dy = (1 * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                for (int i = 1; i <= Screen.PrimaryScreen.Bounds.Height + 100; i += 20)
                {
                    System.Threading.Thread.Sleep(1);
                    mouseInput.mkhi.mi.dx = (Screen.PrimaryScreen.Bounds.Width / 2 * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                    mouseInput.mkhi.mi.dy = ((i) * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                    mouseInput.mkhi.mi.mouseData = 0;
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                    SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                }
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            }
            public static void MoveMouseButton()
            {
                INPUT mouseDownInput = new INPUT
                {
                    type = SendInputEventType.InputMouse
                };
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
            }
            public static void HoldLeftMouseButton()
            {
                INPUT mouseDownInput = new INPUT();
                mouseDownInput.type = SendInputEventType.InputMouse;
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
            }
            public static void LoosenLeftMouseButton()
            {
                INPUT mouseUpInput = new INPUT();
                mouseUpInput.type = SendInputEventType.InputMouse;
                mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
            }
            public static void ClickLeftMouseButton()
            {
                INPUT mouseDownInput = new INPUT();
                mouseDownInput.type = SendInputEventType.InputMouse;
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
                INPUT mouseUpInput = new INPUT();
                mouseUpInput.type = SendInputEventType.InputMouse;
                mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
            }
            public static void ClickRightMouseButton()
            {
                INPUT mouseDownInput = new INPUT();
                mouseDownInput.type = SendInputEventType.InputMouse;
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
                INPUT mouseUpInput = new INPUT();
                mouseUpInput.type = SendInputEventType.InputMouse;
                mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
                SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
            }
            public static void ClickRightMouseButton(int x, int y)
            {
                INPUT mouseDownInput = new INPUT();
                mouseDownInput.type = SendInputEventType.InputMouse;
                mouseDownInput.mkhi.mi.dx = (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
                mouseDownInput.mkhi.mi.dy = (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
                mouseDownInput.mkhi.mi.mouseData = 0;
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
                mouseDownInput.type = SendInputEventType.InputMouse;
                mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
                SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
                INPUT mouseUpInput = new INPUT();
                mouseUpInput.type = SendInputEventType.InputMouse;
                mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
                SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
            }
            public static void KBDoLWinI()
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)VirtualKeys.VK_I;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)VirtualKeys.VK_I;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            public static void KBDoWinWith(ushort whickKey)
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)whickKey;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)whickKey;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            public static void KBDoCtrlTab(int press)
            {
                INPUT KB_DownINPUT = new INPUT();
                if (press == 1)
                {
                    KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                    KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                    KB_DownINPUT.mkhi.ki.wScan = 91;
                    KB_DownINPUT.mkhi.ki.time = 2777777;
                    SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                    INPUT KB_DownINPUT3 = new INPUT();
                    KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                    KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                    KB_DownINPUT3.mkhi.ki.wVk = (ushort)VirtualKeys.VK_TAB;
                    KB_DownINPUT3.mkhi.ki.wScan = 0;
                    KB_DownINPUT3.mkhi.ki.time = 2777777;
                    SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                    INPUT KB_DownINPUT4 = new INPUT();
                    KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                    KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                    KB_DownINPUT4.mkhi.ki.wVk = (ushort)VirtualKeys.VK_TAB;
                    KB_DownINPUT4.mkhi.ki.wScan = 0;
                    KB_DownINPUT4.mkhi.ki.time = 2777777;
                    SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                }
                else
                {
                    KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                    KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                    KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                    KB_DownINPUT.mkhi.ki.wScan = 91;
                    KB_DownINPUT.mkhi.ki.time = 2777777;
                    SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                }
            }
            public static void KBDoCtrlWith(ushort whickKey)
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_CONTROL;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                //KB_DownINPUT.mkhi.ki.wScan = 91
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)whickKey;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)whickKey;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_CONTROL;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                //KB_DownINPUT.mkhi.ki.wScan = 91
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            public static void KBDo(Enum whickKey)
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;

                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_C;

                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));

                
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_C;

                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            public static void KBDoCtrlD()
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)VirtualKeys.VK_D;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)VirtualKeys.VK_D;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            public static void KBDoWinK()
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)VirtualKeys.VK_K;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)VirtualKeys.VK_K;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
            //[DllImport("user32.dll", SetLastError = true)]
            //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
            //public static void PressKey(Keys key, bool up)
            //{
            //    const int KEYEVENTF_EXTENDEDKEY = 0x1;
            //    const int KEYEVENTF_KEYUP = 0x2;
            //    if (up)
            //    {
            //        keybd_event((byte)key, 0x4F, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            //    }
            //    else
            //    {
            //        keybd_event((byte)key, 0x4F, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            //    }
            //}
            public static void KBDoALTI()
            {
                INPUT KB_DownINPUT = new INPUT();
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY;  
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_MENU;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;   
                //KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT3 = new INPUT();
                KB_DownINPUT3.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT3.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYDOWN;
                KB_DownINPUT3.mkhi.ki.wVk = (ushort)VirtualKeys.VK_I;
                KB_DownINPUT3.mkhi.ki.wScan = 0;
                KB_DownINPUT3.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT3, Marshal.SizeOf(new INPUT()));
                INPUT KB_DownINPUT4 = new INPUT();
                KB_DownINPUT4.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT4.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;
                KB_DownINPUT4.mkhi.ki.wVk = (ushort)VirtualKeys.VK_I;
                KB_DownINPUT4.mkhi.ki.wScan = 0;
                KB_DownINPUT4.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT4, Marshal.SizeOf(new INPUT()));
                KB_DownINPUT.type = SendInputEventType.InputKeyboard;
                KB_DownINPUT.mkhi.ki.dwFlags = (uint)KeyboardEventFlags.KEYEVENTF_KEYUP;
                KB_DownINPUT.mkhi.ki.wVk = (ushort)VirtualKeys.VK_MENU;
                //KB_DownINPUT.mkhi.ki.dwFlags = (uint)HookStructFlags.KF_EXTENDED;    'KB_DownINPUT.mkhi.ki.dwExtraInfo = (IntPtr)0;  
                KB_DownINPUT.mkhi.ki.wScan = 91;
                KB_DownINPUT.mkhi.ki.time = 2777777;
                SendInput(1, ref KB_DownINPUT, Marshal.SizeOf(new INPUT()));
                //Thread.Sleep(1000);   'INPUT KB_DownINPUT2 = new INPUT();   'KB_DownINPUT2.type = SendInputEventType.InputKeyboard;   'KB_DownINPUT2.mkhi.ki.wVk = (ushort)VirtualKeys.VK_LWIN;   'KB_DownINPUT2.mkhi.ki.dwFlags = (uint)WParamFlags.WM_KEYUP;   'SendInput(2, ref KB_DownINPUT2, Marshal.SizeOf(new INPUT()));  End Sub 
            }
        }
    }

}
