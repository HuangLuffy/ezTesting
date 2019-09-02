using System;
using CommonLib.Util.net;

namespace RemoteLib.Listener
{
    public abstract class BaseListener: IListener
    {
        public struct RespondStatus
        {
            public const string Ok = "OK";
            public const string Failed = "Failed";
        }
        public enum ServerStatus
        {
            Running = 1,
            Unavailable = 2,
            Unknown = 3
        }
        public abstract void Start();
        public abstract void Stop();
        public abstract string GetAddress();
        public string GetIp()
        {
            try
            {
                return UtilIp.GetPhysicsNetworkCardIp()[0];
            }
            catch (Exception)
            {
                return "";
            }
        }
        public abstract string GetPort();
    }
}
