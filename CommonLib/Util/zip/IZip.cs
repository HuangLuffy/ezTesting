using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.zip
{
    public interface IZip
    {
        void ExtractZip(string source, string destination);
        void ExtractZip(string source);
    }
}
