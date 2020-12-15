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
            public string KeyCode { get; set; }
            public string VarName { get; set; }
        }
        public class Keys
        {
            public static KeyPros SC_KEY_A = new KeyPros() { ScanCode = 30, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_A) };
            public static KeyPros SC_KEY_B = new KeyPros() { ScanCode = 48, KeyValue = 66, KeyCode = "A", VarName = nameof(SC_KEY_B) };
            public static KeyPros SC_KEY_C = new KeyPros() { ScanCode = 46, KeyValue = 67, KeyCode = "A", VarName = nameof(SC_KEY_C) };
            public static KeyPros SC_KEY_D = new KeyPros() { ScanCode = 32, KeyValue = 68, KeyCode = "A", VarName = nameof(SC_KEY_D) };
            public static KeyPros SC_KEY_E = new KeyPros() { ScanCode = 18, KeyValue = 69, KeyCode = "A", VarName = nameof(SC_KEY_E) };
            public static KeyPros SC_KEY_F = new KeyPros() { ScanCode = 33, KeyValue = 70, KeyCode = "A", VarName = nameof(SC_KEY_F) };
            public static KeyPros SC_KEY_G = new KeyPros() { ScanCode = 34, KeyValue = 71, KeyCode = "A", VarName = nameof(SC_KEY_G) };
            public static KeyPros SC_KEY_H = new KeyPros() { ScanCode = 35, KeyValue = 72, KeyCode = "A", VarName = nameof(SC_KEY_H) };
            public static KeyPros SC_KEY_I = new KeyPros() { ScanCode = 23, KeyValue = 73, KeyCode = "A", VarName = nameof(SC_KEY_I) };
            public static KeyPros SC_KEY_J = new KeyPros() { ScanCode = 36, KeyValue = 74, KeyCode = "A", VarName = nameof(SC_KEY_J) };
            public static KeyPros SC_KEY_K = new KeyPros() { ScanCode = 37, KeyValue = 75, KeyCode = "A", VarName = nameof(SC_KEY_K) };
            public static KeyPros SC_KEY_L = new KeyPros() { ScanCode = 38, KeyValue = 76, KeyCode = "A", VarName = nameof(SC_KEY_L) };
            public static KeyPros SC_KEY_M = new KeyPros() { ScanCode = 50, KeyValue = 77, KeyCode = "A", VarName = nameof(SC_KEY_M) };
            public static KeyPros SC_KEY_N = new KeyPros() { ScanCode = 49, KeyValue = 78, KeyCode = "A", VarName = nameof(SC_KEY_N) };
            public static KeyPros SC_KEY_O = new KeyPros() { ScanCode = 24, KeyValue = 79, KeyCode = "A", VarName = nameof(SC_KEY_O) };
            public static KeyPros SC_KEY_P = new KeyPros() { ScanCode = 25, KeyValue = 80, KeyCode = "A", VarName = nameof(SC_KEY_P) };
            public static KeyPros SC_KEY_Q = new KeyPros() { ScanCode = 16, KeyValue = 81, KeyCode = "A", VarName = nameof(SC_KEY_Q) };
            public static KeyPros SC_KEY_R = new KeyPros() { ScanCode = 19, KeyValue = 82, KeyCode = "A", VarName = nameof(SC_KEY_R) };
            public static KeyPros SC_KEY_S = new KeyPros() { ScanCode = 31, KeyValue = 83, KeyCode = "A", VarName = nameof(SC_KEY_S) };
            public static KeyPros SC_KEY_T = new KeyPros() { ScanCode = 20, KeyValue = 84, KeyCode = "A", VarName = nameof(SC_KEY_T) };
            public static KeyPros SC_KEY_U = new KeyPros() { ScanCode = 22, KeyValue = 85, KeyCode = "A", VarName = nameof(SC_KEY_U) };
            public static KeyPros SC_KEY_V = new KeyPros() { ScanCode = 47, KeyValue = 86, KeyCode = "A", VarName = nameof(SC_KEY_V) };
            public static KeyPros SC_KEY_W = new KeyPros() { ScanCode = 17, KeyValue = 87, KeyCode = "A", VarName = nameof(SC_KEY_W) };
            public static KeyPros SC_KEY_X = new KeyPros() { ScanCode = 45, KeyValue = 88, KeyCode = "A", VarName = nameof(SC_KEY_X) };
            public static KeyPros SC_KEY_Y = new KeyPros() { ScanCode = 21, KeyValue = 89, KeyCode = "A", VarName = nameof(SC_KEY_Y) };
            public static KeyPros SC_KEY_Z = new KeyPros() { ScanCode = 44, KeyValue = 90, KeyCode = "A", VarName = nameof(SC_KEY_Z) };
            public static KeyPros SC_KEY_NO = new KeyPros() { ScanCode = -10, KeyValue = 10, KeyCode = "DISABLE", VarName = nameof(SC_KEY_NO) };
            public static KeyPros SC_KEY_ESC = new KeyPros() { ScanCode = 1, KeyValue = 27, KeyCode = "Escape", VarName = nameof(SC_KEY_ESC) };
            public static KeyPros SC_KEY_F1 = new KeyPros() { ScanCode = 59, KeyValue = 112, KeyCode = "F1", VarName = nameof(SC_KEY_F1) };
            public static KeyPros SC_KEY_F2 = new KeyPros() { ScanCode = 60, KeyValue = 113, KeyCode = "F2", VarName = nameof(SC_KEY_F2) };
            public static KeyPros SC_KEY_F3 = new KeyPros() { ScanCode = 61, KeyValue = 114, KeyCode = "F3", VarName = nameof(SC_KEY_F3) };
            public static KeyPros SC_KEY_F4 = new KeyPros() { ScanCode = 62, KeyValue = 115, KeyCode = "F4", VarName = nameof(SC_KEY_F4) };
            public static KeyPros SC_KEY_F5 = new KeyPros() { ScanCode = 63, KeyValue = 116, KeyCode = "F5", VarName = nameof(SC_KEY_F5) };
            public static KeyPros SC_KEY_F6 = new KeyPros() { ScanCode = 64, KeyValue = 117, KeyCode = "F6", VarName = nameof(SC_KEY_F6) };
            public static KeyPros SC_KEY_F7 = new KeyPros() { ScanCode = 65, KeyValue = 118, KeyCode = "F7", VarName = nameof(SC_KEY_F7) };
            public static KeyPros SC_KEY_F8 = new KeyPros() { ScanCode = 66, KeyValue = 119, KeyCode = "F8", VarName = nameof(SC_KEY_F8) };
            public static KeyPros SC_KEY_F9 = new KeyPros() { ScanCode = 67, KeyValue = 120, KeyCode = "F9", VarName = nameof(SC_KEY_F9) };
            public static KeyPros SC_KEY_F10 = new KeyPros() { ScanCode = 68, KeyValue = 121, KeyCode = "F10", VarName = nameof(SC_KEY_F10) };
            public static KeyPros SC_KEY_F11 = new KeyPros() { ScanCode = 87, KeyValue = 122, KeyCode = "F11", VarName = nameof(SC_KEY_F11) };
            public static KeyPros SC_KEY_F12 = new KeyPros() { ScanCode = 88, KeyValue = 123, KeyCode = "F12", VarName = nameof(SC_KEY_F12) };
            public static KeyPros SC_KEY_TILDE = new KeyPros() { ScanCode = 41, KeyValue = 192, KeyCode = "Oemtilde", VarName = nameof(SC_KEY_TILDE) };
            public static KeyPros SC_KEY_1 = new KeyPros() { ScanCode = 2, KeyValue = 49, KeyCode = "D1", VarName = nameof(SC_KEY_1) };
            public static KeyPros SC_KEY_2 = new KeyPros() { ScanCode = 3, KeyValue = 50, KeyCode = "D2", VarName = nameof(SC_KEY_2) };
            public static KeyPros SC_KEY_3 = new KeyPros() { ScanCode = 4, KeyValue = 51, KeyCode = "D3", VarName = nameof(SC_KEY_3) };
            public static KeyPros SC_KEY_4 = new KeyPros() { ScanCode = 5, KeyValue = 52, KeyCode = "D4", VarName = nameof(SC_KEY_4) };
            public static KeyPros SC_KEY_5 = new KeyPros() { ScanCode = 6, KeyValue = 53, KeyCode = "D5", VarName = nameof(SC_KEY_5) };
            public static KeyPros SC_KEY_6 = new KeyPros() { ScanCode = 7, KeyValue = 54, KeyCode = "D6", VarName = nameof(SC_KEY_6) };
            public static KeyPros SC_KEY_7 = new KeyPros() { ScanCode = 8, KeyValue = 55, KeyCode = "D7", VarName = nameof(SC_KEY_7) };
            public static KeyPros SC_KEY_8 = new KeyPros() { ScanCode = 9, KeyValue = 56, KeyCode = "D8", VarName = nameof(SC_KEY_8) };
            public static KeyPros SC_KEY_9 = new KeyPros() { ScanCode = 10, KeyValue = 57, KeyCode = "D9", VarName = nameof(SC_KEY_9) };
            public static KeyPros SC_KEY_0 = new KeyPros() { ScanCode = 11, KeyValue = 48, KeyCode = "D0", VarName = nameof(SC_KEY_0) };
            public static KeyPros SC_KEY_NEG = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NEG) };
            public static KeyPros SC_KEY_EQUATION = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_EQUATION) };
            public static KeyPros SC_KEY_BACKSPACE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_BACKSPACE) };
            public static KeyPros SC_KEY_TAB = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_TAB) };
            public static KeyPros SC_KEY_L_BACKETS = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_BACKETS) };
            public static KeyPros SC_KEY_R_BACKETS = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_BACKETS) };
            public static KeyPros SC_KEY_BACKSLASH = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_BACKSLASH) };
            public static KeyPros SC_KEY_CAP = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_CAP) };
            public static KeyPros SC_KEY_SEMICOLON = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_SEMICOLON) };
            public static KeyPros SC_KEY_APOSTROPHE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_APOSTROPHE) };
            public static KeyPros SC_KEY_ENTER = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_ENTER) };
            public static KeyPros SC_KEY_L_SHIFT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_SHIFT) };
            public static KeyPros SC_KEY_COMMA = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_COMMA) };
            public static KeyPros SC_KEY_DOT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DOT) };
            public static KeyPros SC_KEY_SLASH = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_SLASH) };
            public static KeyPros SC_KEY_R_SHIFT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_SHIFT) };
            public static KeyPros SC_KEY_L_CTRL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_CTRL) };
            public static KeyPros SC_KEY_L_WIN = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_WIN) };
            public static KeyPros SC_KEY_L_ALT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_ALT) };
            public static KeyPros SC_KEY_SPACE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_SPACE) };
            public static KeyPros SC_KEY_R_ALT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_ALT) };
            public static KeyPros SC_KEY_R_WIN = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_WIN) };
            public static KeyPros SC_KEY_R_CTRL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_CTRL) };
            public static KeyPros SC_KEY_PRINT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PRINT) };
            public static KeyPros SC_KEY_SCROLL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_SCROLL) };
            public static KeyPros SC_KEY_PAUSE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PAUSE) };
            public static KeyPros SC_KEY_INSERT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_INSERT) };
            public static KeyPros SC_KEY_HOME = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_HOME) };
            public static KeyPros SC_KEY_PGUP = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PGUP) };
            public static KeyPros SC_KEY_DEL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DEL) };
            public static KeyPros SC_KEY_END = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_END) };
            public static KeyPros SC_KEY_PGDN = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PGDN) };
            public static KeyPros SC_KEY_UP_ARROW = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_L_ARROW) };
            public static KeyPros SC_KEY_L_ARROW = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_A) };
            public static KeyPros SC_KEY_DN_ARROW = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DN_ARROW) };
            public static KeyPros SC_KEY_R_ARROW = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_R_ARROW) };
            public static KeyPros SC_KEY_NUM_LOCK = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_LOCK) };
            public static KeyPros SC_KEY_NUM_DIV = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_DIV) };
            public static KeyPros SC_KEY_NUM_STAR = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_STAR) };
            public static KeyPros SC_KEY_NUM_NEG = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_NEG) };
            public static KeyPros SC_KEY_NUM_7 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_7) };
            public static KeyPros SC_KEY_NUM_8 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_8) };
            public static KeyPros SC_KEY_NUM_9 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_9) };
            public static KeyPros SC_KEY_NUM_4 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_4) };
            public static KeyPros SC_KEY_NUM_5 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_5) };
            public static KeyPros SC_KEY_NUM_6 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_6) };
            public static KeyPros SC_KEY_NUM_PLUS = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_PLUS) };
            public static KeyPros SC_KEY_NUM_1 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_1) };
            public static KeyPros SC_KEY_NUM_2 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_2) };
            public static KeyPros SC_KEY_NUM_3 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_3) };
            public static KeyPros SC_KEY_NUM_0 = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_0) };
            public static KeyPros SC_KEY_NUM_DOT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_DOT) };
            public static KeyPros SC_KEY_NUM_ENTER = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NUM_ENTER) };
            public static KeyPros SC_KEY_PLAY_PAUSE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PLAY_PAUSE) };
            public static KeyPros SC_KEY_STOP = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_STOP) };
            public static KeyPros SC_KEY_NEXT_TRACK = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_NEXT_TRACK) };
            public static KeyPros SC_KEY_PRE_TRACK = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PRE_TRACK) };
            public static KeyPros SC_KEY_VOL_INC = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_VOL_INC) };
            public static KeyPros SC_KEY_VOL_DEC = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_VOL_DEC) };
            public static KeyPros SC_KEY_MUTE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_MUTE) };
            public static KeyPros SC_KEY_MEDIA_SEL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_MEDIA_SEL) };
            public static KeyPros SC_KEY_MAIL = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_MAIL) };
            public static KeyPros SC_KEY_CALCULATOR = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_CALCULATOR) };
            public static KeyPros SC_KEY_MYCOMPUTER = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_MYCOMPUTER) };
            public static KeyPros SC_KEY_W3SEARCH = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3SEARCH) };
            public static KeyPros SC_KEY_W3HOME = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3HOME) };
            public static KeyPros SC_KEY_W3BACK = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3BACK) };
            public static KeyPros SC_KEY_W3FORWARD = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3FORWARD) };
            public static KeyPros SC_KEY_W3STOP = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3STOP) };
            public static KeyPros SC_KEY_W3REFRESH = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_W3REFRESH) };
            public static KeyPros SC_KEY_FAVORITE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_FAVORITE) };
            public static KeyPros SC_KEY_XY_ONOFF = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_XY_ONOFF) };
            public static KeyPros SC_KEY_PROFILE_NEXT_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PROFILE_NEXT_CYCLE) };
            public static KeyPros SC_KEY_PROFILE_PREV_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_PROFILE_PREV_CYCLE) };
            public static KeyPros SC_KEY_CHANGE_LED_MODE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_CHANGE_LED_MODE) };
            public static KeyPros SC_KEY_CHANGE_LED_COLOR = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_CHANGE_LED_COLOR) };
            public static KeyPros SC_KEY_DPI_NEXT = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DPI_NEXT) };
            public static KeyPros SC_KEY_DPI_PREV = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DPI_PREV) };
            public static KeyPros SC_KEY_DPI_NEXT_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DPI_NEXT_CYCLE) };
            public static KeyPros SC_KEY_DPI_PREV_CYCLE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_DPI_PREV_CYCLE) };
            public static KeyPros SC_KEY_POWER_MODE_DISABLE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_POWER_MODE_DISABLE) };
            public static KeyPros SC_KEY_POWER_MODE_WORKING = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_POWER_MODE_WORKING) };
            public static KeyPros SC_KEY_POWER_MODE_PERFORMANCE = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_POWER_MODE_PERFORMANCE) };
            public static KeyPros SC_KEY_POWER_MODE_GAMING = new KeyPros() { ScanCode = -10, KeyValue = 65, KeyCode = "A", VarName = nameof(SC_KEY_POWER_MODE_GAMING) };
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
        public static Keys GetScanCode(Keys key)
        {
            return key;
        }
        public static string GetKeyVar(Keys key)
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
