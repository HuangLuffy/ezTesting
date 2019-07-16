using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalTestFlows
    {
        public const string OPTION_LAUNCH_TEST = "Launch Test";
        public const string OPTION_PLUGIN_OUT_TEST = "PlugInOut Test";
        public const string OPTION_PLUGIN_OUT_SERVER = "PlugInOut Server";
        public const string OPTION_SIMPLE_PROFILES_SWITCH = "Profiles Simple Switch Test";
        public const string OPTION_IMPORT_EXPORT_PROFILES_SWITCH = "Profiles Import Export Switch Test";
        public const string OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH = "Profiles Import Export Switch AIMPAD Test";


        public static long TEST_TIMES = 9999999;

        public List<string> Options_Cmd = new List<string> { OPTION_LAUNCH_TEST, OPTION_PLUGIN_OUT_TEST, OPTION_PLUGIN_OUT_SERVER, OPTION_SIMPLE_PROFILES_SWITCH, OPTION_IMPORT_EXPORT_PROFILES_SWITCH, OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH };

        public List<string> Options_Devices_Cmd = new List<string> { VMObj.Item_MM830.Name, VMObj.Item_MP860.Name, VMObj.Item_MK850.Name, VMObj.Item_MH752.Name, VMObj.Item_MP750.Name, VMObj.Item_MH650.Name };

        PortalTestActions _PortalTestActions = new PortalTestActions();

        public void Flow_PlugInOutTest()
        {
            _PortalTestActions.LaunchSW();
            for (int i = 1; i < TEST_TIMES; i++)
            {
                _PortalTestActions.SetlaunchTimesAndWriteTestTitle(i);
                _PortalTestActions.IsSWCrash(1,3);
            }
        }
        public void Flow_PlugInOutServer(string deviceNameVM)
        {
            for (int i = 1; i < TEST_TIMES; i++)
            {
                _PortalTestActions.SetlaunchTimesAndWriteTestTitle(i);
                if (deviceNameVM.Equals(VMObj.Item_MH650.Name)) // Show 2 identical devices in VM.
                {
                    _PortalTestActions.VMPlugOutDeviceForShowingTwoidentical(deviceNameVM);
                }
                else
                {
                    _PortalTestActions.PlugOutDeviceFromVM(deviceNameVM);
                }
            }
        }
        public void Flow_LaunchTest()
        {
            for (int i = 1; i < TEST_TIMES; i++)
            {
                _PortalTestActions.SetlaunchTimesAndWriteTestTitle(i);
                _PortalTestActions.LaunchSW();
                _PortalTestActions.CloseSW();
                _PortalTestActions.IsSWCrash(1);
            }
        }
        public void Flow_ProfilesSimpleSwitch()
        {
            _PortalTestActions.SetlaunchTimesAndWriteTestTitle(1);
            _PortalTestActions.LaunchSW();
            _PortalTestActions.ProfilesSimpleSwitch(TEST_TIMES);
        }
        public void Flow_ProfilesImExSwitch()
        {
            _PortalTestActions.SetlaunchTimesAndWriteTestTitle(1);
            _PortalTestActions.LaunchSW();
            _PortalTestActions.ProfilesImExSwitch(TEST_TIMES);
        }
        public void Flow_ProfilesImExAimpadSwitch()
        {
            _PortalTestActions.SetlaunchTimesAndWriteTestTitle(1);
            _PortalTestActions.LaunchSW();
            _PortalTestActions.ProfilesImExAimpadSwitch(TEST_TIMES);
        }
    }
}
