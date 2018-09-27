using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class PortalTestFlow
    {
        private PORTAL _PORTAL;
        private long launchTimes;
        private AT MainWindow_SW = null;
        private int timeout = 11;
        public PortalTestFlow(long num)
        {
            this.launchTimes = num;
        }
        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(_PORTAL.SwLnkPath);
                timeout = 11;                            
                _PORTAL.WriteConsoleTitle(this.launchTimes,  $"Waiting for launching. ({timeout}s)");
                MainWindow_SW = new AT().GetElement(Name: _PORTAL.MainWindowName, Timeout: timeout);
            }
            catch (Exception)
            {
                _PORTAL.HandleStepResult(PORTAL.Log.CRASH, launchTimes);
                throw;
            }
        }
        public void IsSWCrash()
        {
            timeout = 10;
            _PORTAL.WriteConsoleTitle(this.launchTimes, $"Waiting 10s for checking crash. ({timeout}s)");
            UtilTime.WaitTime(10);
            try
            {
                MainWindow_SW = new AT().GetElement(Name: "Cooler Master.*");
            }
            catch (Exception)
            {
                _PORTAL.HandleStepResult(PORTAL.Log.CRASH, launchTimes);
                throw;
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = MainWindow_SW.GetElement(AutomationId: "Close", TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                timeout = 3;
                _PORTAL.WriteConsoleTitle(this.launchTimes, $"Waiting for closing. ({timeout}s)");
                UtilTime.WaitTime(3);
                if (UtilProcess.IsProcessExistedByName(_PORTAL.SwProcessName))
                {
                    _PORTAL.HandleStepResult(PORTAL.Log.CRASH, launchTimes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
