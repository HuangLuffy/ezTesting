using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.Util;

namespace ATLib.Input
{
    public class KbEvent
    {
        public enum ScanCode
        {/// <summary>The A key.</summary>
            SC_KEY_A = 30,
            SC_KEY_B = 48,
            SC_KEY_C = 46,
            SC_KEY_D = 32,
            SC_KEY_E = 18,
            SC_KEY_F = 33,
            SC_KEY_G = 34,
            SC_KEY_H = 35,
            SC_KEY_I = 23,
            SC_KEY_J = 36,
            SC_KEY_K = 37,
            SC_KEY_L = 38,
            SC_KEY_M = 50,
            SC_KEY_N = 49,
            SC_KEY_O = 24,
            SC_KEY_P = 25,
            SC_KEY_Q = 16,
            SC_KEY_R = 19,
            SC_KEY_S = 31,
            SC_KEY_T = 20,
            SC_KEY_U = 22,
            SC_KEY_V = 47,
            SC_KEY_W = 17,
            SC_KEY_X = 45,
            SC_KEY_Y = 21,
            SC_KEY_Z = 44,
            ///// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
            //LWin = Z | Y,
            ///// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
            //RWin = X | T,
            ///// <summary>The bitmask to extract modifiers from a key value.</summary>
            //Modifiers = -65536,
            ///// <summary>No key pressed.</summary>
            //None = 0,
            ///// <summary>The left mouse button.</summary>
            //LButton = 1,
            ///// <summary>The right mouse button.</summary>
            //RButton = 2,
            ///// <summary>The CANCEL key.</summary>
            //Cancel = RButton | LButton,
            ///// <summary>The middle mouse button (three-button mouse).</summary>
            //MButton = 4,
            ///// <summary>The first x mouse button (five-button mouse).</summary>
            //XButton1 = MButton | LButton,
            ///// <summary>The second x mouse button (five-button mouse).</summary>
            //XButton2 = MButton | RButton,
            ///// <summary>The BACKSPACE key.</summary>
            //Back = 8,
            ///// <summary>The TAB key.</summary>
            //Tab = Back | LButton,
            ///// <summary>The LINEFEED key.</summary>
            //LineFeed = Back | RButton,
            ///// <summary>The CLEAR key.</summary>
            //Clear = Back | MButton,
            ///// <summary>The ENTER key.</summary>
            //Enter = Clear | Tab,
            ///// <summary>The RETURN key.</summary>
            //Return = Enter,
            ///// <summary>The SHIFT key.</summary>
            //ShiftKey = 16,
            ///// <summary>The CTRL key.</summary>
            //ControlKey = ShiftKey | LButton,
            ///// <summary>The ALT key.</summary>
            //Menu = ShiftKey | RButton,
            ///// <summary>The PAUSE key.</summary>
            //Pause = Menu | ControlKey,
            ///// <summary>The CAPS LOCK key.</summary>
            //Capital = ShiftKey | MButton,
            ///// <summary>The CAPS LOCK key.</summary>
            //CapsLock = Capital,
            ///// <summary>The IME Hanguel mode key. (maintained for compatibility; use <see langword="HangulMode" />)</summary>
            //HanguelMode = CapsLock | ControlKey,
            ///// <summary>The IME Hangul mode key.</summary>
            //HangulMode = HanguelMode,
            ///// <summary>The IME Kana mode key.</summary>
            //KanaMode = HangulMode,
            ///// <summary>The IME Junja mode key.</summary>
            //JunjaMode = KanaMode | Pause,
            ///// <summary>The IME final mode key.</summary>
            //FinalMode = ShiftKey | Back,
            ///// <summary>The IME Hanja mode key.</summary>
            //HanjaMode = FinalMode | ControlKey,
            ///// <summary>The IME Kanji mode key.</summary>
            //KanjiMode = HanjaMode,
            ///// <summary>The ESC key.</summary>
            //Escape = KanjiMode | Pause,
            ///// <summary>The IME convert key.</summary>
            //IMEConvert = FinalMode | CapsLock,
            ///// <summary>The IME nonconvert key.</summary>
            //IMENonconvert = IMEConvert | KanjiMode,
            ///// <summary>The IME accept key, replaces <see cref="F:System.Windows.Forms.Keys.IMEAceept" />.</summary>
            //IMEAccept = IMEConvert | Menu,
            ///// <summary>The IME accept key. Obsolete, use <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> instead.</summary>
            //IMEAceept = IMEAccept,
            ///// <summary>The IME mode change key.</summary>
            //IMEModeChange = IMEAceept | IMENonconvert,
            ///// <summary>The SPACEBAR key.</summary>
            //Space = 32,
            ///// <summary>The PAGE UP key.</summary>
            //PageUp = Space | LButton,
            ///// <summary>The PAGE UP key.</summary>
            //Prior = PageUp,
            ///// <summary>The PAGE DOWN key.</summary>
            //Next = Space | RButton,
            ///// <summary>The PAGE DOWN key.</summary>
            //PageDown = Next,
            ///// <summary>The END key.</summary>
            //End = PageDown | Prior,
            ///// <summary>The HOME key.</summary>
            //Home = Space | MButton,
            ///// <summary>The LEFT ARROW key.</summary>
            //Left = Home | Prior,
            ///// <summary>The UP ARROW key.</summary>
            //Up = Home | PageDown,
            ///// <summary>The RIGHT ARROW key.</summary>
            //Right = Up | Left,
            ///// <summary>The DOWN ARROW key.</summary>
            //Down = Space | Back,
            ///// <summary>The SELECT key.</summary>
            //Select = Down | Prior,
            ///// <summary>The PRINT key.</summary>
            //Print = Down | PageDown,
            ///// <summary>The EXECUTE key.</summary>
            //Execute = Print | Select,
            ///// <summary>The PRINT SCREEN key.</summary>
            //PrintScreen = Down | Home,
            ///// <summary>The PRINT SCREEN key.</summary>
            //Snapshot = PrintScreen,
            ///// <summary>The INS key.</summary>
            //Insert = Snapshot | Select,
            ///// <summary>The DEL key.</summary>
            //Delete = Snapshot | Print,
            ///// <summary>The HELP key.</summary>
            //Help = Delete | Insert,
            ///// <summary>The 0 key.</summary>
            //D0 = Space | ShiftKey,
            ///// <summary>The 1 key.</summary>
            //D1 = D0 | Prior,
            ///// <summary>The 2 key.</summary>
            //D2 = D0 | PageDown,
            ///// <summary>The 3 key.</summary>
            //D3 = D2 | D1,
            ///// <summary>The 4 key.</summary>
            //D4 = D0 | Home,
            ///// <summary>The 5 key.</summary>
            //D5 = D4 | D1,
            ///// <summary>The 6 key.</summary>
            //D6 = D4 | D2,
            ///// <summary>The 7 key.</summary>
            //D7 = D6 | D5,
            ///// <summary>The 8 key.</summary>
            //D8 = D0 | Down,
            ///// <summary>The 9 key.</summary>
            //D9 = D8 | D1,
            ///// <summary>The application key (Microsoft Natural Keyboard).</summary>
            //Apps = RWin | Y,
            ///// <summary>The computer sleep key.</summary>
            //Sleep = Apps | LWin,
            ///// <summary>The 0 key on the numeric keypad.</summary>
            //NumPad0 = 96,
            ///// <summary>The 1 key on the numeric keypad.</summary>
            //NumPad1 = NumPad0 | A,
            ///// <summary>The 2 key on the numeric keypad.</summary>
            //NumPad2 = NumPad0 | B,
            ///// <summary>The 3 key on the numeric keypad.</summary>
            //NumPad3 = NumPad2 | NumPad1,
            ///// <summary>The 4 key on the numeric keypad.</summary>
            //NumPad4 = NumPad0 | D,
            ///// <summary>The 5 key on the numeric keypad.</summary>
            //NumPad5 = NumPad4 | NumPad1,
            ///// <summary>The 6 key on the numeric keypad.</summary>
            //NumPad6 = NumPad4 | NumPad2,
            ///// <summary>The 7 key on the numeric keypad.</summary>
            //NumPad7 = NumPad6 | NumPad5,
            ///// <summary>The 8 key on the numeric keypad.</summary>
            //NumPad8 = NumPad0 | H,
            ///// <summary>The 9 key on the numeric keypad.</summary>
            //NumPad9 = NumPad8 | NumPad1,
            ///// <summary>The multiply key.</summary>
            //Multiply = NumPad8 | NumPad2,
            ///// <summary>The add key.</summary>
            //Add = Multiply | NumPad9,
            ///// <summary>The separator key.</summary>
            //Separator = NumPad8 | NumPad4,
            ///// <summary>The subtract key.</summary>
            //Subtract = Separator | NumPad9,
            ///// <summary>The decimal key.</summary>
            //Decimal = Separator | Multiply,
            ///// <summary>The divide key.</summary>
            //Divide = Decimal | Subtract,
            ///// <summary>The F1 key.</summary>
            //F1 = NumPad0 | P,
            ///// <summary>The F2 key.</summary>
            //F2 = F1 | NumPad1,
            ///// <summary>The F3 key.</summary>
            //F3 = F1 | NumPad2,
            ///// <summary>The F4 key.</summary>
            //F4 = F3 | F2,
            ///// <summary>The F5 key.</summary>
            //F5 = F1 | NumPad4,
            ///// <summary>The F6 key.</summary>
            //F6 = F5 | F2,
            ///// <summary>The F7 key.</summary>
            //F7 = F5 | F3,
            ///// <summary>The F8 key.</summary>
            //F8 = F7 | F6,
            ///// <summary>The F9 key.</summary>
            //F9 = F1 | NumPad8,
            ///// <summary>The F10 key.</summary>
            //F10 = F9 | F2,
            ///// <summary>The F11 key.</summary>
            //F11 = F9 | F3,
            ///// <summary>The F12 key.</summary>
            //F12 = F11 | F10,
            ///// <summary>The F13 key.</summary>
            //F13 = F9 | F5,
            ///// <summary>The F14 key.</summary>
            //F14 = F13 | F10,
            ///// <summary>The F15 key.</summary>
            //F15 = F13 | F11,
            ///// <summary>The F16 key.</summary>
            //F16 = F15 | F14,
            ///// <summary>The F17 key.</summary>
            //F17 = 128,
            ///// <summary>The F18 key.</summary>
            //F18 = F17 | LButton,
            ///// <summary>The F19 key.</summary>
            //F19 = F17 | RButton,
            ///// <summary>The F20 key.</summary>
            //F20 = F19 | F18,
            ///// <summary>The F21 key.</summary>
            //F21 = F17 | MButton,
            ///// <summary>The F22 key.</summary>
            //F22 = F21 | F18,
            ///// <summary>The F23 key.</summary>
            //F23 = F21 | F19,
            ///// <summary>The F24 key.</summary>
            //F24 = F23 | F22,
            ///// <summary>The NUM LOCK key.</summary>
            //NumLock = F17 | ShiftKey,
            ///// <summary>The SCROLL LOCK key.</summary>
            //Scroll = NumLock | F18,
            ///// <summary>The left SHIFT key.</summary>
            //LShiftKey = F17 | Space,
            ///// <summary>The right SHIFT key.</summary>
            //RShiftKey = LShiftKey | F18,
            ///// <summary>The left CTRL key.</summary>
            //LControlKey = LShiftKey | F19,
            ///// <summary>The right CTRL key.</summary>
            //RControlKey = LControlKey | RShiftKey,
            ///// <summary>The left ALT key.</summary>
            //LMenu = LShiftKey | F21,
            ///// <summary>The right ALT key.</summary>
            //RMenu = LMenu | RShiftKey,
            ///// <summary>The browser back key (Windows 2000 or later).</summary>
            //BrowserBack = LMenu | LControlKey,
            ///// <summary>The browser forward key (Windows 2000 or later).</summary>
            //BrowserForward = BrowserBack | RMenu,
            ///// <summary>The browser refresh key (Windows 2000 or later).</summary>
            //BrowserRefresh = LShiftKey | Down,
            ///// <summary>The browser stop key (Windows 2000 or later).</summary>
            //BrowserStop = BrowserRefresh | RShiftKey,
            ///// <summary>The browser search key (Windows 2000 or later).</summary>
            //BrowserSearch = BrowserRefresh | LControlKey,
            ///// <summary>The browser favorites key (Windows 2000 or later).</summary>
            //BrowserFavorites = BrowserSearch | BrowserStop,
            ///// <summary>The browser home key (Windows 2000 or later).</summary>
            //BrowserHome = BrowserRefresh | LMenu,
            ///// <summary>The volume mute key (Windows 2000 or later).</summary>
            //VolumeMute = BrowserHome | BrowserStop,
            ///// <summary>The volume down key (Windows 2000 or later).</summary>
            //VolumeDown = BrowserHome | BrowserSearch,
            ///// <summary>The volume up key (Windows 2000 or later).</summary>
            //VolumeUp = VolumeDown | VolumeMute,
            ///// <summary>The media next track key (Windows 2000 or later).</summary>
            //MediaNextTrack = LShiftKey | NumLock,
            ///// <summary>The media previous track key (Windows 2000 or later).</summary>
            //MediaPreviousTrack = MediaNextTrack | RShiftKey,
            ///// <summary>The media Stop key (Windows 2000 or later).</summary>
            //MediaStop = MediaNextTrack | LControlKey,
            ///// <summary>The media play pause key (Windows 2000 or later).</summary>
            //MediaPlayPause = MediaStop | MediaPreviousTrack,
            ///// <summary>The launch mail key (Windows 2000 or later).</summary>
            //LaunchMail = MediaNextTrack | LMenu,
            ///// <summary>The select media key (Windows 2000 or later).</summary>
            //SelectMedia = LaunchMail | MediaPreviousTrack,
            ///// <summary>The start application one key (Windows 2000 or later).</summary>
            //LaunchApplication1 = LaunchMail | MediaStop,
            ///// <summary>The start application two key (Windows 2000 or later).</summary>
            //LaunchApplication2 = LaunchApplication1 | SelectMedia,
            ///// <summary>The OEM 1 key.</summary>
            //Oem1 = MediaStop | BrowserSearch,
            ///// <summary>The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemSemicolon = Oem1,
            ///// <summary>The OEM plus key on any country/region keyboard (Windows 2000 or later).</summary>
            //Oemplus = OemSemicolon | MediaPlayPause,
            ///// <summary>The OEM comma key on any country/region keyboard (Windows 2000 or later).</summary>
            //Oemcomma = LaunchMail | BrowserHome,
            ///// <summary>The OEM minus key on any country/region keyboard (Windows 2000 or later).</summary>
            //OemMinus = Oemcomma | SelectMedia,
            ///// <summary>The OEM period key on any country/region keyboard (Windows 2000 or later).</summary>
            //OemPeriod = Oemcomma | OemSemicolon,
            ///// <summary>The OEM 2 key.</summary>
            //Oem2 = OemPeriod | OemMinus,
            ///// <summary>The OEM question mark key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemQuestion = Oem2,
            ///// <summary>The OEM 3 key.</summary>
            //Oem3 = 192,
            ///// <summary>The OEM tilde key on a US standard keyboard (Windows 2000 or later).</summary>
            //Oemtilde = Oem3,
            ///// <summary>The OEM 4 key.</summary>
            //Oem4 = Oemtilde | Scroll | F20 | LWin,
            ///// <summary>The OEM open bracket key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemOpenBrackets = Oem4,
            ///// <summary>The OEM 5 key.</summary>
            //Oem5 = Oemtilde | NumLock | F21 | RWin,
            ///// <summary>The OEM pipe key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemPipe = Oem5,
            ///// <summary>The OEM 6 key.</summary>
            //Oem6 = OemPipe | Scroll,
            ///// <summary>The OEM close bracket key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemCloseBrackets = Oem6,
            ///// <summary>The OEM 7 key.</summary>
            //Oem7 = OemPipe | F23,
            ///// <summary>The OEM singled/double quote key on a US standard keyboard (Windows 2000 or later).</summary>
            //OemQuotes = Oem7,
            ///// <summary>The OEM 8 key.</summary>
            //Oem8 = OemQuotes | OemCloseBrackets,
            ///// <summary>The OEM 102 key.</summary>
            //Oem102 = Oemtilde | LControlKey,
            ///// <summary>The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows 2000 or later).</summary>
            //OemBackslash = Oem102,
            ///// <summary>The PROCESS KEY key.</summary>
            //ProcessKey = Oemtilde | RMenu,
            ///// <summary>Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.</summary>
            //Packet = ProcessKey | OemBackslash,
            ///// <summary>The ATTN key.</summary>
            //Attn = OemBackslash | LaunchApplication1,
            ///// <summary>The CRSEL key.</summary>
            //Crsel = Attn | Packet,
            ///// <summary>The EXSEL key.</summary>
            //Exsel = Oemtilde | MediaNextTrack | BrowserRefresh,
            ///// <summary>The ERASE EOF key.</summary>
            //EraseEof = Exsel | MediaPreviousTrack,
            ///// <summary>The PLAY key.</summary>
            //Play = Exsel | OemBackslash,
            ///// <summary>The ZOOM key.</summary>
            //Zoom = Play | EraseEof,
            ///// <summary>A constant reserved for future use.</summary>
            //NoName = Exsel | OemPipe,
            ///// <summary>The PA1 key.</summary>
            //Pa1 = NoName | EraseEof,
            ///// <summary>The CLEAR key.</summary>
            //OemClear = NoName | Play,
            ///// <summary>The bitmask to extract a key code from a key value.</summary>
            //KeyCode = 65535,
            ///// <summary>The SHIFT modifier key.</summary>
            //Shift = 65536,
            ///// <summary>The CTRL modifier key.</summary>
            //Control = 131072,
            ///// <summary>The ALT modifier key.</summary>
            //Alt = 262144,
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        private static void Press(ScanCode key, bool up)
        {
            //var a = Convert.ToInt32("1E", 16);
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                //keybd_event((byte)key, 30, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                keybd_event((byte)key, (byte)(key), KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                keybd_event((byte)key, (byte)(key), 0, (UIntPtr)0);
            }
        }
        public static void Press(ScanCode key)
        {
            Press(key, false);
            Press(key, true);
        }
        public static void Press(int key)
        {
            Press(key, false);
            Press(key, true);
        }
        private static void Press(int key, bool up)
        {
            //var a = Convert.ToInt32("1E", 16);
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {
                //keybd_event((byte)key, 30, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                keybd_event((byte)key, (byte)(key), KEYEVENTF_KEYUP, (UIntPtr)0);
            }
            else
            {
                keybd_event((byte)key, (byte)(key), 0, (UIntPtr)0);
            }
        }
        //PressKey(Keys.A, false);
        //PressKey(Keys.A, true);
        //public static void PressBy(Keys key, bool up)
        //{
        //    const int KEYEVENTF_EXTENDEDKEY = 0x1;
        //    const int KEYEVENTF_KEYUP = 0x2;
        //    if (up)
        //    {
        //        //keybd_event((byte)key, bScan, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
        //        keybd_event((byte)key, 0x45, KEYEVENTF_KEYUP, (UIntPtr)0);
        //    }
        //    else
        //    {
        //        keybd_event((byte)key, 0x45, 0, (UIntPtr)0);
        //    }
        //}
        //void TestProc()
        //{
        //    PressKey(Keys.ControlKey, false);
        //    PressKey(Keys.P, false);
        //    PressKey(Keys.P, true);
        //    PressKey(Keys.ControlKey, true);
        //}
    }
}
