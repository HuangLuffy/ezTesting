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
            SwMainWindow = new AT().GetElement(ATElementStruct: MasterPlusObj.MainWindow_SW, Timeout: Timeout);
        }
        public void CloseSw()
        {
            var buttonClose = SwMainWindow.GetElement(ATElementStruct: MasterPlusObj.Btn_CloseMainWindow);
            buttonClose.DoClick();
            Timeout = 6;
            WriteConsoleTitle(LaunchTimes, $"Waiting for closing. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(Timeout);
            //if (UtilProcess.IsProcessExistedByName(_Portal.SwProcessName))
            //{
            //    _Portal.HandleStepResult(Portal.Log.PROCESSSTILLEXISTS, launchTimes);
            //}
        }
    }
}
