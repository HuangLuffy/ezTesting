using ATLib;
using CommonLib.Util;
using OpenIt.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class SW
    {
        public AbsSWObj Obj;
        protected AT MainWindow_SW = null;
        protected int Timeout { get; set; }
        public long LaunchTimes { get; set; }

        public string LogPath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"); }
        }

        public string ImagePath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots"); }
        }

        public string SwName { get; set; }
        public string SwProcessName { get; set; }
        public string SwLnkPath { get; set; }

        public struct Result
        {
            public const string FAIL = "Failed";
            public const string PASS = "Passed";
        }
        public struct msg
        {
            public const string PROCESSSTILLEXISTS = "This SW's process still exists after closing it.";
            public const string NOITEMSINUI = "No Items in UI.";
            public const string CRASH = "Crashed.";
        }

        //public string MainWindowName { get; set; }

        public void HandleStepResult(string comment, long num)
        {
            if (comment != "")
            {
                string tmp = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {Result.FAIL} - Num > [{num}]. Error > [{comment}]";
                UtilCapturer.Capture(Path.Combine(this.ImagePath, num.ToString()));
                UtilFile.WriteFile(Path.Combine(this.LogPath), tmp, true);
                Console.WriteLine(tmp);
                UtilProcess.KillProcessByName(this.SwProcessName);
                UtilTime.WaitTime(2);
            }

        }
        public void WriteConsoleTitle(long launchTimes, string c, int timeout = 0)
        {
            Console.Title = launchTimes.ToString() + " | " + c;
            String t = Console.Title;
            if (timeout != 0)
            {
                UtilTime.CountDown(timeout, (s) => { Console.Title = t + " > " + s.ToString(); });
            }
        }
        public void Initialize()
        {
            UtilFolder.DeleteDirectory(this.ImagePath);
            //UtilTime.WaitTime(0.5);
            UtilFolder.CreateDirectory(this.ImagePath);
            UtilProcess.KillProcessByName(this.SwProcessName);
            //try
            //{

            //    UtilFile.WriteFile(Path.Combine(_SW.LogPath), "", false);
            //}
            //catch (Exception)
            //{
            //}    
        }
    }
}
