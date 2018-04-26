using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLib
{
    public interface IReporter
    {
        void AddTestStep(string classname, string stepTime, string functionName, string stepNumber, string description, string expectedResult, string needToCheck, string result);
        string SetAsLink(string link);
        string SetNewLine(string line);
        string setManualCheck(string comment, string link);
    }
}
