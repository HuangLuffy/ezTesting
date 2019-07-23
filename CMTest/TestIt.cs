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
        private const string OPTION_MasterPlus = "MasterPlus";
        private const string OPTION_PORTAL = "PORTAL";
        private const string FOUND_TEST = "FOUND_TEST";
        private const string DO_NOTHING = "DO_NOTHING";
        private List<string> Options_Projects = new List<string> { OPTION_PORTAL, OPTION_MasterPlus };

        PortalTestFlows _PortalTestFlows;
        MasterPlusTestFlows _MasterPlusTestFlows;

        UtilCmd _CMD = new UtilCmd();

        private dynamic Flow_CMD_SHOW_MENU_AGAIN()
        {
            _CMD.WriteOptions(_CMD.List_Current_Menu, lineUpWithNumber: false);
            return UtilCmd.OPTION_SHOW_MENU_AGAIN;
        }
        private dynamic Flow_CMD_Back()
        {
            _CMD.WriteOptions(_CMD.List_Last_Menu, lineUpWithNumber: false);
            return UtilCmd.OPTION_BACK;
        }

        private bool IsTestExisted(string testName, string selectedNum, string loopName)
        {
            return $"{selectedNum.Trim()}{UtilCmd.STRING_CONNECTOR}{testName}".Equals(loopName);
        }

        public void OpenCMD()
        {
            List<string> projectOptions = _CMD.WriteOptions(Options_Projects);
            while (true)
            {
                try
                {
                    projectOptions = _CMD.WriteOptions(projectOptions, true, false);
                    string s = Console.ReadLine();
                    string result = this.ProjectMatcher(s, projectOptions);
                    if (result.Equals(FOUND_TEST))
                    {
                        Console.WriteLine (" >>>>>>>>>>>>>> Test Done! PASS");
                        return;
                    }
                    projectOptions = _CMD.WriteOptions(Options_Projects, true);
                }
                catch (Exception ex)
                {
                    this.HandleWrongStepResult(ex.Message);
                    Console.WriteLine("Please press any key to continue.");
                    Console.ReadLine();
                }
            }
        }
        private string ProjectMatcher(string selected, List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (IsTestExisted(TestIt.OPTION_MasterPlus, selected, options[i]))
                {
                    _MasterPlusTestFlows = new MasterPlusTestFlows();
                    AssembleMasterPlusPlugInOutTests();
                    return ShowTestMenuAndRun(Options_MasterPlus_Tests_With_Funcs);
                }
                else if (IsTestExisted(TestIt.OPTION_PORTAL, selected, options[i]))
                {
                    _PortalTestFlows = new PortalTestFlows();
                    AssemblePortalPlugInOutTests();
                    return ShowTestMenuAndRun(Options_Portal_Tests_With_Funcs);
                }
            }
            return DO_NOTHING;
        }
        private string ShowTestMenuAndRun(IDictionary<string, Func<dynamic>> testFuncsByTestName)
        {
            List<string> testOptions = _CMD.WriteOptions(testFuncsByTestName.Keys.ToList());
            while (true)
            {
                testOptions = _CMD.WriteOptions(testOptions, true, false);
                string input = Console.ReadLine();
                string result = MatchTestAndRun(testFuncsByTestName, input, testOptions);
                if (result == null)
                {
                    //Back from submenu, so it should stay here.
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
        private string DeviceMatcher(List<string> options)
        {
            string selected = Console.ReadLine();
            string deviceName = "";
            for (int i = 0; i < options.Count; i++)
            {
                deviceName = GetSelectedName(Options_Portal_PlugInOut_Devices_Name, selected, options[i]);
                if (deviceName != null)
                {
                    return deviceName;
                }
            }
            return null;
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
        private string MatchTestAndRun(IDictionary<string, Func<dynamic>> testFuncsByTestName, string selected, List<string> options)
        {
            string testName = "";
            for (int i = 0; i < options.Count; i++)
            {
                testName = GetSelectedName(testFuncsByTestName.Keys.ToList<string>(), selected, options[i]);
                if (testName != null)
                {
                    return testFuncsByTestName[testName].Invoke();
                }
            }
            return testName;
        } 
    }
}
