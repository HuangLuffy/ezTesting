using System;
using System.Globalization;

namespace CommonLib.Util
{
    public class UtilString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static string[] GetSplitArray(string value, string rule)
        {
            var ids = value.Split(new[] { rule }, StringSplitOptions.RemoveEmptyEntries);
            return ids;
        }
        public static string RemoveLastChar(string ori)
        {
            return ori.Substring(0, ori.Length - 1);
        }
        public static string RemoveFirstChar(string ori)
        {
            if (ori.Substring(0, 1) == "1")
            {
                ori = ori.Substring(1);
            }
            return ori;
        }
        public static string RemoveAllMatchedFirstChar(string ori, string match)
        {
            for (var i = 0; i < ori.Length; i++)
            {
                if (ori.StartsWith(match) && ori.Substring(0, 1) == "1")
                {
                    ori = ori.Substring(1);
                }
            }
            return ori;
        }
        public static string GetAddressByRemoteFolderPath(string remoteFolderPath)
        {
            return !remoteFolderPath.StartsWith(@"\\") ? remoteFolderPath : remoteFolderPath.Replace(@"\\", "").Split('\\')[0];
        }
        public class ConvertIt
        {///  
         /// 转换数字成单个16进制字符，要求输入值小于16
         ///  
         /// value
         ///  
            public static string GetHexChar(string value)
            {
                var sReturn = string.Empty;
                switch (value)
                {
                    case "10":
                        sReturn = "A";
                        break;
                    case "11":
                        sReturn = "B";
                        break;
                    case "12":
                        sReturn = "C";
                        break;
                    case "13":
                        sReturn = "D";
                        break;
                    case "14":
                        sReturn = "E";
                        break;
                    case "15":
                        sReturn = "F";
                        break;
                    default:
                        sReturn = value;
                        break;
                }
                return sReturn;
            }
            public static string ConvertHex(string value)
            {
                var sReturn = string.Empty;
                try
                {

                    while (ulong.Parse(value) >= 16)
                    {
                        var v = ulong.Parse(value);
                        sReturn = GetHexChar((v % 16).ToString()) + sReturn;
                        value = Math.Floor(Convert.ToDouble(v / 16)).ToString(CultureInfo.InvariantCulture);
                    }
                    sReturn = GetHexChar(value) + sReturn;
                }
                catch
                {
                    sReturn = "###Valid Value!###";
                }
                return sReturn;
            }
            public static long ConvertHexToDecimal(string value)
            {
                return Convert.ToInt32(value, 16);
            }
        } 
    }
}
