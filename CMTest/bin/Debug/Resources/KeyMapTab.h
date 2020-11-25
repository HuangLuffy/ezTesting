/**
 * @file : KeyMapTab.h
 *
 * @brief : controlpad keyboard text map
 *
 * @date : 2019-10-11 15:31
 *
 * @author : min.yang
 *
 */

#ifndef __INCLUDE_KEYMAPTAB_H
#define __INCLUDE_KEYMAPTAB_H
#include <vector>
#include <QString>
#include "app/CMSettings.h"
#pragma execution_character_set("utf-8")    //solve garbled characters

using namespace Settings;

namespace CMData {

namespace KeyMapTab {

enum ScanCode
{
    SC_KEY_MACRO = 555,
    SC_KEY_DEFAULT = 666,
    SC_KEY_UNKNOWN = 1024,  //key not in table
    SC_KEY_NO = 0,  //disable
    SC_KEY_F1 = 59,
    SC_KEY_F2,
    SC_KEY_F3,
    SC_KEY_F4,
    SC_KEY_F5,
    SC_KEY_F6,
    SC_KEY_F7,
    SC_KEY_F8,
    SC_KEY_F9,
    SC_KEY_F10,
    SC_KEY_F11 = 87,
    SC_KEY_F12,
    SC_KEY_F13 = 777,
    SC_KEY_F14,
    SC_KEY_F15,
    SC_KEY_F16,
    SC_KEY_F17,
    SC_KEY_F18,
    SC_KEY_F19,
    SC_KEY_F20,
    SC_KEY_F21,
    SC_KEY_F22,
    SC_KEY_F23,
    SC_KEY_F24,
    SC_KEY_ESC = 1,
    SC_KEY_1,
    SC_KEY_2,
    SC_KEY_3,
    SC_KEY_4,
    SC_KEY_5,
    SC_KEY_6,
    SC_KEY_7,
    SC_KEY_8,
    SC_KEY_9,
    SC_KEY_0,
    SC_KEY_NEG,         //-_
    SC_KEY_EQUATION,    //=+
    SC_KEY_BACKSPACE,
    SC_KEY_TAB,
    SC_KEY_Q,
    SC_KEY_W,
    SC_KEY_E,
    SC_KEY_R,
    SC_KEY_T,
    SC_KEY_Y,
    SC_KEY_U,
    SC_KEY_I,
    SC_KEY_O,
    SC_KEY_P,
    SC_KEY_L_BACKETS,   //[{
    SC_KEY_R_BACKETS,   //]}
    SC_KEY_CAP = 58,
    SC_KEY_A = 30,
    SC_KEY_S,
    SC_KEY_D,
    SC_KEY_F,
    SC_KEY_G,
    SC_KEY_H,
    SC_KEY_J,
    SC_KEY_K,
    SC_KEY_L,
    SC_KEY_SEMICOLON,   //;:
    SC_KEY_APOSTROPHE,  //'"
    SC_KEY_ENTER = 28,
    SC_KEY_TILDE = 41,  //`~
    SC_KEY_L_SHIFT,
    SC_KEY_BACKSLASH,   //\|
    SC_KEY_Z,
    SC_KEY_X,
    SC_KEY_C,
    SC_KEY_V,
    SC_KEY_B,
    SC_KEY_N,
    SC_KEY_M,
    SC_KEY_COMMA,       //,<
    SC_KEY_DOT,         //.>
    SC_KEY_SLASH,       ///?
    SC_KEY_R_SHIFT,
    SC_KEY_L_CTRL = 29,
    SC_KEY_L_WIN = 347,
    SC_KEY_L_ALT = 56,
    SC_KEY_SPACE,
    SC_KEY_R_ALT = 312,
    SC_KEY_R_WIN = 348,
    SC_KEY_R_CTRL = 285,
    SC_KEY_PRINT = 84,
    SC_KEY_SCROLL = 70,
    SC_KEY_PAUSE = 69,
    SC_KEY_INSERT = 338,
    SC_KEY_HOME = 327,
    SC_KEY_PGUP = 329,
    SC_KEY_DEL = 339,
    SC_KEY_END = 335,
    SC_KEY_PGDN = 337,
    SC_KEY_UP_ARROW = 328,
    SC_KEY_L_ARROW = 331,
    SC_KEY_DN_ARROW = 336,
    SC_KEY_R_ARROW = 333,
    SC_KEY_NUM_LOCK = 325,
    SC_KEY_NUM_DIV = 309,
    SC_KEY_NUM_STAR = 55,
    SC_KEY_NUM_7 = 71,
    SC_KEY_NUM_8,
    SC_KEY_NUM_9,
    SC_KEY_NUM_NEG,
    SC_KEY_NUM_4,
    SC_KEY_NUM_5,
    SC_KEY_NUM_6,
    SC_KEY_NUM_PLUS,
    SC_KEY_NUM_1,
    SC_KEY_NUM_2,
    SC_KEY_NUM_3,
    SC_KEY_NUM_0,
    SC_KEY_NUM_DOT,
    SC_KEY_NUM_ENTER = 284,
    //half button
    SC_KEY_CODE42 = SC_KEY_BACKSLASH,   //left of iso enter
    SC_LSHIFT_RIGHT_HALF = 86,
    SC_RSHIFT_LEFT_HALF = 115,
    SC_BACK_LEFT_HALF = 125,
    SC_SPACE_LEFT_HALF = 123,
    SC_SPACE_RIGHT_HALF_1 = 121,
    SC_SPACE_RIGHT_HALF_2 = 112,
    SC_NUM_PLUS_DOWN_HALF = 126,

    //mouse key
    SC_MOUSE_LEFT = 700,
    SC_MOUSE_RIGHT,
    SC_MOUSE_WHEEL,
    SC_MOUSE_SCROLL_UP,
    SC_MOUSE_SCROLL_DN,
    SC_MOUSE_MIDDLE,
    SC_MOUSE_BROWSER_FORWARD,
    SC_MOUSE_BROWSER_BACKWARD,
    SC_FN_MOUSE_LEF,
    SC_FN_MOUSE_RIGHT,
    SC_FN_MOUSE_WHEEL,
    SC_FN_MOUSE_LEFT_SIDE_TOP,
    SC_FN_MOUSE_LEFT_SIDE_BOTTOM,

