using System.Net;

namespace CommonLib.Util.net
{
    public class UtilIp
    {
        public static bool IsIp(string ip)
        {
            return IPAddress.TryParse(ip, out var ip1);
        }
    }
}
