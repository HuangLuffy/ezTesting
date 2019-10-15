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
        private const string FOUND_TEST = "FOUND_TEST";
        private const string DO_NOTHING = "DO_NOTHING";
        private const string OPTION_COMMENT_SEPARATOR_PREFIX = " - \"";
        private const string OPTION_COMMENT_SEPARATOR_SUFFIX = "\"";
        private const string OPTION_CONNECT_IP = "Connect the Remote IP if it is correct";
        private const string OPTION_INPUT_IP = "Input your Remote IP";
        private readonly PortalTestFlows _portalTestFlows;
        private readonly MasterPlusTestFlows _masterPlusTestFlows;
        private readonly List<string> _optionsProjects;
        private readonly UtilCmd _cmd = new UtilCmd();
        private readonly XmlOps _xmlOps = new XmlOps();
        private readonly MonitorAction _monitorAction = new MonitorAction();
        private RemoteOS _remoteOS;

        public static object UtilRegrex { get; private set; }

        public TestIt()
        {
            _masterPlusTestFlows = new MasterPlusTestFlows();
            _portalTestFlows = new PortalTestFlows();
            _optionsProjects = new List<string> { _portalTestFlows.PortalTestActions.SwName, _masterPlusTestFlows.MasterPlusTestActions.SwName
                , AddCommentForOption(OPTION_CONNECT_IP, _xmlOps.GetRemoteOsIp().Trim()), OPTION_INPUT_IP };
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
        public void OpenCmd()
        {
            //UtilCmd.ReadLine();
            //UtilProcess.StartProcess(@"C:\Users\Administrator\Desktop\Dbgview.exe");
            //UtilProcess.StartProcess(@"C:\Program Files (x86)\CoolerMaster\PORTAL\MasterPlus(PER. Only).exe");
            var projectOptions = _cmd.WriteCmdMenu(_optionsProjects);
            while (true)
            {
                try
                {
                    projectOptions = _cmd.WriteCmdMenu(projectOptions, true, false);
                    var s = UtilCmd.ReadLine();
                    var result = ShowCmdProjects(s, projectOptions);
                    if (result.Equals(FOUND_TEST))
                    {
                        UtilCmd.WriteLine(" >>>>>>>>>>>>>> Test Done! PASS");
                        return;
                    }
                    projectOptions = _cmd.WriteCmdMenu(_optionsProjects, true);
                }
                catch (Exception ex)
                {
                    HandleWrongStepResult(ex.Message);
                    UtilCmd.PressAnyContinue();
                }
            }
        }
        private string ShowCmdProjects(string selected, IEnumerable<string> options)
        {
            foreach (var option in options)
            {
                if (IsTestExisted(_masterPlusTestFlows.MasterPlusTestActions.SwName, selected, option))
                {
                    AssembleMasterPlusPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(_optionsMasterPlusTestsWithFuncs);
                }
                if (IsTestExisted(_portalTestFlows.PortalTestActions.SwName, selected, option))
                {
                    AssemblePortalPlugInOutTests();
                    return ShowCmdTestsBySelectedProject(_optionsPortalTestsWithFuncs);
                }
                if (IsTestExisted(OPTION_CONNECT_IP, selected, option))
                {
                    var remoteOsIp = GetCommentFromOption(option);
                    IsRemoteOsAvailable(remoteOsIp);
                }
                if (IsTestExisted(OPTION_INPUT_IP, selected, option))
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
                            IsRemoteOsAvailable(remoteOsIp);
                            break;
                        }
                        else
                        {
                            UtilCmd.WriteLine("The words you input is not a invalid IP. Re-input or input \"q\" to quit.");
                        }
                    }
                }
            }
            return DO_NOTHING;
        }
        private void IsRemoteOsAvailable(string remoteOsIp)
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
                UtilCmd.WriteTitle(currentTitle);
                WaitAnimation.Abort();
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
                    UtilCmd.WriteTitle($"{comment} {s++}s Please Wait... ");
                }
            });
        }
        private string ShowCmdTestsBySelectedProject(IDictionary<string, Func<dynamic>> testFuncsByTestName)
        {
            var options = _cmd.WriteCmdMenu(testFuncsByTestName.Keys.ToList());
            while (true)
            {
                options = _cmd.WriteCmdMenu(options, true, false);
                var input = UtilCmd.ReadLine();
                var result = FindMatchedTest(testFuncsByTestName, input, options);
                switch (result)
                {
                    case null:
                        //Back from submenu, so it should stay here. Or incorrect input
                        break; // break只终止了最近的switch，并没有终止while
                    case FOUND_TEST:
                        return FOUND_TEST;
                    case UtilCmd.OptionBack:
                        return UtilCmd.OptionBack;
                    default:
                        continue; // continue不止跳出了switch，还跳过了这一次while循环，没有输出。
                }
            }
        }
        private static T FindMatchedOption<T>(IReadOnlyList<string> listAll, string selected, IEnumerable<string> comparedOptions)
        {
            foreach (var deviceName in comparedOptions.Select(comparedOption => GetSelectedName(listAll, selected, comparedOption)).Where(deviceName => deviceName != null))
            {
                return (T)Convert.ChangeType(deviceName, typeof(T));
            }
            //foreach (var comparedOption in comparedOptions)
            //{
            //    var deviceName = GetSelectedName(listAll, selected, comparedOption);
            //    if (deviceName != null)
            //    {
            //        return (T)Convert.ChangeType(deviceName, typeof(T));
            //    }
            //}
            return default(T);
        }
        private static string FindMatchedDevice(IReadOnlyList<string> Options_Portal_PlugInOut_Device_Names, string selected, IEnumerable<string> comparedOptions)
        {
            return FindMatchedOption<string>(Options_Portal_PlugInOut_Device_Names, selected, comparedOptions);
        }
        private static string FindMatchedTest(IDictionary<string, Func<dynamic>> testFuncsByTestName, string selected, IEnumerable<string> comparedOptions)
        {
            var t = FindMatchedOption<string>(testFuncsByTestName.Keys.ToList(), selected, comparedOptions);
            return t == null ? null : testFuncsByTestName[t].Invoke();
        }
        private static string GetSelectedName(IEnumerable<string> listAll, string selectedNum, string comparedOptions)
        {
            return listAll.FirstOrDefault(t => IsTestExisted(t, selectedNum, comparedOptions));
            //for (var j = 0; j < listAll.Count; j++)
            //{
            //    if (IsTestExisted(listAll[j], selectedNum, comparedOptions))
            //    {
            //        return listAll[j];
            //    }
            //}
            //return null;
        }
        private static bool IsTestExisted(string testName, string selectedNum, string optionOneByOne)
        {
            return $"{selectedNum.Trim()}{UtilCmd.StringConnector}{testName}".Equals(RemoveCommentFromOption(optionOneByOne));
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
