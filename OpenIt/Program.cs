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
            UtilProcess.KillProcessByName("LightingControl");
            UtilProcess.StartProcess(@"C:\Users\Public\Desktop\LightingControl.lnk");
          


            AT window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*", Timeout:10);
            AT tab_CONFIGURATION = window_CoolerMasterLightingControl.GetElement(Name: "CONFIGURATION", TreeScope:AT.TreeScope.Descendants, Timeout:4);
            const string FAIL = "Fail";
            string PROCESSSTILLEXISTS = "[Process Still Exists]";
            string NOITEMSINUI = "[No Items in UI]";
            AT button_Close = new AT().GetElement(AutomationId: "Close", TreeScope: AT.TreeScope.Descendants);
            UtilTime.WaitTime(10);
            button_Close.DoClick();
            string ctt = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {FAIL} Num > {args[0]}";
            UtilFile.WriteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), true);


    
            //Console.WriteLine(tab_CONFIGURATION.GetElementInfo().Exists());
        }
    }
}
