using CommonLib.Util;
using System;
using System.IO;
using System.Linq;
using CommonLib.Util.IO;
using CommonLib.Util.Project;
using ReportLib;

namespace CMTest.Project
{
    public abstract class AbsResult
    {
        public struct Const
        {
            public const string Screenshots = "Screenshots";
            public const string Result = "Result";
        }
        
        public string LogPathLaunch => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log");
        public string LogPathRestart = Path.Combine(ProjectPath.GetProjectFullPath(), "RestartLog.log");
        //public string ProjectPath => AppDomain.CurrentDomain.BaseDirectory;
        public string ResultPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Const.Result);
        public string CurrentTestFolderName;
        public string ResultTimePath => Path.Combine(ResultPath, CurrentTestFolderName);
        public string ScreenshotsPath => Path.Combine(ResultTimePath, Const.Screenshots);
        public string RestartScreenshotPath = Path.Combine("Screenshots\\Restart");
        public string ScreenshotsRelativePath;


        public AbsResult()
        {
            CurrentTestFolderName = GetTestTimeString();//This function would run twice if put it with "public string CurrentTestFolderName";
            ScreenshotsRelativePath = Path.Combine(Const.Result, CurrentTestFolderName, Const.Screenshots);
            //ScreenshotsRelativePath = UtilString.GetSplitArray(ScreenshotsPath, "\\").ElementAt(UtilString.GetSplitArray(ScreenshotsPath, "\\").Count() - 2);
        }
        //public string ImagePath
        //{
        //    get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots"); }
        //}
        //public struct Result
        //{
        //    public const string Fail = "Failed";
        //    public const string Pass = "Passed";
        //}
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
            var tmp = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {Reporter.Result.FAIL}. Error > [{comment}]";
            UtilCapturer.Capture(Path.Combine(ScreenshotsPath, num.ToString()));
            UtilFile.WriteFile(Path.Combine(LogPathLaunch), tmp);
            Console.WriteLine(tmp);
            //UtilProcess.KillProcessByName(this.SwProcessName);
            UtilTime.WaitTime(2);
        }
    }
}
