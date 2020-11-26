using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Util;

namespace CommonLib
{
    public class UtilCmdSimple
    {
        private const string MARK_FOUND_RESULT = "FOUND_TEST";
        private const string MARK_DO_NOTHING = "DO_NOTHING";
        private const string OPTION_COMMENT_SEPARATOR_PREFIX = " - \"";
        private const string OPTION_COMMENT_SEPARATOR_SUFFIX = "\"";
        private Dictionary<string, Func<UtilCmdSimple>> _currentScreenDic;
        public const string StringConnector = ". ";
        private List<string> _listLastMenu = new List<string>();
        private List<string> _listCurrentMenu = new List<string>();
        private Dictionary<string, UtilCmdSimple> _subScreenDic = new Dictionary<string, UtilCmdSimple>();
        public UtilCmdSimple()
        {
            _currentScreenDic = NewScreenDic();
        }
        public struct Result
        {
            public const string FOUND_RESULT = "FOUND_RESULT";
            public const string FOUND_NULL = "FOUND_NULL";
            public const string DO_NOTHING = "DO_NOTHING";
            public const string SHOW_MENU_AGAIN = "Show Menu Again";
            public const string BACK = "Back";
        }
        private Dictionary<string, Func<UtilCmdSimple>> NewScreenDic()
        {
           return new Dictionary<string, Func<UtilCmdSimple>>();
        }
        public UtilCmdSimple AddOption(string option, Func<UtilCmdSimple> func, string comment = "")
        {
            _currentScreenDic.Add(comment.Equals("") ? option : AddCommentForOption(option, comment), func);
            var newSubScreen = new UtilCmdSimple();
            _subScreenDic.Add(comment.Equals("") ? option : AddCommentForOption(option, comment), newSubScreen);
            return newSubScreen;
        }
        public List<string> WriteCmdMenu(bool clear = false, bool lineUpWithNumber = true, List<string> options = null)
        {
            var t = new List<string>();
            if (clear)
            {
                Console.Clear();
            }
            for (var i = 1; i <= options.Count; i++)
            {
                t.Add(lineUpWithNumber ? $"{i.ToString()}{StringConnector}{options[i - 1]}" : $"{options[i - 1]}");
                Console.WriteLine(t[i - 1]);
            }

            if (!IsMenuChanged(_listCurrentMenu, t)) return t;
            _listLastMenu = _listCurrentMenu;
            _listCurrentMenu = t;
            return t;
        }
        public dynamic ShowCmdMenu(IDictionary<string, Func<UtilCmdSimple>> optionDictionary = null, IDictionary<string, Func<UtilCmdSimple>> parentOptionDictionary = null)
        {
            if (optionDictionary == null)
            {
                optionDictionary = _currentScreenDic;
            }
            var menuOptions = WriteCmdMenu(options: optionDictionary.Keys.ToList());
            while (true)
            {
                menuOptions = WriteCmdMenu(true, false, menuOptions);
                var input = ReadLine();
                var result = FindMatchedFuncAndRun(optionDictionary, input, menuOptions);
                switch (result)
                {
                    case null:
                        continue;
                    case Result.BACK:
                    {
                        if (parentOptionDictionary != null)
                        {
                            ShowCmdMenu(parentOptionDictionary);
                        }
                        break;
                    }
                    default:
                    {
                        return result;
                    }
                }
            }
        }
        private dynamic FindMatchedFuncAndRun(IDictionary<string, Func<UtilCmdSimple>> optionDictionary, string selected, IEnumerable<string> comparedOptions)
        {
            dynamic r = null;
            var matchedOption = FindMatchedOption<string>(optionDictionary.Keys.ToList(), selected, comparedOptions);
            if (matchedOption != null)
            {
                r = optionDictionary[matchedOption].Invoke(); 
            }

            var a = r as UtilCmdSimple;
            if (matchedOption != null && a._subScreenDic.ContainsKey(matchedOption) && a._subScreenDic[matchedOption]._currentScreenDic.Count > 0)
            {
                ShowCmdMenu(a._subScreenDic[matchedOption]._currentScreenDic);
            }

            return r;  //t == null ? null : optionDictionary[t].Invoke();
        }
        private static T FindMatchedOption<T>(IReadOnlyList<string> listAll, string selected, IEnumerable<string> comparedOptions)
        {
            foreach (var option in comparedOptions.Select(comparedOption => GetSelectedName(listAll, selected, comparedOption)).Where(option => option != null))
            {
                return (T)Convert.ChangeType(option, typeof(T));
            }
            //foreach (var comparedOption in comparedOptions)
            //{
            //    var option = GetSelectedName(listAll, selected, comparedOption);
            //    if (option != null)
            //    {
            //        return (T)Convert.ChangeType(option, typeof(T));
            //    }
            //}
            return default(T);
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
            return $"{selectedNum.Trim()}{UtilCmd.StringConnector}{testName}".Equals(optionOneByOne);
            // return $"{selectedNum.Trim()}{UtilCmd.StringConnector}{testName}".Equals(RemoveCommentFromOption(optionOneByOne));
        }
        public static void PressAnyContinue(string s = "Please press any key to continue.")
        {
            Console.WriteLine(s);
            ReadLine();
        }
        public static void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        private void AssembleTopMenu1(string option, Func<dynamic> func, string comment)
        {
            var optionsTopMenu = new Dictionary<string, Func<UtilCmdSimple>>();

            //if (optionsTopMenu.Any()) return;
            //optionsTopMenu.Add(AddCommentForOption(OPTION_CONNECT_IP, _xmlOps.GetRemoteOsIp().Trim()), () => {
            //    var remoteOsIp = _xmlOps.GetRemoteOsIp().Trim();
            //    ConnectRemoteOsAvailable(remoteOsIp);
            //    return _cmd.ShowCmdMenu(optionsTopMenu);
            //});
            //optionsTopMenu.Add(OPTION_INPUT_IP, () => {
            //    CustomizeIp();
            //    return _cmd.ShowCmdMenu(optionsTopMenu);
            //});
            //optionsTopMenu.Add(_portalTestFlows.PortalTestActions.SwName, () => {
            //    AssemblePortalTests();
            //    return _cmd.ShowCmdMenu(_optionsPortalTestsWithFuncs, optionsTopMenu);
            //});
            //optionsTopMenu.Add(_mpTestFlows.MpActions.SwName, () => {
            //    AssembleMasterPlusPlugInOutTests();
            //    return _cmd.ShowCmdMenu(_optionsTestsWithFuncs, optionsTopMenu);
            //});
        }
        private static bool IsMenuChanged(IReadOnlyList<string> a, IReadOnlyList<string> b)
        {
            if (a.Count != b.Count)
            {
                return true;
            }

            if (!a.Any()) return false;
            for (var i = 0; i < a.Count(); i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return true;
                }
            }
            return false;
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
    }
}
