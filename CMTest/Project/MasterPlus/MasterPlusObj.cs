using ATLib;
using CommonLib.Util.OS;

namespace CMTest.Project.MasterPlus
{
    public static class MPObj
    {
        public static readonly ATElementStruct MainWindow = new ATElementStruct()
        {
            //ClassName = "Qt5QWindowOwnDCIcon",
            Name = "Cooler Master MasterPlus.*"
        };
        public static ATElementStruct ButtonCloseMainWindow => 
            UtilOs.GetOsVersion().Contains(UtilOs.Name.Win10) ? new ATElementStruct() { AutomationId = "Close", ControlType = ATElement.ControlType.Button } : new ATElementStruct() { Name = "Close", ControlType = ATElement.ControlType.Button };

        public static ATElementStruct DialogWarning = new ATElementStruct()
        {
            Name = "WARNING||警告",
            ControlType = ATElement.ControlType.TabItem
        };
        public static ATElementStruct ButtonQuit = new ATElementStruct()
        {
            Name = "Quit",
            ControlType = ATElement.ControlType.Button
        };

        public static ATElementStruct TabItemOverview = new ATElementStruct()
        {
            Name = "OVERVIEW",
            ControlType = ATElement.ControlType.TabItem
        };
        public static ATElementStruct DeviceList = new ATElementStruct()
        {
            Name = "DeviceList"
        };
        public static ATElementStruct AssignContainer = new ATElementStruct()
        {
            Name = "AssignContainer"
        };
        public static ATElementStruct KeyMappingResetButton = new ATElementStruct()
        {
            Name = "KeyMappingResetButton",
            ControlType = ATElement.ControlType.Button
        };
        public static ATElementStruct CommonDialogParent = new ATElementStruct()
        {
            ClassName = "QQuickPopupItem"
        };
        public static ATElementStruct CommonDialog = new ATElementStruct()
        {
            Name = "CommonDialog"
        };
        public static ATElementStruct ReassignDialog = new ATElementStruct()
        {
            ClassName = "QQuickPopupItem",
            Name = "ReassignDialog"
        };
        public static ATElementStruct ReassignDropdown = new ATElementStruct()
        {
            Name = "ReassignDropdown"
        };
        

        public static ATElementStruct AssignedValue = new ATElementStruct()
        {
            Name = "AssignedValue"
        };
        public static ATElementStruct ReassignSaveButton = new ATElementStruct()
        {
            FullDescriton = "ReassignSaveButton"
        };
        public static ATElementStruct ReassignCollapseButton = new ATElementStruct()
        {
            Name = "CollapseButton"
        };
    }
}
