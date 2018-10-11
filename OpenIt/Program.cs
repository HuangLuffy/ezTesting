using ATLib;
using CommonLib.Util;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace OpenIt
{
    class Program
    {
        


        static void Main(string[] args)
        {
            //Console.WriteLine("1. \n");
            //string s = Console.ReadLine();

            //string s1 = Console.ReadLine();

            //bool showHelp = false;
            //string bsshToken = Environment.GetEnvironmentVariable("LIMSMock.bsshToken");
            //string eventServiceToken = Environment.GetEnvironmentVariable("LIMSMock.eventServiceToken");
            //bool alwaysPass = false;
            //string barcodeLogFilepath = "";
            //var options = new OptionSet()
            //{
            //    { "a|alwaysPass", "Always update BioSample and other status to PASS", (v) => alwaysPass = true },
            //    { "b|bsshToken=", "BSSH API access token", (v) => bsshToken = v },
            //    { "n|newmanToken=", "Event Service API Bearer token", (v) => eventServiceToken = v },
            //    { "f|barcodeLogFilepath=", "The file stores generated flowcell barcode", (v) => barcodeLogFilepath = v},
            //    { "h|?|help", "print out this message and exit", (v) => showHelp = true },
            //};
            //options.Parse(args);

            //Console.WriteLine("Usage:");
            //options.WriteOptionDescriptions(Console.Out);
            //Thread keyboardPolling = new Thread(() =>
            //{
            //    Console.ReadKey(true);
            //    // cts.Cancel();
            //});
            //keyboardPolling.Start();
            //Task.WaitAll();
            OpenIt openIt = new OpenIt();
            openIt.Run();
            Console.ReadKey();
        }
    }
}
