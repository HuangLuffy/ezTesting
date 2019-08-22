using System;
using static System.String;

namespace CommonLib.Util
{
    public class UtilMatch
    {
        public static string AssembleMatchStrings(string[] arrString, string mode = Var.Mark.And, string wildcard = Var.Mark.Wildcard)
        {
            string t = "";
            for (int i = 0; i < arrString.Length; i++)
            {
                t += arrString[i];
                if (!IsNullOrEmpty(wildcard) && !arrString[i].Contains(Var.Mark.Wildcard))
                {
                    t += Var.Mark.Wildcard;
                }
                if (i != arrString.Length - 1)
                {
                    t += mode;
                }
            }
            return t;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTargetString"></param>
        /// <param name="strMatchStrings"></param>
        /// <returns></returns>
        public static bool NameMatch(string strTargetString, string strMatchStrings)
        {
            var mode = strMatchStrings.Contains(Var.Mark.Or) ? Var.Mark.Or : Var.Mark.And;
            var names = strMatchStrings.Split(new[] { mode }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var n in names)
            {
                if (strMatchStrings.Contains(Var.Mark.And))
                {
                    if (strMatchStrings.Contains(Var.Mark.Wildcard))
                    {
                        if (!strTargetString.ToLower().Contains(n.Replace(Var.Mark.Wildcard, "").ToLower())) return false;
                    }
                    else
                    {
                        if (!strTargetString.ToLower().Equals(n.ToLower())) return false;
                    }
                }
                else
                {
                    if (strMatchStrings.Contains(Var.Mark.Wildcard))
                    {
                        if (strTargetString.ToLower().Contains(n.Replace(Var.Mark.Wildcard, "").ToLower())) return true;
                    }
                    else
                    {
                        if (strTargetString.ToLower().Equals(n.ToLower())) return true;
                    }
                }
            }
            return strMatchStrings.Contains(Var.Mark.And);
        }
    }
}
