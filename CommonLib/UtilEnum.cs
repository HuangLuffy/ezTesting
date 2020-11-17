using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class UtilEnum
    {
        public static string GetEnumNameByValue<T>(Enum e)
        {
            return Enum.GetName(typeof(T), e);
        }
    }
}
