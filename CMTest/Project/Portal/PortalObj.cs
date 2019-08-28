using ATLib;

namespace CMTest.Project.Portal
{
    public class PortalObj : AbsSwObj
    {
        //public override string Name_MainWidow
        //{
        //    get { return "PORTAL"; }
        //}
        public override string NameMainWidow => "PORTAL";

        public override string ClassNameMainWidow => "Qt5QWindowIcon";

        public override string ButtonCloseMainWindow => "sw_close";

        public string Button_OK_PlugOut => "plugout_ok";

        public override string NameCrashMainWidow => "PORTAL.exe";

        public static ATElementStruct Window_Main = new ATElementStruct()
        {
            Name = "PORTAL",
            ClassName = "Qt5QWindowIcon"
        };

        public static ATElementStruct TabItem_PROFILES = new ATElementStruct()
        {
            Name = "TAB_PROFILES",
            ControlType = ATElement.ControlType.CheckBox
        };

        public static ATElementStruct Lable_LOADING = new ATElementStruct()
        {
            Name = "LOADING",
            ControlType = ATElement.ControlType.Custom
        };
        public static ATElementStruct Profile_1 = new ATElementStruct()
        {
            Name = "PROFILE1",
            ControlType = ATElement.ControlType.CheckBox
        };
        public static ATElementStruct Profile_2 = new ATElementStruct()
        {
            Name = "PROFILE2",
            ControlType = ATElement.ControlType.CheckBox
        };
        public static ATElementStruct Profile_3 = new ATElementStruct()
        {
            Name = "PROFILE3",
            ControlType = ATElement.ControlType.CheckBox
        };
        public static ATElementStruct Profile_4 = new ATElementStruct()
        {
            Name = "PROFILE4",
            ControlType = ATElement.ControlType.CheckBox
        };
        public static ATElementStruct Profile_RESET = new ATElementStruct()
        {
            Name = "PROFILE_RESET",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Profile_RENAME = new ATElementStruct()
        {
            Name = "PROFILE_RENAME",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Profile_IMPORT = new ATElementStruct()
        {
            Name = "PROFILE_IMPORT",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Profile_EXPORT = new ATElementStruct()
        {
            Name = "PROFILE_EXPORT",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Profile_OPENWITHEXE = new ATElementStruct()
        {
            Name = "PROFILE_OPEN",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Profile_EDIT = new ATElementStruct()
        {
            Name = "PROFILE_EDIT",
            ControlType = ATElement.ControlType.Edit
        };
        public static ATElementStruct Window_OpenFIle = new ATElementStruct()
        {
            ControlType = ATElement.ControlType.Window
        };
        public static ATElementStruct Button_Reset_Yes = new ATElementStruct()
        {
            Name = "Yes_Enter",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Button_Save = new ATElementStruct()
        {
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct Button_Exist_Yes = new ATElementStruct()
        {
            AutomationId = "CommandButton_6"
        };
        

    }
}
