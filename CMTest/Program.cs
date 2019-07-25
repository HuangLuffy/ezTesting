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
            XmlOps _XmlOps = new XmlOps();
            _XmlOps.GetVmPlugInOutRunDevice();

            TestIt _TestIt = new TestIt();
            _TestIt.OpenCMD();
            Console.ReadKey();
        }
    }
}
