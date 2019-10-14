using CommonLib.Util;
using System.IO;
using System.Net;

namespace RemoteLib.Request
{
    public class RequestApi
    {
        public string Address { set; get; }
        public object UtilStirng { get; }

        public struct RequestMethod
        {
            public const string PUT = "PUT";
            public const string POST = "POST";
        }
        public RequestApi(string ip)
        {
            if (!ip.ToLower().Contains("http"))
            {
                Address = $"http://{ip}";
            }
        }
        public string SendApi(string command, string requestMethod = RequestMethod.POST)
        {
            var address = $"{Address}/{command}";
            return Send(address);
        }
        public string GetApi(string command)
        {
            var address = $"{Address}/{command}";
            return Get(address);
        }

        public static string Send(string address, string requestMethod = RequestMethod.POST)
        {
            var request = WebRequest.Create(address);
            request.Method = requestMethod;
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            using (var writer = new StreamWriter(request.GetRequestStream()) { AutoFlush = true })
            {
                using (var responseStream = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    return responseStream.ReadToEnd();
                }
            }
        }
        public static string Get(string address)
        {
            var response = WebRequest.Create(address).GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        private static string AssembleAddressFromIp(string ip, string command)
        {
            return $"http://{ip}/{command}";
        }
    }
}
