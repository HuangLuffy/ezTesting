﻿using CMTest.Xml;
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
        private readonly IDictionary<string, Func<dynamic>> _optionsPortalPlugInOutDeviceNames = new Dictionary<string, Func<dynamic>>();
        private void AssemblePortalPlugInOutTests(bool fromConf = true)
        {
            AssemblePortalPlugInOutDevices(fromConf);
            if (_optionsPortalTestsWithFuncs.Any()) return;
            //Select Back that will trigger this function again, so try-catch.
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_LAUNCH_TEST, Flow_Portal_LaunchTest);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_PLUGIN_OUT_SERVER, () => { return _cmd.ShowCmdMenu(_optionsPortalPlugInOutDeviceNames, _optionsPortalTestsWithFuncs); });
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_SIMPLE_PROFILES_SWITCH, Flow_ProfilesSimpleSwitch);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH, Flow_ProfilesImExAimpadSwitch);
            _optionsPortalTestsWithFuncs.Add(PortalTestFlows.TestNames.OPTION_INSTALLATION, Flow_Installation);
            _optionsPortalTestsWithFuncs.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }
        private void AssemblePortalPlugInOutDevices(bool fromConf = true)
        {
            if (_optionsPortalPlugInOutDeviceNames.Any()) return;
            //_optionsPortalPlugInOutDeviceNames = UtilCloner.CloneList(_xmlOps.GetDeviceNameList());
            //_optionsPortalPlugInOutDeviceNames.Add(UtilCmd.OptionShowMenuAgain);
            foreach (var item in _xmlOps.GetDeviceNameList())
            {
                _optionsPortalPlugInOutDeviceNames.Add(item, () => { return RunDirectly_Flow_PlugInOutServer(item, _xmlOps); });
            }
            _optionsPortalPlugInOutDeviceNames.Add(UtilCmd.Result.BACK, _cmd.MenuGoBack);
        }
        private dynamic Flow_Portal_LaunchTest()
        {
            _portalTestFlows.Flow_LaunchTest();
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_PlugInOutTest()
        {
            _portalTestFlows.Flow_PlugInOutTest();
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_ProfilesSimpleSwitch()
        {
            _portalTestFlows.Flow_ProfilesSimpleSwitch();
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_ProfilesImExAimpadSwitch()
        {
            _portalTestFlows.Flow_ProfilesImExAimpadSwitch();
            return MARK_FOUND_RESULT;
        }
        private dynamic RunDirectly_Flow_PlugInOutServer(string deviceName, XmlOps deviceInfo)
        {
            _portalTestFlows.Flow_PlugInOutServer(deviceName, deviceInfo);
            return MARK_FOUND_RESULT;
        }
        private dynamic Flow_Installation()
        {
            _portalTestFlows.Flow_Installation(_xmlOps, true);
            return MARK_FOUND_RESULT;
        }
    }
}
