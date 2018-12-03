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
                UtilTime.WaitTime(2);
                this.Timeout = 20;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for launching. ({this.Timeout}s)", this.Timeout);
                this.MainWindow_SW = new AT().GetElement(Name: this.Obj.Name_MainWidow, ClassName: this.Obj.ClassName_MainWidow, Timeout: this.Timeout);
                //Qt5QWindowIcon
                UtilTime.WaitTime(2);
            }
            catch (Exception ex)
            {
                this.MainWindow_SW = new AT().GetElement(Name: this.Obj.Name_MainWidow, ClassName: this.Obj.ClassName_MainWidow, Timeout: this.Timeout);
                this.HandleWrongStepResult("Cannot find App.", this.LaunchTimes);
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
            AT Crash_Window = new AT().GetElement(Name: this.Obj.Name_CrashMainWidow, Timeout: checkTime, ReturnNullWhenException: true);
            if (Crash_Window != null)
            {
                this.HandleWrongStepResult(SW.msg.CRASH, this.LaunchTimes);
                throw new Exception(SW.msg.CRASH);
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = this.MainWindow_SW.GetElement(Name: this.Obj.Button_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                Timeout = 3;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing1. ({Timeout}s)", Timeout);
                UtilTime.WaitTime(Timeout);
                try
                {
                    Timeout = 3;
                    this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing2. ({Timeout}s)", Timeout);
                    button_Close = this.MainWindow_SW.GetElement(Name: this.Obj.Button_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                    //button_Close.DoClickWithNewThread();
                    button_Close.DoClickPoint();//Don't know why sometimes "button_Close.DoClick();" does not work
                    UtilTime.WaitTime(Timeout);
                }
                catch (Exception)
                {

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
