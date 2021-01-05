using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Util;
using CommonLib.Util.IO;

namespace ATLib.Input
{
    public class Hw
    {
        private static readonly IDictionary<string, IEnumerable<string>> LocDic = new Dictionary<string, IEnumerable<string>>();
        //public static string CurrentLanguage = Language.EN
        private static readonly IDictionary<string, int> LanguageIndexDic = new Dictionary<string, int>
            {
                {"English", 0}, {"中文（简体）", 1}, {"繁體中文", 2}, {"Français", 3}, {"Deutsch", 4}, {"Italiano", 5}, {"日本語", 6}, {"Korean", 7}, {"Malay", 8},
                {"Português (Portugal)", 9}, {"Русский", 10}, {"Español", 11}, {"Thai", 12}, {"Türkçe", 13}, {"Vietnamese", 14}
            };

        //public static Language GetLanguage()
        //{
        //    return new Language();
        //}

        public class LanguageItems
        {
            public LanguageItems()
            {

            }
            public LanguageItems(int v1, string v2, string v3, string v4, string v5)
            {
                this.Index = v1;
                this.Install = v2;
                this.English = v3;
                this.Overview = v4;
                this.TsFile = v5;
            }
            public int Index { set; get; }
            public string Install { set; get; }
            public string English { set; get; }
            public string Overview { set; get; }
            public string TsFile { set; get; }
        }
        public class Language
        {
            public static LanguageItems English = new LanguageItems { Index = 0, Install = "English", English = "English", Overview = "OVERVIEW", TsFile = "" };
            public static LanguageItems ChineseSimplified = new LanguageItems { Index = 1, Install = "中文（简体", English = "ChineseSimplified", Overview = "概观", TsFile = "masterplus_zh_cn.ts"};
            public static LanguageItems ChineseTraditional = new LanguageItems(2, "繁體中文", "ChineseTraditional", "概觀", "masterplus_zh_tw.ts");
            public static LanguageItems French = new LanguageItems(3, "Français", "French", "VUE D’ENSEMBLE", "masterplus_fr.ts");
            public static LanguageItems German = new LanguageItems(4, "Deutsch", "German", "ÜBERSICHT", "masterplus_de.ts");
            public static LanguageItems Italian = new LanguageItems(5, "Italiano", "Italian", "DESCRIZIONE", "masterplus_it.ts");
            public static LanguageItems Japanese = new LanguageItems(6, "日本語", "Japanese", "概要", "masterplus_ja.ts");
            public static LanguageItems Korean = new LanguageItems(7, "Korean", "Korean", "개요", "masterplus_ko.ts");
            public static LanguageItems Malay = new LanguageItems(8, "Malay", "Malay", "GAMBARAN KESELURUHAN", "masterplus_ms.ts");
            public static LanguageItems Portuguese = new LanguageItems(9, "Português (Portugal)", "Portuguese", "VISÃO GERAL", "masterplus_pt.ts");
            public static LanguageItems Russian = new LanguageItems(10, "Русский", "Russian", "ОБЩИЕ СВЕДЕНИЯ", "masterplus_ru.ts");
            public static LanguageItems Spanish = new LanguageItems(11, "Español", "Spanish", "INFORMACIÓN GENERAL", "masterplus_es.ts");
            public static LanguageItems Thai = new LanguageItems(12, "Thai", "Thai", "ภาพรวม", "masterplus_th.ts");
            public static LanguageItems Turkish = new LanguageItems(13, "Türkçe", "Turkish", "GENEL BAKIŞ", "masterplus_tr.ts");
            public static LanguageItems Vietnamese = new LanguageItems(14, "Vietnamese", "Vietnamese", "TỔNG QUAN", "masterplus_vi.ts");


            //public Tuple<int, string, string, string, string> English = new Tuple<int, string, string, string, string>(0, "English", "English", "OVERVIEW", "");
            //public Tuple<int, string, string, string, string> ChineseSimplified = new Tuple<int, string, string, string, string>(1, "中文（简体", "ChineseSimplified", "概观", "masterplus_zh_cn.ts");
            //public Tuple<int, string, string, string, string> ChineseTraditional = new Tuple<int, string, string, string, string>(2, "繁體中文", "ChineseTraditional", "概觀", "masterplus_zh_tw.ts");
            //public Tuple<int, string, string, string, string> French = new Tuple<int, string, string, string, string>(3, "Français", "French", "VUE D’ENSEMBLE", "masterplus_fr.ts");
            //public Tuple<int, string, string, string, string> German = new Tuple<int, string, string, string, string>(4, "Deutsch", "German", "ÜBERSICHT", "masterplus_de.ts");
            //public Tuple<int, string, string, string, string> Italian = new Tuple<int, string, string, string, string>(5, "Italiano", "Italian", "DESCRIZIONE", "masterplus_it.ts");
            //public Tuple<int, string, string, string, string> Japanese = new Tuple<int, string, string, string, string>(6, "日本語", "Japanese", "概要", "masterplus_ja.ts");
            //public Tuple<int, string, string, string, string> Korean = new Tuple<int, string, string, string, string>(7, "Korean", "Korean", "개요", "masterplus_ko.ts");
            //public Tuple<int, string, string, string, string> Malay = new Tuple<int, string, string, string, string>(8, "Malay", "Malay", "GAMBARAN KESELURUHAN", "masterplus_ms.ts");
            //public Tuple<int, string, string, string, string> Portuguese = new Tuple<int, string, string, string, string>(9, "Português (Portugal)", "Portuguese", "VISÃO GERAL", "masterplus_pt.ts");
            //public Tuple<int, string, string, string, string> Russian = new Tuple<int, string, string, string, string>(10, "Русский", "Russian", "ОБЩИЕ СВЕДЕНИЯ", "masterplus_ru.ts");
            //public Tuple<int, string, string, string, string> Spanish = new Tuple<int, string, string, string, string>(11, "Español", "Spanish", "INFORMACIÓN GENERAL", "masterplus_es.ts");
            //public Tuple<int, string, string, string, string> Thai = new Tuple<int, string, string, string, string>(12, "Thai", "Thai", "ภาพรวม", "masterplus_th.ts");
            //public Tuple<int, string, string, string, string> Turkish = new Tuple<int, string, string, string, string>(13, "Türkçe", "Turkish", "GENEL BAKIŞ", "masterplus_tr.ts");
            //public Tuple<int, string, string, string, string> Vietnamese = new Tuple<int, string, string, string, string>(14, "Vietnamese", "Vietnamese", "TỔNG QUAN", "masterplus_vi.ts");
            //public string GetMasterPlusLanguage(string overview)
            //{
            //    var field =  typeof(Language).GetFields().FirstOrDefault((x) => ((Tuple<int, string, string, string, string>)x.GetValue(new Language())).Item4.Equals(overview));
            //    return ((Tuple<int, string, string, string, string>)field.GetValue(new Language())).Item3;
            //}
        }



