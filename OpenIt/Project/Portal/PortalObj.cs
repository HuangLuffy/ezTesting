using ATLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalObj : AbsSWObj
    {
        //private string name_MainWidow;
        //private string btn_CloseMainWindow;
        ////Personality
        //private string btn_Ok_PlugOut;

        public override string Name_MainWidow
        {
            get { return "PORTAL"; }
        }
        public override string ClassName_MainWidow
        {
            get { return "Qt5QWindowIcon"; }
            //set { name_MainWidow = value; }
        }
        
        public override string Button_CloseMainWindow
        {
            get { return "sw_close"; }
        }

        public string Button_OK_PlugOut
        {
            get { return "plugout_ok"; }
        }

        public override string Name_CrashMainWidow
        {
            get { return "PORTAL.exe"; }
        }

        public static ATElementStruct Window_Main = new ATElementStruct()
        {
            Name = "PORTAL",
            ClassName = "Qt5QWindowIcon"
        };

        public static ATElementStruct TabItem_PROFILES = new ATElementStruct()
        {
            Name = "TAB_PROFILES",
            ControlType = AT.ControlType.CheckBox
        };

        public static ATElementStruct Lable_LOADING = new ATElementStruct()
        {
            Name = "LOADING",
            ControlType = AT.ControlType.Custom
        };
        public static ATElementStruct Profile_1 = new ATElementStruct()
        {
            Name = "PROFILE1",
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Profile_2 = new ATElementStruct()
        {
            Name = "PROFILE2",
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Profile_3 = new ATElementStruct()
        {
            Name = "PROFILE3",
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Profile_4 = new ATElementStruct()
        {
            Name = "PROFILE4",
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Profile_RESET = new ATElementStruct()
        {
            Name = "PROFILE_RESET",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Profile_RENAME = new ATElementStruct()
        {
            Name = "PROFILE_RENAME",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Profile_IMPORT = new ATElementStruct()
        {
            Name = "PROFILE_IMPORT",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Profile_EXPORT = new ATElementStruct()
        {
            Name = "PROFILE_EXPORT",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Profile_OPENWITHEXE = new ATElementStruct()
        {
            Name = "PROFILE_OPEN",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Profile_EDIT = new ATElementStruct()
        {
            Name = "PROFILE_EDIT",
            ControlType = AT.ControlType.Edit
        };
        public static ATElementStruct Window_OpenFIle = new ATElementStruct()
        {
            ControlType = AT.ControlType.Window
        };
        public static ATElementStruct Button_Reset_Yes = new ATElementStruct()
        {
            Name = "Yes_Enter",
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Button_Save = new ATElementStruct()
        {
            ControlType = AT.ControlType.Button
        };
        public static ATElementStruct Button_Exist_Yes = new ATElementStruct()
        {
            AutomationId = "CommandButton_6"
        };
        

    }
}
