using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.net
{
    public class IP
    {
        public static bool IsIP(string ip)
        {
            IPAddress ip1;
            return IPAddress.TryParse(ip, out ip1);
        }
    }
}
