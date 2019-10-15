using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.RemoteModule
{
    public struct Apis
    {
        public const string Do_StartMonitorCrash = "StartMonitorCrash";
        public const string Do_AbortMonitorCrash = "AbortMonitorCrash";
        public const string Status_CrashOccurred = "CrashOccurred";
        public const string Status_ListenerIsRunning = "ListenerIsRunning";
    }
}
