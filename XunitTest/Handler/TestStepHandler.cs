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
            TestFunctionInfo _XunitInfo = new TestFunctionInfo(3, _IReporter);
            TestFunctionInfo _StepInfo = new TestFunctionInfo(2, _IReporter);
            string result = string.Empty;
            DateTime dt = DateTime.Now;
            try
            {
                if (this.needToBlockTest)
                {
                    result = TestStepHandler.Result.BLOCK;
                }
                else
                {
                    base.Execute(action);
                    result = TestStepHandler.Result.PASS;
                }
            }
            catch (Exception ex)
            {
                result = TestStepHandler.Result.FAIL;
                throw ex;
            }
            finally
            {
                _IReporter.AddTestStep(_XunitInfo.ClassFullName, UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second).ToString(), _XunitInfo.FunctionName, stepNumber++.ToString(), _StepInfo.Descriptions, _StepInfo.ExpectedResults, "", result);
            }  
        }
        private class TestFunctionInfo
        {
            public string ClassName { set; get; }
            public string ClassFullName { set; get; }
            public string FunctionName { set; get; }
            public string Descriptions = "NA";
            public string ExpectedResults = "NA";

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
                        this.Descriptions = this.AssembleStepInstructions(attri.ConstructorArguments[0].Value, _IReporter);
                    }
                    else if (attri.AttributeType.Name.Equals(typeof(ExpectedResults).Name))
                    {
                        this.ExpectedResults = this.AssembleStepInstructions(attri.ConstructorArguments[0].Value, _IReporter);
                    }
                }
            }
            private string AssembleStepInstructions(object cads, IReporter _IReporter)
            {
                string t = "";
                var list = (IReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)cads;
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
        }
    }
}
