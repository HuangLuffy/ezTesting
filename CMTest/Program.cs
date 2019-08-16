using ATLib;
using CommonLib.Util;
using CMTest.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using CMTest.Xml;

namespace CMTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestIt _TestIt = new TestIt();
            if (args.Length >= 2 && args[0].Trim().Equals("-d", StringComparison.CurrentCultureIgnoreCase) && !args[1].Trim().Equals(""))
            {
                TestIt.RunDirectly _RunDirectly = new TestIt.RunDirectly() { run = true, device = args[1].Trim() };
                _TestIt.RunDirectly_Flow_PlugInOutServer(args[1].Trim());
            }
            else
            {
                _TestIt.OpenCMD();
            }
            Console.ReadKey();
        }
    }
}
