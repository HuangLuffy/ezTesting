using CommonLib.Util;
using CommonLib.Util.os;
using ReportLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestLib;
using static ReportLib.Reporter;

namespace XunitTest.Handler
{
    public class TestStepHandler : TestStep
    {
        private IReporter _IReporter;
        private Result_TestInfo _Result_TestInfo;
        public TestStepHandler(string pathReportXml = "")
        {
            _IReporter = ReporterManager.GeReporter(pathReportXml);
            _Result_TestInfo = new Result_TestInfo();
            _Result_TestInfo.Attribute_project = "CM";
            _Result_TestInfo.Attribute_os = UtilOS.GetOsVersion();
            _Result_TestInfo.Attribute_language = System.Globalization.CultureInfo.InstalledUICulture.Name.Replace("-","_");
            _Result_TestInfo.Attribute_region = System.Globalization.CultureInfo.InstalledUICulture.Name.Split('-')[1];
            _Result_TestInfo.Attribute_deviceModel = "NA";
            _Result_TestInfo.Attribute_deviceName = "NA";
            _Result_TestInfo.Attribute_version = "NA";
            _Result_TestInfo.Attribute_tests = 0;
            _Result_TestInfo.Attribute_passes = 0;
            _Result_TestInfo.Attribute_failures = 0;
            _Result_TestInfo.Attribute_tbds = 0;
            _Result_TestInfo.Attribute_blocks = 0;

            //(string project, string os, string language, string region, string time, string deviceModel, string deviceName, string testTotalNumber, string version, string name, string testName
            //, string testName, string testName, string testName, string testName, string testName)
            this.pathReportFile = pathReportXml;
        }
        private T Exec<T>(Func<T> action)
        {
            try
            {
                if (this.needToBlockAllTests)
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
            _Result_TestInfo.Attribute_name = _XunitInfo.ClassName;
            TestFunctionInfo _StepInfo = new TestFunctionInfo(2, _IReporter);
            Result_TestCase _Result_TestCase = new Result_TestCase();
            string result = string.Empty;
            DateTime dt = DateTime.Now;
            try
            {
                if (_StepInfo.DoNotBlock == true)
                {
                    this.needToBlockAllTests = false;
                }
                if (this.needToBlockAllTests)
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
                this.needToBlockAllTests = true;
                throw ex;
            }
            finally
            {
                _Result_TestCase.Attribute_classname = _XunitInfo.ClassFullName;
                _Result_TestCase.Attribute_time = UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second).ToString();
                _Result_TestCase.Node_stepNumber = stepNumber++.ToString();
                _Result_TestCase.Node_description = _StepInfo.Descriptions;
                _Result_TestCase.Node_needToCheck = "";
                _Result_TestCase.Node_result = result;
                _IReporter.AddTestStep(_Result_TestCase);
            }  
        }
        private class TestFunctionInfo
        {
            public string ClassName { set; get; }
            public string ClassFullName { set; get; }
            public string FunctionName { set; get; }
            public string Descriptions = "NA";
            public string ExpectedResults = "NA";
            public bool DoNotBlock = false;

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
                    else if (attri.AttributeType.Name.Equals(typeof(DoNotBlock).Name))
                    {
                        this.DoNotBlock = true;
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
