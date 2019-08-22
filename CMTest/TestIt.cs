using ATLib;
using CommonLib.Util;
using CMTest.Project;
using CMTest.Project.MasterPlus;
using CMTest.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest
{
    public partial class TestIt : AbsResult
    {
        private const string FOUND_TEST = "FOUND_TEST";
        private const string DO_NOTHING = "DO_NOTHING";
        private PortalTestFlows _PortalTestFlows ;
        private MasterPlusTestFlows _MasterPlusTestFlows;
        private List<string> Options_Projects;
        private UtilCmd _CMD = new UtilCmd();

        public TestIt()
        {
            _MasterPlusTestFlows = new MasterPlusTestFlows();
            _PortalTestFlows = new PortalTestFlows();
            Options_Projects = new List<string> { _PortalTestFlows._PortalTestActions.SwName, _MasterPlusTestFlows._MasterPlusTestActions.SwName };
        }
        public struct RunDirectly
        {
            public bool run;
            public string device;
        }
        public void OpenCmd()
        {
            var projectOptions = _CMD.WriteCmdMenu(Options_Projects);
            while (true)
            {
                try
                {
                    projectOptions = _CMD.WriteCmdMenu(projectOptions, true, false);
                    var s = _CMD.ReadLine();
                    var result = ShowCmdProjects(s, projectOptions);
                    if (result.Equals(FOUND_TEST))
                    {
                        _CMD.WriteLine (" >>>>>>>>>>>>>> Test Done! PASS");
                        return;
                    }
                    projectOptions = _CMD.WriteCmdMenu(Options_Projects, true);
                }
                catch (Exception ex)
                {
                    HandleWrongStepResult(ex.Message);
                    _CMD.PressAnyContinue();
                }
            }
        }
        private string ShowCmdProjects(string selected, IEnumerable<string> options)
        {
            foreach (var option in options)
            {
                if (IsTestExisted(_MasterPlusTestFlows._MasterPlusTestActions.SwName, selected, option))
                {
                    AssembleMasterPlusPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(Options_MasterPlus_Tests_With_Funcs);
                }
                else if (IsTestExisted(_PortalTestFlows._PortalTestActions.SwName, selected, option))
                {
                    AssemblePortalPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(Options_Portal_Tests_With_Funcs);
                }
            }
            return DO_NOTHING;
        }
        private string ShowCmdTestsBySelectedProject(IDictionary<string, Func<dynamic>> testFuncsByTestName)
        {
            var options = _CMD.WriteCmdMenu(testFuncsByTestName.Keys.ToList());
            while (true)
            {
                options = _CMD.WriteCmdMenu(options, true, false);
                string input = _CMD.ReadLine();
                string result = FindMatchedTest(testFuncsByTestName, input, options);
                if (result == null)
                {
                    //Back from submenu, so it should stay here. Or incorrect input
                }
                else if (result.Equals(FOUND_TEST))
                {
                    return FOUND_TEST;
                }
                else if (result.Equals(UtilCmd.OPTION_BACK))
                {
                    return UtilCmd.OPTION_BACK;
                }
            }
        }
        private T FindMatchedOption<T>(IReadOnlyList<string> listAll, string selected, List<string> comparedOptions)
        {
            foreach (var comparedOption in comparedOptions)
            {
                string deviceName = GetSelectedName(listAll, selected, comparedOption);
                if (deviceName != null)
                {
                    return (T)Convert.ChangeType(deviceName, typeof(T));
                }
            }
            return default(T);
        }
        private string FindMatchedDevice(IReadOnlyList<string> Options_Portal_PlugInOut_Device_Names, string selected, List<string> comparedOptions)
        {
            return FindMatchedOption<string>(Options_Portal_PlugInOut_Device_Names, selected, comparedOptions);
        }
        private string FindMatchedTest(IDictionary<string, Func<dynamic>> testFuncsByTestName, string selected, List<string> comparedOptions)
        {
            var t = FindMatchedOption<string>(testFuncsByTestName.Keys.ToList(), selected, comparedOptions);
            return t == null ? null : testFuncsByTestName[t].Invoke();
        }
        private string GetSelectedName(IReadOnlyList<string> listAll, string selectedNum, string comparedOptions)
        {
            for (var j = 0; j < listAll.Count(); j++)
            {
                if (this.IsTestExisted(listAll[j], selectedNum, comparedOptions))
                {
                    return listAll[j];
                }
            }
            return null;
        }
        private bool IsTestExisted(string testName, string selectedNum, string loopName)
        {
            return $"{selectedNum.Trim()}{UtilCmd.STRING_CONNECTOR}{testName}".Equals(loopName);
        }
    }
}
