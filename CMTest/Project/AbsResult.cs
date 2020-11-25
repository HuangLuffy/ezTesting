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
            public const string Resources = "Resources";
        }

        protected string LogPathLaunch => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launch.log");
        protected readonly string LogPathRestart = Path.Combine(ProjectPath.GetProjectFullPath(), "RestartLog.log");
        //public string ProjectPath => AppDomain.CurrentDomain.BaseDirectory;
        protected string ResultPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Const.Result);
        private readonly string _currentTestFolderName;
        private string ResultTimePath => Path.Combine(ResultPath, _currentTestFolderName);
        protected string ScreenshotsPath => Path.Combine(ResultTimePath, Const.Screenshots);
        protected readonly string RestartScreenshotPath = Path.Combine("Screenshots\\Restart");
        private readonly string _screenshotsRelativePath;
        public string ResourcesPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Const.Resources);
        public string ResourcesKeysRelativePath => Path.Combine(Const.Resources, "KeyMapTab.h");

        public AbsResult()
        {
            _currentTestFolderName = GetTestTimeString();//This function would run twice if put it with "public string CurrentTestFolderName";
            _screenshotsRelativePath = Path.Combine(Const.Result, _currentTestFolderName, Const.Screenshots);
            //ScreenshotsRelativePath = UtilString.GetSplitArray(ScreenshotsPath, "\\").ElementAt(UtilString.GetSplitArray(ScreenshotsPath, "\\").Count() - 2);
        }
        public string GetScreenshotsRelativePath()
        {
            return _screenshotsRelativePath;
        }
        public string GetResultTimePath()
        {
            return ResultTimePath;
        }
        public string GetTestTimeString()
        {
            return UtilTime.GetTimeString();
        }
        protected string GetRestartLogTime()
        {
            return $"{DateTime.Now:yyyy.MM.dd_HH.mm.ss}";
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
