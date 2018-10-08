using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalTestFlow
    {
        public Portal _Portal;
        private AT MainWindow_SW = null;
        private int timeout;
        private long launchTimes;
        public long LaunchTimes
        {
            get { return launchTimes; }
            set { launchTimes = value; }
        }

        public PortalTestFlow()
        {
            this._Portal = new Portal();
            this._Portal.Initialize();
        }

        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(_Portal.SwLnkPath);
                timeout = 11;                            
                _Portal.WriteConsoleTitle(this.launchTimes,  $"Waiting for launching. ({timeout}s)");
                this.MainWindow_SW = new AT().GetElement(Name: _Portal.UIA.Name_MainWidow, Timeout: timeout);
            }
            catch (Exception)
            {
                _Portal.HandleStepResult(Portal.Log.CRASH, launchTimes);
            }
        }
        public void IsSWCrash()
        {
            timeout = 10;
            _Portal.WriteConsoleTitle(this.launchTimes, $"Waiting for checking crash. ({timeout}s)");     
            MainWindow_SW = new AT().GetElement(Name: _Portal.UIA.Name_CrashMainWidow, Timeout: timeout, returnNullWhenException: true);
            if (MainWindow_SW != null) {
                _Portal.HandleStepResult(Portal.Log.CRASH, launchTimes);
                throw new Exception();
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = MainWindow_SW.GetElement(Name: _Portal.UIA.Btn_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                timeout = 3;
                _Portal.WriteConsoleTitle(this.launchTimes, $"Waiting for closing. ({timeout}s)");
                UtilTime.WaitTime(3);
                //if (UtilProcess.IsProcessExistedByName(_Portal.SwProcessName))
                //{
                //    _Portal.HandleStepResult(Portal.Log.PROCESSSTILLEXISTS, launchTimes);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
