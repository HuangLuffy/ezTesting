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
        private Dictionary<string, Func<dynamic>> _currentScreenDic;
        private const string StringConnector = ". ";
        private IDictionary<string, Tuple<UtilCmdSimple, UtilCmdSimple>> _dic = new Dictionary<string, Tuple<UtilCmdSimple, UtilCmdSimple>>();
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
        private Dictionary<string, Func<dynamic>> NewScreenDic()
        {
           return new Dictionary<string, Func<dynamic>>();
        }
        public UtilCmdSimple AddOption(string option, Func<dynamic> func, string comment = "")
        {
            _currentScreenDic.Add(comment.Equals("") ? option : AddCommentForOption(option, comment), func);
            var newSubScreen = new UtilCmdSimple();
            _dic.Add(comment.Equals("") ? option : AddCommentForOption(option, comment), new Tuple<UtilCmdSimple, UtilCmdSimple>(this, newSubScreen));
            return newSubScreen;
        }

        private List<string> WriteCmdMenu(bool clear = false, bool lineUpWithNumber = true, IReadOnlyList<string> options = null)
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
            return t;
        }
        public dynamic ShowCmdMenu(IDictionary<string, Func<dynamic>> optionDictionary = null, IDictionary<string, Func<dynamic>> parentOptionDictionary = null)
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
                var result = FindMatchedFuncAndRun(input, menuOptions);
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
        private dynamic FindMatchedFuncAndRun(string selected, IEnumerable<string> comparedOptions)
        {
            dynamic r = null;
            var matchedOption = FindMatchedOption<string>(_currentScreenDic.Keys.ToList(), selected, comparedOptions);
            if (matchedOption != null)
            {
                r = _currentScreenDic[matchedOption].Invoke();
            }
            if (matchedOption != null && _dic.ContainsKey(matchedOption) && _dic[matchedOption].Item2._currentScreenDic.Count > 0)
            {
                if (!_dic[matchedOption].Item2._currentScreenDic.ContainsKey(Result.BACK))
                {
                    _dic[matchedOption].Item2._currentScreenDic.Add(Result.BACK, () => ShowCmdMenu(_currentScreenDic));
                }
                _dic[matchedOption].Item2.ShowCmdMenu(_dic[matchedOption].Item2._currentScreenDic);
            }
            return r;  
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
        //How to use it
        //var cmd = new UtilCmdSimple();
        //var option1Screen = cmd.AddOption("option1",
        //    () =>
        //    {
        //        Console.WriteLine("1-1");

        //        return cmd;
        //    }
        //);
        //var option2Screen = cmd.AddOption("option2",
        //    () =>
        //    {
        //        Console.WriteLine("1-2");
        //        return cmd;
        //    }
        //);
        //var option1SubScreen = option1Screen.AddOption("option21",
        //    () =>
        //    {
        //        Console.WriteLine("2-1");
        //        return option1Screen;
        //    }
        //);
        //option1SubScreen.AddOption("option31",
        //() =>
        //{
        //    Console.WriteLine("3-1");
        //    return option1SubScreen;
        //}
        //);
        //cmd.ShowCmdMenu();
    }
}
