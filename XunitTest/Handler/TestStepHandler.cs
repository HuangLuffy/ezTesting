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
            //TestFunctionInfo _XunitInfo = new TestFunctionInfo(3);
            TestFunctionInfo _StepInfo = new TestFunctionInfo(2, _IReporter);
            if (this.needToBlockTest)
                return;
            DateTime dt = DateTime.Now;
            //_IReporter.AddTestStep(_XunitInfo.ClassFullName, UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second), _XunitInfo.FunctionName, stepNumber++, );
            base.Execute(action);
            //trace.GetFrame(1).GetMethod().ReflectedType.FullName
        }
        private class TestFunctionInfo
        {
            public string ClassName { set; get; }
            public string ClassFullName { set; get; }
            public string FunctionName { set; get; }
            public string Descriptions { set; get; }
            public string ExpectedResults { set; get; }

            public TestFunctionInfo(int level, IReporter _IReporter)
            {
                StackFrame frame = new StackTrace().GetFrame(level);
                this.ClassName = frame.GetMethod().ReflectedType.Name;
                this.ClassFullName = frame.GetMethod().ReflectedType.FullName;
                this.FunctionName = frame.GetMethod().Name;
                foreach (var attri in frame.GetMethod().CustomAttributes)
                {
                    if (attri.AttributeType.Name.Equals(typeof(Descriptions).Name))
                    {
                        this.AssembleDescriptions(attri, _IReporter);
                    }
                    else if (attri.AttributeType.Name.Equals(typeof(ExpectedResults).Name))
                    {

                    }
                }
            }
            private string AssembleDescriptions(CustomAttributeData cads, IReporter _IReporter)
            {
                string t = "";
                var list = (IReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)cads.ConstructorArguments[0].Value;
                if (list.Count() == 1)
                {
                    t = list.ElementAt(0).Value.ToString();
                }
                else
                {
                    foreach (var item in list)
                    {
                        t += _IReporter.SetNewLine(item.Value.ToString());
                    }
                }
                return t;
            }
            private string AssembleExpectedResults(CustomAttributeData cads, IReporter _IReporter)
            {
                return "";
            }
        }
    }
}
