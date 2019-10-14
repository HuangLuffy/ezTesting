using System;
using System.Threading;
using CMTest.Project.MasterPlusPer;
using CommonLib.Util;
using Nancy;
using RemoteLib.Request;

namespace CMTest.Project.RemoteModule
{
    public class MonitorAction
    {
        private readonly RequestApi _requestApi;
        public struct ApiNames{
            public const string StartMonitorCrash = "StartMonitorCrash";
            public const string AbortMonitorCrash = "AbortMonitorCrash";
            public const string CrashOccurred = "CrashOccurred";
        }
        //private readonly PortalTestActions _portalTestActions = new PortalTestActions();
        private readonly Portal _portal = new Portal();
        public static Thread MonitorCrashThread;
        public HttpStatusCode StartMonitorCrash()
        {
            UtilCmd.Clear();
            UtilCmd.WriteLine("Crash Monitor is running!");
            //UtilCmd.WriteLine("*********************************************");
            UtilProcess.StartProcess(_portal.SwLnkPath);
            UtilTime.WaitTime(1);
            MonitorCrashThread = new Thread(() =>
            {
                while (true)
                {
                    if (UtilProcess.IsProcessExistedByName(_portal.SwProcessName))
                    {
                        UtilTime.WaitTime(0.5);
                    }
                    else
                    {
                        UtilCmd.WriteLine("Crash occurred!");
                        var t = RequestApi.Get("http://10.10.51.59:9100/Crashed");
                        AbortMonitorCrash();
                        return;
                    }
                }
            });
            MonitorCrashThread.Start();
            return HttpStatusCode.OK;
        }
        public HttpStatusCode AbortMonitorCrash()
        {
            if (MonitorCrashThread != null && MonitorCrashThread.IsAlive)
            {
                MonitorCrashThread.Abort();
            }
            UtilCmd.WriteLine("Crash Monitor aborted!");
            return HttpStatusCode.OK;
        }
        public HttpStatusCode CrashOccurred()
        {
            if (MonitorCrashThread != null && MonitorCrashThread.IsAlive)
            {
                MonitorCrashThread.Abort();
            }
            UtilCmd.WriteLine("Crash occurred!");
            return HttpStatusCode.OK;
        }
        public bool IsRemoteOsAvailable()
        {
            return true;
        }
    }
}
