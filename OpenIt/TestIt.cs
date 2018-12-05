using ATLib;
using CommonLib.Util;
using OpenIt.Project;
using OpenIt.Project.MasterPlus;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class TestIt : AbsResult
    {
        public const string OPTION_MasterPlus = "MasterPlus";
        public const string OPTION_PORTAL = "PORTAL";

        public List<string> Options_Projects = new List<string> { OPTION_PORTAL, OPTION_MasterPlus };

        PortalTestFlows _PortalTestFlows;
        MasterPlusTestFlows _MasterPlusTestFlows;

        UtilCmd _CMD = new UtilCmd();

        private bool IsTestExisted(string testName, string selectedNum, string loopName)
        {
            return $"{selectedNum.Trim()}{UtilCmd.STRING_CONNECTOR}{testName}".Equals(loopName);
        }

        public void Run()
        {
            List<string> projectOptions = _CMD.WriteOptions(Options_Projects);
            while (true)
            {
                try
                {
                    string s = Console.ReadLine();
                    bool? result = this.ProjectMatcher(s, projectOptions);
                    if (result == true)
                    {
                        Console.WriteLine (" >>>>>>>>>>>>>> Test Done! PASS");
                        return;
                    }
                    projectOptions = _CMD.WriteOptions(Options_Projects, true);
                }
                catch (Exception ex)
                {
                    this.HandleWrongStepResult(ex.Message);
                }
            }
        }

        private bool? ProjectMatcher(string selected, List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (this.IsTestExisted(TestIt.OPTION_MasterPlus, selected, options[i]))
                {
                    this._MasterPlusTestFlows = new MasterPlusTestFlows();
                    return this.TestRun(_MasterPlusTestFlows.Options_Cmd, this.MasterPlusTestMatcher);
                }
                else if (this.IsTestExisted(TestIt.OPTION_PORTAL, selected, options[i]))
                {
                    this._PortalTestFlows = new PortalTestFlows();
                    return this.TestRun(_PortalTestFlows.Options_Cmd, this.PortalTestMatcher);
                }
            }
            return null;
        }

        private bool TestRun(List<string> options, Func<string, List<string>, bool?> func)
        {
            options.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN);
            options.Add(UtilCmd.OPTION_BACK);
            List<string> testOptions = _CMD.WriteOptions(options);
            while (true)
            {
                string s = Console.ReadLine();
                bool? result = func(s, testOptions);
                if (result == true)
                {
                    return true;
                }
                else if (result == null)
                {
                    return false;
                }
            }
        }
        
        private bool? MasterPlusTestMatcher(string selected, List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (this.IsTestExisted(PortalTestFlows.OPTION_LAUNCH_TEST, selected, options[i]))
                {
                    this._MasterPlusTestFlows.Flow_LaunchTest();
                    return true;
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(this._MasterPlusTestFlows.Options_Cmd);
                    return false;
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_BACK, selected, options[i]))
                {
                    return null;
                }
            }
            return false;
        }

        private bool? PortalTestMatcher(string selected, List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (this.IsTestExisted(PortalTestFlows.OPTION_LAUNCH_TEST, selected, options[i]))
                {
                    _PortalTestFlows.Flow_LaunchTest();
                    return true;
                }
                else if (this.IsTestExisted(PortalTestFlows.OPTION_PLUGIN_OUT_TEST, selected, options[i]))
                {
                    _PortalTestFlows.Flow_PlugInOutTest();
                    return true;
                }
                else if (this.IsTestExisted(PortalTestFlows.OPTION_SIMPLE_PROFILES_SWITCH, selected, options[i]))
                {
                    _PortalTestFlows.Flow_ProfilesSimpleSwitch();
                    return true;
                }
                else if (this.IsTestExisted(PortalTestFlows.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, selected, options[i]))
                {
                    _PortalTestFlows.Flow_ProfilesImExSwitch();
                    return true;
                }
                else if (this.IsTestExisted(PortalTestFlows.OPTION_PLUGIN_OUT_SERVER, selected, options[i]))
                {  
                    string name = "";
                    while (name.Equals(""))
                    {
                        _PortalTestFlows.Options_Devices_Cmd.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN);
                        _PortalTestFlows.Options_Devices_Cmd.Add(UtilCmd.OPTION_BACK);
                        name = this.DeviceMatcher(_CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd));
                        if (UtilCmd.OPTION_BACK.Equals(name))
                        {
                            _PortalTestFlows.Options_Devices_Cmd.Remove(UtilCmd.OPTION_SHOW_MENU_AGAIN);
                            _PortalTestFlows.Options_Devices_Cmd.Remove(UtilCmd.OPTION_BACK);
                            return false;
                        }
                    }
                    _PortalTestFlows.Flow_PlugInOutServer(name);
                    return true;
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                    return false;
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_BACK, selected, options[i]))
                {
                    return null;
                }
            }
            return false;
        }

        private string DeviceMatcher(List<string> options)
        {
            string selected = Console.ReadLine();
            for (int i = 0; i < options.Count; i++)
            {
                if (this.IsTestExisted(VMObj.Item_MH752.Name, selected, options[i]))
                {
                    return VMObj.Item_MH752.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MK850.Name, selected, options[i]))
                {
                    _PortalTestFlows.Flow_PlugInOutTest();
                    return VMObj.Item_MK850.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MP750.Name, selected, options[i]))
                {
                    return VMObj.Item_MP750.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MM830.Name, selected, options[i]))
                {
                    return VMObj.Item_MM830.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MP860.Name, selected, options[i]))
                {
                    return VMObj.Item_MP860.Name;
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                }
                else if (this.IsTestExisted(UtilCmd.OPTION_BACK, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                    return UtilCmd.OPTION_BACK;
                }
            }
            return "";
        }  
    }
}
