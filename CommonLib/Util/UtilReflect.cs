using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLib.Util
{
    public static class UtilReflect
    {
        public static FieldInfo[] GetFieldsInfoBy<T>(object obj)
        {
            var t = obj.GetType();
            //Type t = typeof();
            return t.GetFields();
        }

        public static string GetVarNameByValue(Type obj, string value)
        {
            return obj.GetFields().First(x => x.GetValue(0).Equals(value)).Name;
            //typeof(ob).GetFields().First(x => x.GetValue(0).Equals("#00ff00")).Name;
        }
        public static IEnumerable<string> GetFieldsValues(Type obj)
        {
            return obj.GetFields().Select((x) => x.GetValue(0).ToString());
        }
        public static IEnumerable<string> GetFieldsNames(Type obj)
        {
            return obj.GetFields().Select((x) => x.Name.ToString());
        }
        public static IDictionary<string, string> GetFieldsNamesAndValuesDic(Type obj)
        {
            var tDic = new Dictionary<string, string>();
            foreach (var field in obj.GetFields())
            {
                tDic.Add(field.Name, field.GetValue(0).ToString());
            }
            return tDic;
        }
        public static IEnumerable<Tuple<string, string>> GetFieldsNamesAndValuesList(Type obj)
        {
            IEnumerable<Tuple<string, string>> tIEnumerable = new List<Tuple<string, string>>();
            foreach (var field in obj.GetFields())
            {
                tIEnumerable.Add(Tuple.Create(field.Name, field.GetValue(0).ToString()));
            }
            return tIEnumerable;
        }
        //public string GetMasterPlusLanguage(string overview)
        //{
        //    var field = typeof(Language).GetFields().FirstOrDefault((x) => ((Tuple<int, string, string, string, string>)x.GetValue(new Language())).Item4.Equals(overview));
        //    return ((Tuple<int, string, string, string, string>)field.GetValue(new Language())).Item3;
        //}
    }
}
