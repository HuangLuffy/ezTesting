using ATLib;
using CommonLib.Util;
using System.Threading;

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
            Timeout = 1;
            try
            {
                UtilProcess.StartProcess(SwLnkPath);
                Thread t = UtilWait.WaitAnimationThread("Wait the MP+ launching. 30s", 1);
                t.Start();
                t.Join();
            }
            catch (System.Exception)
            {

            }
            var DialogWarning = UtilWait.ForAnyResultCatch(() => {
                UtilCmd.WriteTitle("Search MP+ UI.");
                SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);  // The MP+ will change after a while.
                UtilCmd.WriteTitle("Search Warning dialog of the MP+.");
                return SwMainWindow.GetElement(MasterPlusObj.DialogWarning, Timeout);
            }, 30);
            if (SwMainWindow == null)
            {
                UtilCmd.WriteTitle("Can not open MasterPlus.");
                UtilCmd.PressAnyContinue();
            }     
            if (DialogWarning != null)
            {
                UtilCmd.WriteTitle("The device is not displayed");
            }
            else
            {
                UtilProcess.KillProcessByName(this.SwProcessName);
                UtilProcess.ExecuteCmd();
            }
            UtilCmd.PressAnyContinue();
        }
    }
}
