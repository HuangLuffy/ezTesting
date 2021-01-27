using CommonLib.Util;
using CMTest.Project.MasterPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using CMTest.Project.MasterPlusPer;
using CMTest.Xml;
using CMTest.Project.RemoteModule;
using CommonLib.Util.ComBus;
using static ATLib.Input.Hw;
using CMTest.Tool;
using System.IO;
using ATLib;

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
        private readonly MasterPlusTestCases _MpCases;
        private readonly UtilCmd _cmd = new UtilCmd();
        private readonly XmlOps _xmlOps = new XmlOps();
        private readonly MonitorAction _monitorAction = new MonitorAction();
        public static UtilSerialRelayController USBController = new UtilSerialRelayController();
        public static KeysSpyOp KeysSpy;
        private RemoteOS _remoteOs;
        //public static object UtilRegrex { get; private set; }
        private readonly IDictionary<string, Func<dynamic>> _optionsTopMenu = new Dictionary<string, Func<dynamic>>();
        private readonly IDictionary<string, Func<dynamic>> _optionsXmlPlugInOutDeviceNames = new Dictionary<string, Func<dynamic>>();
        private readonly IReadOnlyList<string> _listXmlTestLanguages = new List<string>() {"Deutsch", "English",
            "Español", "Français", "Italiano", "Korean", "Malay", "Português (Portugal)", "Thai", "Türkçe", "Vietnamese", "Русский", "繁體中文", "中文（简体）" };

        public IEnumerable<string> Aaa()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"i = {i}");
                for (int j = 0; j < 2; i++)
                {
                    yield return $"j = {j}";
                }
            }
        }


        public TestIt()
        {
            Console.WriteLine(Aaa());





            _MpCases = new MasterPlusTestCases();
            KeysSpy = new KeysSpyOp(_MpCases.MpActions.KeySpyRelativePath);
            _portalTestFlows = new PortalTestFlows();
            //AssembleTopMenu();
            GetKeyboardKeysFromKeyMapTabFile();
            GetMatrixFromFile();
            UtilKeys.SetKeyByKeyStatus(KbKeys.SC_KEY_NUM_LOCK.KeyValue, UtilKeys.Status.On);
            UtilKeys.SetKeyByKeyStatus(KbKeys.SC_KEY_CAP.KeyValue, UtilKeys.Status.Off);

            UtilLoop.testA();


            _MpCases.Case_AssignInLoop(true, false, true);
            this.Suit_KeyMappingBaseTest("SK652");
            UtilProcess.KillAllProcessesByName("wmplayer");
            UtilTime.WaitTime(1);
            var p =UtilWmp.StartWmpWithMedias(Path.Combine(_MpCases.MpActions.MediaFolderPath, "1.mp3"), Path.Combine(_MpCases.MpActions.MediaFolderPath, "2.mp3"), Path.Combine(_MpCases.MpActions.MediaFolderPath, "3.mp3"));
            var wmpWindow = new AT().GetElementFromHwndAndWaitAppears(p);
            var sliderbar = wmpWindow.GetElementFromDescendants(new ATElementStruct() { ControlType = AT.ControlType.Slider });
            var barValue1 = sliderbar.DoGetValue();
            UtilTime.WaitTime(0.5);
            var barValue2 = sliderbar.DoGetValue();
            _MpCases.Case_CheckAllKeysOnRelayController();
        }
        private void SetKeyStatus()
        {

        }
        public static void SendUsbKeyAndCheck(KeyPros key, string expectedResult = null)
        {
            TestIt.KeysSpy.ClickClear();
            TestIt.USBController.SendToPort(key.Port, executedWait:0.3);
            var actualResultList = TestIt.KeysSpy.GetContentList();
            if (expectedResult == null)
            {
                if (actualResultList != null)
                {
                    throw new Exception($"The key still has output. - VarName: [{key.VarName}] - Actual: [{TestIt.KeysSpy.GetContentList().ElementAt(0)}] - Expected:[No any output] Port:[{key.Port}] Ui:[{key.UiaName}]");
                }
                return;
            }
            if (actualResultList != null)
            {    
                if (!string.Join("", actualResultList.ToArray()).Equals(expectedResult))
                {
                    throw new Exception($"Inconsistent keys. - VarName: [{key.VarName}] - Actual: [{string.Join("", actualResultList.ToArray())}] - Expected:[{expectedResult}] Port:[{key.Port}] Ui:[{key.UiaName}]");
                }
            }
            else
            {
                throw new Exception($"No key captured. - VarName: [{key.VarName}] - Expected:[{expectedResult}] Port:[{key.Port}] Ui:[{key.UiaName}]");
            }
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
                 _optionsTopMenu.Add(_MpCases.MpActions.SwName, () => {
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
