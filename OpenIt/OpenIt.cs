using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class OpenIt
    {
        PortalTestFlow _PortalTestFlow = new PortalTestFlow();
        public void Run()
        {
            for (int i = 1; i < 99999999; i++)
            {
                _PortalTestFlow.LaunchTimes = i;
                try
                {
                    _PortalTestFlow.LaunchSW();
                    _PortalTestFlow.CloseSW();
                    _PortalTestFlow.IsSWCrash();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}
