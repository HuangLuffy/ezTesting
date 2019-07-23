﻿using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestActions : MasterPlus
    {
        public MasterPlusTestActions()
        {
            this.Initialize();
        }
        public void LaunchSW()
        { 
            UtilProcess.StartProcess(this.SwLnkPath);
            this.Timeout = 11;
            this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for launching. ({this.Timeout}s)", this.Timeout);
            var TabItem_OVERVIEW = new AT().GetElement(ATElementStruct: MasterPlusObj.TabItem_OVERVIEW, Timeout: this.Timeout);
            this.MainWindow_SW = new AT().GetElement(ATElementStruct: MasterPlusObj.MainWindow_SW, Timeout: this.Timeout);
        }
        public void CloseSW()
        {
            AT button_Close = this.MainWindow_SW.GetElement(ATElementStruct: MasterPlusObj.Btn_CloseMainWindow, TreeScope: AT.TreeScope.Descendants);
            button_Close.DoClick();
            this.Timeout = 6;
            this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing. ({this.Timeout}s)", this.Timeout);
            UtilTime.WaitTime(this.Timeout);
            //if (UtilProcess.IsProcessExistedByName(_Portal.SwProcessName))
            //{
            //    _Portal.HandleStepResult(Portal.Log.PROCESSSTILLEXISTS, launchTimes);
            //}
        }
    }
}