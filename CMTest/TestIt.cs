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

        public void OpenCMD()
        {
            List<string> projectOptions = _CMD.WriteCmdMenu(Options_Projects);
            while (true)
            {
                try
                {
                    projectOptions = _CMD.WriteCmdMenu(projectOptions, true, false);
                    string s = _CMD.ReadLine();
                    string result = ShowCmdProjects(s, projectOptions);
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
        private string ShowCmdProjects(string selected, List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (IsTestExisted(_MasterPlusTestFlows._MasterPlusTestActions.SwName, selected, options[i]))
                {
                    AssembleMasterPlusPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(Options_MasterPlus_Tests_With_Funcs);
                }
                else if (IsTestExisted(_PortalTestFlows._PortalTestActions.SwName, selected, options[i]))
                {
                    AssemblePortalPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(Options_Portal_Tests_With_Funcs);
                }
            }
            return DO_NOTHING;
        }
        private string ShowCmdTestsBySelectedProject(IDictionary<string, Func<dynamic>> testFuncsByTestName)
        {
            List<string> options = _CMD.WriteCmdMenu(testFuncsByTestName.Keys.ToList());
            string input = "";
            while (true)
            {
                options = _CMD.WriteCmdMenu(options, true, false);
                input = _CMD.ReadLine();
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
        private T FindMatchedOption<T>(List<string> list_all, string selected, List<string> compared_options)
        {
            string deviceName = "";
            for (int i = 0; i < compared_options.Count; i++)
            {
                deviceName = GetSelectedName(list_all, selected, compared_options[i]);
                if (deviceName != null)
                {
                    return (T)Convert.ChangeType(deviceName, typeof(T));
                }
            }
            return default(T);
        }
        private string FindMatchedDevice(List<string> Options_Portal_PlugInOut_Device_Names, string selected, List<string> compared_options)
        {
            return FindMatchedOption<string>(Options_Portal_PlugInOut_Device_Names, selected, compared_options);
        }
        private string FindMatchedTest(IDictionary<string, Func<dynamic>> testFuncsByTestName, string selected, List<string> compared_options)
        {
            var t = FindMatchedOption<string>(testFuncsByTestName.Keys.ToList(), selected, compared_options);
            if (t == null)
            {
                return null;
            }
            return testFuncsByTestName[t].Invoke();
        }
        private string GetSelectedName(List<string> list_all, string selected_num, string compared_option)
        {
            for (int j = 0; j < list_all.Count(); j++)
            {
                if (this.IsTestExisted(list_all[j], selected_num, compared_option))
                {
                    return list_all[j];
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
