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
        private const string NameLightingControl = "LightingControl";
        private const string LinkPathLightingControl = @"C:\Users\Public\Desktop\LightingControl.lnk";
        private string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log");
        private string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
        private const string FAIL = "Fail";
        private const string PASS = "Pass";
        private const string PROCESSSTILLEXISTS = "Process still exists after closing the LC window.";
        private const string NOITEMSINUI = "No Items in UI.";
        private const string CRASH = "Crashed.";
        private string RunAndGet(long num)
        {
            UtilProcess.StartProcess(OpenCM.LinkPathLightingControl);
            AT window_CoolerMasterLightingControl = null;
            try
            {
                Console.Title = num.ToString() + " | Waiting LC launching 11s.";
                window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*", Timeout: 11);
            }
            catch (Exception)
            {
                return getComment(OpenCM.CRASH, num);
            }
            Console.Title = num.ToString() + " | Waiting 10s for checking crash.";
            UtilTime.WaitTime(10); 
            try
            {
                window_CoolerMasterLightingControl = new AT().GetElement(Name: "Cooler Master.*");
            }
            catch (Exception)
            {
                return getComment(OpenCM.CRASH, num);
            }
            try
            {
                AT tab_CONFIGURATION = window_CoolerMasterLightingControl.GetElement(Name: "CONFIGURATION", TreeScope: AT.TreeScope.Descendants, Timeout: 5);
            }
            catch (Exception)
            {
                return getComment(OpenCM.NOITEMSINUI, num);
            }
            AT button_Close = window_CoolerMasterLightingControl.GetElement(AutomationId: "Close", TreeScope: AT.TreeScope.Descendants);
            button_Close.DoClick();
            Console.Title = num.ToString() + " | Waiting 2s for LC closing.";
            UtilTime.WaitTime(2);
            if (UtilProcess.IsProcessExistedByName(OpenCM.NameLightingControl))
            {
                return getComment(OpenCM.PROCESSSTILLEXISTS, num);
            }       
            return "";
        }
        public void Run()
        {
            string tmp = "";
            UtilFolder.DeleteDirectory(imagePath);
            UtilTime.WaitTime(1);
            UtilFolder.CreateDirectory(imagePath);
            try
            {
                
                UtilFile.WriteFile(Path.Combine(this.logPath), "", false);
            }
            catch (Exception)
            {
            }    
            UtilProcess.KillProcessByName(OpenCM.NameLightingControl);
            for (int i = 1; i < 99999999; i++)
            {
                tmp = this.RunAndGet(i);
                try
                {
                    if (tmp != "")
                    {
                        UtilCapturer.Capture(Path.Combine(imagePath, i.ToString()));
                        UtilFile.WriteFile(Path.Combine(this.logPath), tmp, true);
                        Console.WriteLine(tmp);
                    }
                    UtilProcess.KillProcessByName(OpenCM.NameLightingControl);
                    UtilTime.WaitTime(2);
                }
                catch (Exception ex)
                {
                    UtilFile.WriteFile(Path.Combine(this.logPath), ex.Message, true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private string getComment(string comment, long num)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {FAIL} - Num > [{num}]. Error > [{comment}]";
        }
    }
}
