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

namespace XunitTest.Handler
{
    public class TestStepHandler : TestStep
    {
        IReporter _IReporter;
        public string pathReportXml = "";
        public int stepNumber = 0;
        public TestStepHandler(string pathReportXml = "")
        {
            _IReporter = ReporterManager.GeReporter(pathReportXml);
            this.pathReportXml = pathReportXml;
        }
        private T Exec<T>(Func<T> action)
        {
            try
            {
                if (this.needToBlockTest)
                    return default(T);
                DateTime dt = DateTime.Now;
                T result = base.Execute(action);
                return base.Execute(action);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Exec(Action action)
        {
            TestFunctionInfo _Info = new TestFunctionInfo(3);
            if (this.needToBlockTest)
                return;
            DateTime dt = DateTime.Now;
            //_IReporter.AddTestStep(_Info.ClassFullName, UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second), _Info.FunctionName, stepNumber++, );
            base.Execute(action);
            //trace.GetFrame(1).GetMethod().ReflectedType.FullName
        }
        private class TestFunctionInfo
        {
            public String ClassName { set; get; }
            public String ClassFullName { set; get; }
            public String FunctionName { set; get; }
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