    //media key
    SC_KEY_PLAY_PAUSE = 800,
    SC_KEY_STOP,
    SC_KEY_NEXT_TRACK,
    SC_KEY_PRE_TRACK,
    SC_KEY_VOL_DEC,
    SC_KEY_VOL_INC,
    SC_KEY_MUTE,
    SC_KEY_MEDIA_SEL,

    SC_KEY_MAIL = 810,
    SC_KEY_CALCULATOR,
    SC_KEY_MYCOMPUTER,
    SC_KEY_W3SEARCH,
    SC_KEY_W3HOME,
    SC_KEY_W3BACK,
    SC_KEY_W3FORWARD,
    SC_KEY_W3STOP,
    SC_KEY_W3REFRESH,
    SC_KEY_FAVORITE,

    //profile
    SC_KEY_PROFILE_0 = 820,
    SC_KEY_PROFILE_1,
    SC_KEY_PROFILE_2,
    SC_KEY_PROFILE_3,
    SC_KEY_PROFILE_4,
    SC_KEY_PROFILE_5,
    SC_KEY_PROFILE_6,
    SC_KEY_PROFILE_7,
    SC_KEY_PROFILE_8,
    SC_KEY_PROFILE_9,
    SC_KEY_PROFILE_10,
    SC_KEY_PROFILE_11,
    SC_KEY_PROFILE_12,
    SC_KEY_PROFILE_13,
    SC_KEY_PROFILE_14,
    SC_KEY_PROFILE_15,
    SC_KEY_PROFILE_16,
    SC_KEY_PROFILE_17,
    SC_KEY_PROFILE_18,
    SC_KEY_PROFILE_19,
    SC_KEY_PROFILE_20,
    SC_KEY_PROFILE_21,
    SC_KEY_PROFILE_22,
    SC_KEY_PROFILE_23,
    SC_KEY_PROFILE_NEXT_CYCLE,
    SC_KEY_PROFILE_PREV_CYCLE,
    SC_KEY_CHANGE_LED_MODE,
    SC_KEY_CHANGE_LED_COLOR,

    //DPI
    SC_KEY_DPI_SET_1 = 850,
    SC_KEY_DPI_SET_2,
    SC_KEY_DPI_SET_3,
    SC_KEY_DPI_SET_4,
    SC_KEY_DPI_SET_5,
    SC_KEY_DPI_SET_6,
    SC_KEY_DPI_SET_7,
    SC_KEY_DPI_NEXT,
    SC_KEY_DPI_PREV,
    SC_KEY_DPI_NEXT_CYCLE,
    SC_KEY_DPI_PREV_CYCLE,
    SC_KEY_XY_ONOFF,

    //LED BORDER
    SC_KEY_BORDER_TOP_01 = 900,
    SC_KEY_BORDER_TOP_02,
    SC_KEY_BORDER_TOP_03,
    SC_KEY_BORDER_TOP_04,
    SC_KEY_BORDER_TOP_05,
    SC_KEY_BORDER_TOP_06,
    SC_KEY_BORDER_TOP_07,
    SC_KEY_BORDER_TOP_08,
    SC_KEY_BORDER_TOP_09,
    SC_KEY_BORDER_TOP_10,
    SC_KEY_BORDER_TOP_11,
    SC_KEY_BORDER_TOP_12,
    SC_KEY_BORDER_TOP_13,
    SC_KEY_BORDER_TOP_14,
    SC_KEY_BORDER_TOP_15,
    SC_KEY_BORDER_TOP_16,
    SC_KEY_BORDER_TOP_17,
    SC_KEY_BORDER_TOP_18,
    SC_KEY_BORDER_TOP_19,
    SC_KEY_BORDER_TOP_20,

    SC_KEY_BORDER_RIGHT_01,
    SC_KEY_BORDER_RIGHT_02,
    SC_KEY_BORDER_RIGHT_03,
    SC_KEY_BORDER_RIGHT_04,
    SC_KEY_BORDER_RIGHT_05,
    SC_KEY_BORDER_RIGHT_06,
    SC_KEY_BORDER_RIGHT_07,
    SC_KEY_BORDER_RIGHT_08,
    SC_KEY_BORDER_RIGHT_09,
    SC_KEY_BORDER_RIGHT_10,

    SC_KEY_BORDER_BOTTOM_01,
    SC_KEY_BORDER_BOTTOM_02,
    SC_KEY_BORDER_BOTTOM_03,
    SC_KEY_BORDER_BOTTOM_04,
    SC_KEY_BORDER_BOTTOM_05,
    SC_KEY_BORDER_BOTTOM_06,
    SC_KEY_BORDER_BOTTOM_07,
    SC_KEY_BORDER_BOTTOM_08,
    SC_KEY_BORDER_BOTTOM_09,
    SC_KEY_BORDER_BOTTOM_10,
    SC_KEY_BORDER_BOTTOM_11,
    SC_KEY_BORDER_BOTTOM_12,
    SC_KEY_BORDER_BOTTOM_13,
    SC_KEY_BORDER_BOTTOM_14,
    SC_KEY_BORDER_BOTTOM_15,
    SC_KEY_BORDER_BOTTOM_16,
    SC_KEY_BORDER_BOTTOM_17,
    SC_KEY_BORDER_BOTTOM_18,
    SC_KEY_BORDER_BOTTOM_19,
    SC_KEY_BORDER_BOTTOM_20,

    SC_KEY_BORDER_LEFT_01,
    SC_KEY_BORDER_LEFT_02,
    SC_KEY_BORDER_LEFT_03,
    SC_KEY_BORDER_LEFT_04,
    SC_KEY_BORDER_LEFT_05,
    SC_KEY_BORDER_LEFT_06,
    SC_KEY_BORDER_LEFT_07,
    SC_KEY_BORDER_LEFT_08,
    SC_KEY_BORDER_LEFT_09,
    SC_KEY_BORDER_LEFT_10,

