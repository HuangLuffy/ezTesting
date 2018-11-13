﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class UtilCmd
    {
        public const string OPTION_SHOW_MENU_AGAIN = "Show Menu Again";
        public const string OPTION_BACK = "Back";
        public const string STRING_CONNECTOR = " .";
        public List<string> WriteOptions(List<string> options, bool clear = false, bool lineUpInNumber = true)
        {
            List<string> t = new List<string>();
            if (clear)
            {
                Console.Clear();
            }
            for (int i = 1; i <= options.Count; i++)
            {
                if (lineUpInNumber)
                {
                    t.Add($"{ i.ToString()}{ STRING_CONNECTOR }{options[i - 1]}");
                }
                Console.WriteLine(t[i - 1]);
            }
            return t;
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}