using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    class Program
    {
        static void Main(string[] args)
        {
            int launchTimes = 0;
            var a = System.Diagnostics.Process.Start(@"C:\Users\Public\Desktop\LightingControl.lnk");


            AT window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*", Timeout:10);
            AT tab_CONFIGURATION = window_CoolerMasterLightingControl.GetElement(Name: "CONFIGURATION", TreeScope:AT.TreeScope.Descendants, Timeout:4);


            UtilFile.WriteFileWhenNotExists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"), "123", true);


    
            //Console.WriteLine(tab_CONFIGURATION.GetElementInfo().Exists());
        }
    }
}
