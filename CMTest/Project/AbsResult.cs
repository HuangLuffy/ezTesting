using CommonLib.Util;
using System;
using System.IO;
using CommonLib.Util.IO;
using CommonLib.Util.Project;

namespace CMTest.Project
{
    public abstract class AbsResult
    {
        public string LogPathLaunch => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log");
        public string LogPathRestart = Path.Combine(ProjectPath.GetProjectFullPath(), "RestartLog.log");
        public string ResultPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Result");
        public string ScreenshotsPath => Path.Combine(ResultPath, "Screenshots");
        public string RestartScreenshotPath = Path.Combine("Screenshots\\Restart");
        //public string ImagePath
        //{
        //    get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots"); }
        //}
        public struct Result
        {
            public const string Fail = "Failed";
            public const string Pass = "Passed";
        }
        public string GetTestTimeString()
        {
            return UtilTime.GetTimeString();
        }
        protected string GetRestartLogTime()
        {
            return $"{DateTime.Now:yyyy.MM.dd_hh.mm.ss}";
        }
        protected void HandleWrongStepResult(string comment, long num = 0)
        {
            if (comment == "") return;
            //string tmp = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {Result.FAIL} - Num > [{num}]. Error > [{comment}]";
            var tmp = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {Result.Fail}. Error > [{comment}]";
            UtilCapturer.Capture(Path.Combine(ScreenshotsPath, num.ToString()));
            UtilFile.WriteFile(Path.Combine(LogPathLaunch), tmp);
            Console.WriteLine(tmp);
            //UtilProcess.KillProcessByName(this.SwProcessName);
            UtilTime.WaitTime(2);
        }
    }
}
