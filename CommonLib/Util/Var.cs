using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class Var
    {
        public struct Mark
        {
            public const string __ = "__";
            public const string Or = "||";
            public const string And = "&&";
            public const string Wildcard = ".*";
            public const string GreaterEqual = ">=";
        }
    }
}
