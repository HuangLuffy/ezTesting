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
    public class OpenIt
    {
        PORTAL _PORTAL = new PORTAL();
        private string RunAndGet(long num)
        {
            UtilProcess.StartProcess(_PORTAL.SwLnkPath);
            AT MainWindow_SW = null;
            try
            {
                Console.Title = num.ToString() + " | Waiting for launching. (11s)";
                MainWindow_SW = new AT().GetElement(Name: "Cooler Master.*", Timeout: 11);
            }
            catch (Exception)
            {
                return getComment(PORTAL.Log.CRASH, num);
            }
            Console.Title = num.ToString() + " | Waiting 10s for checking crash.";
            UtilTime.WaitTime(10); 
            try
            {
                MainWindow_SW = new AT().GetElement(Name: "Cooler Master.*");
            }
            catch (Exception)
            {
                return getComment(PORTAL.Log.CRASH, num);
            }
            try
            {
                AT tab_CONFIGURATION = MainWindow_SW.GetElement(Name: "CONFIGURATION", TreeScope: AT.TreeScope.Descendants, Timeout: 5);
            }
            catch (Exception)
            {
                return getComment(OpenIt.NOITEMSINUI, num);
            }
            AT button_Close = MainWindow_SW.GetElement(AutomationId: "Close", TreeScope: AT.TreeScope.Descendants);
            button_Close.DoClick();
            Console.Title = num.ToString() + " | Waiting 2s for LC closing.";
            UtilTime.WaitTime(2);
            if (UtilProcess.IsProcessExistedByName(OpenIt.NameLightingControl))
            {
                return getComment(OpenIt.PROCESSSTILLEXISTS, num);
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
            UtilProcess.KillProcessByName(OpenIt.NameLightingControl);
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
                    UtilProcess.KillProcessByName(OpenIt.NameLightingControl);
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
