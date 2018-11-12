using ATLib;
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
        public static ATElementStruct Btn_CloseMainWindow = new ATElementStruct()
        {
            AutomationId = "Close",
            ControlType = "Button"
        };
    }
}
