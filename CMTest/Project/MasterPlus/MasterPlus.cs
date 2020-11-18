﻿using System;

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
            public static string GetVarName(System.Linq.Expressions.Expression<Func<string, string>> exp)
            {
                return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
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
    }
}
