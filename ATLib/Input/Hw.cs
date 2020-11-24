using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Util;
using CommonLib.Util.IO;

namespace ATLib.Input
{
    public static class Hw
    {
        private static readonly IDictionary<string, IEnumerable<string>> LocDic = new Dictionary<string, IEnumerable<string>>();
        //public static string CurrentLanguage = Language.EN
        private static readonly IDictionary<string, int> LanguageIndexDic = new Dictionary<string, int>
            {
                {"English", 0}, {"中文（简体）", 1}, {"繁體中文", 2}, {"Français", 3}, {"Deutsch", 4}, {"Italiano", 5}, {"日本語", 6}, {"Korean", 7}, {"Malay", 8},
                {"Português (Portugal)", 9}, {"Русский", 10}, {"Español", 11}, {"Thai", 12}, {"Türkçe", 13}, {"Vietnamese", 14}
            };
        
        public class Language
        {
            public Tuple<int, string, string> English = new Tuple<int, string, string>(0, "English", "English");
            public Tuple<int, string, string> ChineseSimplified = new Tuple<int, string, string>(1, "中文（简体", "ChineseSimplified");
            public Tuple<int, string, string> ChineseTraditional = new Tuple<int, string, string>(2, "繁體中文", "ChineseTraditional");
            public Tuple<int, string, string> French = new Tuple<int, string, string>(3, "Français", "French");
            public Tuple<int, string, string> German = new Tuple<int, string, string>(4, "Deutsch", "German");
            public Tuple<int, string, string> Italian = new Tuple<int, string, string>(5, "Italiano", "Italian");
            public Tuple<int, string, string> Japanese = new Tuple<int, string, string>(6, "日本語", "Japanese");
            public Tuple<int, string, string> Korean = new Tuple<int, string, string>(7, "Korean", "Korean");
            public Tuple<int, string, string> Malay = new Tuple<int, string, string>(8, "Malay", "Malay");
            public Tuple<int, string, string> Portuguese = new Tuple<int, string, string>(9, "Português (Portugal)", "Portuguese");
            public Tuple<int, string, string> Russian = new Tuple<int, string, string>(10, "Русский", "Russian");
            public Tuple<int, string, string> Spanish = new Tuple<int, string, string>(11, "Español", "Spanish");
            public Tuple<int, string, string> Thai = new Tuple<int, string, string>(12, "Thai", "Thai");
            public Tuple<int, string, string> Turkish = new Tuple<int, string, string>(13, "Türkçe", "Turkish");
            public Tuple<int, string, string> Vietnamese = new Tuple<int, string, string>(14, "Vietnamese", "Vietnamese");
            //ZH_CN,
            //ZH_TW,
            //FR,
            //DE,
            //IT,
            //JA,
            //KO,
            //MS,
            //PT,
            //RU,
            //ES,
            //TH,
            //TR,
            //VI
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

