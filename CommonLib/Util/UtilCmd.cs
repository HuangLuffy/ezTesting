using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Util
{
    public class UtilCmd
    {
        public const string OPTION_SHOW_MENU_AGAIN = "Show Menu Again";
        public const string OPTION_BACK = "Back";
        public const string STRING_CONNECTOR = ". ";
        public List<string> List_Last_Menu = new List<string>();
        public List<string> List_Current_Menu = new List<string>();

        public string MenuShowAgain()
        {
            WriteCmdMenu(List_Current_Menu, lineUpWithNumber: false);
            return OPTION_SHOW_MENU_AGAIN;
        }

        public string MenuGoback()
        {
            WriteCmdMenu(List_Last_Menu, lineUpWithNumber: false);
            return OPTION_BACK;
        }

        public List<string> WriteCmdMenu(List<string> options, bool clear = false, bool lineUpWithNumber = true)
        {
            List<string> t = new List<string>();
            if (clear)
            {
                Console.Clear();
            }
            for (int i = 1; i <= options.Count; i++)
            {
                if (lineUpWithNumber)
                {
                    t.Add($"{ i.ToString()}{ STRING_CONNECTOR }{options[i - 1]}");
                }
                else {
                    t.Add($"{options[i - 1]}");
                }
                Console.WriteLine(t[i - 1]);
            }
            if (IsMenuChanged(List_Current_Menu, t))
            {
                List_Last_Menu = List_Current_Menu;
                List_Current_Menu = t;
            }
            return t;
        }
        public void Clear()
        {
            Console.Clear();
        }
        public void PressAnyContinue(string s = "Please press any key to continue.")
        {
            Console.WriteLine(s);
            ReadLine();
        }
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        private bool IsMenuChanged(List<string> a, List<string> b)
        {
            if (a.Count() != b.Count())
            {
                return true;
            }
            if (a.Count() > 0)
            {
                for (int i = 0; i < a.Count(); i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
