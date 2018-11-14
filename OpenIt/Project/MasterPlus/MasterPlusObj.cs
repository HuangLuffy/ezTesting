using ATLib;
using CommonLib.Util.os;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.MasterPlus
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
                    return new ATElementStruct() { AutomationId = "Close", ControlType = "Button" };
                }
                return new ATElementStruct() { Name = "Close", ControlType = "Button" };
            }
        }
    }
}
