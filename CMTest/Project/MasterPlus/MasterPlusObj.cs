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

        public static ATElementStruct CloseMasterPlusButton = new ATElementStruct()
        {
            FullDescriton = "MPCloseButton"
        };
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

        #region KeyMapping

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
            //ClassName = "QQuickPopupItem",
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
        public static ATElementStruct ReassignTitleValue = new ATElementStruct()
        {
            Name = "ReassignTitleValue"
        };
        public static ATElementStruct ReassignSaveButton = new ATElementStruct()
        {
            FullDescriton = "ReassignSaveButton"
        };
        public static ATElementStruct ReassignCloseButton = new ATElementStruct()
        {
            FullDescriton = "ReassignCloseButton"
        };
        public static ATElementStruct ReassignCollapseButton = new ATElementStruct()
        {
            Name = "CollapseButton"
        };
        public static ATElementStruct DisableKeyCheckbox = new ATElementStruct()
        {
            Name = "DisableKeyCheckbox"
        };
        public static ATElementStruct EnableKeyCheckbox = new ATElementStruct()
        {
            Name = "EnableKeyCheckbox"
        };
        public static ATElementStruct ReassignCatalogListItem = new ATElementStruct()
        {
            Name = "ReassignCatalogListItem"
        };
        
        #endregion
    }
}
