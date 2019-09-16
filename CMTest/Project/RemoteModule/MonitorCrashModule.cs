using Nancy;

namespace CMTest.Project.RemoteModule
{
    public sealed class MonitorCrashModule : NancyModule
    {
        private readonly MonitorCrashAction _monitorCrashAction = new MonitorCrashAction();
        public MonitorCrashModule()
        {
            Get("/", x => "Hello World");
            //Get("MonitorCrash/{hostAddress}", x =>
            //{
            //    var a = x.hostAddress;
            //    return _monitorCrashAction.GoMonitorCrashStatus();
            //});
            Get("MonitorCrash", x => _monitorCrashAction.GoMonitorCrashStatus());
        }
    }
}