        public enum Keys
        {
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
            SC_KEY_NO = -10,
            SC_KEY_ESC = -10,
            SC_KEY_F1 = -10,
            SC_KEY_F2 = -10,
            SC_KEY_F3 = -10,
            SC_KEY_F4 = -10,
            SC_KEY_F5 = -10,
            SC_KEY_F6 = -10,
            SC_KEY_F7 = -10,
            SC_KEY_F8 = -10,
            SC_KEY_F9 = -10,
            SC_KEY_F10 = -10,
            SC_KEY_F11 = -10,
            SC_KEY_F12 = -10,
            SC_KEY_TILDE = -10,
            SC_KEY_1 = -10,
            SC_KEY_2 = -10,
            SC_KEY_3 = -10,
            SC_KEY_4 = -10,
            SC_KEY_5 = -10,
            SC_KEY_6 = -10,
            SC_KEY_7 = -10,
            SC_KEY_8 = -10,
            SC_KEY_9 = -10,
            SC_KEY_0 = -10,
            SC_KEY_NEG = -10,
            SC_KEY_EQUATION = -10,
            SC_KEY_BACKSPACE = -10,
            SC_KEY_TAB = -10,
            SC_KEY_L_BACKETS = -10,
            SC_KEY_R_BACKETS = -10,
            SC_KEY_BACKSLASH = -10,
            SC_KEY_CAP = -10,
            SC_KEY_SEMICOLON = -10,
            SC_KEY_APOSTROPHE = -10,
            SC_KEY_ENTER = -10,
            SC_KEY_L_SHIFT = -10,
            SC_KEY_COMMA = -10,
            SC_KEY_DOT = -10,
            SC_KEY_SLASH = -10,
            SC_KEY_R_SHIFT = -10,
            SC_KEY_L_CTRL = -10,
            SC_KEY_L_WIN = -10,
            SC_KEY_L_ALT = -10,
            SC_KEY_SPACE = -10,
            SC_KEY_R_ALT = -10,
            SC_KEY_R_WIN = -10,
            SC_KEY_R_CTRL = -10,
            SC_KEY_PRINT = -10,
            SC_KEY_SCROLL = -10,
            SC_KEY_PAUSE = -10,
            SC_KEY_INSERT = -10,
            SC_KEY_HOME = -10,
            SC_KEY_PGUP = -10,
            SC_KEY_DEL = -10,
            SC_KEY_END = -10,
            SC_KEY_PGDN = -10,
            SC_KEY_UP_ARROW = -10,
            SC_KEY_L_ARROW = -10,
            SC_KEY_DN_ARROW = -10,
            SC_KEY_R_ARROW = -10,
            SC_KEY_NUM_LOCK = -10,
            SC_KEY_NUM_DIV = -10,
            SC_KEY_NUM_STAR = -10,
            SC_KEY_NUM_NEG = -10,
            SC_KEY_NUM_7 = -10,
            SC_KEY_NUM_8 = -10,
            SC_KEY_NUM_9 = -10,
            SC_KEY_NUM_4 = -10,
            SC_KEY_NUM_5 = -10,
            SC_KEY_NUM_6 = -10,
            SC_KEY_NUM_PLUS = -10,
            SC_KEY_NUM_1 = -10,
            SC_KEY_NUM_2 = -10,
            SC_KEY_NUM_3 = -10,
            SC_KEY_NUM_0 = -10,
            SC_KEY_NUM_DOT = -10,
            SC_KEY_NUM_ENTER = -10,
            SC_KEY_PLAY_PAUSE = -10,
            SC_KEY_STOP = -10,
            SC_KEY_NEXT_TRACK = -10,
            SC_KEY_PRE_TRACK = -10,
            SC_KEY_VOL_INC = -10,
            SC_KEY_VOL_DEC = -10,
            SC_KEY_MUTE = -10,
            SC_KEY_MEDIA_SEL = -10,
            SC_KEY_MAIL = -10,
            SC_KEY_CALCULATOR = -10,
            SC_KEY_MYCOMPUTER = -10,
            SC_KEY_W3SEARCH = -10,
            SC_KEY_W3HOME = -10,
            SC_KEY_W3BACK = -10,
            SC_KEY_W3FORWARD = -10,
            SC_KEY_W3STOP = -10,
            SC_KEY_W3REFRESH = -10,
            SC_KEY_FAVORITE = -10,
            SC_KEY_XY_ONOFF = -10,
            SC_KEY_PROFILE_NEXT_CYCLE = -10,
            SC_KEY_PROFILE_PREV_CYCLE = -10,
            SC_KEY_CHANGE_LED_MODE = -10,
            SC_KEY_CHANGE_LED_COLOR = -10,
            SC_KEY_DPI_NEXT = -10,
            SC_KEY_DPI_PREV = -10,
            SC_KEY_DPI_NEXT_CYCLE = -10,
            SC_KEY_DPI_PREV_CYCLE = -10,
            SC_KEY_POWER_MODE_DISABLE = -10,
            SC_KEY_POWER_MODE_WORKING = -10,
            SC_KEY_POWER_MODE_PERFORMANCE = -10,
            SC_KEY_POWER_MODE_GAMING = -10
        }
        public static void GetKeyboardKeys(string filePath)
        {
            var keyboardKeyLines = UtilFile.GetListByLine(filePath);
            foreach (var line in keyboardKeyLines)
            {
                if (!line.Contains("SC_KEY_")) continue;
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
            return UtilEnum.GetEnumNameByValue<Keys>(key);
        }
        //public static string GetKeyValue(Keys key, Language language = Language.EN)
        //{
        //    return LocDic[GetKeyVar(key)].ElementAt((int)language);
        //}
    }
}
