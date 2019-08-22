using ATLib;
using CommonLib.Util.os;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusObj
    {
        public static ATElementStruct MainWindow_SW = new ATElementStruct()
        {
            Name = "Cooler Master.*",
            ClassName = "Qt5QWindowOwnDCIcon"
        };
        public static ATElementStruct Btn_CloseMainWindow
        {
            get
            {
                if (UtilOS.GetOsVersion().Contains(UtilOS.Name.Win10))
                {
                    return new ATElementStruct() { AutomationId = "Close", ControlType = AT.ControlType.Button };
                }
                return new ATElementStruct() { Name = "Close", ControlType = AT.ControlType.Button };
            }
        }

        public static ATElementStruct Dialog_WARNING = new ATElementStruct()
        {
            Name = "WARNING",
            ControlType = AT.ControlType.TabItem
        };
        public static ATElementStruct Btn_Quit = new ATElementStruct()
        {
            Name = "Quit",
            ControlType = AT.ControlType.Button
        };

        public static ATElementStruct TabItem_OVERVIEW = new ATElementStruct()
        {
            Name = "OVERVIEW",
            ControlType = AT.ControlType.TabItem
        };
    }
}
