using ATLib;
using CommonLib.Util.os;

namespace CMTest.Project.MasterPlus
{
    public static class MasterPlusObj
    {
        public static readonly ATElementStruct MainWindowSw = new ATElementStruct()
        {
            Name = "Cooler Master.*",
            ClassName = "Qt5QWindowOwnDCIcon"
        };
        public static ATElementStruct ButtonCloseMainWindow => 
            UtilOS.GetOsVersion().Contains(UtilOS.Name.Win10) ? new ATElementStruct() { AutomationId = "Close", ControlType = ATElement.ControlType.Button } : new ATElementStruct() { Name = "Close", ControlType = ATElement.ControlType.Button };

        public static ATElementStruct DialogWarning = new ATElementStruct()
        {
            Name = "WARNING",
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
    }
}
