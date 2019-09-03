using RemoteLib.Host.Nancy;

namespace RemoteLib.Listener
{
    public static class ListenerManager
    {
        public enum ListenerChooser
        {
            Nancy = 0
        }
        private static BaseListener _listener;
        public static void SetListener(ListenerChooser listenerChooser)
        {
            _listener = ChoseListener(listenerChooser);
        }
        public static BaseListener GetListener()
        {
            return _listener;
        }
        private static BaseListener ChoseListener(ListenerChooser listenerChooser)
        {
            return listenerChooser.Equals(ListenerChooser.Nancy) ? new NancySelfHost() : null;
        }
    }
}
