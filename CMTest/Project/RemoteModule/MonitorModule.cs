using Nancy;

namespace CMTest.Project.RemoteModule
{
    public sealed class MonitorModule : NancyModule
    {
        private readonly MonitorAction _monitorCrashAction = new MonitorAction();
        public MonitorModule()
        {
            Get("/", x => HttpStatusCode.OK);
            //Get("MonitorCrash/{hostAddress}", x =>
            //{
            //    var a = x.hostAddress;
            //    return _monitorCrashAction.GoMonitorCrashStatus();
            //});
            Get("StartMonitorCrash", x => _monitorCrashAction.StartMonitorCrash());
            Get("AbortMonitorCrash", x => _monitorCrashAction.AbortMonitorCrash());
            Get("CrashOccurred", x => _monitorCrashAction.CrashOccurred());
        }
    }
}
