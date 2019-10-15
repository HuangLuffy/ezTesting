using RemoteLib.Listener;
using RemoteLib.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.RemoteModule
{
    public class RemoteOS
    {
        private RequestApi _requestApi;
        public RemoteOS(string ip)
        {
            IP = ip;
            _requestApi = new RequestApi(IP, ListenerManager.GetListener().GetPort());
        }
        public string IP { get; set; }
        public bool IsRemoteOsAvailable()
        {
            return _requestApi.GetApi().Equals(Apis.Status_ListenerIsRunning);
        }
    }
}
