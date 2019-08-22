using System.Text.RegularExpressions;

namespace CommonLib.Util
{
    public class UtilRegex
    {
        //private string REGULAR_EMAIL = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //private string REGULAR_URL = @"[a-zA-z]+://[^\s]*";
        public static bool IsNumeric(string str)
        {
            Regex _Regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return _Regex.IsMatch(str);
        }
        public static bool IsIP(string ip)
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
            Regex reg = new Regex("^[+-]?[0-9]+$");
            Match match = reg.Match(value);
            return match.Success;
        }
    }
}
