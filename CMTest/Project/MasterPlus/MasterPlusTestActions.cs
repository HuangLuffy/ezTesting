using ATLib;
using CommonLib.Util;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestActions : MasterPlus
    {
        public MasterPlusTestActions()
        {
            Initialize();
        }
        public void LaunchSw()
        { 
            UtilProcess.StartProcess(SwLnkPath);
            Timeout = 11;
            WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
            //var TabItem_OVERVIEW = new AT().GetElement(ATElementStruct: MasterPlusObj.TabItem_OVERVIEW, Timeout: Timeout);
            SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);
        }
        public void CloseSw()
        {
            var buttonClose = SwMainWindow.GetElement(MasterPlusObj.ButtonCloseMainWindow);
            buttonClose.DoClick();
            Timeout = 6;
            WriteConsoleTitle(LaunchTimes, $"Waiting for closing. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(Timeout);
        }
        public void RestartFlow()
        {
            Timeout = 20;
            //WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
            try
            {
                UtilProcess.StartProcess(SwLnkPath);
            }
            catch (System.Exception)
            {

            }
            SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout, ReturnNullWhenException: true);
            if (SwMainWindow == null)
            {
                UtilCmd.WriteTitle("Can not open MasterPlus.");
                UtilCmd.PressAnyContinue();
            }
            AT DialogWarning = SwMainWindow.GetElement(MasterPlusObj.DialogWarning, Timeout, ReturnNullWhenException: true);
            if (DialogWarning != null)
            {
                UtilCmd.WriteTitle("The device is not displayed");
            }
            else
            {
                //UtilCmd.WriteTitle("12121212");
                //UtilCmd.PressAnyContinue();
                UtilProcess.KillProcessByName(this.SwProcessName);
                UtilProcess.ExecuteCmd();
            }
            UtilCmd.PressAnyContinue();
        }
    }
}
