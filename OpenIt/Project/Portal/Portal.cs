using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class Portal : SW
    {
        public PortalUIA UIA = new PortalUIA();
        public Portal()
        {
            this.SwLnkPath = @"C:\Program Files (x86)\CoolerMaster\PORTAL\PORTAL.exe";
            this.SwName = "PORTAL";
            this.SwProcessName = "PORTAL";
            this.MainWindowName = "PORTAL";
        }
    }
}
