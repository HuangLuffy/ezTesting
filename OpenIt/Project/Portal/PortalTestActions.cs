using ATLib;
using ATLib.Input;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalTestActions
    {
        public Portal _Portal;
        private AT MainWindow_SW = null;
        private int timeout;
        private long launchTimes;
        public long LaunchTimes
        {
            get { return launchTimes; }
            set { launchTimes = value; }
        }

        public PortalTestActions()
        {
            this._Portal = new Portal();
            this._Portal.Initialize();
        }

        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(_Portal.SwLnkPath);
                timeout = 11;                            
                _Portal.WriteConsoleTitle(this.launchTimes,  $"Waiting for launching. ({timeout}s)", timeout);
                this.MainWindow_SW = new AT().GetElement(Name: _Portal.UIA.Name_MainWidow, ClassName: _Portal.UIA.ClassName_MainWidow, Timeout: timeout);
                UtilTime.WaitTime(2);
            }
            catch (Exception)
            {
                _Portal.HandleStepResult(Portal.Log.CRASH, launchTimes);
            }
        }
        public void IsSWCrash(int checkTime = 0, int checkInternal = 0)
        {
            if (checkInternal > 0)
            {
                UtilTime.WaitTime(checkInternal);
                _Portal.WriteConsoleTitle(this.launchTimes, $"Waits ({checkInternal}s)", checkInternal);
            }
            checkTime = 10;
            _Portal.WriteConsoleTitle(this.launchTimes, $"Waiting for checking crash. ({checkTime}s)", checkTime);
            AT Crash_Window = new AT().GetElement(Name: _Portal.UIA.Name_CrashMainWidow, Timeout: checkTime, returnNullWhenException: true);
            if (Crash_Window != null) {
                _Portal.HandleStepResult(Portal.Log.CRASH, launchTimes);
                throw new Exception();
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = this.MainWindow_SW.GetElement(Name:_Portal.UIA.Btn_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                timeout = 3;
                _Portal.WriteConsoleTitle(this.launchTimes, $"Waiting for closing. ({timeout}s)", timeout);
                UtilTime.WaitTime(3);
                //if (UtilProcess.IsProcessExistedByName(_Portal.SwProcessName))
                //{
                //    _Portal.HandleStepResult(Portal.Log.PROCESSSTILLEXISTS, launchTimes);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void VMPlugOutDevice(string deviceNameVM)
        {
            try
            {
                AT Window_VM = new AT().GetElement(VM.Window_VM);
                Window_VM.DoSetFocus();
                UtilTime.WaitTime(1);
                AT Tab_TestVM = Window_VM.GetElement(VM.Tab_TestVM);
                Tab_TestVM.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.RIGHT);
                UtilTime.WaitTime(0.5);
                //AT Item_MenuContext = new AT().GetElement(VM.Menu_Context);
                //AT Item_RemovableDevices = Item_MenuContext.GetElement(VM.Item_RemovableDevices);
                AT Item_RemovableDevices = new AT().GetElement(VM.Item_RemovableDevices);
                Item_RemovableDevices.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
                UtilTime.WaitTime(0.5);
                AT Item_Target = new AT().GetElement(Name: deviceNameVM, TreeScope: AT.TreeScope.Descendants, ControlType: AT.ControlType.MenuItem);
                Item_Target.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.NOTCLICK);
                UtilTime.WaitTime(1);
                AT Item_Con = null;
                try
                {
                    Item_Con = new AT().GetElement(VM.Item_Connect);
                }
                catch (Exception)
                {
                    try
                    {
                        Item_Con = new AT().GetElement(VM.Item_Disconnect);
                    }
                    catch (Exception)
                    {

                    }
                }
                if (Item_Con != null)
                {
                    Item_Con.DoClickPoint(mk: HWSimulator.HWSend.MouseKeys.LEFT);
                    timeout = 20;
                    UtilTime.WaitTime(20);
                    _Portal.WriteConsoleTitle(this.launchTimes, $"Waiting for connecting/disconnecting. ({timeout}s)", timeout);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
