using ATLib;
using CommonLib.Util;
using System;

namespace CMTest.Project
{
    public class SWCommonActions : SW
    {
        protected SWCommonActions()
        {
            Initialize();
        }

        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(SwLnkPath);
                UtilTime.WaitTime(2);
                Timeout = 20;
                WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
                MainWindow_SW = new AT().GetElement(Name: Obj.Name_MainWidow, ClassName: Obj.ClassName_MainWidow, Timeout: Timeout);
                //Qt5QWindowIcon
                UtilTime.WaitTime(2);
            }
            catch (Exception)
            {
                MainWindow_SW = new AT().GetElement(Name: Obj.Name_MainWidow, ClassName: Obj.ClassName_MainWidow, Timeout: Timeout);
                HandleWrongStepResult("Cannot find the App.", LaunchTimes);
            }
        }

        public void IsSwCrash(int checkTime = 0, int checkInternal = 0)
        {
            if (checkInternal > 0)
            {
                UtilTime.WaitTime(checkInternal);
                WriteConsoleTitle(LaunchTimes, $"Wait ({checkInternal}s)", checkInternal);
            }
            WriteConsoleTitle(LaunchTimes, $"Waiting for checking crash. ({checkTime}s)", checkTime);
            AT Crash_Window = new AT().GetElement(Name: Obj.Name_CrashMainWidow, Timeout: checkTime, ReturnNullWhenException: true);
            if (Crash_Window != null)
            {
                HandleWrongStepResult(Msg.CRASH, LaunchTimes);
                throw new Exception(Msg.CRASH);
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = MainWindow_SW.GetElement(Name: Obj.Button_CloseMainWindow, ControlType: ATElement.ControlType.Button, TreeScope: ATElement.TreeScope.Descendants);
                button_Close.DoClick();
                Timeout = 2;
                WriteConsoleTitle(LaunchTimes, $"Waiting for closing1. ({Timeout}s)", Timeout);
                UtilTime.WaitTime(Timeout);
                try
                {
                    WriteConsoleTitle(LaunchTimes, $"Waiting for closing2. ({Timeout}s)", Timeout);
                    button_Close = MainWindow_SW.GetElement(Name: Obj.Button_CloseMainWindow, ControlType: ATElement.ControlType.Button, TreeScope: ATElement.TreeScope.Descendants);
                    //button_Close.DoClickWithNewThread();
                    button_Close.DoClickPoint();//Don't know why sometimes "button_Close.DoClick();" does not work
                    UtilTime.WaitTime(Timeout);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
