using static ReportLib.Reporter;

namespace ReportLib
{
    public interface IReporter
    {
        //void AddTestStep(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result);
        void AddTestStep(ResultTestCase _Result_TestCase);
        void ModifyTestInfo(ResultTestInfo _Result_TestInfo);
        string SetAsLink(string link);
        string SetNewLine(string line);
        string setManualCheck(string comment, string link);
        string GetResultPercent(int result, int total, int keepPoint = 2);
    }
}
