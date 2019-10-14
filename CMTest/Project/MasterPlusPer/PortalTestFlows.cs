using System;
using System.Collections.Generic;
using CMTest.Vm;
using CMTest.Xml;
using CommonLib.Util;

namespace CMTest.Project.MasterPlusPer
{
    public class PortalTestFlows
    {
        public struct TestNames{
            public const string OPTION_LAUNCH_TEST = "Launch Test";
            public const string OPTION_PLUGIN_OUT_TEST = "PlugInOut Test";
            public const string OPTION_PLUGIN_OUT_SERVER = "PlugInOut Server";
            public const string OPTION_SIMPLE_PROFILES_SWITCH = "Profiles Simple Switch Test";
            public const string OPTION_IMPORT_EXPORT_PROFILES_SWITCH = "Profiles Import Export Switch Test";
            public const string OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH = "Profiles Import Export Switch AIMPAD Test";
        }

        private static readonly long TestTimes = 9999999;

        public List<string> OptionsPortalPlugInOutDevicesName = VmObj.GetDevicesItemList();
        //new List<string> { VMObj.DeviceItem.Item_MM830.Name, VMObj.DeviceItem.Item_MP860.Name, VMObj.DeviceItem.Item_MK850.Name, VMObj.DeviceItem.Item_MH752.Name, VMObj.DeviceItem.Item_MP750.Name, VMObj.DeviceItem.Item_MH650.Name };

        public readonly PortalTestActions PortalTestActions = new PortalTestActions();

        public void Flow_PlugInOutTest()
        {
            PortalTestActions.LaunchSw();
            for (var i = 1; i < TestTimes; i++)
            {
                PortalTestActions.SetLaunchTimesAndWriteTestTitle(i);
                PortalTestActions.IsSwCrash(1, 3);
            }
        }
        //public void Flow_PlugInOutServer(string deviceNameVm, string waitTime, string index, string runTimes)
        public void Flow_PlugInOutServer(string deviceNamesVm, XmlOps deviceInfo)
        {
            var testTimes = 8;
            try
            {
                testTimes = Convert.ToInt32(deviceInfo.GetRunTimes(deviceNamesVm));
            }
            catch (Exception)
            {
                // ignored
            }
            var waitTime = deviceInfo.GetWaitTime(deviceNamesVm);
            var timeout = Convert.ToInt16(waitTime);
            for (var i = 1; i < testTimes; i++)
            {
                var devices = UtilRegrex.GetSplitArray(deviceNamesVm, ",");
                foreach (var device in devices)
                {
                    var index = deviceInfo.GetIndex(device);
                    PortalTestActions.SetLaunchTimesAndWriteTestTitle(i);
                    PortalTestActions.PlugOutDeviceFromVm(device, waitTime, index);
                }
                SW.WriteConsoleTitle(i, $"Waiting for connecting/disconnecting. ({timeout}s)", timeout);
            }
        }
        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TestTimes; i++)
            {
                PortalTestActions.SetLaunchTimesAndWriteTestTitle(i);
                PortalTestActions.LaunchSw();
                PortalTestActions.CloseSw();
                PortalTestActions.IsSwCrash(1);
            }
        }
        public void Flow_ProfilesSimpleSwitch()
        {
            PortalTestActions.SetLaunchTimesAndWriteTestTitle(1);
            PortalTestActions.LaunchSw();
            PortalTestActions.ProfilesSimpleSwitch(TestTimes);
        }
        public void Flow_ProfilesImExSwitch()
        {
            PortalTestActions.SetLaunchTimesAndWriteTestTitle(1);
            PortalTestActions.LaunchSw();
            PortalTestActions.ProfilesImExSwitch(TestTimes);
        }
        public void Flow_ProfilesImExAimpadSwitch()
        {
            PortalTestActions.SetLaunchTimesAndWriteTestTitle(1);
            PortalTestActions.LaunchSw();
            PortalTestActions.ProfilesImExAimpadSwitch(TestTimes);
        }
    }
}
