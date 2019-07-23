using CMTest.Project.Portal;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest
{
    public partial class TestIt
    {
        IDictionary<string, Func<dynamic>> Options_Portal_PlugInOut_Tests_With_Funcs = new Dictionary<string, Func<dynamic>>();
        public List<string> Options_Portal_PlugInOut_Devices_Name = new List<string>();

        private void AssemblePortalPlugInOutTests()
        {
            if (Options_Portal_PlugInOut_Tests_With_Funcs.Count() == 0)
            {
                //Select Back that will trigger this function again, so try-catch.
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_LAUNCH_TEST, this.Flow_LaunchTest);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_PLUGIN_OUT_SERVER, this.Flow_PlugInOutServer);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_SIMPLE_PROFILES_SWITCH, this.Flow_LaunchTest);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, this.Flow_LaunchTest);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(PortalTestFlows.Test_Names.OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH, this.Flow_LaunchTest);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN, this.Flow_CMD_SHOW_MENU_AGAIN);
                Options_Portal_PlugInOut_Tests_With_Funcs.Add(UtilCmd.OPTION_BACK, this.Flow_CMD_Back);
            }
        }
        private void AssemblePortalPlugInOutDevices()
        {
            if (Options_Portal_PlugInOut_Devices_Name.Count() == 0)
            {
                Options_Portal_PlugInOut_Devices_Name = UtilCloner.CloneList(VMObj.GetDevicesItemList());
                Options_Portal_PlugInOut_Devices_Name.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN);
                Options_Portal_PlugInOut_Devices_Name.Add(UtilCmd.OPTION_BACK);
            }
        }
        private dynamic Flow_LaunchTest()
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
            string name = "";
            while (name.Equals(""))
            {
                name = DeviceMatcher(_CMD.WriteOptions(Options_Portal_PlugInOut_Devices_Name));
                if (UtilCmd.OPTION_BACK.Equals(name))
                {
                    return false;
                }
            }
            _PortalTestFlows.Flow_PlugInOutServer(name);
            return FOUND_TEST;
        }

        //else if (this.IsTestExisted(PortalTestFlows.OPTION_SIMPLE_PROFILES_SWITCH, selected, options[i]))
        //{
        //    _PortalTestFlows.Flow_ProfilesSimpleSwitch();
        //    return true;
        //}
        //else if (this.IsTestExisted(PortalTestFlows.OPTION_IMPORT_EXPORT_PROFILES_SWITCH, selected, options[i]))
        //{
        //    _PortalTestFlows.Flow_ProfilesImExSwitch();
        //    return true;
        //}
        //else if (this.IsTestExisted(PortalTestFlows.OPTION_IMPORT_EXPORT_AIMPAD_PROFILES_SWITCH, selected, options[i]))
        //{
        //    _PortalTestFlows.Flow_ProfilesImExAimpadSwitch();
        //    return true;
        //}
        //else if (this.IsTestExisted(PortalTestFlows.OPTION_PLUGIN_OUT_SERVER, selected, options[i]))
        //{
        //    string name = "";
        //    while (name.Equals(""))
        //    {
        //        _PortalTestFlows.Options_Devices_Cmd.Add(UtilCmd.OPTION_SHOW_MENU_AGAIN);
        //        _PortalTestFlows.Options_Devices_Cmd.Add(UtilCmd.OPTION_BACK);
        //        name = this.DeviceMatcher(_CMD.WriteOptions(_PortalTestFlows.Options_Devices_Cmd));
        //        if (UtilCmd.OPTION_BACK.Equals(name))
        //        {
        //            _PortalTestFlows.Options_Devices_Cmd.Remove(UtilCmd.OPTION_SHOW_MENU_AGAIN);
        //            _PortalTestFlows.Options_Devices_Cmd.Remove(UtilCmd.OPTION_BACK);
        //            return false;
        //        }
        //    }
        //    _PortalTestFlows.Flow_PlugInOutServer(name);
        //    return true;
        //}
    }
}
