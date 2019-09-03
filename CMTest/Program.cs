using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CommonLib.Util;
using RemoteLib.Listener;

namespace CMTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OpenMonitor();
            var testIt = new TestIt();    
            if (!testIt.IsNeededRunCmdDirectly(args))
            {
                testIt.OpenCmd();
            }
            Console.ReadKey();
            ListenerManager.IfListenerIsRunningThenStop();
        }

        private static async void OpenMonitor()
        {
            await Task.Run(() =>
            {
                var listener = ListenerManager.GetListener();
                listener.Start();
                UtilCmd.WriteTitle($"Current Address: [{listener.GetAddress()}]");
            });
        }
    }
}

