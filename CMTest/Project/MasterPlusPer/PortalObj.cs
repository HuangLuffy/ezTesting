using ATLib;

namespace CMTest.Project.MasterPlusPer
{
    public class PortalObj : AbsSwObj
    {
        //public override string Name_MainWidow
        //{
        //    get { return "PORTAL"; }
        //}
        public override string NameMainWidow => "MasterPlus(PER. Only)";

        public override string ClassNameMainWindow => "Qt5QWindowIcon";

        public override string ButtonCloseMainWindow => "sw_close";

        public string Button_OK_PlugOut => "plugout_ok";

        public override string NameCrashMainWindow => "MasterPlus(PER. Only)";

        public static ATElementStruct Window_MasterPlusPer = new ATElementStruct()
        {
            Name = "MasterPlus(PER. Only)",
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
        public static ATElementStruct Button_Install_OK = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 0
        };
        public static ATElementStruct ComboBox_Install = new ATElementStruct()
        {
            ClassName = "TNewComboBox",
        };   
        public static ATElementStruct Button_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 0
        };
        public static ATElementStruct ComboBox_SelectLauguage = new ATElementStruct()
        {
            ClassName = "TNewComboBox"
        };
        public static ATElementStruct CheckBox_RemoveCache = new ATElementStruct()
        {
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Window_SelectLauguage = new ATElementStruct()
        {
            ClassName = "TSelectLanguageForm"
        };
        public static ATElementStruct Window_InstallWizard = new ATElementStruct()
        {
            ClassName = "TWizardForm"
        };
        public static ATElementStruct Button_Location_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 2
        };
        public static ATElementStruct Button_Folder_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 0
        };
        public static ATElementStruct Button_Shortcut_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 0
        };
        public static ATElementStruct Button_Install = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 0
        };
        public static ATElementStruct Dialog_FolderExists = new ATElementStruct()
        {
            FrameworkId = AT.FrameworkId.Win32,
            ClassName = AT.ClassName.P32770
        };
        public static ATElementStruct Button_DialogFolderExists_Yes = new ATElementStruct()
        {
            FrameworkId = AT.FrameworkId.Win32,
            ControlType = AT.ControlType.Button,
            Index = 0
        };
        public static ATElementStruct Button_SelectStartMemuFolder_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 2
        };
        public static ATElementStruct Edit_SelectStartMemuFolder = new ATElementStruct()
        {
            ClassName = "TNewEdit"
        };
        public static ATElementStruct Button_AddtionalTask_Next = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 1
        };
        public static ATElementStruct Button_ReadyToInstall_Install = new ATElementStruct()
        {
            ClassName = "TNewButton",
            Index = 1
        };
        public static ATElementStruct CheckBox_LaunchPortal = new ATElementStruct()
        {
            ControlType = AT.ControlType.CheckBox
        };
        public static ATElementStruct Button_Finish = new ATElementStruct()
        {
            ClassName = "TNewButton"
        };
    }
}
