using System;
using System.Threading.Tasks;
using CommonLib.Util;
using RemoteLib.Listener;

namespace CMTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var om = OpenListener();
            var testIt = new TestIt();    
            if (!testIt.IsNeededRunCmdDirectly(args))
            {
                testIt.OpenCmd();
            }
            //Task.WaitAll(om);
            Console.ReadKey();
            ListenerManager.GetListener().Stop();
        }
        
        private static async Task OpenListener()
        {
            await Task.Run(() =>
            {
                ListenerManager.SetListener(ListenerManager.ListenerChooser.Nancy);
                var listener = ListenerManager.GetListener();
                listener.Start();
                UtilCmd.WriteTitle($"Current Address: [{listener.GetAddress()}]");
            });
        }
    }
}

