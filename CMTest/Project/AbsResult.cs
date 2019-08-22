using CommonLib.Util;
using System;
using System.IO;

namespace CMTest.Project
{
    public abstract class AbsResult
    {
        public string LogPath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log"); }
        }

        public string ImagePath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots"); }
        }
        public struct Result
        {
            public const string FAIL = "Failed";
            public const string PASS = "Passed";
        }
        public void HandleWrongStepResult(string comment, long num = 0)
        {
            if (comment != "")
            {
                //string tmp = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {Result.FAIL} - Num > [{num}]. Error > [{comment}]";
                string tmp = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {Result.FAIL}. Error > [{comment}]";
                UtilCapturer.Capture(Path.Combine(this.ImagePath, num.ToString()));
                UtilFile.WriteFile(Path.Combine(this.LogPath), tmp, true);
                Console.WriteLine(tmp);
                //UtilProcess.KillProcessByName(this.SwProcessName);
                UtilTime.WaitTime(2);
            }
        }
    }
}
