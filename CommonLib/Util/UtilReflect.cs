using System;
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
    }
}
