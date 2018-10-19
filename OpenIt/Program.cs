using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace OpenIt
{
    class Program
    {
        static void Main(string[] args)
        {
            TestIt _TestIt = new TestIt();
            _TestIt.Run();
            Console.ReadKey();
        }
    }
}
