using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.log;
using CommonLib.Util.OS;
using CommonLib.Util.Project;
using System;
using System.IO;
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
        public void RestartSystemAndCheckDeviceRecognitionFlow(XmlOps xmlOps)
        {
            string LogTime = $"{DateTime.Now:yyyy.MM.dd_hh.mm.ss}";
            var logFullPath = Path.Combine(ProjectPath.GetProjectFullPath(), "RestartLog.log");
            UtilFolder.CreateDirectory(Path.Combine(ImagePath, "Restart"));
            var screenshotPath = Path.Combine("Screenshots\\Restart", LogTime);
            var logLines = UtilFile.ReadFileByLine(logFullPath);
            logLines.ForEach(line => UtilCmd.WriteLine(line));
            var titleLaunchTimes = xmlOps.GetRestartTimes();
            var titleTotal = $"Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count}";
            Thread t = UtilWait.WaitAnimationThread($"{titleTotal} - Waiting 30s.", 30);
            t.Start();
            t.Join();
            if (!File.Exists(SwLnkPath))
            {
                UtilCmd.WriteTitle($"{titleTotal} - Could not find {SwLnkPath}.");
                UtilCmd.PressAnyContinue();
            }
            UtilProcess.StartProcess(SwLnkPath);
            Timeout = 1;
            UtilCmd.WriteTitle($"{titleTotal} - Searching MP+ UI.");
            var DialogWarning = UtilWait.ForAnyResultCatch(() => {
                SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);  // The MP+ will change after a while.
                UtilCmd.WriteTitle($"{titleTotal} - Searching Warning dialog of the MP+.");
                return SwMainWindow.GetElement(MasterPlusObj.DialogWarning, Timeout);
            }, 30);
            if (SwMainWindow == null)
            {
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
                UtilCapturer.Capture(screenshotPath);
                UtilFile.WriteFile(logFullPath, $"{LogTime}: Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
            }     
            else if (DialogWarning != null)
            {
                UtilCapturer.Capture(screenshotPath);
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - The device was not displayed");
                UtilFile.WriteFile(logFullPath, $"{LogTime}: Restart Times: {titleLaunchTimes} - The device was not displayed.");
            }
            xmlOps.SetRestartTimes(Convert.ToInt16(titleLaunchTimes) + 1);
            UtilTime.WaitTime(1);
            UtilProcess.KillProcessByName(this.SwProcessName);
            //UtilProcess.ExecuteCmd();// sometimes it does not work somehow.
            UtilOS.Reboot();
        }
    }
}
