using CMTest.Xml;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using CMTest.Project.MasterPlusPer;

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
            _optionsPortalTestsWithFuncs.Add(UtilCmd.OptionShowMenuAgain, _cmd.MenuShowAgain);
            _optionsPortalTestsWithFuncs.Add(UtilCmd.OptionBack, _cmd.MenuGoBack);
        }
        private void AssemblePortalPlugInOutDevices(bool fromConf = true)
        {
            if (_optionsPortalPlugInOutDeviceNames.Any()) return;
            _optionsPortalPlugInOutDeviceNames = UtilCloner.CloneList(_xmlOps.GetDeviceNameList());
            _optionsPortalPlugInOutDeviceNames.Add(UtilCmd.OptionShowMenuAgain);
            _optionsPortalPlugInOutDeviceNames.Add(UtilCmd.OptionBack);
        }
        private dynamic Flow_Portal_LaunchTest()
        {
            _portalTestFlows.Flow_LaunchTest();
            return FOUND_TEST;
        }
        private dynamic Flow_PlugInOutTest()
        {
            _portalTestFlows.Flow_PlugInOutTest();
            return FOUND_TEST;
        }
        private dynamic Flow_ProfilesSimpleSwitch()
        {
            _portalTestFlows.Flow_ProfilesSimpleSwitch();
            return FOUND_TEST;
        }
        private dynamic Flow_ProfilesImExAimpadSwitch()
        {
            _portalTestFlows.Flow_ProfilesImExAimpadSwitch();
            return FOUND_TEST;
        }
        private dynamic Flow_PlugInOutServer()
        {
            AssemblePortalPlugInOutDevices();
            var options = _cmd.WriteCmdMenu(_optionsPortalPlugInOutDeviceNames);
            while (true)
            {
                options = _cmd.WriteCmdMenu(options, true, false);
                var input = UtilCmd.ReadLine();
                var deviceName = FindMatchedDevice(_optionsPortalPlugInOutDeviceNames, input, options);
                switch (deviceName)
                {
                    case null:
                        continue;
                    case UtilCmd.OptionBack:
                        return null; //cannot return "back" since it needs to stand at previous level.
                    case UtilCmd.OptionShowMenuAgain:
                        UtilCmd.Clear();
                        break;
                    default:
                        RunDirectly_Flow_PlugInOutServer(deviceName, _xmlOps);
                        return FOUND_TEST;
                }
            }
        }

        private void RunDirectly_Flow_PlugInOutServer(string deviceName, XmlOps deviceInfo)
        {
            _portalTestFlows.Flow_PlugInOutServer(deviceName, deviceInfo);
        }
    }
}
