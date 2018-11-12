using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{//            Dictionary<string, Func<object>> a = new Dictionary<string, Func<object>>();
    public class TestIt
    {
        PortalTestFlows _PortalTestFlows = new PortalTestFlows();
        Cmd _CMD = new Cmd();
        public void Run()
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
                    bool result = this.TestMatcher(s, testOptions);
                    if (result)
                    {
                        Console.Title += " >>>>> Tested Done! PASS";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.Title = ex.Message;
                    return;
                }  
            }
        }
       private bool IsTestExisted(string testName, string selectedNum, string loopName)
       {
            return $"{selectedNum.Trim()}{Cmd.STRING_CONNECTOR}{testName}".Equals(loopName);
       }
        private bool TestMatcher(string selected, List<string> options)
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
                else if (this.IsTestExisted(VMObj.Item_MM830.Name, selected, options[i]))
                {
                    List<string> deviceOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
                    return VMObj.Item_MM830.Name;
                }
                else if (this.IsTestExisted(VMObj.Item_MP860.Name, selected, options[i]))
                {
                    List<string> deviceOptions = _CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd);
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
    }
}
