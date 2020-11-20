using System;
using System.Linq;
using System.Reflection;

namespace CommonLib.Util
{
    public class UtilReflect
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
    }
}