        public enum Language1
        {
            EN,
            ZH_CN,
            ZH_TW,
            FR,
            DE,
            IT,
            JA,
            KO,
            MS,
            PT,
            RU,
            ES,
            TH,
            TR,
            VI
        }

        public class KeyPros
        {
            public int ScanCode { get; set; }
            public int KeyValue { get; set; }
            public int Flag { get; set; }
            public string KeyCode { get; set; }
            public string VarName { get; set; }
            public string UiaName { get; set; }
            public string Port { get; set; }
        }
        public class KBKeys
        {
            public static KeyPros SC_KEY_A = new KeyPros() { ScanCode = 30, KeyValue = 65, Flag = 0, KeyCode = "A", VarName = nameof(SC_KEY_A), UiaName = "A", Port = "" };
            public static KeyPros SC_KEY_B = new KeyPros() { ScanCode = 48, KeyValue = 66, Flag = 0, KeyCode = "B", VarName = nameof(SC_KEY_B), UiaName = "B", Port = "" };
            public static KeyPros SC_KEY_C = new KeyPros() { ScanCode = 46, KeyValue = 67, Flag = 0, KeyCode = "C", VarName = nameof(SC_KEY_C), UiaName = "C", Port = "" };
            public static KeyPros SC_KEY_D = new KeyPros() { ScanCode = 32, KeyValue = 68, Flag = 0, KeyCode = "D", VarName = nameof(SC_KEY_D), UiaName = "D", Port = "" };
            public static KeyPros SC_KEY_E = new KeyPros() { ScanCode = 18, KeyValue = 69, Flag = 0, KeyCode = "E", VarName = nameof(SC_KEY_E), UiaName = "E", Port = "" };
            public static KeyPros SC_KEY_F = new KeyPros() { ScanCode = 33, KeyValue = 70, Flag = 0, KeyCode = "F", VarName = nameof(SC_KEY_F), UiaName = "F", Port = "" };
            public static KeyPros SC_KEY_G = new KeyPros() { ScanCode = 34, KeyValue = 71, Flag = 0, KeyCode = "G", VarName = nameof(SC_KEY_G), UiaName = "G", Port = "" };
            public static KeyPros SC_KEY_H = new KeyPros() { ScanCode = 35, KeyValue = 72, Flag = 0, KeyCode = "H", VarName = nameof(SC_KEY_H), UiaName = "H", Port = "" };
            public static KeyPros SC_KEY_I = new KeyPros() { ScanCode = 23, KeyValue = 73, Flag = 0, KeyCode = "I", VarName = nameof(SC_KEY_I), UiaName = "I", Port = "" };
            public static KeyPros SC_KEY_J = new KeyPros() { ScanCode = 36, KeyValue = 74, Flag = 0, KeyCode = "J", VarName = nameof(SC_KEY_J), UiaName = "J", Port = "" };
            public static KeyPros SC_KEY_K = new KeyPros() { ScanCode = 37, KeyValue = 75, Flag = 0, KeyCode = "K", VarName = nameof(SC_KEY_K), UiaName = "K", Port = "" };
            public static KeyPros SC_KEY_L = new KeyPros() { ScanCode = 38, KeyValue = 76, Flag = 0, KeyCode = "L", VarName = nameof(SC_KEY_L), UiaName = "L", Port = "" };
            public static KeyPros SC_KEY_M = new KeyPros() { ScanCode = 50, KeyValue = 77, Flag = 0, KeyCode = "M", VarName = nameof(SC_KEY_M), UiaName = "M", Port = "" };
            public static KeyPros SC_KEY_N = new KeyPros() { ScanCode = 49, KeyValue = 78, Flag = 0, KeyCode = "N", VarName = nameof(SC_KEY_N), UiaName = "N", Port = "" };
            public static KeyPros SC_KEY_O = new KeyPros() { ScanCode = 24, KeyValue = 79, Flag = 0, KeyCode = "O", VarName = nameof(SC_KEY_O), UiaName = "O", Port = "" };
            public static KeyPros SC_KEY_P = new KeyPros() { ScanCode = 25, KeyValue = 80, Flag = 0, KeyCode = "P", VarName = nameof(SC_KEY_P), UiaName = "P", Port = "" };
            public static KeyPros SC_KEY_Q = new KeyPros() { ScanCode = 16, KeyValue = 81, Flag = 0, KeyCode = "Q", VarName = nameof(SC_KEY_Q), UiaName = "Q", Port = "" };
            public static KeyPros SC_KEY_R = new KeyPros() { ScanCode = 19, KeyValue = 82, Flag = 0, KeyCode = "R", VarName = nameof(SC_KEY_R), UiaName = "R", Port = "" };
            public static KeyPros SC_KEY_S = new KeyPros() { ScanCode = 31, KeyValue = 83, Flag = 0, KeyCode = "S", VarName = nameof(SC_KEY_S), UiaName = "S", Port = "" };
            public static KeyPros SC_KEY_T = new KeyPros() { ScanCode = 20, KeyValue = 84, Flag = 0, KeyCode = "T", VarName = nameof(SC_KEY_T), UiaName = "T", Port = "" };
            public static KeyPros SC_KEY_U = new KeyPros() { ScanCode = 22, KeyValue = 85, Flag = 0, KeyCode = "U", VarName = nameof(SC_KEY_U), UiaName = "U", Port = "" };
            public static KeyPros SC_KEY_V = new KeyPros() { ScanCode = 47, KeyValue = 86, Flag = 0, KeyCode = "V", VarName = nameof(SC_KEY_V), UiaName = "V", Port = "" };
            public static KeyPros SC_KEY_W = new KeyPros() { ScanCode = 17, KeyValue = 87, Flag = 0, KeyCode = "W", VarName = nameof(SC_KEY_W), UiaName = "W", Port = "" };
            public static KeyPros SC_KEY_X = new KeyPros() { ScanCode = 45, KeyValue = 88, Flag = 0, KeyCode = "X", VarName = nameof(SC_KEY_X), UiaName = "X", Port = "" };
            public static KeyPros SC_KEY_Y = new KeyPros() { ScanCode = 21, KeyValue = 89, Flag = 0, KeyCode = "Y", VarName = nameof(SC_KEY_Y), UiaName = "Y", Port = "" };
            public static KeyPros SC_KEY_Z = new KeyPros() { ScanCode = 44, KeyValue = 90, Flag = 0, KeyCode = "Z", VarName = nameof(SC_KEY_Z), UiaName = "Z", Port = "" };
            public static KeyPros SC_KEY_ESC = new KeyPros() { ScanCode = 1, KeyValue = 27, Flag = 0, KeyCode = "Escape", VarName = nameof(SC_KEY_ESC), UiaName = "ESC", Port = "" };
            public static KeyPros SC_KEY_F1 = new KeyPros() { ScanCode = 59, KeyValue = 112, Flag = 0, KeyCode = "F1", VarName = nameof(SC_KEY_F1), UiaName = "F1", Port = "" };
            public static KeyPros SC_KEY_F2 = new KeyPros() { ScanCode = 60, KeyValue = 113, Flag = 0, KeyCode = "F2", VarName = nameof(SC_KEY_F2), UiaName = "F2", Port = "" };
            public static KeyPros SC_KEY_F3 = new KeyPros() { ScanCode = 61, KeyValue = 114, Flag = 0, KeyCode = "F3", VarName = nameof(SC_KEY_F3), UiaName = "F3", Port = "" };
            public static KeyPros SC_KEY_F4 = new KeyPros() { ScanCode = 62, KeyValue = 115, Flag = 0, KeyCode = "F4", VarName = nameof(SC_KEY_F4), UiaName = "F4", Port = "" };
            public static KeyPros SC_KEY_F5 = new KeyPros() { ScanCode = 63, KeyValue = 116, Flag = 0, KeyCode = "F5", VarName = nameof(SC_KEY_F5), UiaName = "F5", Port = "" };
            public static KeyPros SC_KEY_F6 = new KeyPros() { ScanCode = 64, KeyValue = 117, Flag = 0, KeyCode = "F6", VarName = nameof(SC_KEY_F6), UiaName = "F6", Port = "" };
            public static KeyPros SC_KEY_F7 = new KeyPros() { ScanCode = 65, KeyValue = 118, Flag = 0, KeyCode = "F7", VarName = nameof(SC_KEY_F7), UiaName = "F7", Port = "" };
            public static KeyPros SC_KEY_F8 = new KeyPros() { ScanCode = 66, KeyValue = 119, Flag = 0, KeyCode = "F8", VarName = nameof(SC_KEY_F8), UiaName = "F8", Port = "" };
            public static KeyPros SC_KEY_F9 = new KeyPros() { ScanCode = 67, KeyValue = 120, Flag = 0, KeyCode = "F9", VarName = nameof(SC_KEY_F9), UiaName = "F9", Port = "" };
            public static KeyPros SC_KEY_F10 = new KeyPros() { ScanCode = 68, KeyValue = 121, Flag = 0, KeyCode = "F10", VarName = nameof(SC_KEY_F10), UiaName = "F10", Port = "" };
            public static KeyPros SC_KEY_F11 = new KeyPros() { ScanCode = 87, KeyValue = 122, Flag = 0, KeyCode = "F11", VarName = nameof(SC_KEY_F11), UiaName = "F11", Port = "" };
            public static KeyPros SC_KEY_F12 = new KeyPros() { ScanCode = 88, KeyValue = 123, Flag = 0, KeyCode = "F12", VarName = nameof(SC_KEY_F12), UiaName = "F12", Port = "" };
            public static KeyPros SC_KEY_TILDE = new KeyPros() { ScanCode = 41, KeyValue = 192, Flag = 0, KeyCode = "Oemtilde", VarName = nameof(SC_KEY_TILDE), UiaName = "`~", Port = "" };
            public static KeyPros SC_KEY_1 = new KeyPros() { ScanCode = 2, KeyValue = 49, Flag = 0, KeyCode = "D1", VarName = nameof(SC_KEY_1), UiaName = "1!", Port = "" };
            public static KeyPros SC_KEY_2 = new KeyPros() { ScanCode = 3, KeyValue = 50, Flag = 0, KeyCode = "D2", VarName = nameof(SC_KEY_2), UiaName = "2@", Port = "" };
            public static KeyPros SC_KEY_3 = new KeyPros() { ScanCode = 4, KeyValue = 51, Flag = 0, KeyCode = "D3", VarName = nameof(SC_KEY_3), UiaName = "3#", Port = "" };
            public static KeyPros SC_KEY_4 = new KeyPros() { ScanCode = 5, KeyValue = 52, Flag = 0, KeyCode = "D4", VarName = nameof(SC_KEY_4), UiaName = "4$", Port = "" };
            public static KeyPros SC_KEY_5 = new KeyPros() { ScanCode = 6, KeyValue = 53, Flag = 0, KeyCode = "D5", VarName = nameof(SC_KEY_5), UiaName = "5%", Port = "" };
            public static KeyPros SC_KEY_6 = new KeyPros() { ScanCode = 7, KeyValue = 54, Flag = 0, KeyCode = "D6", VarName = nameof(SC_KEY_6), UiaName = "6^", Port = "" };
            public static KeyPros SC_KEY_7 = new KeyPros() { ScanCode = 8, KeyValue = 55, Flag = 0, KeyCode = "D7", VarName = nameof(SC_KEY_7), UiaName = "7&", Port = "" };
            public static KeyPros SC_KEY_8 = new KeyPros() { ScanCode = 9, KeyValue = 56, Flag = 0, KeyCode = "D8", VarName = nameof(SC_KEY_8), UiaName = "8*", Port = "" };
            public static KeyPros SC_KEY_9 = new KeyPros() { ScanCode = 10, KeyValue = 57, Flag = 0, KeyCode = "D9", VarName = nameof(SC_KEY_9), UiaName = "9(", Port = "" };
            public static KeyPros SC_KEY_0 = new KeyPros() { ScanCode = 11, KeyValue = 48, Flag = 0, KeyCode = "D0", VarName = nameof(SC_KEY_0), UiaName = "0(", Port = "" };
            public static KeyPros SC_KEY_NEG = new KeyPros() { ScanCode = 12, KeyValue = 189, Flag = 0, KeyCode = "OemMinus", VarName = nameof(SC_KEY_NEG), UiaName = "-_", Port = "" };
            public static KeyPros SC_KEY_EQUATION = new KeyPros() { ScanCode = 13, KeyValue = 187, Flag = 0, KeyCode = "Oemplus", VarName = nameof(SC_KEY_EQUATION), UiaName = "=+", Port = "" };
            public static KeyPros SC_KEY_BACKSPACE = new KeyPros() { ScanCode = 14, KeyValue = 8, Flag = 0, KeyCode = "Back", VarName = nameof(SC_KEY_BACKSPACE), UiaName = "BACKSPACE", Port = "" };
            public static KeyPros SC_KEY_TAB = new KeyPros() { ScanCode = 15, KeyValue = 9, Flag = 0, KeyCode = "Tab", VarName = nameof(SC_KEY_TAB), UiaName = "TAB", Port = "" };
            public static KeyPros SC_KEY_L_BACKETS = new KeyPros() { ScanCode = 26, KeyValue = 219, Flag = 0, KeyCode = "OemOpenBrackets", VarName = nameof(SC_KEY_L_BACKETS), UiaName = "{[", Port = "" };
            public static KeyPros SC_KEY_R_BACKETS = new KeyPros() { ScanCode = 27, KeyValue = 221, Flag = 0, KeyCode = "Oem6", VarName = nameof(SC_KEY_R_BACKETS), UiaName = "}]", Port = "" };
            public static KeyPros SC_KEY_BACKSLASH = new KeyPros() { ScanCode = 43, KeyValue = 220, Flag = 0, KeyCode = "Oem5", VarName = nameof(SC_KEY_BACKSLASH), UiaName = "|\\", Port = "" };
            public static KeyPros SC_KEY_CAP = new KeyPros() { ScanCode = 58, KeyValue = 20, Flag = 0, KeyCode = "Capital", VarName = nameof(SC_KEY_CAP), UiaName = "CAPSLK", Port = "" };
            public static KeyPros SC_KEY_SEMICOLON = new KeyPros() { ScanCode = 39, KeyValue = 186, Flag = 0, KeyCode = "Oem1", VarName = nameof(SC_KEY_SEMICOLON), UiaName = ":;", Port = "" };
            public static KeyPros SC_KEY_APOSTROPHE = new KeyPros() { ScanCode = 40, KeyValue = 222, Flag = 0, KeyCode = "Oem7", VarName = nameof(SC_KEY_APOSTROPHE), UiaName = "\"'", Port = "" };
            public static KeyPros SC_KEY_ENTER = new KeyPros() { ScanCode = 28, KeyValue = 13, Flag = 0, KeyCode = "Return", VarName = nameof(SC_KEY_ENTER), UiaName = "ENTER", Port = "" };
            public static KeyPros SC_KEY_L_SHIFT = new KeyPros() { ScanCode = 42, KeyValue = 160, Flag = 0, KeyCode = "LShiftKey", VarName = nameof(SC_KEY_L_SHIFT), UiaName = "L_SHIFT", Port = "" };
            public static KeyPros SC_KEY_COMMA = new KeyPros() { ScanCode = 51, KeyValue = 188, Flag = 0, KeyCode = "Oemcomma", VarName = nameof(SC_KEY_COMMA), UiaName = "<,", Port = "" };
            public static KeyPros SC_KEY_DOT = new KeyPros() { ScanCode = 52, KeyValue = 190, Flag = 0, KeyCode = "OemPeriod", VarName = nameof(SC_KEY_DOT), UiaName = ">.", Port = "" };
            public static KeyPros SC_KEY_SLASH = new KeyPros() { ScanCode = 53, KeyValue = 191, Flag = 0, KeyCode = "OemQuestion", VarName = nameof(SC_KEY_SLASH), UiaName = "?/", Port = "" };
            public static KeyPros SC_KEY_R_SHIFT = new KeyPros() { ScanCode = 54, KeyValue = 161, Flag = 0, KeyCode = "RShiftKey", VarName = nameof(SC_KEY_R_SHIFT), UiaName = "R_SHIFT", Port = "" };
            public static KeyPros SC_KEY_L_CTRL = new KeyPros() { ScanCode = 29, KeyValue = 162, Flag = 0, KeyCode = "LControlKey", VarName = nameof(SC_KEY_L_CTRL), UiaName = "L_CTRL", Port = "" };
            public static KeyPros SC_KEY_L_WIN = new KeyPros() { ScanCode = 91, KeyValue = 91, Flag = 0, KeyCode = "LWin", VarName = nameof(SC_KEY_L_WIN), UiaName = "L_WIN", Port = "" };
            public static KeyPros SC_KEY_L_ALT = new KeyPros() { ScanCode = 56, KeyValue = 164, Flag = 0, KeyCode = "LMenu", VarName = nameof(SC_KEY_L_ALT), UiaName = "L_ALT", Port = "" };
            public static KeyPros SC_KEY_SPACE = new KeyPros() { ScanCode = 57, KeyValue = 32, Flag = 0, KeyCode = "Space", VarName = nameof(SC_KEY_SPACE), UiaName = "SPACE", Port = "" };
            public static KeyPros SC_KEY_R_ALT = new KeyPros() { ScanCode = 56, KeyValue = 165, Flag = 0, KeyCode = "RMenu", VarName = nameof(SC_KEY_R_ALT), UiaName = "R_ALT", Port = "" };
            public static KeyPros SC_KEY_R_WIN = new KeyPros() { ScanCode = 92, KeyValue = 92, Flag = 0, KeyCode = "RWin", VarName = nameof(SC_KEY_R_WIN), UiaName = "R_WIN", Port = "" };
            public static KeyPros SC_KEY_R_CTRL = new KeyPros() { ScanCode = 29, KeyValue = 163, Flag = 0, KeyCode = "RControlKey", VarName = nameof(SC_KEY_R_CTRL), UiaName = "R_CTRL", Port = "" };
            public static KeyPros SC_KEY_PRINT = new KeyPros() { ScanCode = 55, KeyValue = 44, Flag = 0, KeyCode = "PrintScreen", VarName = nameof(SC_KEY_PRINT), UiaName = "PRTSC", Port = "" };
            public static KeyPros SC_KEY_SCROLL = new KeyPros() { ScanCode = 70, KeyValue = 145, Flag = 0, KeyCode = "Scroll", VarName = nameof(SC_KEY_SCROLL), UiaName = "SCRLK", Port = "" };
            public static KeyPros SC_KEY_PAUSE = new KeyPros() { ScanCode = 69, KeyValue = 19, Flag = 0, KeyCode = "Pause", VarName = nameof(SC_KEY_PAUSE), UiaName = "PAUSE", Port = "" };
            public static KeyPros SC_KEY_INSERT = new KeyPros() { ScanCode = 82, KeyValue = 45, Flag = 0, KeyCode = "Insert", VarName = nameof(SC_KEY_INSERT), UiaName = "INS", Port = "" };
            public static KeyPros SC_KEY_HOME = new KeyPros() { ScanCode = 71, KeyValue = 36, Flag = 0, KeyCode = "Home", VarName = nameof(SC_KEY_HOME), UiaName = "HOME", Port = "" };
            public static KeyPros SC_KEY_PGUP = new KeyPros() { ScanCode = 73, KeyValue = 33, Flag = 0, KeyCode = "PageUp", VarName = nameof(SC_KEY_PGUP), UiaName = "PGUP", Port = "" };
            public static KeyPros SC_KEY_DEL = new KeyPros() { ScanCode = 83, KeyValue = 46, Flag = 0, KeyCode = "Delete", VarName = nameof(SC_KEY_DEL), UiaName = "DEL", Port = "" };
            public static KeyPros SC_KEY_END = new KeyPros() { ScanCode = 79, KeyValue = 35, Flag = 0, KeyCode = "End", VarName = nameof(SC_KEY_END), UiaName = "END", Port = "" };
            public static KeyPros SC_KEY_PGDN = new KeyPros() { ScanCode = 81, KeyValue = 34, Flag = 0, KeyCode = "Next", VarName = nameof(SC_KEY_PGDN), UiaName = "PGDN", Port = "" };
            public static KeyPros SC_KEY_UP_ARROW = new KeyPros() { ScanCode = 72, KeyValue = 38, Flag = 0, KeyCode = "Up", VarName = nameof(SC_KEY_L_ARROW), UiaName = "^", Port = "" };
            public static KeyPros SC_KEY_L_ARROW = new KeyPros() { ScanCode = 75, KeyValue = 37, Flag = 0, KeyCode = "Left", VarName = nameof(SC_KEY_A), UiaName = "<", Port = "" };
            public static KeyPros SC_KEY_DN_ARROW = new KeyPros() { ScanCode = 80, KeyValue = 40, Flag = 0, KeyCode = "Down", VarName = nameof(SC_KEY_DN_ARROW), UiaName = "v", Port = "" };
            public static KeyPros SC_KEY_R_ARROW = new KeyPros() { ScanCode = 77, KeyValue = 39, Flag = 0, KeyCode = "Right", VarName = nameof(SC_KEY_R_ARROW), UiaName = ">", Port = "" };
            public static KeyPros SC_KEY_NUM_LOCK = new KeyPros() { ScanCode = 69, KeyValue = 144, Flag = 0, KeyCode = "NumLock", VarName = nameof(SC_KEY_NUM_LOCK), UiaName = "NUMLK", Port = "" };
            public static KeyPros SC_KEY_NUM_DIV = new KeyPros() { ScanCode = 53, KeyValue = 111, Flag = 0, KeyCode = "Divide", VarName = nameof(SC_KEY_NUM_DIV), UiaName = "/", Port = "" };
            public static KeyPros SC_KEY_NUM_STAR = new KeyPros() { ScanCode = 55, KeyValue = 106, Flag = 0, KeyCode = "Multiple", VarName = nameof(SC_KEY_NUM_STAR), UiaName = "*", Port = "" };
            public static KeyPros SC_KEY_NUM_NEG = new KeyPros() { ScanCode = 74, KeyValue = 109, Flag = 0, KeyCode = "Subtract", VarName = nameof(SC_KEY_NUM_NEG), UiaName = "-", Port = "" };
            public static KeyPros SC_KEY_NUM_7 = new KeyPros() { ScanCode = 71, KeyValue = 103, Flag = 0, KeyCode = "NumPad7", VarName = nameof(SC_KEY_NUM_7), UiaName = "7", Port = "" };
            public static KeyPros SC_KEY_NUM_8 = new KeyPros() { ScanCode = 72, KeyValue = 104, Flag = 0, KeyCode = "NumPad8", VarName = nameof(SC_KEY_NUM_8), UiaName = "8", Port = "" };
            public static KeyPros SC_KEY_NUM_9 = new KeyPros() { ScanCode = 73, KeyValue = 105, Flag = 0, KeyCode = "NumPad9", VarName = nameof(SC_KEY_NUM_9), UiaName = "9", Port = "" };
            public static KeyPros SC_KEY_NUM_4 = new KeyPros() { ScanCode = 75, KeyValue = 100, Flag = 0, KeyCode = "NumPad4", VarName = nameof(SC_KEY_NUM_4), UiaName = "4", Port = "" };
            public static KeyPros SC_KEY_NUM_5 = new KeyPros() { ScanCode = 76, KeyValue = 101, Flag = 0, KeyCode = "NumPad5", VarName = nameof(SC_KEY_NUM_5), UiaName = "5", Port = "" };
            public static KeyPros SC_KEY_NUM_6 = new KeyPros() { ScanCode = 77, KeyValue = 102, Flag = 0, KeyCode = "NumPad6", VarName = nameof(SC_KEY_NUM_6), UiaName = "6", Port = "" };
            public static KeyPros SC_KEY_NUM_PLUS = new KeyPros() { ScanCode = 78, KeyValue = 107, Flag = 0, KeyCode = "Add", VarName = nameof(SC_KEY_NUM_PLUS), UiaName = "+", Port = "" };
            public static KeyPros SC_KEY_NUM_1 = new KeyPros() { ScanCode = 79, KeyValue = 97, Flag = 0, KeyCode = "NumPad1", VarName = nameof(SC_KEY_NUM_1), UiaName = "1", Port = "" };
            public static KeyPros SC_KEY_NUM_2 = new KeyPros() { ScanCode = 80, KeyValue = 98, Flag = 0, KeyCode = "NumPad2", VarName = nameof(SC_KEY_NUM_2), UiaName = "2", Port = "" };
            public static KeyPros SC_KEY_NUM_3 = new KeyPros() { ScanCode = 81, KeyValue = 99, Flag = 0, KeyCode = "NumPad3", VarName = nameof(SC_KEY_NUM_3), UiaName = "3", Port = "" };
            public static KeyPros SC_KEY_NUM_0 = new KeyPros() { ScanCode = 82, KeyValue = 96, Flag = 0, KeyCode = "NumPad0", VarName = nameof(SC_KEY_NUM_0), UiaName = "0", Port = "" };
            public static KeyPros SC_KEY_NUM_DOT = new KeyPros() { ScanCode = 83, KeyValue = 110, Flag = 0, KeyCode = "Decimal", VarName = nameof(SC_KEY_NUM_DOT), UiaName = ".", Port = "" };
            public static KeyPros SC_KEY_NUM_ENTER = new KeyPros() { ScanCode = 28, KeyValue = 13, Flag = 1, KeyCode = "Return", VarName = nameof(SC_KEY_NUM_ENTER), UiaName = "NP_ENTER", Port = "" };

