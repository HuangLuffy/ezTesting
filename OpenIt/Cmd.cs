using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class Cmd
    {
        public string[] WriteOptions(string[] options, bool clear = false, bool lineUpInNumber = true)
        {
            if (clear)
            {
                Console.Clear();
            }
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
