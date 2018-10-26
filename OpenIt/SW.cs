using CommonLib.Util;
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
        private string logPath;
        public string LogPath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"); }
        }
        private string imagePath;
        public string ImagePath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots"); }
        }
        private string swName;
        public string SwName
        {
            get { return swName; }
            set { swName = value; }
        }
        private string swProcessName;
        public string SwProcessName
        {
            get { return swProcessName; }
            set { swProcessName = value; }
        }
        private string swLnkPath;
        public string SwLnkPath
        {
            get { return swLnkPath; }
            set { swLnkPath = value; }
        }
        public struct Result
        {
            public const string FAIL = "Failed";
            public const string PASS = "Passed";
        }
        public struct Log
        {
            public const string PROCESSSTILLEXISTS = "This SW's process still exists after closing it.";
            public const string NOITEMSINUI = "No Items in UI.";
            public const string CRASH = "Crashed.";
        }
        private string mainWindowName;
        public string MainWindowName
        {
            get { return mainWindowName; }
            set { mainWindowName = value; }
        }
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
            UtilTime.WaitTime(0.5);
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
