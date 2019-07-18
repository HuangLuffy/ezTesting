using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.Portal
{
    public class Portal : SWCommonActions
    {
        public Portal()
        {
            this.Obj = new PortalObj();
            this.SwLnkPath = @"C:\Program Files (x86)\CoolerMaster\PORTAL\PORTAL.exe";
            this.SwName = "PORTAL";
            this.SwProcessName = "PORTAL";
        }
    }
}
