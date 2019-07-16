using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
