using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CommonLib.Util;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlus : SW
    {
        public MasterPlus()
        {
            SwLnkPath = @"C:\Users\Public\Desktop\MasterPlus.lnk";
            SwName = "MasterPlus+";
            SwProcessName = "MasterPlusApp";
            SwBuildPath = @"D:\Dev\CM\MPP\MasterPlus(PER. Only)_v1.8.5.exe";
        }
        public struct KeyMappingGridColor
        {
            public const string Green = "#00ff00";
            public const string Purple = "#84329b";
            public const string Red = "#ff0000";
            public const string Cyan = "#00aeef";
            public static string GetVarName(string value)
            {
                return UtilReflect.GetVarNameByValue(typeof(KeyMappingGridColor), value);
            }
        }

        public enum Tabs
        {
            Wireless,
            Lighting,
            KeyMapping,
            Macros,
            Profiles
        }

        public int GetTabIndexByTabCount(int tabCount, Tabs tabs = Tabs.KeyMapping)
        {
            if (tabCount == 4)
            {
                return Convert.ToInt16(tabs - 1);
            }
            else
            {
                return Convert.ToInt16(tabs);
            }
        }

        public class ReassignMenuItems
        {
            public const string LettersNumbers = "Letters & Numbers";
            public const string Macro = "Macro";
            public const string MediaKeys = "Media Keys";
            public const string MiscKeys = "MiscKeys";
            public const string ModifierSpacingKeys = "Modifier & Spacing Keys";
            public const string NavigationKeys = "Navigation Keys";
            public const string NumpadKeys = "Numpad Keys";
            public const string PunctuationKeys = "Punctuation Keys";
            //Letters & Numbers, Macro, Media Keys, Misc Keys, Modifier & Spacing Keys, Navigation Keys, Numpad Keys, Punctuation Keys
            public class MediaKeysItems
            {
                public const string SC_KEY_PLAY_PAUSE = "PLAY/PAUSE";
                public const string SC_KEY_STOP = "STOP";
                public const string SC_KEY_NEXT_TRACK = "NEXT TRACK";
                public const string SC_KEY_PRE_TRACK = "PREVIOUS TRACK";
                public const string SC_KEY_VOL_DEC = "VOLUME DOWN";
                public const string SC_KEY_VOL_INC = "VOLUME UP";
                public const string SC_KEY_MUTE = "MUTE";
                public const string SC_KEY_MAIL = "E-MAIL";
                public const string SC_KEY_CALCULATOR = "CALCULATOR";
                public const string SC_KEY_W3HOME = "WEB BROWSER";
            }
            public class LettersNumbersItems
            {
                public const string SC_KEY_A = "A";
                public const string SC_KEY_B = "B";
                public const string SC_KEY_C = "C";
                public const string SC_KEY_D = "D";
                public const string SC_KEY_E = "E";
                public const string SC_KEY_F = "F";
                public const string SC_KEY_G = "G";
                public const string SC_KEY_H = "H";
                public const string SC_KEY_I = "I";
                public const string SC_KEY_O = "O";
                public const string SC_KEY_P = "P";
                public const string SC_KEY_Q = "Q";
                public const string SC_KEY_R = "R";
                public const string SC_KEY_S = "S";
                public const string SC_KEY_T = "T";
                public const string SC_KEY_U = "U";
                public const string SC_KEY_V = "V";
                public const string SC_KEY_W = "W";
                public const string SC_KEY_X = "X";
                public const string SC_KEY_Y = "Y";
                public const string SC_KEY_Z = "Z";
                public const string SC_KEY_1 = "1!";
                public const string SC_KEY_2 = "2@";
                public const string SC_KEY_3 = "3#";
                public const string SC_KEY_4 = "4$";
                public const string SC_KEY_5 = "5%";
                public const string SC_KEY_6 = "6^";
                public const string SC_KEY_7 = "7&";
                public const string SC_KEY_8 = "8*";
                public const string SC_KEY_9 = "9(";
                public const string SC_KEY_0 = "0)";
            }
            public class MiscKeysItems
            {
                public const string SC_KEY_L_WIN = "L_WIN";
                public const string SC_KEY_R_WIN = "R_WIN";
                public const string SC_KEY_PRINT = "PRTSC";
                public const string SC_KEY_SCROLL = "SCRLK";
                public const string SC_KEY_PAUSE = "PAUSE";
            }
            public class ModifierSpacingKeysItems
            {
                public const string SC_KEY_TAB = "TAB";
                public const string SC_KEY_CAP = "CAPSLK";
                public const string SC_KEY_L_SHIFT = "L_SHIFT";
                public const string SC_KEY_L_CTRL = "L_CTRL";
                public const string SC_KEY_L_ALT = "L_ALT";
                public const string SC_KEY_SPACE = "SPACE";
                public const string SC_KEY_R_ALT = "R_ALT";
                public const string SC_KEY_R_CTRL = "R_CTRL";
                public const string SC_KEY_R_SHIFT = "R_SHIFT";
                public const string SC_KEY_ENTER = "ENTER";
                public const string SC_KEY_BACKSPACE = "BACKSPACE";
            }
            public class NavigationKeysItems
            {
                public const string SC_KEY_INSERT = "INS";
                public const string SC_KEY_HOME = "HOME";
                public const string SC_KEY_PGUP = "PGUP";
                public const string SC_KEY_DEL = "DEL";
                public const string SC_KEY_END = "END";
                public const string SC_KEY_PGDN = "PGDN";
                public const string SC_KEY_UP_ARROW = "^";
                public const string SC_KEY_L_ARROW = "<";
                public const string SC_KEY_DN_ARROW = "v";
                public const string SC_KEY_R_ARROW = ">";
            }
            public class NumpadKeysItems
            {
                public const string SC_KEY_NUM_LOCK = "NUMLK";
                public const string SC_KEY_NUM_DIV = "/";
                public const string SC_KEY_NUM_STAR = "*";
                public const string SC_KEY_NUM_NEG = "-";
                public const string SC_KEY_NUM_PLUS = "+";
                public const string SC_KEY_NUM_ENTER = "MP_ENTER";
                public const string SC_KEY_1 = "1";
                public const string SC_KEY_2 = "2";
                public const string SC_KEY_3 = "3";
                public const string SC_KEY_4 = "4";
                public const string SC_KEY_5 = "5";
                public const string SC_KEY_6 = "6";
                public const string SC_KEY_7 = "7";
                public const string SC_KEY_8 = "8";
                public const string SC_KEY_9 = "9";
                public const string SC_KEY_0 = "0";
            }
            public class PunctuationKeysItems
            {
                public const string SC_KEY_NEG = "-_";
                public const string SC_KEY_EQUATION = "=+";
                public const string SC_KEY_L_BACKETS = "{[";
                public const string SC_KEY_R_BACKETS = "}]";
                public const string SC_KEY_BACKSLASH = "|\\";
                public const string SC_KEY_SEMICOLON = ":;";
                public const string SC_KEY_APOSTROPHE = "\"\"'";
                public const string SC_KEY_COMMA = "<,";
                public const string SC_KEY_DOT = ">.";
                public const string SC_KEY_SLASH = "?/";
            }
        }
    }
}
