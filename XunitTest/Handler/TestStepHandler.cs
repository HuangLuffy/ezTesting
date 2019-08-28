using CommonLib.Util;
using CommonLib.Util.os;
using ReportLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestLib;
using XUnitTest.Manager;
using static CommonLib.Util.UtilCapturer;
using static CommonLib.Util.UtilTime;
using static ReportLib.Reporter;

namespace XUnitTest.Handler
{
    public class TestStepHandler : TestStep
    {
        //private string folderNameProject = "";
        //private string folderNameTest = "";
        //private string folderNameScreenshots = "Shots";

        private bool _needToBlockAllTests;
        //private string _pathReportFile;
        private int _stepNumber = 1;
        private const string BlockedDescription = "This step is blocked since the previous step was failed.";

        private const string DefaultContent = "NA";
        private readonly IReporter _iReporter;
        private readonly ResultTestInfo _resultTestInfo;
        private string _manualCheckLink = DefaultContent;
        public TestStepHandler(string pathReportXml = "")
        {
            _iReporter = ReporterManager.GeReporter(pathReportXml);
            _resultTestInfo = new ResultTestInfo
            {
                AttrProject = "CM",
                AttrOs = UtilOs.GetOsVersion(),
                AttrLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name.Replace("-", "_"),
                AttrRegion = System.Globalization.CultureInfo.InstalledUICulture.Name.Split('-')[1],
                AttrDeviceModel = DefaultContent,
                AttrDeviceName = DefaultContent,
                AttrVersion = DefaultContent,
                AttrTests = 0,
                AttrPasses = 0,
                AttrFailures = 0,
                AttrTbds = 0,
                AttrBlocks = 0
            };

            //(string project, string os, string language, string region, string time, string deviceModel, string deviceName, string testTotalNumber, string version, string name, string testName
            //, string testName, string testName, string testName, string testName, string testName)
           // _pathReportFile = pathReportXml;
        }

        public void Capture(string pathSave, string comment = "Shot", ImageType imageType = ImageType.PNG)
        {
            _manualCheckLink += _iReporter.SetManualCheck(comment, pathSave);
            UtilCapturer.Capture(pathSave, imageType);
        }
        private T Exec<T>(Func<T> action)
        {
            if (_needToBlockAllTests)
                return default(T);
            //var dt = DateTime.Now;
            var result = Execute(action);
            return result;
        }
        public void Exec(Action action)
        {
            var xunitInfo = new TestFunctionInfo(3, _iReporter);
            _resultTestInfo.AttrName = xunitInfo.ClassName;
            var stepInfo = new TestFunctionInfo(2, _iReporter);
            var resultTestCase = new ResultTestCase();
            var result = string.Empty;
            var dt = DateTime.Now;
            try
            {
                if (stepInfo.DoNotBlock)
                {
                    _needToBlockAllTests = false;
                }
                if (_needToBlockAllTests)
                {
                    result = Result.BLOCK;
                    _resultTestInfo.AttrBlocks += 1;
                }
                else
                {
                    Execute(action);
                    result = Result.PASS;
                    _resultTestInfo.AttrPasses += 1;
                }
            }
            catch (Exception)
            {
                result = Result.FAIL;
                _resultTestInfo.AttrFailures += 1;
                _needToBlockAllTests = true;
                throw;
            }
            finally
            {
                Capture("End Shot", "");

                _resultTestInfo.AttrTests += 1;

                resultTestCase.AttrClassname = xunitInfo.ClassFullName;
                resultTestCase.AttrName = xunitInfo.FunctionName;
                resultTestCase.AttrTime = DateDiff(dt, DateTime.Now);
                resultTestCase.NodeStepNumber = _stepNumber++;
                resultTestCase.NodeDescription = stepInfo.Descriptions;
                resultTestCase.NodeExpectedResult = stepInfo.ExpectedResults;
                resultTestCase.NodeNeedToCheck = _manualCheckLink;
                resultTestCase.NodeResult = result;

                _iReporter.AddTestStep(resultTestCase);
                _resultTestInfo.AttrTestName = xunitInfo.ClassName;
                _resultTestInfo.AttrTime += resultTestCase.AttrTime;
                _resultTestInfo.AttrPassesPercent = _iReporter.GetResultPercent(_resultTestInfo.AttrPasses, _resultTestInfo.AttrTests, 1);
                _resultTestInfo.AttrFailuresPercent = _iReporter.GetResultPercent(_resultTestInfo.AttrFailures, _resultTestInfo.AttrTests, 1);
                _resultTestInfo.AttrErrorsPercent = _iReporter.GetResultPercent(_resultTestInfo.AttrErrors, _resultTestInfo.AttrTests, 1);
                _resultTestInfo.AttrBlocksPercent = _iReporter.GetResultPercent(_resultTestInfo.AttrBlocks, _resultTestInfo.AttrTests, 1);
                _resultTestInfo.AttrTbdsPercent = _iReporter.GetResultPercent(_resultTestInfo.AttrTbds, _resultTestInfo.AttrTests, 1);
                _iReporter.ModifyTestInfo(_resultTestInfo);
            }  
        }
        private class TestFunctionInfo
        {
            public string ClassName { private set; get; }
            public string ClassFullName { private set; get; }
            public string FunctionName { private set; get; }
            public readonly string Descriptions = DefaultContent;
            public readonly string ExpectedResults = DefaultContent;
            public readonly bool DoNotBlock;

            public TestFunctionInfo(int level, IReporter iReporter)
            {
                StackFrame frame = new StackTrace().GetFrame(level);
                ClassName = frame.GetMethod().ReflectedType?.Name;
                ClassFullName = frame.GetMethod().ReflectedType?.FullName;
                FunctionName = frame.GetMethod().Name;
                foreach (var attri in frame.GetMethod().CustomAttributes)
                {
                    if (attri.AttributeType.Name.Equals(typeof(Descriptions).Name))
                    {
                        Descriptions = AssembleStepInstructions(attri.ConstructorArguments[0].Value, iReporter);
                    }
                    else if (attri.AttributeType.Name.Equals(typeof(ExpectedResults).Name))
                    {
                        ExpectedResults = AssembleStepInstructions(attri.ConstructorArguments[0].Value, iReporter);
                    }
                    else if (attri.AttributeType.Name.Equals(typeof(DoNotBlock).Name))
                    {
                        DoNotBlock = true;
                    }
                }
            }
            private string AssembleStepInstructions(object cads, IReporter iReporter)
            {
                var t = "";
                var list = (IReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)cads;
                t = list.Count == 1 ? list.ElementAt(0).Value.ToString() : list.Aggregate(t, (current, item) => current + iReporter.SetNewLine(item.Value.ToString()));
                //if (list.Count() == 1)
                //{
                //    t = list.ElementAt(0).Value.ToString();
                //}
                //else
                //{
                //    foreach (var item in list)
                //    {
                //        t += iReporter.SetNewLine(item.Value.ToString());
                //    }
                //}
                return t;
            }
        }
    }
}
