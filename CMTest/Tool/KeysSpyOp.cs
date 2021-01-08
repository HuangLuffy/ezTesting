﻿using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Tool
{
    public class KeysSpyOp
    {
        private readonly AT app;
        public KeysSpyOp(string appPath)
        {
            UtilProcess.KillProcessByName(System.IO.Path.GetFileName(appPath));
            UtilTime.WaitTime(1);
            var p = UtilProcess.StartProcessReturn(appPath);
            UtilWait.ForTrue(() => p.MainWindowHandle != IntPtr.Zero, 3);           
            app = new AT().GetElementFromHwnd(p.MainWindowHandle);
        }

        //public IEnumerable<string> KeysList { get => _keysList; set => _keysList = value; }

        public void ClickClear()
        {
            app.GetElementFromChild(new ATElementStruct() { Name = "Clear"}).DoClick();
        }
        public void ClickClose()
        {
            app.GetElementFromChild(new ATElementStruct() { Name = "Close" }).DoClick();
        }
        public IEnumerable<string> GetContentList()
        {
            var c = app.GetElementFromChild(new ATElementStruct() { Name = "RichEdit Control" }).DoGetDocumentValue();
            if (!c.Equals("\r"))
            {
                return UtilString.GetSplitArray(c, "\r").Reverse();
            }
            return null;
        }
    }
}