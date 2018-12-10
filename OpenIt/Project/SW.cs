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
    public class SW : AbsResult
    {
        public AbsSWObj Obj;
        protected AT MainWindow_SW = null;
        protected int Timeout { get; set; }
        public long LaunchTimes { get; set; }

        public string SwName { get; set; }
        public string SwProcessName { get; set; }
        public string SwLnkPath { get; set; }


        public struct Msg
        {
            public const string PROCESSSTILLEXISTS = "This SW's process still exists after closing it.";
            public const string NOITEMSINUI = "No Items in UI.";
            public const string CRASH = "Crashed.";
        }

        public void WriteConsoleTitle(long launchTimes, string c, int timeout = 0)
        {
            Console.Title = launchTimes.ToString() + " | " + c;
            string t = Console.Title;
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
