using CommonLib.Util;
using CMTest.Project.MasterPlus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using CMTest.Project.MasterPlusPer;
using CMTest.Xml;
using CMTest.Project.RemoteModule;
using CommonLib;
using CommonLib.Util.IO;
using CommonLib.Util.Xml;

namespace CMTest
{
    public partial class TestIt
    {
        private const string MARK_FOUND_RESULT = "FOUND_TEST";
        private const string MARK_DO_NOTHING = "DO_NOTHING";
        private const string OPTION_COMMENT_SEPARATOR_PREFIX = " - \"";
        private const string OPTION_COMMENT_SEPARATOR_SUFFIX = "\"";
        private const string OPTION_CONNECT_IP = "Connect the Remote IP if it is correct";
        private const string OPTION_INPUT_IP = "Input your Remote IP";
        private readonly PortalTestFlows _portalTestFlows;
        private readonly MasterPlusTestFlows _mpTestFlows;
        private readonly UtilCmd _cmd = new UtilCmd();
        private readonly XmlOps _xmlOps = new XmlOps();
        private readonly MonitorAction _monitorAction = new MonitorAction();
        private RemoteOS _remoteOs;
        //public static object UtilRegrex { get; private set; }
        private readonly IDictionary<string, Func<dynamic>> _optionsTopMenu = new Dictionary<string, Func<dynamic>>();
        private readonly IDictionary<string, Func<dynamic>> _optionsXmlPlugInOutDeviceNames = new Dictionary<string, Func<dynamic>>();
        private readonly IReadOnlyList<string> _listXmlTestLanguages = new List<string>() {"Deutsch", "English",
            "Español", "Français", "Italiano", "Korean", "Malay", "Português (Portugal)", "Thai", "Türkçe", "Vietnamese", "Русский", "繁體中文", "中文（简体）" };
        public TestIt()
        {
            _mpTestFlows = new MasterPlusTestFlows();
            _portalTestFlows = new PortalTestFlows();
            AssembleTopMenu();
            //GetKeyboardKeys();
        }

