﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CommonLib.Util
{
    public class UtilRegex
    {
        //private string REGULAR_EMAIL = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //private string REGULAR_URL = @"[a-zA-z]+://[^\s]*";
        public static bool IsNumeric(string str)
        {
            var regex = new Regex(@"^[0-9]\d*$");
            return regex.IsMatch(str);
        }
        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip.Trim(), @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$");
        }
        //public static bool IsNumeric(String value)
        //{
        //    return Regex.IsMatch(value, @" ^[+-]?\d*[.]?\d*$");
        //}

        public static string[] SplitByString(string oriString, string splitString)
        {
            return Regex.Split(oriString, splitString, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            var reg = new Regex("^[+-]?[0-9]+$");
            var match = reg.Match(value);
            return match.Success;
        }
        public static string GetMatchMidValue(string sourse, string startString, string endString)
        {
            var rg = new Regex("(?<=(" + startString + "))[.\\s\\S]*?(?=(" + endString + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(sourse).Value;
        }

        public static IEnumerable<string> GetStringsFromDoubleQuo(string sourse)
        {
            var re = new Regex("\"([^\"]*)\"", RegexOptions.IgnoreCase);
            return re.Matches(sourse).Cast<Match>().Select(m => m.Value.Replace("\"", ""))
                .ToArray(); ;
        }
    }
}
