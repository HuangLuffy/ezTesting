﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.Util;
using CommonLib.Util.IO;
using CommonLib.Util.OS;
using RemoteLib.Listener;

namespace CMTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var testIt = new TestIt();
            //Restart check Device Manager
            //testIt.RestartSystemAndCheckDeviceName();
            //Restart check MP+
            //testIt.RestartSystemAndCheckDeviceRecognition();
            //Console.ReadKey();
            //return;
            var om = OpenListener();
            if (!testIt.IsNeededRunCmdDirectly(args))
            {
                testIt.ShowTopMenu();
            }
            Console.ReadKey();
            ListenerManager.GetListener().Stop();
        }

        private static string Tmp(string ggg)
        {
            return string.Format("{0:000}", Convert.ToInt16(ggg));
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

