using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReportLib.Reporter;

namespace ReportLib
{
    public interface IReporter
    {
        //void AddTestStep(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result);
        void AddTestStep(Result_TestCase _Result_TestCase);
        void ModifyTestInfo(Result_TestInfo _Result_TestInfo);
        string SetAsLink(string link);
        string SetNewLine(string line);
        string setManualCheck(string comment, string link);
        string GetResultPercent(int result, int total, int keepPoint = 2);
    }
}
