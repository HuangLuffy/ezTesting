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
            var testIt = new TestIt();
            //testIt.RestartSystemAndCheckDeviceRecognition();
            //return;
            var om = OpenListener();
            if (!testIt.IsNeededRunCmdDirectly(args))
            {
                testIt.ShowTopMenu();
            }
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
                UtilCmd.WriteTitle($"Local OS Address: [{listener.GetAddress()}]");
            });
        }
    }
}

