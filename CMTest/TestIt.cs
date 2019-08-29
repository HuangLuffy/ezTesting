using CommonLib.Util;
using CMTest.Project;
using CMTest.Project.MasterPlus;
using CMTest.Project.Portal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMTest
{
    public partial class TestIt : AbsResult
    {
        private const string FOUND_TEST = "FOUND_TEST";
        private const string DO_NOTHING = "DO_NOTHING";
        private readonly PortalTestFlows _portalTestFlows ;
        private readonly MasterPlusTestFlows _masterPlusTestFlows;
        private readonly List<string> _optionsProjects;
        private readonly UtilCmd _cmd = new UtilCmd();

        public TestIt()
        {
            _masterPlusTestFlows = new MasterPlusTestFlows();
            _portalTestFlows = new PortalTestFlows();
            _optionsProjects = new List<string> { _portalTestFlows.PortalTestActions.SwName, _masterPlusTestFlows.MasterPlusTestActions.SwName };
        }
        public struct RunDirectly
        {
            public bool run;
            public string device;
        }
        public void OpenCmd()
        {
            var projectOptions = _cmd.WriteCmdMenu(_optionsProjects);
            while (true)
            {
                try
                {
                    projectOptions = _cmd.WriteCmdMenu(projectOptions, true, false);
                    var s = UtilCmd.ReadLine();
                    var result = ShowCmdProjects(s, projectOptions);
                    if (result.Equals(FOUND_TEST))
                    {
                        UtilCmd.WriteLine (" >>>>>>>>>>>>>> Test Done! PASS");
                        return;
                    }
                    projectOptions = _cmd.WriteCmdMenu(_optionsProjects, true);
                }
                catch (Exception ex)
                {
                    HandleWrongStepResult(ex.Message);
                    UtilCmd.PressAnyContinue();
                }
            }
        }
        private string ShowCmdProjects(string selected, IEnumerable<string> options)
        {
            foreach (var option in options)
            {
                if (IsTestExisted(_masterPlusTestFlows.MasterPlusTestActions.SwName, selected, option))
                {
                    AssembleMasterPlusPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(_optionsMasterPlusTestsWithFuncs);
                }
                if (IsTestExisted(_portalTestFlows.PortalTestActions.SwName, selected, option))
                {
                    AssemblePortalPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(_optionsPortalTestsWithFuncs);
                }
            }
            return DO_NOTHING;
        }
        private string ShowCmdTestsBySelectedProject(IDictionary<string, Func<dynamic>> testFuncsByTestName)
        {
            var options = _cmd.WriteCmdMenu(testFuncsByTestName.Keys.ToList());
            while (true)
            {
                options = _cmd.WriteCmdMenu(options, true, false);
                var input = UtilCmd.ReadLine();
                var result = FindMatchedTest(testFuncsByTestName, input, options);
                switch (result)
                {
                    case null:
                        //Back from submenu, so it should stay here. Or incorrect input
                        break; // break只终止了最近的switch，并没有终止while
                    case FOUND_TEST:
                        return FOUND_TEST;
                    case UtilCmd.OptionBack:
                        return UtilCmd.OptionBack;
                    default:
                        continue; // continue不止跳出了switch，还跳过了这一次while循环，没有输出。
                }
            }
        }
        private static T FindMatchedOption<T>(IReadOnlyList<string> listAll, string selected, IEnumerable<string> comparedOptions)
        {
            foreach (var deviceName in comparedOptions.Select(comparedOption => GetSelectedName(listAll, selected, comparedOption)).Where(deviceName => deviceName != null))
            {
                return (T)Convert.ChangeType(deviceName, typeof(T));
            }
            //foreach (var comparedOption in comparedOptions)
            //{
            //    var deviceName = GetSelectedName(listAll, selected, comparedOption);
            //    if (deviceName != null)
            //    {
            //        return (T)Convert.ChangeType(deviceName, typeof(T));
            //    }
            //}
            return default(T);
        }
        private static string FindMatchedDevice(IReadOnlyList<string> Options_Portal_PlugInOut_Device_Names, string selected, IEnumerable<string> comparedOptions)
        {
            return FindMatchedOption<string>(Options_Portal_PlugInOut_Device_Names, selected, comparedOptions);
        }
        private static string FindMatchedTest(IDictionary<string, Func<dynamic>> testFuncsByTestName, string selected, IEnumerable<string> comparedOptions)
        {
            var t = FindMatchedOption<string>(testFuncsByTestName.Keys.ToList(), selected, comparedOptions);
            return t == null ? null : testFuncsByTestName[t].Invoke();
        }
        private static string GetSelectedName(IEnumerable<string> listAll, string selectedNum, string comparedOptions)
        {
            return listAll.FirstOrDefault(t => IsTestExisted(t, selectedNum, comparedOptions));
            //for (var j = 0; j < listAll.Count; j++)
            //{
            //    if (IsTestExisted(listAll[j], selectedNum, comparedOptions))
            //    {
            //        return listAll[j];
            //    }
            //}
            //return null;
        }
        private static bool IsTestExisted(string testName, string selectedNum, string loopName)
        {
            return $"{selectedNum.Trim()}{UtilCmd.StringConnector}{testName}".Equals(loopName);
        }
    }
}
