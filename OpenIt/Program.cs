using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
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
            //PortalTestFlows _PortalTestFlows = new PortalTestFlows();
            //_PortalTestFlows.Flow_ProfilesComplexSwitch();
            TestIt _TestIt = new TestIt();
            _TestIt.Run();
            Console.ReadKey();
        }
    }
}
