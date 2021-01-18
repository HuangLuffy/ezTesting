using System;
using CommonLib.Util;
using static ReportLib.Reporter;

namespace ReportLib
{
    public interface IReporter
    {
        //void AddTestStep(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result);
        void AddTestStep(ResultTestCase resultTestCase, ResultTestInfo resultTestInfo = null);
        void ModifyResultTestInfo(ResultTestInfo resultTestInfo);
        ResultTestInfo GetResultTestInfo();
        string SetAsLink(string link);
        string SetNewLine(string line);
        string SetAsLines(params string[] lines);
        string SetNeedToCheck(string comment, string link);
        string GetResultPercent(int result, int total, int keepPoint = 2);
        string GetResultFullPath();
        string GetCaptureRelativePath();
        void SetCaptureRelativePath(string captureRelativePath);
        void Capture(string commentOnWeb = "Step_End", string imageName = "", string screenshotsRelativePath = "",
            UtilCapturer.ImageType imageType = UtilCapturer.ImageType.PNG);
        void Exec(Action action, string nodeDescription, string nodeExpectedResult, string nodeErrorMessage, WhenCaseFailed blockOrRun = WhenCaseFailed.Default);
        void RecordActionFailedDuringCaseRunning(string errorMessage = "Failed", string commentOnWeb = "Failed", string imageName = "", bool blContinueTest = false);
        Reporter.ResultTestCase CurrentTestCase { set; get; }
    }
}
