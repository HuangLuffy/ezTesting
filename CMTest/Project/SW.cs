using System;
using ATLib;
using CommonLib.Util;
using CommonLib.Util.io;

namespace CMTest.Project
{
    public class SW : AbsResult
    {
        protected AbsSwObj Obj;
        protected AT SwMainWindow = null;
        protected int Timeout { get; set; }
        public long LaunchTimes { get; set; }
        public string SwName { get; protected set; }
        protected string SwProcessName { private get; set; }
        protected string SwLnkPath { get; set; }

        protected struct Msg
        {
            public const string ProcessStillExists = "This SW's process still exists after closing it.";
            public const string NoItemsInUi = "No Items in UI.";
            public const string Crash = "Crashed.";
        }

        public void SetLaunchTimesAndWriteTestTitle(long launchTimes, string comments = "")
        {
            LaunchTimes = launchTimes;
            WriteConsoleTitle(launchTimes, comments);
        }

        protected static void WriteConsoleTitle(long launchTimes, string comments = "", int timeout = 0)
        {
            Console.Title = launchTimes + " | " + comments;
            var t = Console.Title;
            if (timeout != 0)
            {
                UtilTime.CountDown(timeout, s => { Console.Title = t + " > " + s; });
            }
            UtilTime.WaitTime(timeout);
        }

        protected void Initialize()
        {
            UtilFolder.DeleteDirectory(ImagePath);
            UtilFolder.CreateDirectory(ImagePath);
            UtilProcess.KillProcessByName(SwProcessName); 
        }
    }
}
