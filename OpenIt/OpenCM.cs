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
    public class OpenCM
    {
        public const string NameLightingControl = "LightingControl";
        public const string LinkPathLightingControl = @"C:\Users\Public\Desktop\LightingControl.lnk";
        public const string FAIL = "Fail";
        public const string PROCESSSTILLEXISTS = "[Process Still Exists after closing the LC window.]";
        public const string NOITEMSINUI = "[No Items in UI.]";
        public const string CRASH = "[Crash.]";
        public int launchTimes = 0;
        public void testc()
        {
            string comment = "";
            UtilProcess.KillProcessByName(OpenCM.NameLightingControl);
            UtilProcess.StartProcess(OpenCM.LinkPathLightingControl);
            AT window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*", Timeout: 10);
            try
            {
                AT tab_CONFIGURATION = window_CoolerMasterLightingControl.GetElement(Name: "CONFIGURATION", TreeScope: AT.TreeScope.Descendants, Timeout: 4);
            }
            catch (Exception)
            {
                comment = OpenCM.NOITEMSINUI;
            }
            AT button_Close = new AT().GetElement(AutomationId: "Close", TreeScope: AT.TreeScope.Descendants);
            UtilTime.WaitTime(10);
            try
            {
                window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*");
            }
            catch (Exception)
            {
                comment = OpenCM.CRASH;
            }

            button_Close.DoClick();
            UtilTime.WaitTime(2);
            if (UtilProcess.IsProcessExistedByName(OpenCM.NameLightingControl))
            {
                comment = OpenCM.PROCESSSTILLEXISTS;
            }
            string ctt = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {FAIL} Num > [{launchTimes++}] Error > [{comment}]";
            UtilFile.WriteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), true);
            UtilProcess.KillProcessByName(OpenCM.NameLightingControl);
        }

    }
}
