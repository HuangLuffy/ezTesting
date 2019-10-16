using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Util
{
    public class UtilCmd
    {

        public const string StringConnector = ". ";
        private List<string> _listLastMenu = new List<string>();
        private List<string> _listCurrentMenu = new List<string>();
        public struct Result
        {
            public const string FOUND_RESULT = "FOUND_RESULT";
            public const string FOUND_NULL = "FOUND_NULL";
            public const string DO_NOTHING = "DO_NOTHING";
            public const string SHOW_MENU_AGAIN = "Show Menu Again";
            public const string BACK = "Back";
        }
        //public enum ImageType
        //{
        //    PNG = 1,
        //    BMP = 2,
        //    JPG = 3
        //}
        public string MenuShowAgain()
        {
            WriteCmdMenu(_listCurrentMenu, lineUpWithNumber: false);
            return Result.SHOW_MENU_AGAIN;
        }

        public string MenuGoBack()
        {
            WriteCmdMenu(_listLastMenu, lineUpWithNumber: false);
            return Result.BACK;
        }

        public List<string> WriteCmdMenu(List<string> options, bool clear = false, bool lineUpWithNumber = true)
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
        public static void Clear()
        {
            Console.Clear();
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
        public static string GetTitle()
        {
            return Console.Title;
        }
        public static void WriteTitle(string title = "")
        {
            Console.Title = title;
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
        public dynamic ShowCmdMenu(IDictionary<string, Func<dynamic>> optionDictionary, IDictionary<string, Func<dynamic>> parentOptionDictionary = null)
        {
            var menuOptions = WriteCmdMenu(optionDictionary.Keys.ToList());
            while (true)
            {
                menuOptions = WriteCmdMenu(menuOptions, true, false);
                var input = ReadLine();
                var result = FindMatchedFuncAndRun(optionDictionary, input, menuOptions);
                if (result == null)
                {
                    continue;
                }
                else if (result.Equals(Result.BACK))
                {
                    if (parentOptionDictionary != null)
                    {
                        ShowCmdMenu(parentOptionDictionary, null);
                    }
                }
                else if (result != null)
                {
                    return result;
                }
            }
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
        private static string FindMatchedString(IReadOnlyList<string> optionList, string selected, IEnumerable<string> comparedOptions)
        {
            return FindMatchedOption<string>(optionList, selected, comparedOptions);
        }
        private static string FindMatchedFuncAndRun(IDictionary<string, Func<dynamic>> optionDictionary, string selected, IEnumerable<string> comparedOptions)
        {
            var t = FindMatchedOption<string>(optionDictionary.Keys.ToList(), selected, comparedOptions);
            return t == null ? null : optionDictionary[t].Invoke();
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
    }
}
