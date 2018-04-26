using CommonLib.Util;
using ReportLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace XunitTest.Wrapper
{
    public class DetailedTestStep : TestStep
    {
        IReporter _IReporter;
        public string pathReportXml = "";
        public DetailedTestStep(string pathReportXml = "")
        {
            _IReporter = ReporterManager.GeReporter(pathReportXml);
            this.pathReportXml = pathReportXml;
        }
        private T Rec<T>(Func<T> action)
        {
            try
            {
                if (this.needToBlockTest)
                    return default(T);
                DateTime dt = DateTime.Now;
                T result = Exec<T>(action);
                return Exec<T>(action);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Rec(Action action)
        {
            if (this.needToBlockTest)
                return;
            DateTime dt = DateTime.Now;
            // _IReporter.AddTestStep(classname, UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second), );
            Exec(action);
            //trace.GetFrame(1).GetMethod().ReflectedType.FullName
        }
        private class TestFunctionInfo
        {
            String ClassName { set; get; }
            String ClassFullName { set; get; }
            String FunctionName { set; get; }
            public TestFunctionInfo(int level)
            {
                StackFrame frame = new StackTrace().GetFrame(level);
                this.ClassName = frame.GetMethod().ReflectedType.Name;
                this.ClassFullName = frame.GetMethod().ReflectedType.FullName;
                this.FunctionName = frame.GetMethod().Name;
            }
        }
    }
}
