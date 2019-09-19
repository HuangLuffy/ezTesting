using System.IO;
using System.Net;

namespace RemoteLib.Request
{
    public class RequestApi
    {
        public struct RequestMethod
        {
            public const string PUT = "PUT";
            public const string POST = "POST";
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
    }
}