        private void AssembleTopMenu()
        {
            if (_optionsTopMenu.Any()) return;
                _optionsTopMenu.Add(AddCommentForOption(OPTION_CONNECT_IP, _xmlOps.GetRemoteOsIp().Trim()), () => {
                var remoteOsIp = _xmlOps.GetRemoteOsIp().Trim();
                ConnectRemoteOsAvailable(remoteOsIp);
                return _cmd.ShowCmdMenu(_optionsTopMenu);
            });
                _optionsTopMenu.Add(OPTION_INPUT_IP, () => {
                CustomizeIp();
                return _cmd.ShowCmdMenu(_optionsTopMenu);
            });
                _optionsTopMenu.Add(_portalTestFlows.PortalTestActions.SwName, () => {
                AssemblePortalTests();
                return _cmd.ShowCmdMenu(_optionsPortalTestsWithFuncs, _optionsTopMenu);
            } );
                 _optionsTopMenu.Add(_mpTestFlows.MpActions.SwName, () => {
                AssembleMasterPlusPlugInOutTests();
                return _cmd.ShowCmdMenu(_optionsTestsWithFuncs, _optionsTopMenu);
            });
        }
        public bool IsNeededRunCmdDirectly(string[] args)
        {
            if (args.Length < 2 || !args[0].Trim().Equals("-d", StringComparison.CurrentCultureIgnoreCase))
                return false;
            var args1 = args[1].Trim();
            if (args1.Equals("")) return false;
            //TestIt.RunDirectly _RunDirectly = new TestIt.RunDirectly() { run = true, device = args1 };
            RunDirectly_Flow_PlugInOutServer(args1, new XmlOps());
            return true;
        }
        public void ShowTopMenu()
        { 
            try
            {
                var result = _cmd.ShowCmdMenu(_optionsTopMenu);
                if (result.Equals(MARK_FOUND_RESULT))
                {
                    UtilCmd.WriteLine(" >>>>>>>>>>>>>> Test Done!");
                }
                //_cmd.WriteCmdMenu(_optionsProjects, true);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                //HandleWrongStepResult(ex.Message);
                UtilCmd.PressAnyContinue();
                ShowTopMenu();
            }
        }   
        private void CustomizeIp()
        {
            while (true)
            {
                var remoteOsIp = UtilCmd.ReadLine();
                if (remoteOsIp.Equals("q", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
                if (UtilRegex.IsIp(remoteOsIp))
                {
                    _xmlOps.SetRemoteOsIp(remoteOsIp);
                    ConnectRemoteOsAvailable(remoteOsIp);
                    break;
                }
                else
                {
                    UtilCmd.WriteLine("The words you input is not a invalid IP. Re-input or input \"q\" to quit.");
                }
            }
        }
        private void ConnectRemoteOsAvailable(string remoteOsIp)
        {
            var currentTitle = UtilCmd.GetTitle();
            var waitAnimation = UtilWait.WaitTimeElapseThread("Connecting...");
            try
            {
                _remoteOs = new RemoteOS(remoteOsIp);
                waitAnimation.Start();
                _remoteOs.IsRemoteOsAvailable();
                waitAnimation.Abort();
                UtilCmd.PressAnyContinue("The communication between the Local OS and the Remote OS established successfully. Press any key to continue.");
            }
            catch (Exception)
            {
                waitAnimation.Abort();
                UtilCmd.WriteTitle(currentTitle);
                throw;
            }
        }
        private static string AddCommentForOption(string oriOptionValue, string addedComment)
        {
            return $"{oriOptionValue}{OPTION_COMMENT_SEPARATOR_PREFIX}{addedComment}{OPTION_COMMENT_SEPARATOR_SUFFIX}";
        }
        private static string RemoveCommentFromOption(string comment)
        {
            return UtilString.GetSplitArray(comment, OPTION_COMMENT_SEPARATOR_PREFIX).ToList()[0];
        }
        private static string GetCommentFromOption(string comment)
        {
            return UtilRegex.GetMatchMidValue(comment, OPTION_COMMENT_SEPARATOR_PREFIX, OPTION_COMMENT_SEPARATOR_SUFFIX);
        }






        /// <summary>
        /// //ignore below codes
        /// </summary>
        /// <param name="deviceName"></param>
        //public void RestartSystemAndCheckDeviceName(string deviceName = "USB Audio Device")
        //{
        //    var restartLogTime = GetRestartLogTime();
        //    var logLines = PrepareRestartAndGetLogCounts();
        //    var titleLaunchTimes = _xmlOps.GetRestartTimes();
        //    UtilProcess.StartProcess("devmgmt.msc");
        //    var titleTotal = $"Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count}";
        //    var t = UtilWait.WaitTimeElapseThread($"{titleTotal} - Waiting 30s.", 30);
        //    t.Start();
        //    t.Join();
        //    var foundUSBAudioDevice = UtilOs.GetDevices().Find(d => d.ToUpper().Contains("USB Audio Device".ToUpper()));
        //    var devicesCount = UtilOs.GetDevices().Count(d => d.ToUpper().Contains("MH650".ToUpper()));
        //    if (foundUSBAudioDevice != null)
        //    {
        //        UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count} - USB Audio Device found.");
        //    }
        //    else if(devicesCount != 3)
        //    {
        //        UtilFile.WriteFile(LogPathRestart, $"{restartLogTime}: Restart Times: {titleLaunchTimes} - Error Times: {logLines.Count} - Could not find 3 MH650 items.");
        //    }  
        //    else
        //    {
        //        _xmlOps.SetRestartTimes(Convert.ToInt16(titleLaunchTimes) + 1);
        //        UtilTime.WaitTime(1);
        //        UtilOS.Reboot();
        //    } 
        //}
        //private List<string> PrepareRestartAndGetLogCounts()
        //{
        //    UtilFolder.CreateDirectory(Path.Combine(ScreenshotsPath, "Restart"));
        //    var logLines = UtilFile.ReadFileByLine(LogPathRestart);
        //    logLines.ForEach(line => UtilCmd.WriteLine(line));
        //    return logLines;
        //}
        //public struct RunDirectly
        //{
        //    public bool run;
        //    public string device;
        //}
    }
}
