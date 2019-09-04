using System;
using System.Threading;
using CMTest.Project.MasterPlusPer;
using CommonLib.Util;
using Nancy;

namespace CMTest.Project.RemoteModule
{
    public class MonitorCrashAction
    {
        private readonly PortalTestActions _portalTestActions = new PortalTestActions();
        private readonly Portal _portal = new Portal();
        public HttpStatusCode GoMonitorCrashStatus()
        {
            UtilCmd.Clear();
            UtilCmd.WriteLine("Crash Monitor is running!");
            UtilCmd.WriteLine("*********************************************");
            UtilProcess.StartProcess(_portal.SwLnkPath);
            UtilTime.WaitTime(1);
            var monitorExe = new Thread(() =>
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
                        return;
                    }
                }
            });
            monitorExe.Start();
            return HttpStatusCode.OK;
        }
        public string IsIp()
        {
            return ";";
        }

        public string GetHostAddress()
        {
            return ";";
        }
    }
}
