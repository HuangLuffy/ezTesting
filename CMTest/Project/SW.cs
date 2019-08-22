using ATLib;
using CommonLib.Util;
using CMTest.Project;
using System;

namespace CMTest
{
    public class SW : AbsResult
    {
        protected AbsSWObj Obj;
        protected AT MainWindow_SW = null;
        protected int Timeout { get; set; }
        public long LaunchTimes { get; set; }

        public string SwName { get; protected set; }

        protected string SwProcessName { private get; set; }

        //protected string SwProcessName { get; set; }
        protected string SwLnkPath { get; set; }

        protected struct Msg
        {
            public const string PROCESSSTILLEXISTS = "This SW's process still exists after closing it.";
            public const string NOITEMSINUI = "No Items in UI.";
            public const string CRASH = "Crashed.";
        }

        public void SetLaunchTimesAndWriteTestTitle(long launchTimes, string c = "")
        {
            this.LaunchTimes = launchTimes;
            this.WriteConsoleTitle(launchTimes, c);
        }

        protected void WriteConsoleTitle(long launchTimes, string c = "", int timeout = 0)
        {
            Console.Title = launchTimes.ToString() + " | " + c;
            var t = Console.Title;
            if (timeout != 0)
            {
                UtilTime.CountDown(timeout, (s) => { Console.Title = t + " > " + s.ToString(); });
            }
            UtilTime.WaitTime(timeout);
        }

        protected void Initialize()
        {
            UtilFolder.DeleteDirectory(this.ImagePath);
            UtilFolder.CreateDirectory(this.ImagePath);
            UtilProcess.KillProcessByName(this.SwProcessName); 
        }
    }
}
