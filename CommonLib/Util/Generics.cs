using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public static class Generics
    {
        public static bool IsEmpty<T>(this IEnumerable<T> data)
        {
            try
            {
                data.Any();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
