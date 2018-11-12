﻿using ATLib;
using CommonLib.Util;
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
    public class TestIt
    {
        public static string OPTION_MasterPlus = "MasterPlus";
        public static string OPTION_PORTAL = "PORTAL";

        public List<string> Options_Projects = new List<string> { OPTION_PORTAL, OPTION_MasterPlus };

        PortalTestFlows _PortalTestFlows;
        MasterPlusTestFlows _MasterPlusTestFlows;

        Cmd _CMD = new Cmd();
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
                        Console.WriteLine (" >>>>>>>>>>>>>> Tested Done! PASS");
                        return;
                    }
                    projectOptions = _CMD.WriteOptions(Options_Projects, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} >>>>>>>>>>>>>> Tested Done! PASS");
                    return;
                }
            }
        }
        public bool MasterPlusTestRun()
        {
            _MasterPlusTestFlows.Options_Cmd.Add(Cmd.OPTION_SHOW_MENU_AGAIN);
            _MasterPlusTestFlows.Options_Cmd.Add(Cmd.OPTION_BACK);
            List<string> testOptions = _CMD.WriteOptions(_MasterPlusTestFlows.Options_Cmd);
            while (true)
            {
                try
                {
                    string s = Console.ReadLine();
                    bool? result = this.MasterPlusTestMatcher(s, testOptions);
                    if (result == true)
                    {
                        return true;
                    }
                    else if (result == null)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Title = ex.Message;
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
                else if (this.IsTestExisted(Cmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(this._MasterPlusTestFlows.Options_Cmd);
                    return false;
                }
                else if (this.IsTestExisted(Cmd.OPTION_BACK, selected, options[i]))
                {
                    return null;
                }
            }
            return false;
        }
        private bool PortalTestMatcher(string selected, List<string> options)
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
                else if (this.IsTestExisted(PortalTestFlows.OPTION_PLUGIN_OUT_SERVER, selected, options[i]))
                {  
                    string name = "";
                    while (name.Equals(""))
                    {
                        name = this.DeviceMatcher();
                        if (Cmd.OPTION_BACK.Equals(name))
                        {
                            return false;
                        }
                    }
                    _PortalTestFlows.Flow_PlugInOutServer(name);
                    return true;
                }
                else if (this.IsTestExisted(Cmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                    return false;
                }
            }
            return false;
        }
        private string DeviceMatcher()
        {
            List<string> options = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
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
                    //List<string> deviceOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                    return VMObj.Item_MP750.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MM830.Name, selected, options[i]))
                {
                    //List<string> deviceOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                    return VMObj.Item_MM830.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MP860.Name, selected, options[i]))
                {
                    //List<string> deviceOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                    return VMObj.Item_MP860.Name;
                }
                else if (this.IsTestExisted(Cmd.OPTION_SHOW_MENU_AGAIN, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                }
                else if (this.IsTestExisted(Cmd.OPTION_BACK, selected, options[i]))
                {
                    _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                    return Cmd.OPTION_BACK;
                }
            }
            return "";
        }
        public bool PortalTestRun()
        {
            _PortalTestFlows.Options_Cmd.Add(Cmd.OPTION_SHOW_MENU_AGAIN);
            _PortalTestFlows.Options_Devices_Cmd.Add(Cmd.OPTION_SHOW_MENU_AGAIN);
            _PortalTestFlows.Options_Devices_Cmd.Add(Cmd.OPTION_BACK);
            List<string> testOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
            while (true)
            {
                try
                {
                    string s = Console.ReadLine();
                    bool? result = this.PortalTestMatcher(s, testOptions);
                    if (result == true)
                    {
                        return true;
                    }
                    else if (result == null)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Title = ex.Message;
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
                    return this.MasterPlusTestRun();
                }
                else if (this.IsTestExisted(TestIt.OPTION_PORTAL, selected, options[i]))
                {
                    this._PortalTestFlows = new PortalTestFlows();
                    return this.PortalTestRun();
                }
            }
            return null;
        }
        private bool IsTestExisted(string testName, string selectedNum, string loopName)
        {
            return $"{selectedNum.Trim()}{Cmd.STRING_CONNECTOR}{testName}".Equals(loopName);
        }
    }
}
