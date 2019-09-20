using ATLib;
using CommonLib.Util;
using System;

namespace CMTest.Project
{
    public class SwCommonActions : SW
    {
        protected SwCommonActions()
        {
            //Initialize();
        }

        public void LaunchSw()
        {
            try
            {
                UtilProcess.StartProcess(SwLnkPath);
                UtilTime.WaitTime(2);
                Timeout = 20;
                WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
                SwMainWindow = new AT().GetElement(Name: Obj.NameMainWidow, ClassName: Obj.ClassNameMainWindow, Timeout: Timeout);
                //Qt5QWindowIcon
                UtilTime.WaitTime(2);
            }
            catch (Exception)
            {
                SwMainWindow = new AT().GetElement(Name: Obj.NameMainWidow, ClassName: Obj.ClassNameMainWindow, Timeout: Timeout);
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
            var crashWindow = new AT().GetElement(Name: Obj.NameCrashMainWindow, Timeout: checkTime, ReturnNullWhenException: true);
            if (crashWindow == null) return;
            HandleWrongStepResult(Msg.Crash, LaunchTimes);
            throw new Exception(Msg.Crash);
        }
        public void CloseSw()
        {
            var buttonClose = SwMainWindow.GetElement(Name: Obj.ButtonCloseMainWindow, ControlType: ATElement.ControlType.Button, TreeScope: ATElement.TreeScope.Descendants);
            buttonClose.DoClick();
            Timeout = 2;
            WriteConsoleTitle(LaunchTimes, $"Waiting for closing1. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(Timeout);
            try
            {
                WriteConsoleTitle(LaunchTimes, $"Waiting for closing2. ({Timeout}s)", Timeout);
                buttonClose = SwMainWindow.GetElement(Name: Obj.ButtonCloseMainWindow, ControlType: ATElement.ControlType.Button, TreeScope: ATElement.TreeScope.Descendants);
                //button_Close.DoClickWithNewThread();
                buttonClose.DoClickPoint();//Don't know why sometimes "button_Close.DoClick();" does not work
                UtilTime.WaitTime(Timeout);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
