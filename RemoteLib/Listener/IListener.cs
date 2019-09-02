namespace RemoteLib.Listener
{
    public interface IListener
    {
        void Start();
        void Stop();
        string GetAddress();
        string GetIp();
        string GetPort();
    }
}
