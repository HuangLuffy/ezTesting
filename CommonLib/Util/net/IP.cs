using System.Net;

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
