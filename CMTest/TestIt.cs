using CommonLib.Util;
using CMTest.Project;
using CMTest.Project.MasterPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using CMTest.Project.MasterPlusPer;
using CMTest.Xml;
using CMTest.Project.RemoteModule;
using System.Threading;

namespace CMTest
{
    public partial class TestIt : AbsResult
    {
        private const string MARK_FOUND_RESULT = "FOUND_TEST";
        private const string MARK_DO_NOTHING = "DO_NOTHING";
        private const string OPTION_COMMENT_SEPARATOR_PREFIX = " - \"";
        private const string OPTION_COMMENT_SEPARATOR_SUFFIX = "\"";
        private const string OPTION_CONNECT_IP = "Connect the Remote IP if it is correct";
        private const string OPTION_INPUT_IP = "Input your Remote IP";
        private readonly PortalTestFlows _portalTestFlows;
        private readonly MasterPlusTestFlows _masterPlusTestFlows;
        private readonly UtilCmd _cmd = new UtilCmd();
        private readonly XmlOps _xmlOps = new XmlOps();
        private readonly MonitorAction _monitorAction = new MonitorAction();
        private RemoteOS _remoteOS;

        public static object UtilRegrex { get; private set; }
        private readonly IDictionary<string, Func<dynamic>> _optionsTopMenu = new Dictionary<string, Func<dynamic>>();
        public TestIt()
        {
            _masterPlusTestFlows = new MasterPlusTestFlows();
            _portalTestFlows = new PortalTestFlows();
            AssembleTopMenu();
        }

        private void AssembleTopMenu()
        {
            if (_optionsTopMenu.Any()) return;
                _optionsTopMenu.Add(AddCommentForOption(OPTION_CONNECT_IP, _xmlOps.GetRemoteOsIp().Trim()), () => {
                var remoteOsIp = _xmlOps.GetRemoteOsIp().Trim();
                ConnectRemoteOsAvailable(remoteOsIp); return _cmd.ShowCmdMenu(_optionsTopMenu);
            });
                _optionsTopMenu.Add(OPTION_INPUT_IP, () => {
                CustomizeIp(); return _cmd.ShowCmdMenu(_optionsTopMenu);
            });
                _optionsTopMenu.Add(_portalTestFlows.PortalTestActions.SwName, () => {
                AssemblePortalPlugInOutTests();
                return _cmd.ShowCmdMenu(_optionsPortalTestsWithFuncs, _optionsTopMenu);
            } );
                 _optionsTopMenu.Add(_masterPlusTestFlows.MasterPlusTestActions.SwName, () => {
                AssembleMasterPlusPlugInOutTests();
                return _cmd.ShowCmdMenu(_optionsMasterPlusTestsWithFuncs, _optionsTopMenu);
            });
        }

        public struct RunDirectly
        {
            public bool run;
            public string device;
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
        // Top memu
        public void ShowTopMenu()
        { 
            try
            {
                var result = _cmd.ShowCmdMenu(_optionsTopMenu);
                if (result.Equals(MARK_FOUND_RESULT))
                {
                    UtilCmd.WriteLine(" >>>>>>>>>>>>>> Test Done! PASS");
                    return;
                }
                //_cmd.WriteCmdMenu(_optionsProjects, true);
            }
            catch (Exception ex)
            {
                HandleWrongStepResult(ex.Message);
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
            string currentTitle = UtilCmd.GetTitle();
            Thread WaitAnimation = WaitAnimationThread("Connecting...");
            try
            {
                _remoteOS = new RemoteOS(remoteOsIp);
                WaitAnimation.Start();
                _remoteOS.IsRemoteOsAvailable();
                WaitAnimation.Abort();
                UtilCmd.PressAnyContinue("The communication between the Local OS and the Remote OS established successfully. Press any key to continue.");
            }
            catch (Exception)
            {
                WaitAnimation.Abort();
                UtilCmd.WriteTitle(currentTitle);
                throw;
            }
        }

        private Thread WaitAnimationThread(string comment)
        {
            return new Thread(() =>
            {
                int s = 0;
                while (true)
                {
                    UtilTime.WaitTime(1);
                    UtilCmd.WriteTitle($"{comment}  Please Wait...  Time elapsed {s++}s ");
                }
            });
        }

        private static string AddCommentForOption(string oriComment, string addedComment)
        {
            return $"{oriComment}{OPTION_COMMENT_SEPARATOR_PREFIX}{addedComment}{OPTION_COMMENT_SEPARATOR_SUFFIX}";
        }
        private static string RemoveCommentFromOption(string commnet)
        {
            return UtilString.GetSplitArray(commnet, OPTION_COMMENT_SEPARATOR_PREFIX).ToList()[0];
        }
        private static string GetCommentFromOption(string commnet)
        {
            return UtilRegex.GetMatchMidValue(commnet, OPTION_COMMENT_SEPARATOR_PREFIX, OPTION_COMMENT_SEPARATOR_SUFFIX);
        }
    }
}
