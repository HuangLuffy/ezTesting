using CMTest.Project.Portal;
using CMTest.Xml;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMTest
{
    public partial class TestIt
    {
        private readonly IDictionary<string, Func<dynamic>> _optionsPortalTestsWithFuncs = new Dictionary<string, Func<dynamic>>();
        private List<string> _optionsPortalPlugInOutDeviceNames = new List<string>();
        private readonly XmlOps _xmlOps = new XmlOps();
        private void AssemblePortalPlugInOutTests()
        {
            if (_optionsPortalTestsWithFuncs.Any()) return;
            //Select Back that will trigger this function again, so try-catch.
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_LAUNCH_TEST, Flow_Portal_LaunchTest);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_PLUGIN_OUT_SERVER, Flow_PlugInOutServer);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_SIMPLE_PROFILES_SWITCH, Flow_ProfilesSimpleSwitch);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
            _optionsPortalTestsWithFuncs.Add(UtilCmd.OptionShowMenuAgain, _CMD.MenuShowAgain);
            _optionsPortalTestsWithFuncs.Add(UtilCmd.OptionBack, _CMD.MenuGoBack);
        }
        private void AssemblePortalPlugInOutDevices(bool fromConf = true)
        {
            if (_optionsPortalPlugInOutDeviceNames.Any()) return;
            _optionsPortalPlugInOutDeviceNames = UtilCloner.CloneList(_xmlOps.GetDeviceNameList());
            //Options_Portal_PlugInOut_Device_Names = UtilCloner.CloneList(VMObj.GetDevicesItemList());
            _optionsPortalPlugInOutDeviceNames.Add(UtilCmd.OptionShowMenuAgain);
            _optionsPortalPlugInOutDeviceNames.Add(UtilCmd.OptionBack);
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
            var options = _CMD.WriteCmdMenu(_optionsPortalPlugInOutDeviceNames);
            while (true)
            {
                options = _CMD.WriteCmdMenu(options, true, false);
                var input = _CMD.ReadLine();
                var deviceName = FindMatchedDevice(_optionsPortalPlugInOutDeviceNames, input, options);
                switch (deviceName)
                {
                    case null:
                        continue;
                    case UtilCmd.OptionBack:
                        return null; //cannot return "back" since it needs to stand at previous level.
                    case UtilCmd.OptionShowMenuAgain:
                        _CMD.Clear();
                        break;
                    default:
                        RunDirectly_Flow_PlugInOutServer(deviceName);
                        return FOUND_TEST;
                }
            }
        }
        public void RunDirectly_Flow_PlugInOutServer(string deviceName)
        {
            _PortalTestFlows.Flow_PlugInOutServer(deviceName, _xmlOps.GetWaitTime(deviceName), _xmlOps.GetIndex(deviceName));
        }
    }
}
