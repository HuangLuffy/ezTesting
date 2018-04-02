using ATLib.Invoke;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ATLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Invoker _Invoker = new Invoker(args);
            Console.Write(_Invoker.HandleEvent());
            //System.Environment.Exit(System.Environment.ExitCode);
            Console.ReadKey();
        }
    }
}
