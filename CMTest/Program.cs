using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using RemoteLib.Listener;

namespace CMTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var listener = ListenerManager.GetListener();
            listener.Start();
            Console.Title = $"Current OS Address: [{listener.GetAddress()}]";

        var testIt = new TestIt();    
            if (!testIt.IsNeededRunCmdDirectly(args))
            {
                testIt.OpenCmd();
            }
            Console.ReadKey();
            listener.Stop();
        }
    }
}

