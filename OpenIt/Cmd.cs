using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class Cmd
    {
        public static string OPTION_SHOW_MENU_AGAIN = "Show Menu Again";
        public string[] WriteOptions(string[] options, bool clear = false, bool lineUpInNumber = true)
        {
            if (clear)
            {
                Console.Clear();
            }
            var a = options.ToList();
            a.Add(OPTION_SHOW_MENU_AGAIN);
            options = a.ToArray();
            for (int i = 1; i <= options.Length; i++)
            {
                if (lineUpInNumber)
                {
                    options[i - 1] = $"{ i.ToString()}. {options[i - 1]}";
                }
                Console.WriteLine(options[i - 1]);
            }
            return options;
        }
    }
}
