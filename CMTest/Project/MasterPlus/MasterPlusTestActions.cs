using ATLib;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
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
        public void LaunchMasterPlus()
        { 
            UtilProcess.StartProcess(SwLnkPath);
            Timeout = 11;
            WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
            new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);
        }
        public void CloseMasterPlus()
        {
            var buttonClose = SwMainWindow.GetElement(MasterPlusObj.ButtonCloseMainWindow);
            buttonClose.DoClick();
            Timeout = 6;
            WriteConsoleTitle(LaunchTimes, $"Waiting for closing. ({Timeout}s)", Timeout);
            UtilTime.WaitTime(Timeout);
        }

        public void LaunchAndCheckCrash(long testTimes)
        {
            var crashTimes = 0;
            UtilCmd.Clear();
            for (var i = 1; i < testTimes; i++)
            {
                var titleTotal = $"Reopen Times: {i} - Crash Times: {crashTimes}";
                var logLines = UtilFile.ReadFileByLine(LogPathLaunch);
                logLines.ForEach(line => UtilCmd.WriteLine(line));
                var launchLogTime = GetRestartLogTime();
                var screenshotPath = Path.Combine(ImagePath, crashTimes.ToString());
                UtilProcess.StartProcess(SwLnkPath);
                Timeout = 1;
                UtilCmd.WriteTitle($"{titleTotal} - Searching MP+ UI.");
                var startTime = DateTime.Now;
                var dialogWarning = UtilWait.ForAnyResultCatch(() => {
                    UtilCmd.WriteTitle($"{titleTotal} - Searching Warning dialog of the MP+ in 60s. Time elapsed: {(DateTime.Now - startTime).Seconds}s.");
                    SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);  // The MP+ will change after a while.
                    return SwMainWindow.GetElement(MasterPlusObj.DialogWarning, Timeout);
                }, 60, 2);
                if (SwMainWindow == null)
                {
                    UtilCapturer.Capture(i.ToString());
                    UtilFile.WriteFile(LogPathLaunch, $"{launchLogTime}: Reopen Times: {i} - Could not open MasterPlus.");
                    crashTimes++;
                }
                UtilTime.WaitTime(1);
                UtilProcess.KillProcessByName(this.SwProcessName);
                UtilTime.WaitTime(1);
            }
        }
        public void RestartSystemAndCheckDeviceRecognitionFlow(XmlOps xmlOps)
        {
            UtilFolder.CreateDirectory(Path.Combine(ImagePath, "Restart"));
            var restartLogTime = GetRestartLogTime();
            var screenshotPath = Path.Combine(RestartScreenshotPath, restartLogTime);
            var logLines = UtilFile.ReadFileByLine(LogPathRestart);
            logLines.ForEach(line => UtilCmd.WriteLine(line));
            var titleLaunchTimes = xmlOps.GetRestartTimes();
            var titleTotal = $"Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count}";
            var t = UtilWait.WaitTimeElapseThread($"{titleTotal} - Waiting 35s.", 35);
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
            var startTime = DateTime.Now;
            var dialogWarning = UtilWait.ForAnyResultCatch(() => {
                UtilCmd.WriteTitle($"{titleTotal} - Searching Warning dialog of the MP+ in 60s. Time elapsed: {(DateTime.Now - startTime).Seconds}s.");
                SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);  // The MP+ will change after a while.
                return SwMainWindow.GetElement(MasterPlusObj.DialogWarning, Timeout);
            }, 60, 3);
            if (SwMainWindow == null)
            {
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
                UtilCapturer.Capture(screenshotPath);
                UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - Could not open MasterPlus.");
            }     
            else if (dialogWarning != null)
            {
                UtilCapturer.Capture(screenshotPath);
                //UtilCmd.WriteTitle($"Restart Times: {titleLaunchTimes} - The device was not displayed");
                UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - The device was not displayed.");
            }
            xmlOps.SetRestartTimes(Convert.ToInt16(titleLaunchTimes) + 1);
            UtilTime.WaitTime(1);
            UtilProcess.KillProcessByName(this.SwProcessName);
            //UtilProcess.ExecuteCmd();// sometimes it does not work somehow.
            UtilOS.Reboot();
        }
        public void KeyMappingTest(string deviceName)
        {
            Timeout = 11;
            //WriteConsoleTitle(LaunchTimes, $"Waiting for launching. ({Timeout}s)", Timeout);
            SwMainWindow = new AT().GetElement(MasterPlusObj.MainWindowSw, Timeout);
            var deviceList = SwMainWindow.GetElement(MasterPlusObj.DeviceList);
            var devices = deviceList.GetElementsAllChild();
            var dut = devices.GetElementByIA(new ATElementStruct() { IADescription = deviceName });

            dut.DoClickPoint();
        }
    }
}
