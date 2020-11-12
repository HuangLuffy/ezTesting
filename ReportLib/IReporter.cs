using static ReportLib.Reporter;

namespace ReportLib
{
    public interface IReporter
    {
        //void AddTestStep(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result);
        void AddTestStep(ResultTestCase resultTestCase, ResultTestInfo resultTestInfo = null);
        void ModifyTestInfo(ResultTestInfo resultTestInfo);
        string SetAsLink(string link);
        string SetNewLine(string line);
        string SetManualCheck(string comment, string link);
        string GetResultPercent(int result, int total, int keepPoint = 2);
    }
}
