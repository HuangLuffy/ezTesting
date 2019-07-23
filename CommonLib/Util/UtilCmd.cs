using System;
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
        public const string STRING_CONNECTOR = ". ";
        public List<string> List_Last_Menu = new List<string>();
        public List<string> List_Current_Menu = new List<string>();

        public List<string> WriteOptions(List<string> options, bool clear = false, bool lineUpWithNumber = true)
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
            if (this.IsMenuChanged(this.List_Current_Menu, t))
            {
                this.List_Last_Menu = this.List_Current_Menu;
                this.List_Current_Menu = t;
            }
            return t;
        }
        public void Clear()
        {
            Console.Clear();
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
