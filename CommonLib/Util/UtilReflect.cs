using System;
using System.Reflection;

namespace CommonLib.Util
{
    public class UtilReflect
    {
        public static FieldInfo[] GetFieldsInfoBy<T>(object obj)
        {
            Type t = obj.GetType();
            //Type t = typeof();
            return t.GetFields();
        }
    }
}
