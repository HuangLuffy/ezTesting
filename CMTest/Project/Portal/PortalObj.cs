﻿using ATLib;

namespace CMTest.Project.Portal
{
    public class PortalObj : AbsSWObj
    {
        //public override string Name_MainWidow
        //{
        //    get { return "PORTAL"; }
        //}
        public override string Name_MainWidow => "PORTAL";

        public override string ClassName_MainWidow => "Qt5QWindowIcon";

        public override string Button_CloseMainWindow => "sw_close";

        public string Button_OK_PlugOut => "plugout_ok";

        public override string Name_CrashMainWidow => "PORTAL.exe";

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
