using RemoteLib.Host.Nancy;

namespace RemoteLib.Listener
{
    public static class ListenerManager
    {
        private static BaseListener _listener;
        public static BaseListener GetListener()
        {
            return _listener = _listener ?? new NancySelfHost();
            //return listener = ruleSession == null ? listener : new NancySelfHost(ruleSession);
        }
        public static bool IsListenerAvailable()
        {
            return _listener != null;
        }
    }
}
