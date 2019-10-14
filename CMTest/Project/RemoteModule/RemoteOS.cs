using RemoteLib.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.RemoteModule
{
    public class RemoteOS
    {
        private RequestApi _requestApi;
        public RemoteOS(string ip)
        {
            _requestApi = new RequestApi(IP);
        }
        public string IP { get; set; }
        public bool IsRemoteOsAvailable()
        {
            return true;
        }
    }
}