            public static KeyPros SC_KEY_NO = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_NO), UiaName = "DISABLE", Port = "" };

            public static KeyPros SC_KEY_PLAY_PAUSE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_PLAY_PAUSE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_STOP = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_STOP), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_NEXT_TRACK = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_NEXT_TRACK), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_PRE_TRACK = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_PRE_TRACK), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_VOL_INC = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_VOL_INC), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_VOL_DEC = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_VOL_DEC), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_MUTE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_MUTE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_MEDIA_SEL = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_MEDIA_SEL), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_MAIL = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_MAIL), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_CALCULATOR = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_CALCULATOR), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_MYCOMPUTER = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_MYCOMPUTER), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3SEARCH = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3SEARCH), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3HOME = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3HOME), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3BACK = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3BACK), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3FORWARD = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3FORWARD), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3STOP = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3STOP), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_W3REFRESH = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_W3REFRESH), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_FAVORITE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_FAVORITE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_XY_ONOFF = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_XY_ONOFF), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_PROFILE_NEXT_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_PROFILE_NEXT_CYCLE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_PROFILE_PREV_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_PROFILE_PREV_CYCLE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_CHANGE_LED_MODE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_CHANGE_LED_MODE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_CHANGE_LED_COLOR = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_CHANGE_LED_COLOR), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_DPI_NEXT = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_DPI_NEXT), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_DPI_PREV = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_DPI_PREV), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_DPI_NEXT_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_DPI_NEXT_CYCLE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_DPI_PREV_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_DPI_PREV_CYCLE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_POWER_MODE_DISABLE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_POWER_MODE_DISABLE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_POWER_MODE_WORKING = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_POWER_MODE_WORKING), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_POWER_MODE_PERFORMANCE = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_POWER_MODE_PERFORMANCE), UiaName = "", Port = "" };
            public static KeyPros SC_KEY_POWER_MODE_GAMING = new KeyPros() { ScanCode = -10, KeyValue = -10, Flag = 0, KeyCode = "", VarName = nameof(SC_KEY_POWER_MODE_GAMING), UiaName = "", Port = "" };
        }
        //public enum Keys
        //{
        //    SC_KEY_A = 30,
        //    SC_KEY_B = 48,
        //    SC_KEY_C = 46,
        //    SC_KEY_D = 32,
        //    SC_KEY_E = 18,
        //    SC_KEY_F = 33,
        //    SC_KEY_G = 34,
        //    SC_KEY_H = 35,
        //    SC_KEY_I = 23,
        //    SC_KEY_J = 36,
        //    SC_KEY_K = 37,
        //    SC_KEY_L = 38,
        //    SC_KEY_M = 50,
        //    SC_KEY_N = 49,
        //    SC_KEY_O = 24,
        //    SC_KEY_P = 25,
        //    SC_KEY_Q = 16,
        //    SC_KEY_R = 19,
        //    SC_KEY_S = 31,
        //    SC_KEY_T = 20,
        //    SC_KEY_U = 22,
        //    SC_KEY_V = 47,
        //    SC_KEY_W = 17,
        //    SC_KEY_X = 45,
        //    SC_KEY_Y = 21,
        //    SC_KEY_Z = 44,
        //    SC_KEY_NO = -10,
        //    SC_KEY_ESC = -10,
        //    SC_KEY_F1 = -10,
        //    SC_KEY_F2 = -10,
        //    SC_KEY_F3 = -10,
        //    SC_KEY_F4 = -10,
        //    SC_KEY_F5 = -10,
        //    SC_KEY_F6 = -10,
        //    SC_KEY_F7 = -10,
        //    SC_KEY_F8 = -10,
        //    SC_KEY_F9 = -10,
        //    SC_KEY_F10 = -10,
        //    SC_KEY_F11 = -10,
        //    SC_KEY_F12 = -10,
        //    SC_KEY_TILDE = -10,
        //    SC_KEY_1 = -10,
        //    SC_KEY_2 = -10,
        //    SC_KEY_3 = -10,
        //    SC_KEY_4 = -10,
        //    SC_KEY_5 = -10,
        //    SC_KEY_6 = -10,
        //    SC_KEY_7 = -10,
        //    SC_KEY_8 = -10,
        //    SC_KEY_9 = -10,
        //    SC_KEY_0 = -10,
        //    SC_KEY_NEG = -10,
        //    SC_KEY_EQUATION = -10,
        //    SC_KEY_BACKSPACE = -10,
        //    SC_KEY_TAB = -10,
        //    SC_KEY_L_BACKETS = -10,
        //    SC_KEY_R_BACKETS = -10,
        //    SC_KEY_BACKSLASH = -10,
        //    SC_KEY_CAP = -10,
        //    SC_KEY_SEMICOLON = -10,
        //    SC_KEY_APOSTROPHE = -10,
        //    SC_KEY_ENTER = -10,
        //    SC_KEY_L_SHIFT = -10,
        //    SC_KEY_COMMA = -10,
        //    SC_KEY_DOT = -10,
        //    SC_KEY_SLASH = -10,
        //    SC_KEY_R_SHIFT = -10,
        //    SC_KEY_L_CTRL = -10,
        //    SC_KEY_L_WIN = -10,
        //    SC_KEY_L_ALT = -10,
        //    SC_KEY_SPACE = -10,
        //    SC_KEY_R_ALT = -10,
        //    SC_KEY_R_WIN = -10,
        //    SC_KEY_R_CTRL = -10,
        //    SC_KEY_PRINT = -10,
        //    SC_KEY_SCROLL = -10,
        //    SC_KEY_PAUSE = -10,
        //    SC_KEY_INSERT = -10,
        //    SC_KEY_HOME = -10,
        //    SC_KEY_PGUP = -10,
        //    SC_KEY_DEL = -10,
        //    SC_KEY_END = -10,
        //    SC_KEY_PGDN = -10,
        //    SC_KEY_UP_ARROW = -10,
        //    SC_KEY_L_ARROW = -10,
        //    SC_KEY_DN_ARROW = -10,
        //    SC_KEY_R_ARROW = -10,
        //    SC_KEY_NUM_LOCK = -10,
        //    SC_KEY_NUM_DIV = -10,
        //    SC_KEY_NUM_STAR = -10,
        //    SC_KEY_NUM_NEG = -10,
        //    SC_KEY_NUM_7 = -10,
        //    SC_KEY_NUM_8 = -10,
        //    SC_KEY_NUM_9 = -10,
        //    SC_KEY_NUM_4 = -10,
        //    SC_KEY_NUM_5 = -10,
        //    SC_KEY_NUM_6 = -10,
        //    SC_KEY_NUM_PLUS = -10,
        //    SC_KEY_NUM_1 = -10,
        //    SC_KEY_NUM_2 = -10,
        //    SC_KEY_NUM_3 = -10,
        //    SC_KEY_NUM_0 = -10,
        //    SC_KEY_NUM_DOT = -10,
        //    SC_KEY_NUM_ENTER = -10,
        //    SC_KEY_PLAY_PAUSE = -10,
        //    SC_KEY_STOP = -10,
        //    SC_KEY_NEXT_TRACK = -10,
        //    SC_KEY_PRE_TRACK = -10,
        //    SC_KEY_VOL_INC = -10,
        //    SC_KEY_VOL_DEC = -10,
        //    SC_KEY_MUTE = -10,
        //    SC_KEY_MEDIA_SEL = -10,
        //    SC_KEY_MAIL = -10,
        //    SC_KEY_CALCULATOR = -10,
        //    SC_KEY_MYCOMPUTER = -10,
        //    SC_KEY_W3SEARCH = -10,
        //    SC_KEY_W3HOME = -10,
        //    SC_KEY_W3BACK = -10,
        //    SC_KEY_W3FORWARD = -10,
        //    SC_KEY_W3STOP = -10,
        //    SC_KEY_W3REFRESH = -10,
        //    SC_KEY_FAVORITE = -10,
        //    SC_KEY_XY_ONOFF = -10,
        //    SC_KEY_PROFILE_NEXT_CYCLE = -10,
        //    SC_KEY_PROFILE_PREV_CYCLE = -10,
        //    SC_KEY_CHANGE_LED_MODE = -10,
        //    SC_KEY_CHANGE_LED_COLOR = -10,
        //    SC_KEY_DPI_NEXT = -10,
        //    SC_KEY_DPI_PREV = -10,
        //    SC_KEY_DPI_NEXT_CYCLE = -10,
        //    SC_KEY_DPI_PREV_CYCLE = -10,
        //    SC_KEY_POWER_MODE_DISABLE = -10,
        //    SC_KEY_POWER_MODE_WORKING = -10,
        //    SC_KEY_POWER_MODE_PERFORMANCE = -10,
        //    SC_KEY_POWER_MODE_GAMING = -10
        //}
        public static void GetKeyboardKeys(string filePath)
        {
            var keyboardKeyLines = UtilFile.GetListByLine(filePath);
            foreach (var line in keyboardKeyLines)
            {
                if (!line.Contains("{ SC_KEY")) continue;
                var keys = UtilRegex.GetStringsFromDoubleQuo(line);
                LocDic.Add(line.Split(',')[0].Replace("    { ", ""), keys);
                //UtilFile.WriteFile("D:\\a.txt", line.Split(',')[0].Replace("    { ", "") + $" = -10,");
            }
        }
        public static KBKeys GetScanCode(KBKeys key)
        {
            return key;
        }
        public static string GetKeyVar(KBKeys key)
        {
            return "";
            //return UtilEnum.GetEnumNameByValue<Keys>(key);
        }
        //public static string GetKeyValue(Keys key, Language language = Language.EN)
        //{
        //    return LocDic[GetKeyVar(key)].ElementAt((int)language);
        //}
        public static LanguageItems GetLanguageItems(string configurationName)
        {
            if (configurationName.Equals("OVERVIEW"))
            {
                return Language.English;
            }
            if (configurationName.Equals("概观"))
            {
                return Language.ChineseSimplified;
            }
            if (configurationName.Equals("概觀"))
            {
                return Language.ChineseTraditional;
            }
            if (configurationName.Equals("ÜBERSICHT"))
            {
                return Language.German;
            }
            if (configurationName.Equals("INFORMACIÓN GENERAL"))
            {
                return Language.Spanish;
            }
            if (configurationName.Equals("VUE D’ENSEMBLE"))
            {
                return Language.French;
            }
            if (configurationName.Equals("DESCRIZIONE"))
            {
                return Language.Italian;
            }
            if (configurationName.Equals("概要"))
            {
                return Language.Japanese;
            }
            if (configurationName.Equals("개요"))
            {
                return Language.Korean;
            }
            if (configurationName.Equals("GAMBARAN KESELURUHAN"))
            {
                return Language.Malay;
            }
            if (configurationName.Equals("VISÃO GERAL"))
            {
                return Language.Portuguese;
            }
            if (configurationName.Equals("ОБЩИЕ СВЕДЕНИЯ"))
            {
                return Language.Russian;
            }
            if (configurationName.Equals("ภาพรวม"))
            {
                return Language.Thai;
            }
            if (configurationName.Equals("GENEL BAKIŞ"))
            {
                return Language.Turkish;
            }
            if (configurationName.Equals("TỔNG QUAN"))
            {
                return Language.Vietnamese;
            }
            return null;
        }
    }
}
