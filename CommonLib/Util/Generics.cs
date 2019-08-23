using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Util
{
    public static class Generics
    {
        public static bool IsEmpty<T>(this IEnumerable<T> data)
        {
            try
            {
                return data.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
