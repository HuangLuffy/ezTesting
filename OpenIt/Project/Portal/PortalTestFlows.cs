using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalTestFlows
    {
        private string OPTION_LAUNCH_TEST = "Launch Test";
        private string OPTION_PLUGIN_OUT_TEST = "PlugInOut Test";
        private string OPTION_PLUGIN_OUT_Server = "PlugInOut Server";


        private string[] options_cmd;
        public string[] Options_Cmd
        {
            get { return new string[]{ OPTION_LAUNCH_TEST, OPTION_PLUGIN_OUT_TEST, OPTION_PLUGIN_OUT_Server }; }
            set { options_cmd = value; }
        }
        PortalTestActions _PortalTestActions = new PortalTestActions();
        public void Flow_PlugInOutTest()
        {
            for (int i = 1; i < 99999999; i++)
            {
                _PortalTestActions.LaunchTimes = i;
                _PortalTestActions.IsSWCrash();
            }
        }
        public void Flow_PlugInOutServer()
        {
            for (int i = 1; i < 99999999; i++)
            {
                _PortalTestActions.VMPlugOutDevice();
            }
        }
        public void Flow_LaunchTest()
        {
            for (int i = 1; i < 99999999; i++)
            {
                _PortalTestActions.LaunchTimes = i;
                _PortalTestActions.LaunchSW();
                _PortalTestActions.CloseSW();
                _PortalTestActions.IsSWCrash();
            }
        }
    }
}
