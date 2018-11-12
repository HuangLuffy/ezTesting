using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project
{
    public class SWCommonActions : SW
    {

        public SWCommonActions()
        {
            this.Initialize();
        }

        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(this.SwLnkPath);
                this.Timeout = 11;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for launching. ({this.Timeout}s)", this.Timeout);
                this.MainWindow_SW = new AT().GetElement(Name: this.Obj.Name_MainWidow, ClassName: this.Obj.ClassName_MainWidow, Timeout: this.Timeout);
                UtilTime.WaitTime(2);
            }
            catch (Exception)
            {
                this.HandleStepResult(SW.msg.CRASH, this.LaunchTimes);
            }
        }

        public void IsSWCrash(int checkTime = 0, int checkInternal = 0)
        {
            if (checkInternal > 0)
            {
                UtilTime.WaitTime(checkInternal);
                this.WriteConsoleTitle(this.LaunchTimes, $"Waits ({checkInternal}s)", checkInternal);
            }
            checkTime = 10;
            this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for checking crash. ({checkTime}s)", checkTime);
            AT Crash_Window = new AT().GetElement(Name: this.Obj.Name_CrashMainWidow, Timeout: checkTime, returnNullWhenException: true);
            if (Crash_Window != null)
            {
                this.HandleStepResult(SW.msg.CRASH, this.LaunchTimes);
                throw new Exception();
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = this.MainWindow_SW.GetElement(Name: this.Obj.Btn_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                Timeout = 3;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing. ({Timeout}s)", Timeout);
                UtilTime.WaitTime(3);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
