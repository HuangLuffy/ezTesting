using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CommonLib.Util;
using ATLib.Input;

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

        //public static IEnumerable<Tuple<string, IEnumerable<Tuple<string, string>>>> ReassignMenuItemsList = null;

        //{
        //    [ReassignMenuItems.LettersNumbers] = { ["ReassignMenuItems.LettersNumbers"] = "" }
        //};
        public class ReassignMenuOptionAndSubItems
        {
            public string MenuOption { set; get; }
            public IEnumerable<ReassignMenuSubItem> MenuSubItems;
        }
        public class ReassignMenuSubItem
        {
            public string Name { set; get; }
            public string Value { set; get; }
        }
        private static IEnumerable<ReassignMenuSubItem> GetReassignMenuSubItems(Type obj)
        {
            return obj.GetFields().Select(field => new ReassignMenuSubItem {Name = field.Name, Value = field.GetValue(0).ToString()}).ToList();
        }
        public class ReassignMenuItems
        {
            public const string LettersNumbers = "Letters & Numbers";//Letters &amp; Numbers
            //public const string Macro = "Macro";
            public const string MediaKeys = "Media Keys";
            public const string MiscKeys = "Misc Keys";
            public const string ModifierSpacingKeys = "Modifier & Spacing Keys";//MODIFIER &amp; SPACING KEYS
            public const string NavigationKeys = "Navigation Keys";
            public const string NumpadKeys = "Numpad Keys";
            public const string PunctuationKeys = "Punctuation Keys";
            //Letters & Numbers, Macro, Media Keys, Misc Keys, Modifier & Spacing Keys, Navigation Keys, Numpad Keys, Punctuation Keys
            public static IEnumerable<ReassignMenuOptionAndSubItems> GetReassignMenuItemsList()
            {
                IEnumerable<ReassignMenuOptionAndSubItems> t = new List<ReassignMenuOptionAndSubItems>();
                var types = typeof(ReassignMenuItems).GetNestedTypes();
                foreach (var type in types)
                {
                    t.Add(new ReassignMenuOptionAndSubItems
                    {
                        MenuOption = typeof(ReassignMenuItems).GetFields().First((x) => x.Name.Equals(type.Name.Replace("Items", ""))).GetValue(0).ToString(),
                        MenuSubItems = GetReassignMenuSubItems(type)
                    });
                }
                return t;
            }
            public class LettersNumbersItems
            {
                public static string SC_KEY_A = Hw.KbKeys.SC_KEY_A.UiaName;
                public static string SC_KEY_B = Hw.KbKeys.SC_KEY_B.UiaName;
                public static string SC_KEY_C = Hw.KbKeys.SC_KEY_C.UiaName;
                public static string SC_KEY_D = Hw.KbKeys.SC_KEY_D.UiaName;
                public static string SC_KEY_E = Hw.KbKeys.SC_KEY_E.UiaName;
                public static string SC_KEY_F = Hw.KbKeys.SC_KEY_F.UiaName;
                public static string SC_KEY_G = Hw.KbKeys.SC_KEY_G.UiaName;
                public static string SC_KEY_H = Hw.KbKeys.SC_KEY_H.UiaName;
                public static string SC_KEY_I = Hw.KbKeys.SC_KEY_I.UiaName;
                public static string SC_KEY_O = Hw.KbKeys.SC_KEY_O.UiaName;
                public static string SC_KEY_P = Hw.KbKeys.SC_KEY_P.UiaName;
                public static string SC_KEY_Q = Hw.KbKeys.SC_KEY_Q.UiaName;
                public static string SC_KEY_R = Hw.KbKeys.SC_KEY_R.UiaName;
                public static string SC_KEY_S = Hw.KbKeys.SC_KEY_S.UiaName;
                public static string SC_KEY_T = Hw.KbKeys.SC_KEY_T.UiaName;
                public static string SC_KEY_U = Hw.KbKeys.SC_KEY_U.UiaName;
                public static string SC_KEY_V = Hw.KbKeys.SC_KEY_V.UiaName;
                public static string SC_KEY_W = Hw.KbKeys.SC_KEY_W.UiaName;
                public static string SC_KEY_X = Hw.KbKeys.SC_KEY_X.UiaName;
                public static string SC_KEY_Y = Hw.KbKeys.SC_KEY_Y.UiaName;
                public static string SC_KEY_Z = Hw.KbKeys.SC_KEY_Z.UiaName;
                public static string SC_KEY_1 = Hw.KbKeys.SC_KEY_1.UiaName;
                public static string SC_KEY_2 = Hw.KbKeys.SC_KEY_2.UiaName;
                public static string SC_KEY_3 = Hw.KbKeys.SC_KEY_3.UiaName;
                public static string SC_KEY_4 = Hw.KbKeys.SC_KEY_4.UiaName;
                public static string SC_KEY_5 = Hw.KbKeys.SC_KEY_5.UiaName;
                public static string SC_KEY_6 = Hw.KbKeys.SC_KEY_6.UiaName;
                public static string SC_KEY_7 = Hw.KbKeys.SC_KEY_7.UiaName;
                public static string SC_KEY_8 = Hw.KbKeys.SC_KEY_8.UiaName;
                public static string SC_KEY_9 = Hw.KbKeys.SC_KEY_9.UiaName;
                public static string SC_KEY_0 = Hw.KbKeys.SC_KEY_0.UiaName;
            }
            public class MediaKeysItems
            {
                public static string SC_KEY_PLAY_PAUSE = Hw.KbKeys.SC_KEY_PLAY_PAUSE.UiaName;
                public static string SC_KEY_STOP = Hw.KbKeys.SC_KEY_STOP.UiaName;
                public static string SC_KEY_NEXT_TRACK = Hw.KbKeys.SC_KEY_NEXT_TRACK.UiaName;
                public static string SC_KEY_PRE_TRACK = Hw.KbKeys.SC_KEY_PRE_TRACK.UiaName;
                public static string SC_KEY_VOL_DEC = Hw.KbKeys.SC_KEY_VOL_DEC.UiaName;
                public static string SC_KEY_VOL_INC = Hw.KbKeys.SC_KEY_VOL_INC.UiaName;
                public static string SC_KEY_MUTE = Hw.KbKeys.SC_KEY_MUTE.UiaName;
                public static string SC_KEY_MAIL = Hw.KbKeys.SC_KEY_MAIL.UiaName;
                public static string SC_KEY_CALCULATOR = Hw.KbKeys.SC_KEY_CALCULATOR.UiaName;
                public static string SC_KEY_W3HOME = Hw.KbKeys.SC_KEY_W3HOME.UiaName;
            }
            public class MiscKeysItems
            {
                public static string SC_KEY_L_WIN = Hw.KbKeys.SC_KEY_L_WIN.UiaName;
                public static string SC_KEY_R_WIN = Hw.KbKeys.SC_KEY_R_WIN.UiaName;
                public static string SC_KEY_PRINT = Hw.KbKeys.SC_KEY_PRINT.UiaName;
                public static string SC_KEY_SCROLL = Hw.KbKeys.SC_KEY_SCROLL.UiaName;
                public static string SC_KEY_PAUSE = Hw.KbKeys.SC_KEY_PAUSE.UiaName;
            }
            public class ModifierSpacingKeysItems
            {
                public static string SC_KEY_TAB = Hw.KbKeys.SC_KEY_TAB.UiaName;
                public static string SC_KEY_CAP = Hw.KbKeys.SC_KEY_CAP.UiaName;
                public static string SC_KEY_L_SHIFT = Hw.KbKeys.SC_KEY_L_SHIFT.UiaName;
                public static string SC_KEY_L_CTRL = Hw.KbKeys.SC_KEY_L_CTRL.UiaName;
                public static string SC_KEY_L_ALT = Hw.KbKeys.SC_KEY_L_ALT.UiaName;
                public static string SC_KEY_SPACE = Hw.KbKeys.SC_KEY_SPACE.UiaName;
                public static string SC_KEY_R_ALT = Hw.KbKeys.SC_KEY_R_ALT.UiaName;
                public static string SC_KEY_R_CTRL = Hw.KbKeys.SC_KEY_R_CTRL.UiaName;
                public static string SC_KEY_R_SHIFT = Hw.KbKeys.SC_KEY_R_SHIFT.UiaName;
                public static string SC_KEY_ENTER = Hw.KbKeys.SC_KEY_ENTER.UiaName;
                public static string SC_KEY_BACKSPACE = Hw.KbKeys.SC_KEY_BACKSPACE.UiaName;
            }
            public class NavigationKeysItems
            {
                public static string SC_KEY_INSERT = Hw.KbKeys.SC_KEY_INSERT.UiaName;
                public static string SC_KEY_HOME = Hw.KbKeys.SC_KEY_HOME.UiaName;
                public static string SC_KEY_PGUP = Hw.KbKeys.SC_KEY_PGUP.UiaName;
                public static string SC_KEY_DEL = Hw.KbKeys.SC_KEY_DEL.UiaName;
                public static string SC_KEY_END = Hw.KbKeys.SC_KEY_END.UiaName;
                public static string SC_KEY_PGDN = Hw.KbKeys.SC_KEY_PGDN.UiaName; //SK622
                public static string SC_KEY_UP_ARROW = Hw.KbKeys.SC_KEY_UP_ARROW.UiaName;
                public static string SC_KEY_L_ARROW = Hw.KbKeys.SC_KEY_L_ARROW.UiaName;
                public static string SC_KEY_DN_ARROW = Hw.KbKeys.SC_KEY_DN_ARROW.UiaName;
                public static string SC_KEY_R_ARROW = Hw.KbKeys.SC_KEY_R_ARROW.UiaName;
            }
            public class NumpadKeysItems
            {
                public static string SC_KEY_NUM_LOCK = Hw.KbKeys.SC_KEY_NUM_LOCK.UiaName;
                public static string SC_KEY_NUM_DIV = Hw.KbKeys.SC_KEY_NUM_DIV.UiaName;
                public static string SC_KEY_NUM_STAR = Hw.KbKeys.SC_KEY_NUM_STAR.UiaName;
                public static string SC_KEY_NUM_NEG = Hw.KbKeys.SC_KEY_NUM_NEG.UiaName;
                public static string SC_KEY_NUM_PLUS = Hw.KbKeys.SC_KEY_NUM_PLUS.UiaName;
                public static string SC_KEY_NUM_ENTER = Hw.KbKeys.SC_KEY_NUM_ENTER.UiaName;
                public static string SC_KEY_1 = Hw.KbKeys.SC_KEY_NUM_1.UiaName;
                public static string SC_KEY_2 = Hw.KbKeys.SC_KEY_NUM_2.UiaName;
                public static string SC_KEY_3 = Hw.KbKeys.SC_KEY_NUM_3.UiaName;
                public static string SC_KEY_4 = Hw.KbKeys.SC_KEY_NUM_4.UiaName;
                public static string SC_KEY_5 = Hw.KbKeys.SC_KEY_NUM_5.UiaName;
                public static string SC_KEY_6 = Hw.KbKeys.SC_KEY_NUM_6.UiaName;
                public static string SC_KEY_7 = Hw.KbKeys.SC_KEY_NUM_7.UiaName;
                public static string SC_KEY_8 = Hw.KbKeys.SC_KEY_NUM_8.UiaName;
                public static string SC_KEY_9 = Hw.KbKeys.SC_KEY_NUM_9.UiaName;
                public static string SC_KEY_0 = Hw.KbKeys.SC_KEY_NUM_0.UiaName;
            }
            public class PunctuationKeysItems
            {
                public static string SC_KEY_NEG = Hw.KbKeys.SC_KEY_NEG.UiaName;
                public static string SC_KEY_EQUATION = Hw.KbKeys.SC_KEY_EQUATION.UiaName;
                public static string SC_KEY_L_BACKETS = Hw.KbKeys.SC_KEY_L_BACKETS.UiaName;
                public static string SC_KEY_R_BACKETS = Hw.KbKeys.SC_KEY_R_BACKETS.UiaName;
                public static string SC_KEY_BACKSLASH = Hw.KbKeys.SC_KEY_BACKSLASH.UiaName;
                public static string SC_KEY_SEMICOLON = Hw.KbKeys.SC_KEY_SEMICOLON.UiaName;
                public static string SC_KEY_APOSTROPHE = Hw.KbKeys.SC_KEY_APOSTROPHE.UiaName;
                public static string SC_KEY_COMMA = Hw.KbKeys.SC_KEY_COMMA.UiaName;
                public static string SC_KEY_DOT = Hw.KbKeys.SC_KEY_DOT.UiaName;
                public static string SC_KEY_SLASH = Hw.KbKeys.SC_KEY_SLASH.UiaName;
            }




            //public class LettersNumbersItems
            //{
            //    public const string SC_KEY_A = "A";
            //    public const string SC_KEY_B = "B";
            //    public const string SC_KEY_C = "C";
            //    public const string SC_KEY_D = "D";
            //    public const string SC_KEY_E = "E";
            //    public const string SC_KEY_F = "F";
            //    public const string SC_KEY_G = "G";
            //    public const string SC_KEY_H = "H";
            //    public const string SC_KEY_I = "I";
            //    public const string SC_KEY_O = "O";
            //    public const string SC_KEY_P = "P";
            //    public const string SC_KEY_Q = "Q";
            //    public const string SC_KEY_R = "R";
            //    public const string SC_KEY_S = "S";
            //    public const string SC_KEY_T = "T";
            //    public const string SC_KEY_U = "U";
            //    public const string SC_KEY_V = "V";
            //    public const string SC_KEY_W = "W";
            //    public const string SC_KEY_X = "X";
            //    public const string SC_KEY_Y = "Y";
            //    public const string SC_KEY_Z = "Z";
            //    public const string SC_KEY_1 = "1!";
            //    public const string SC_KEY_2 = "2@";
            //    public const string SC_KEY_3 = "3#";
            //    public const string SC_KEY_4 = "4$";
            //    public const string SC_KEY_5 = "5%";
            //    public const string SC_KEY_6 = "6^";
            //    public const string SC_KEY_7 = "7&";
            //    public const string SC_KEY_8 = "8*";
            //    public const string SC_KEY_9 = "9(";
            //    public const string SC_KEY_0 = "0)";
            //}
            //public class MediaKeysItems
            //{
            //    public const string SC_KEY_PLAY_PAUSE = "PLAY/PAUSE";
            //    public const string SC_KEY_STOP = "STOP";
            //    public const string SC_KEY_NEXT_TRACK = "NEXT TRACK";
            //    public const string SC_KEY_PRE_TRACK = "PREVIOUS TRACK";
            //    public const string SC_KEY_VOL_DEC = "VOLUME DOWN";
            //    public const string SC_KEY_VOL_INC = "VOLUME UP";
            //    public const string SC_KEY_MUTE = "MUTE";
            //    public const string SC_KEY_MAIL = "E-MAIL";
            //    public const string SC_KEY_CALCULATOR = "CALCULATOR";
            //    public const string SC_KEY_W3HOME = "WEB BROWSER";
            //}
            //public class MiscKeysItems
            //{
            //    public const string SC_KEY_L_WIN = "L_WIN";
            //    public const string SC_KEY_R_WIN = "R_WIN";
            //    public const string SC_KEY_PRINT = "PRTSC";
            //    public const string SC_KEY_SCROLL = "SCRLK";
            //    public const string SC_KEY_PAUSE = "PAUSE";
            //}
            //public class ModifierSpacingKeysItems
            //{
            //    public const string SC_KEY_TAB = "TAB";
            //    public const string SC_KEY_CAP = "CAPSLK";
            //    public const string SC_KEY_L_SHIFT = "L_SHIFT";
            //    public const string SC_KEY_L_CTRL = "L_CTRL";
            //    public const string SC_KEY_L_ALT = "L_ALT";
            //    public const string SC_KEY_SPACE = "SPACE";
            //    public const string SC_KEY_R_ALT = "R_ALT";
            //    public const string SC_KEY_R_CTRL = "R_CTRL";
            //    public const string SC_KEY_R_SHIFT = "R_SHIFT";
            //    public const string SC_KEY_ENTER = "ENTER";
            //    public const string SC_KEY_BACKSPACE = "BACKSPACE";
            //}
            //public class NavigationKeysItems
            //{
            //    public const string SC_KEY_INSERT = "INS";
            //    public const string SC_KEY_HOME = "HOME";
            //    public const string SC_KEY_PGUP = "PGUP";
            //    public const string SC_KEY_DEL = "DEL";
            //    public const string SC_KEY_END = "END";
            //    public const string SC_KEY_PGDN = "PGDN"; //SK622
            //    public const string SC_KEY_UP_ARROW = "^";
            //    public const string SC_KEY_L_ARROW = "<";
            //    public const string SC_KEY_DN_ARROW = "v";
            //    public const string SC_KEY_R_ARROW = ">";
            //}
            //public class NumpadKeysItems
            //{
            //    public const string SC_KEY_NUM_LOCK = "NUMLK";
            //    public const string SC_KEY_NUM_DIV = "/";
            //    public const string SC_KEY_NUM_STAR = "*";
            //    public const string SC_KEY_NUM_NEG = "-";
            //    public const string SC_KEY_NUM_PLUS = "+";
            //    public const string SC_KEY_NUM_ENTER = "NP_ENTER";
            //    public const string SC_KEY_1 = "1";
            //    public const string SC_KEY_2 = "2";
            //    public const string SC_KEY_3 = "3";
            //    public const string SC_KEY_4 = "4";
            //    public const string SC_KEY_5 = "5";
            //    public const string SC_KEY_6 = "6";
            //    public const string SC_KEY_7 = "7";
            //    public const string SC_KEY_8 = "8";
            //    public const string SC_KEY_9 = "9";
            //    public const string SC_KEY_0 = "0";
            //}
            //public class PunctuationKeysItems
            //{
            //    public const string SC_KEY_NEG = "-_";
            //    public const string SC_KEY_EQUATION = "=+";
            //    public const string SC_KEY_L_BACKETS = "{[";
            //    public const string SC_KEY_R_BACKETS = "}]";
            //    public const string SC_KEY_BACKSLASH = "|\\";
            //    public const string SC_KEY_SEMICOLON = ":;";
            //    public const string SC_KEY_APOSTROPHE = "\"'";
            //    public const string SC_KEY_COMMA = "<,";
            //    public const string SC_KEY_DOT = ">.";
            //    public const string SC_KEY_SLASH = "?/";
            //}
        }
    }
}
