﻿using ATLib;
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
            if (args.Length >= 2 && args[0].Trim().Equals("-d", StringComparison.CurrentCultureIgnoreCase))
            {
                string args1 = args[1].Trim();
                if (!args1.Equals(""))
                {
                    //TestIt.RunDirectly _RunDirectly = new TestIt.RunDirectly() { run = true, device = args1 };
                    _TestIt.RunDirectly_Flow_PlugInOutServer(args1);
                }
            }
            else
            {
                _TestIt.OpenCMD();
            }
            Console.ReadKey();
        }
    }
}
