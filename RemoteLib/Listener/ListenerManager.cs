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
        public static bool IsListenerRunning()
        {
            return _listener != null;
        }
        public static void IfListenerIsRunningThenStop()
        {
            if (IsListenerRunning())
            {
                _listener.Stop();
            }
        }
    }
}
