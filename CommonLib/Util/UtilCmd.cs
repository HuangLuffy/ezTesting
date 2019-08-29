using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Util
{
    public class UtilCmd
    {
        public const string OptionShowMenuAgain = "Show Menu Again";
        public const string OptionBack = "Back";
        public const string StringConnector = ". ";
        private List<string> _listLastMenu = new List<string>();
        private List<string> _listCurrentMenu = new List<string>();

        public string MenuShowAgain()
        {
            WriteCmdMenu(_listCurrentMenu, lineUpWithNumber: false);
            return OptionShowMenuAgain;
        }

        public string MenuGoBack()
        {
            WriteCmdMenu(_listLastMenu, lineUpWithNumber: false);
            return OptionBack;
        }

        public List<string> WriteCmdMenu(List<string> options, bool clear = false, bool lineUpWithNumber = true)
        {
            var t = new List<string>();
            if (clear)
            {
                Console.Clear();
            }
            for (var i = 1; i <= options.Count; i++)
            {
                t.Add(lineUpWithNumber ? $"{i.ToString()}{StringConnector}{options[i - 1]}" : $"{options[i - 1]}");
                Console.WriteLine(t[i - 1]);
            }

            if (!IsMenuChanged(_listCurrentMenu, t)) return t;
            _listLastMenu = _listCurrentMenu;
            _listCurrentMenu = t;
            return t;
        }
        public static void Clear()
        {
            Console.Clear();
        }
        public static void PressAnyContinue(string s = "Please press any key to continue.")
        {
            Console.WriteLine(s);
            ReadLine();
        }
        public static void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        private static bool IsMenuChanged(IReadOnlyList<string> a, IReadOnlyList<string> b)
        {
            if (a.Count != b.Count)
            {
                return true;
            }

            if (!a.Any()) return false;
            for (var i = 0; i < a.Count(); i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