    //Power Mode preset hotkey
    SC_KEY_POWER_MODE_DISABLE = 1025,   //LOGO + D
    SC_KEY_POWER_MODE_WORKING,          //LOGO + W
    SC_KEY_POWER_MODE_PERFORMANCE,      //LOGO + P
    SC_KEY_POWER_MODE_GAMING,           //LOGO + G
};

//scan code : text
static std::map< int, std::vector< const char* > > KeyNameMap = {
                    //EN        ZH_CN       ZH_TW      FR         DE        IT          JA          KO        MS         PT         RU         ES         TH        TR          VI
    { SC_KEY_NO, {"DISABLE", "停用", "停用", "DÉSACTIVER", "DEAKTIVIEREN", "DISABILITA", "無効にする", "사용 안 함", "LUMPUHKAN", "DESATIVAR", "ОТКЛЮЧИТЬ", "DESHABILITAR", "ปิดใช้งาน", "DEVRE DIŞI BIRAK", "TẮT" } },
    { SC_KEY_ESC, { "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
    { SC_KEY_F1, { "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1", "F1" } },
    { SC_KEY_F2, { "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2", "F2" } },
    { SC_KEY_F3, { "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3", "F3" } },
    { SC_KEY_F4, { "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4", "F4" } },
    { SC_KEY_F5, { "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5", "F5" } },
    { SC_KEY_F6, { "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6", "F6" } },
    { SC_KEY_F7, { "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7", "F7" } },
    { SC_KEY_F8, { "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8", "F8" } },
    { SC_KEY_F9, { "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9", "F9" } },
    { SC_KEY_F10, { "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10", "F10" } },
    { SC_KEY_F11, { "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11", "F11" } },
    { SC_KEY_F12, { "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12", "F12" } },
    { SC_KEY_TILDE, { "`\n~", "`\n~", "`\n~", "\n²", "^\n°", "\\\n|", "E/J\n漢字", "`\n~", "`\n~", "\\\n¦", "~\n`    Ё", "ª\nº    \\", "`\n~", "\"\né", "`\n~" } },
    { SC_KEY_1, { "1\n!", "1\n!", "1\n!", "1\n&&", "1\n!", "1\n!", "!\n1", "1\n!", "1\n!", "1\n!", "!\n1", "1    |\n!", "!    ๅ\n1    +", "1\n!", "1\n!" } },
    { SC_KEY_2, { "2\n@", "2\n@", "2\n@", "2\né    ~", "2\n\"", "2\n\"", "\"\n2", "2\n@", "2\n@", "2    @\n\"", "@\n2    \"", "2    @\n\"", "@    ๑\n2    /", "2    £\n’", "2\n@" } },
    { SC_KEY_3, { "3\n#", "3\n#", "3\n#", "3\n\"    #", "3\n§", "3\n£", "#\n3", "3\n#", "3\n#", "3    £\n#", "#\n3    №", "3    #\n.", "#    ๒\n3    -", "3    #\n^", "3\n#" } },
    { SC_KEY_4, { "4\n$", "4\n$", "4\n$", "4\n'    {", "4\n$", "4\n$", "$\n4", "4\n$", "4\n$", "4    §\n$", "$\n4    ;", "4    ~\n$", "$    ๓\n4    ภ", "4    $\n+", "4\n$" } },
    { SC_KEY_5, { "5\n%", "5\n%", "5\n%", "5\n(    [", "5\n%", "5    €\n%", "%\n5", "5\n%", "5    €\n%", "5\n%", "%\n5", "5    €\n%", "%    ๔\n5    ถ", "5    ½\n%", "5\n%" } },
    { SC_KEY_6, { "6\n^", "6\n^", "6\n^", "6\n-    ¦", "6\n&&", "6\n&&", "&&\n6", "6\n^", "6\n^", "6\n&&", "^\n6    :", "6    ¬\n&&", "^    ู\n6    ุ", "6\n&&", "6\n^" } },
    { SC_KEY_7, { "7\n&&", "7\n&&", "7\n&&", "7\nè    `", "7    {\n/", "7\n/", "’\n7", "7\n&&", "7\n&&", "7    {\n/", "&&\n7", "7\n/", "&&    ฿\n7    ึ", "7    {\n/", "7\n&&" } },
    { SC_KEY_8, { "8\n*", "8\n*", "8\n*", "8\n_    \\", "8    [\n(", "8\n(", "(\n8", "8\n*", "8\n*", "8    [\n(", "*\n8", "8\n(", "*    ๕\n8    ค", "8    [\n(", "8\n*" } },
    { SC_KEY_9, { "9\n(", "9\n(", "9\n(", "9\nÇ    ^", "9    ]\n)", "9\n)", ")\n9", "9\n(", "9\n(", "9    ]\n)", "(\n9", "9\n)", "(    ๖\n9    ต", "9    ]\n)", "9\n(" } },
    { SC_KEY_0, { "0\n)", "0\n)", "0\n)", "0\nà    @", "0    }\n=", "0\n=", "\n0", "0\n)", "0\n)", "0    }\n=", ")\n0", "0\n=", ")    ๗\n0    จ", "0    }\n=", "0\n)" } },
    { SC_KEY_NEG, { "-\n_", "-\n_", "-\n_", "°\n)    ]", "ß    \\\n?", "’\n?", "=\n-", "-\n_", "-\n_", "?\n'", "_\n-", "?\n'", "_    ๘\n-    ข", "*    \\\n?", "-\n_" } },
    { SC_KEY_EQUATION, { "=\n+", "=\n+", "=\n+", "+\n=    }", "`\n´", "ì\n^", "~\n^", "=\n+", "=\n+", "»\n«", "+\n=", "¿\n¡", "+    ๙\n=    ช", "-\n_", "=\n+" } },
    { SC_KEY_BACKSPACE, { "BACKSPACE", "BACKSPACE", "BACKSPACE", "SUPPR\nARRIÈRE", "BACKSPACE", "BACKSPACE", "BACK\nSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE", "BACKSPACE" } },
    { SC_KEY_TAB, { "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB", "TAB" } },
    { SC_KEY_Q, { "Q", "Q", "Q", "A", "Q\n@", "Q", "Q", "Q\nㅃ    ㅂ", "Q", "Q", "Q\nЙ", "Q", "Q    ๐\nๆ", "Q\n@", "Q" } },
    { SC_KEY_W, { "W", "W", "W", "Z", "W", "W", "W", "W\nㅉ    ㅈ", "W", "W", "W\nЦ", "W", "W    \"\nไ", "W", "W" } },
    { SC_KEY_E, { "E", "E", "E", "E\n€", "E\n€", "E\n€", "E", "E\nㄸ    ㄷ", "E", "E\n€", "E\nУ", "E", "E    ฎ\nำ", "E\n€", "E" } },
    { SC_KEY_R, { "R", "R", "R", "R", "R", "R", "R", "R\nㄲ    ㄱ", "R", "R", "R\nК", "R", "R    ฑ\nพ", "R", "R" } },
    { SC_KEY_T, { "T", "T", "T", "T", "T", "T", "T", "T\nㅆ    ㅅ", "T", "T", "T\nЕ", "T", "T    ธ\nะ", "T", "T" } },
    { SC_KEY_Y, { "Y", "Y", "Y", "Y", "Z", "Y", "Y", "Y\nㅛ", "Y", "Y", "Y\nН", "Y", "Y    ํ\nั", "Y", "Y" } },
    { SC_KEY_U, { "U", "U", "U", "U", "U", "U", "U", "U\nㅕ", "U", "U", "U\nГ", "U", "U    ๊\n  ี", "U", "U" } },
    { SC_KEY_I, { "I", "I", "I", "I", "I", "I", "I", "I\nㅑ", "I", "I", "I\nШ", "I", "I    ณ\nร", "I", "I" } },
    { SC_KEY_O, { "O", "O", "O", "O", "O", "O", "O", "O\nㅒ    ㅐ", "O", "O", "O\nЩ", "O", "O    ฯ\nน", "O", "O" } },
    { SC_KEY_P, { "P", "P", "P", "P", "P", "P", "P", "P\nㅖ    ㅔ", "P", "P", "P\nЗ", "P", "P    ญ\nย", "P", "P" } },
    { SC_KEY_L_BACKETS, { "{\n[", "{\n[", "{\n[", "¨\n^", "Ü", "é    {\nè    [", "`\n@", "{\n[", "{\n[", "*\n+    ¨", "{\n[    Х", "^\n`    [", "{    ฐ\n[    บ", "Ğ", "{\n[" } },
    { SC_KEY_R_BACKETS, { "}\n]", "}\n]", "}\n]", "£\n$    ¤", "*\n+    ~", "*    }\n+    ]", "{\n[", "}\n]", "}\n]", "`\n´", "}\n]    Ъ", "*\n+    ]", "}    ,\n]    ล", "Ü\n~", "}\n]" } },
    { SC_KEY_BACKSLASH, { "|\n\\", "|\n\\", "|\n\\", "µ\n*", "'\n#", "§\nù", "}\n]", "|\n₩", "|\n\\", "^\n~", "|\n\\    /", "ç\n}", "|    ฅ\n\\    ฃ", "", "|\n\\" } },
    { SC_KEY_CAP, { "CAPSLK", "CAPSLK", "CAPSLK", "VERR\nMAJ", "CAPSLK", "MAIUSC", "CAPSLK\n英数", "CAPSLK", "CAPSLK", "CAPSLK", "CAPSLK", "CAPSLK", "CAPSLK", "CAPSLK", "CAPSLK" } },
    { SC_KEY_A, { "A", "A", "A", "Q", "A", "A", "A", "A\nㅁ", "A", "A", "A\nФ", "A", "A    ฤ\nฟ", "A", "A" } },
    { SC_KEY_S, { "S", "S", "S", "S", "S", "S", "S", "S\nㄴ", "S", "S", "S\nЫ", "S", "S    ฆ\nห", "S", "S" } },
    { SC_KEY_D, { "D", "D", "D", "D", "D", "D", "D", "D\nㅇ", "D", "D", "D\nВ", "D", "D    ฏ\nก", "D", "D" } },
    { SC_KEY_F, { "F", "F", "F", "F", "F", "F", "F", "F\nㄹ", "F", "F", "F\nА", "F", "F    โ\nด", "F", "F" } },
    { SC_KEY_G, { "G", "G", "G", "G", "G", "G", "G", "G\nㅎ", "G", "G", "G\nП", "G", "G    ฌ\/เ", "G", "G" } },
    { SC_KEY_H, { "H", "H", "H", "H", "H", "H", "H", "H\nㅗ", "H", "H", "H\nР", "H", "H    ็\n ้", "H", "H" } },
    { SC_KEY_J, { "J", "J", "J", "J", "J", "J", "J", "J\nㅓ", "J", "J", "J\nО", "J", "J    ๋\n ่", "J", "J" } },
    { SC_KEY_K, { "K", "K", "K", "K", "K", "K", "K", "K\nㅏ", "K", "K", "K\nЛ", "K", "K    ษ\nา", "K", "K" } },
    { SC_KEY_L, { "L", "L", "L", "L", "L", "L", "L", "L\nㅣ", "L", "L", "L\nД", "L", "L    ศ\nส", "L", "L" } },
    { SC_KEY_SEMICOLON, { ":\n;", ":\n;", ":\n;", "M", "Ö", "ç\nò    @", "+\n;", ":\n;", ":\n;", "Ç", ":\n;    Ж", "Ñ", ":    ซ\n;    ว", "Ş\n´", ":\n;" } },
    { SC_KEY_APOSTROPHE, { "\"\n'", "\"\n'", "\"\n'", "%\nÙ", "Ä", "°\nà    #", "*\n:", "\"\n'", "\"\n'", "ª\nº", "\"\n’    Э", "¨\n´    {", "\"\    .\"\n'    ง", "i", "\"\n'" } },
    { SC_KEY_ENTER, { "ENTER", "ENTER", "ENTER", "\nENTRÉE", "\nENTER", "\nENTER", "\nENTER", "ENTER", "ENTER", "\nENTER", "ENTER", "\nENTER", "ENTER", "\nENTER", "ENTER" } },
    { SC_KEY_L_SHIFT, { "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_MAJ", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT", "L_SHIFT" } },
    { SC_KEY_Z, { "Z", "Z", "Z", "W", "Y", "Z", "Z", "Z\nㅋ", "Z", "Z", "Z\nЯ", "Z", "Z    (\nผ", "Z", "Z" } },
    { SC_KEY_X, { "X", "X", "X", "X", "X", "X", "X", "X\nㅌ", "X", "X", "X\nЧ", "X", "X    )\nป", "X", "X" } },
    { SC_KEY_C, { "C", "C", "C", "C", "C", "C", "C", "C\nㅊ", "C", "C", "C\nС", "C", "C    ฉ\nแ", "C", "C" } },
    { SC_KEY_V, { "V", "V", "V", "V", "V", "V", "V", "V\nㅍ", "V", "V", "V\nМ", "V", "V    ฮ\nอ", "V", "V" } },
    { SC_KEY_B, { "B", "B", "B", "B", "B", "B", "B", "B\nㅠ", "B", "B", "B\nИ", "B", "B    ฺ\n  ิ", "B", "B" } },
    { SC_KEY_N, { "N", "N", "N", "N", "N", "N", "N", "N\nㅜ", "N", "N", "N\nТ", "N", "\n", "N", "N" } },
    { SC_KEY_M, { "M", "M", "M", "?\n,", "M\nµ", "M", "M", "M\nㅡ", "M", "M", "M\nЬ", "M", "M    ?\nท", "M", "M" } },
    { SC_KEY_COMMA, { "<\n,", "<\n,", "<\n,", ".\n;", ";\n,", ";\n,", "<\n,", "<\n,", "<\n,", ";\n,", "<\n,    Б", ";\n,", "<    ฒ\n,    ม", "Ö", "<\n," } },
    { SC_KEY_DOT, { ">\n.", ">\n.", ">\n.", "/:", ":\n.", ":\n.", ">\n.", ">\n.", ">\n.", ":\n.", ">\n.    Ю", ":\n.", ">    ฬ\n.    ใ", "Ç", ">\n." } },
    { SC_KEY_SLASH, { "?\n/", "?\n/", "?\n/", "§\n!", "¯\n-", "_\n-", "?\n/", "?\n/", "?\n/", "¯\n_", "?    '\n/    .", "¯\n_", "?    ฦ\n/    ฝ", ":\n.", "?\n/" } },
    { SC_KEY_R_SHIFT, { "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_MAJ", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT", "R_SHIFT" } },
    { SC_KEY_L_CTRL, { "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_STRG", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL", "L_CTRL" } },
    { SC_KEY_L_WIN, { "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN", "L_WIN" } },
    { SC_KEY_L_ALT, { "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT", "L_ALT" } },
    { SC_KEY_SPACE, { "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE", "SPACE" } },
    { SC_KEY_R_ALT, { "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT", "R_ALT" } },
    { SC_KEY_R_WIN, { "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN", "R_WIN" } },
    { SC_KEY_R_CTRL, { "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_STRG", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL", "R_CTRL" } },
    { SC_KEY_PRINT, { "PRT\nSC", "PRT\nSC", "PRT\nSC", "Impr.\nEcran", "DRUCK\nS-ABF", "STAMP", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC", "PRT\nSC" } },
    { SC_KEY_SCROLL, { "SCR\nLK", "SCR\nLK", "SCR\nLK", "Arrêt\nDéfil", "ROLLEN", "BLOC\nSCORR", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK", "SCR\nLK" } },
    { SC_KEY_PAUSE, { "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE\nUNTBR", "PAUSA", "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE", "PAUSE" } },
    { SC_KEY_INSERT, { "INS", "INS", "INS", "INS", "EINFG", "INS", "INS", "INS", "INS", "INS", "INS", "INS", "INS", "INS", "INS" } },
    { SC_KEY_HOME, { "HOME", "HOME", "HOME", "Début", "POS1", "INIZIO", "HOME", "HOME", "HOME", "HOME", "HOME", "HOME", "HOME", "HOME", "HOME" } },
    { SC_KEY_PGUP, { "PGUP", "PGUP", "PGUP", "PG\nHaut", "BILD", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP", "PGUP" } },
    { SC_KEY_DEL, { "DEL", "DEL", "DEL", "Supper", "ENTF", "CANC", "DEL", "DEL", "DEL", "DEL", "DEL", "DEL", "DEL", "DEL", "DEL" } },
    { SC_KEY_END, { "END", "END", "END", "Fin", "ENDE", "FINE", "END", "END", "END", "END", "END", "END", "END", "END", "END" } },
    { SC_KEY_PGDN, { "PGDN", "PGDN", "PGDN", "PG\nBas", "BILD", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN", "PGDN" } },
    { SC_KEY_UP_ARROW, { "^", "^", "^", "^", "^", "^", "^", "^", "^", "^", "^", "^", "^", "^", "^" } },
    { SC_KEY_L_ARROW, { "<", "<", "<", "<", "<", "<", "<", "<", "<", "<", "<", "<", "<", "<", "<" } },
    { SC_KEY_DN_ARROW, { "v", "v", "v", "v", "v", "v", "v", "v", "v", "v", "v", "v", "v", "v", "v" } },
    { SC_KEY_R_ARROW, { ">", ">", ">", ">", ">", ">", ">", ">", ">", ">", ">", ">", ">", ">", ">" } },
    { SC_KEY_NUM_LOCK, { "NUMLK", "NUMLK", "NUMLK", "VERR\nNUM", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK", "NUMLK" } },
    { SC_KEY_NUM_DIV, { "/", "/", "/", "/", "/", "/", "/", "/", "/", "/", "/", "/", "/", "/", "/" } },
    { SC_KEY_NUM_STAR, { "*", "*", "*", "*", "*", "*", "*", "*", "*", "*", "*", "*", "*", "*", "*" } },
    { SC_KEY_NUM_NEG, { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" } },
    { SC_KEY_NUM_7, { "7", "7", "7", "7", "7", "7", "7", "7", "7", "7", "7", "7", "7", "7", "7" } },
    { SC_KEY_NUM_8, { "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8" } },
    { SC_KEY_NUM_9, { "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9" } },
    { SC_KEY_NUM_4, { "4", "4", "4", "4", "4", "4", "4", "4", "4", "4", "4", "4", "4", "4", "4" } },
    { SC_KEY_NUM_5, { "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5" } },
    { SC_KEY_NUM_6, { "6", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6", "6" } },
    { SC_KEY_NUM_PLUS, { "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+", "\n+" } },
    { SC_KEY_NUM_1, { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" } },
    { SC_KEY_NUM_2, { "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2", "2" } },
    { SC_KEY_NUM_3, { "3", "3", "3", "3", "3", "3", "3", "3", "3", "3", "3", "3", "3", "3", "3" } },
    { SC_KEY_NUM_0, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } },
    { SC_KEY_NUM_DOT, { ".", ".", ".", ".", ",", ".", "", ".", ".", ".", ".", ".", ".", ".", "." } },
    { SC_KEY_NUM_ENTER, { "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTRÉE", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER", "NP_ENTER" } },
    { SC_LSHIFT_RIGHT_HALF, { "NULL", "NULL", "NULL", ">\n<", ">\n<    |", ">\n<", "NULL", "ESC", "ESC", ">\n<    \\", "ESC", ">\n<", "ESC", ">\n<    |", "" } },
   { SC_RSHIFT_LEFT_HALF, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "-\n\\", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
   { SC_BACK_LEFT_HALF, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "|\n￥", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
   { SC_SPACE_LEFT_HALF, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "無変換", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
   { SC_SPACE_RIGHT_HALF_1, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "変換", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
   { SC_SPACE_RIGHT_HALF_2, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "カタカナ\nひらがな", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
   { SC_NUM_PLUS_DOWN_HALF, { "NULL", "NULL", "NULL", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC", "ESC" } },
    { SC_KEY_PLAY_PAUSE, {"PLAY/PAUSE", "播放/暂停", "播放/暫停", "LECTURE/PAUSE", "WIEDERGABE/PAUSE", "RIPRODUCI/INTERROMPI", "再生/一時停止", "재생/일시 중지", "MAIN/JEDA", "REPRODUZIR/PAUSA", "ПУСК/ПАУЗА", "REPRODUCIR/PAUSAR", "เล่น/หยุดชั่วคราว", "OYNAT/DURAKLAT", "PHÁT/TẠM DỪNG" } },
    { SC_KEY_STOP, {"STOP", "停止", "停止", "ARRÊTER", "STOPP", "ARRESTA", "停止", "중지", "BERHENTI", "PARAR", "СТОП", "DETENER", "หยุด", "DURDUR", "DỪNG" } },
    { SC_KEY_NEXT_TRACK, {"NEXT TRACK", "下一首", "下一首", "MORCEAU SUIV", "NÄCHSTER TITEL", "TRACCIA SUCCESSIVA", "次の曲", "다음 트랙", "TREK SETERUSNYA", "FAIXA SEGUINTE", "СЛЕДУЮЩИЙ ТРЕК", "PISTA SIGUIENTE", "แทร็กถัดไป", "SONRAKİ PARÇA", "BÀI SAU" } },
    { SC_KEY_PRE_TRACK, {"PREVIOUS TRACK", "上一首", "上一首", "MORCEAU PRÉC", "VORHERIGER TITEL", "TRACCIA PRECEDENTE", "前の曲", "이전 트랙", "TREK SEBELUMNYA", "FAIXA ANTERIOR", "ПРЕДЫДУЩИЙ ТРЕК", "PISTA ANTERIOR", "แทร็กก่อนหน้า", "ÖNCEKİ PARÇA", "BÀI TRƯỚC" } },
    { SC_KEY_VOL_INC, {"VOLUME UP", "增加音量", "音量升高", "VOLUME PLUS", "LAUTSTÄRKE ERHÖHEN", "VOLUME SU", "音量を上げる", "볼륨 증가", "NAIK KELANTANGAN", "AUMENTAR O VOLUME", "УВЕЛИЧИТЬ ГРОМКОСТЬ", "SUBIR VOLUMEN", "เพิ่มระดับเสียง", "SES YÜKSELT", "TĂNG ÂM LƯỢNG" } },
    { SC_KEY_VOL_DEC, {"VOLUME DOWN", "减少音量", "音量降低", "VOLUME MOINS", "LAUTSTÄRKE VERRINGERN", "VOLUME GIÙ", "音量を下げる", "볼륨 감소", "TURUN KELANTANGAN ", "DIMINUIR O VOLUME", "УМЕНЬШИТЬ ГРОМКОСТЬ", "BAJAR VOLUMEN", "ลดระดับเสียง", "SES AZALT", "GIẢM ÂM LƯỢNG" } },
    { SC_KEY_MUTE, {"MUTE", "静音", "靜音", "MUTE", "STUMM", "disattivare", "消音", "음소거", "BIS", "SEM SOM", "ОТКЛ. ЗВУКА", "SILENCIO", "ปิดเสียง", "SESSİZ", "TẮT ÂM" } },
    { SC_KEY_MEDIA_SEL, {"MEDIA PLAYER", "媒体播放器", "媒體播放器", "LECTEUR MULTIMÉDIA", "MEDIENPLAYER", "LETTORE MULTIMEDIALE", "メディアプレーヤー", "미디어 플레이어", "PEMAIN MEDIA", "LEITOR MULTIMÉDIA", "МЕДИАПЛЕЕР", "REPRODUCTOR MULTIMEDIA", "เครื่องเล่นมีเดีย", "MEDYA OYNATICI", "TRÌNH PHÁT MEDIA" } },
    { SC_KEY_MAIL, {"E-MAIL", "电子邮件", "電子郵件", "E-MAIL", "E-MAIL", "E-MAIL", "電子メール", "이메일", "E-MEL", "E-MAIL", "ЭЛ. ПОЧТА", "CORREO ELECTRÓNICO", "อีเมล", "E-POSTA", "EMAIL" } },
    { SC_KEY_CALCULATOR, {"CALCULATOR", "计算器", "計算機", "CALCULATRICE", "RECHNER", "CALCOLATRICE", "計算機", "계산기", "KALKULATOR", "CALCULADORA", "КАЛЬКУЛЯТОР", "CALCULADORA", "เครื่องคิดเลข", "HESAP MAKİNESİ", "MÁY TÍNH" } },
    { SC_KEY_MYCOMPUTER, { } },
    { SC_KEY_W3SEARCH, { } },
    { SC_KEY_W3HOME, {"WEB BROWSER", "网页浏览器", "網頁瀏覽器", "NAVIGATEUR WEB", "WEBBROWSER", "BROWSER", "WEB ブラウザー", "웹 브라우저", "PELAYAR WEB", "NAVEGADOR WEB", "ВЕБ-БРАУЗЕР", "NAVEGADOR WEB", "เว็บเบราว์เซอร์", "WEB TARAYICI", "TRÌNH DUYỆT WEB" } },
    { SC_KEY_W3BACK, { } },
    { SC_KEY_W3FORWARD, { } },
    { SC_KEY_W3STOP, { } },
    { SC_KEY_W3REFRESH, { } },
    { SC_KEY_FAVORITE, { } },
    { SC_MOUSE_LEFT, {"LEFT CLICK", "鼠标左键", "滑鼠左鍵", "CLIC GAUCHE", "LINKSKLICK", "CLIC CON IL TASTO SINISTRO", "左クリック", "왼쪽 클릭", "KLIK KIRI", "CLIQUE COM O BOTÃO ESQUERDO", "ЩЕЛЧОК ЛКМ", "CLIC IZQUIERDO", "คลิกซ้าย", "SOL TIK", "NHẤP CHUỘT TRÁI" } },
    { SC_MOUSE_RIGHT, {"RIGHT CLICK", "鼠标右键", "滑鼠右鍵", "CLIC DROIT", "RECHTSKLICK", "CLIC DEL TASTO DESTRO", "右クリック", "오른쪽 클릭", "KLIK KANAN", "CLIQUE COM O BOTÃO DIREITO", "ЩЕЛЧОК ПРАВОЙ КНОПКОЙ", "CLIC CON EL BOTÓN DERECHO", "คลิกขวา", "SAĞ TIKLAMA", "NHẤP CHUỘT PHẢI" } },
    { SC_MOUSE_WHEEL, {"WHEEL CLICK", "按滚轮键", "按滾輪鍵", "CLIC MOLETTE", "MAUSRADKLICK", "CLIC DELLA ROTELLINA", "ホイールクリック", "횔 클릭", "KLIK RODA", "CLIQUE COM A RODA", "ЩЕЛЧОК КОЛЕСИКОМ", "CLIC CON LA RUEDA", "คลิกล้อ", "TEKER TIKLAMA", "NHẤP BÁNH XE" } },
    { SC_MOUSE_SCROLL_UP, {"SCROLL UP", "滚轮往上", "滾輪往上", "DÉFILER VERS LE HAUT", "NACH OBEN BLÄTTERN", "SCORRIMENTO SU", "上にスクロール", "위로 스크롤", "SKROL KE ATAS", "DESLOCAR PARA CIMA", "ПРОКРУТКА ВВЕРХ", "DESPLAZAMIENTO HACIA ARRIBA", "เลื่อนขึ้น", "YUKARI KAYDIRMA", "CUỘN LÊN" } },
    { SC_MOUSE_SCROLL_DN, {"SCROLL DOWN", "滚轮往下", "滾輪往下", "DÉFILER VERS LE BAS", "NACH UNTEN BLÄTTERN", "SCORRIMENTO GIÙ", "下にスクロール", "아래로 스크롤", "SKROL KE BAWAH", "DESLOCAR PARA BAIXO", "ПРОКРУТКА ВНИЗ", "DESPLAZAMIENTO HACIA ABAJO", "เลื่อนลง", "AŞAĞI KAYDIRMA", "CUỘN XUỐNG" } },
    { SC_MOUSE_MIDDLE, {"MIDDLE", "中键", "中鍵", "Centre", "Mitte", "Medio", "ミドル", "중간", "Tengah", "Meio", "Средн.", "Medio", "ปานกลาง", "Orta", "Giữa" } },
    { SC_MOUSE_BROWSER_FORWARD, {"BROWSER FORWARD", "浏览器下一页", "瀏覽器下一頁", "NAVIGATEUR AVANCE", "BROWSERVORLAUF", "NAVIGA AVANTI", "ブラウザーを進める", "브라우저 앞으로", "PELAYAR KE DEPAN", "AVANÇAR NO NAVEGADOR", "БРАУЗЕР – ВПЕРЕД", "AVANZAR EN EL EXPLORADOR", "เบราว์เซอร์ไปข้างหน้า", "TARAYICI İLERİ", "C.TIẾP TRÌNH DUYỆT" } },
    { SC_MOUSE_BROWSER_BACKWARD, {"BROWSER BACKWARD", "浏览器上一页", "瀏覽器上一頁", "NAVIGATEUR RETOUR", "BROWSERRÜCKLAUF", "NAVIGA INDIETRO", "ブラウザーを戻す", "브라우저 뒤로", "PELAYAR UNDUR", "RETROCEDER NO NAVEGADOR", "БРАУЗЕР – НАЗАД", "RETROCEDER EN EL EXPLORADOR", "เบราว์เซอร์ถอยหลัง", "TARAYICI GERİ", "CHUYỂN LÙI TRÌNH DUYỆT" } },
    { SC_KEY_XY_ONOFF, {"SENSOR ON/OFF", "传感器 开/关", "感應器 開/關", "CAPTEUR ACTIVÉ/DÉSACTIVÉ", "SENSOR EIN/AUS", "SENSORE ON/OFF", "センサー オン/オフ", "센서 켜기/끄기", "PENDERIA  HIDUP/MATI", "SENSOR  ATIVADO/DESATIVADO", "ДАТЧИК ВКЛ/ВЫКЛ", "SENSOR ACTIVAR/DESACTIVAR", "เซนเซอร์  เปิด/ปิด", "ALGILAYICI AÇIK/KAPALI", "CẢM BIẾN BẬT/TẮT" } },
    { SC_KEY_PROFILE_NEXT_CYCLE, {"PROFILE CYCLE[+]", "配置文件循环[+]", "設定檔循環[+]", "CYCLE PROFIL[+]", "PROFIL-ZYKLUS[+]", "CICLO PROFILO[+]", "プロファイル サイクル[+]", "프로필 사이클[+]", "KITARAN PROFIL[+]", "CICLO DE PERFIL[+]", "ПРОФИЛЬ[+]", "RECORRER PERFIL[+]", "รอบ โปรไฟล์[+]", "PROFIL DÖNGÜSÜ[+]", "CHU TRÌNH CẤU HÌNH[+]" } },
    { SC_KEY_PROFILE_PREV_CYCLE, {"PROFILE CYCLE[-]", "配置文件循环[-]", "設定檔循環[-]", "CYCLE PROFIL[-]", "PROFIL-ZYKLUS[-]", "CICLO PROFILO[-]", "プロファイル サイクル[-]", "프로필 사이클[-]", "KITARAN PROFIL[-]", "CICLO DE PERFIL[-]", "ПРОФИЛЬ[-]", "RECORRER PERFIL[-]", "รอบ โปรไฟล์[-]", "PROFIL DÖNGÜSÜ[-]", "CHU TRÌNH CẤU HÌNH[-]" } },
    { SC_KEY_CHANGE_LED_MODE, {"CHANGE LED MODE", "切换LED模式", "切換LED模式", "CHANGER MODE LED", "LED-MODUS ÄNDERN", "CAMBIA MODALITÀ LED", "LED モードの変更", "LED 모드 변경", "TUKAR MOD LED", "ALTERAR MODO LED", "ПЕРЕКЛЮЧИТЬ РЕЖИМ ПОДСВЕТКИ", "CAMBIAR MODO DE LED", "เปลี่ยนโหมด LED", "LED MODUNU DEĞİŞTİR", "ĐỔI CHẾ ĐỘ ĐÈN LED" } },
    { SC_KEY_CHANGE_LED_COLOR, {"CHANGE LED COLOR", "改变LED颜色", "改變LED顏色", "CHANGEMENT DE COULEUR LED", "WECHSEL LED-FARBE", "CAMBIAMENTO DI COLORE DEL LED", "LEDカラーチェンジ", "LED 색상 변경", "PERUBAHAN WARNA LED", "MUDAM DE COR LED", "ИЗМЕНЕНИЕ ЦВЕТА LED", "CAMBIO DE COLOR LED", "เปลี่ยนสี LED", "DEĞIŞIKLIĞI LED RENGI", "THAY ĐổI MÀU ĐÈN LED" } },
    { SC_KEY_DPI_NEXT,{"DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]","DPI[+]"}},
    { SC_KEY_DPI_PREV,{"DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]","DPI[-]"}},
    { SC_KEY_DPI_NEXT_CYCLE, {"DPI CYCLE[+]", "DPI 循环[+]", "DPI 循環 [+]", "CYCLE DPI[+]", "DPI-ZYKLUS [+]", "CICLO DPI[+]", "DPI サイクル [+]", "DPI 사이클[+]", "KITARAN DPI[+]", "CICLO DE PPP [+]", "ПЕРЕКЛЮЧЕНИЕ РАЗРЕШЕНИЯ[+]", "RECORRER[+] PPP", "รอบ DPI [+]", "DPI DÖNGÜSÜ [+]", "VÒNG DPI [+]" } },
    { SC_KEY_DPI_PREV_CYCLE, {"DPI CYCLE[-]", "DPI 循环[-]", "DPI 循環 [-]", "CYCLE DPI[-]", "DPI-ZYKLUS [-]", "CICLO DPI[-]", "DPI サイクル [-]", "DPI 사이클[-]", "KITARAN DPI[-]", "CICLO DE PPP [-]", "ПЕРЕКЛЮЧЕНИЕ РАЗРЕШЕНИЯ[-]", "RECORRER[-] PPP", "รอบ DPI [-]", "DPI DÖNGÜSÜ [-]", "VÒNG DPI [-]" } },
    { SC_KEY_POWER_MODE_DISABLE, {"Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable", "Power Mode:Disable" } },
    { SC_KEY_POWER_MODE_WORKING, {"Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working", "Power Mode:Working" } },
    { SC_KEY_POWER_MODE_PERFORMANCE, {"Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance", "Power Mode:Performance" } },
    { SC_KEY_POWER_MODE_GAMING, {"Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming", "Power Mode:Gaming" } }
};

static int activeLanguageIndex() {
    static int languageIdx = 0;
    static bool bInit = true;
    if ( bInit ) {
        QString active_language = CMSettings::instance()->value(LanguageKey, "English").toString();
        QStringList lsLanguage;
        lsLanguage << "English"
                   << "ChineseSimplified"
                   << "ChineseTraditional"
                   << "French"
                   << "German"
                   << "Italian"
                   << "Japanese"
                   << "Korean"
                   << "Malay"
                   << "Portuguese"
                   << "Russian"
                   << "Spanish"
                   << "Thai"
                   << "Turkish"
                   << "Vietnamese";
        languageIdx = lsLanguage.indexOf(active_language);
        if ( languageIdx < 0 ) {
            languageIdx = 0;
        }
        bInit = false;
    }
    return languageIdx;
}

static int keyScanCode( const QString &strKeyName, bool bWrap = false ) {
    int languageIdx = activeLanguageIndex();
    std::map< int, std::vector< const char* > >::iterator _it = KeyNameMap.begin();
    std::map< int, std::vector< const char* > >::iterator _end = KeyNameMap.end();
    for ( ; _it != _end; ++_it ) {
        const std::vector< const char * > &keys = _it->second;
        if ( languageIdx < 0 || languageIdx >= keys.size() ) {
            return SC_KEY_UNKNOWN;
        }

        QString strName = keys[languageIdx];
        if ( !bWrap && strName.contains("\n") ) {
            strName.replace("\n", "");
        }

        if ( strName.contains("&&") ) {
            strName.replace("&&", "&");
        }

        if ( strKeyName != strName ) {
            continue;
        }

        return _it->first;
    }
    return SC_KEY_UNKNOWN;
}

static QString KeyName( int iScanCode, bool bWrap = false )
{
    if ( iScanCode >= SC_KEY_PROFILE_0 && iScanCode <= SC_KEY_PROFILE_23 ) {
        return QObject::tr("PROFILE") + QString::number(iScanCode - SC_KEY_PROFILE_0 + 1);
    }

    if ( iScanCode >= SC_KEY_DPI_SET_1 && iScanCode <= SC_KEY_DPI_SET_7 ) {
        return QObject::tr("DPI LEVEL") + QString::number(iScanCode - SC_KEY_DPI_SET_1 + 1);
    }

    if ( KeyNameMap.find( iScanCode ) == KeyNameMap.end() ) {
        return QObject::tr("UNKNOWN");
    }

    //installLanguage
    int languageIdx = activeLanguageIndex();
    if ( languageIdx >= KeyNameMap[ iScanCode ].size() ) {
        languageIdx = 0;
    }

    if ( KeyNameMap[iScanCode].empty() ) {
        return QObject::tr("UNKNOWN");
    }

    QString strName = KeyNameMap[ iScanCode ][ languageIdx ];
    if ( !bWrap && strName.contains("\n") ) {
        strName.replace("\n", "");
    }

    if ( strName.contains("&&") ) {
        strName.replace("&&", "&");
    }
    return strName;
}

}   //end namespace KeyMapTab

}   //end namespace CMData
#endif


