using CMTest.Project.Portal;
using CMTest.Vm;
using CMTest.Xml;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMTest
{
    public partial class TestIt
    {
        IDictionary<string, Func<dynamic>> Options_Portal_Tests_With_Funcs = new Dictionary<string, Func<dynamic>>();
        public List<string> Options_Portal_PlugInOut_Device_Names = new List<string>();
        XmlOps _XmlOps = new XmlOps();
        private void AssemblePortalPlugInOutTests()
        {
            if (Options_Portal_Tests_With_Funcs.Count() == 0)
            {
                //Select Back that will trigger this function again, so try-catch.
                Options_Portal_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_LAUNCH_TEST, Flow_Portal_LaunchTest);
                Options_Portal_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_PLUGIN_OUT_SERVER, Flow_PlugInOutServer);
                Options_Portal_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_SIMPLE_PROFILES_SWITCH, Flow_ProfilesSimpleSwitch);
                Options_Portal_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
                Options_Portal_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
                Options_Portal_Tests_With_Funcs.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN, _CMD.MenuShowAgain);
                Options_Portal_Tests_With_Funcs.Add(UtilCmd.OPTION_BACK, _CMD.MenuGoback);
            }
        }
        private void AssemblePortalPlugInOutDevices(bool fromConf = true)
        {
            if (Options_Portal_PlugInOut_Device_Names.Count() == 0)
            {
                Options_Portal_PlugInOut_Device_Names = UtilCloner.CloneList(_XmlOps.GetDeviceNameList());
                //Options_Portal_PlugInOut_Device_Names = UtilCloner.CloneList(VMObj.GetDevicesItemList());
                Options_Portal_PlugInOut_Device_Names.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN);
                Options_Portal_PlugInOut_Device_Names.Add(UtilCmd.OPTION_BACK);
            }
        }
        private dynamic Flow_Portal_LaunchTest()
        {
            _PortalTestFlows.Flow_LaunchTest();
            return FOUND_TEST;
        }
        private dynamic Flow_PlugInOutTest()
        {
            _PortalTestFlows.Flow_PlugInOutTest();
            return FOUND_TEST;
        }
        private dynamic Flow_ProfilesSimpleSwitch()
        {
            _PortalTestFlows.Flow_ProfilesSimpleSwitch();
            return FOUND_TEST;
        }
        private dynamic Flow_ProfilesImExAimpadSwitch()
        {
            _PortalTestFlows.Flow_ProfilesImExAimpadSwitch();
            return FOUND_TEST;
        }
        private dynamic Flow_PlugInOutServer()
        {
            AssemblePortalPlugInOutDevices();
            string deviceName = "";
            while (true)
            {
                deviceName = FindMatchedDevice(_CMD.WriteCmdMenu(Options_Portal_PlugInOut_Device_Names));
                if (deviceName != null)
                {
                    if (UtilCmd.OPTION_BACK.Equals(deviceName))
                    {
                        return null; //cannot return "back" since it needs to stand at previous level.
                    }
                    else if (UtilCmd.OPTION_SHOW_MENU_AGAIN.Equals(deviceName))
                    {
                        _CMD.Clear();
                    }
                    else
                    {
                        _PortalTestFlows.Flow_PlugInOutServer(deviceName, _XmlOps.GetWaitTime(deviceName), _XmlOps.GetIndex(deviceName));
                        return FOUND_TEST;
                    }
                }      
            }
        }
    }
}
