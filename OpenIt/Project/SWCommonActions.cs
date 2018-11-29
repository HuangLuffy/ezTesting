using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project
{
    public class SWCommonActions : SW
    {

        public SWCommonActions()
        {
            this.Initialize();
        }

        public void LaunchSW()
        {
            try
            {
                UtilProcess.StartProcess(this.SwLnkPath);
                UtilTime.WaitTime(1);
                this.Timeout = 15;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for launching. ({this.Timeout}s)", this.Timeout);
                this.MainWindow_SW = new AT().GetElement(Name: this.Obj.Name_MainWidow, ClassName: this.Obj.ClassName_MainWidow, Timeout: this.Timeout);
                //Qt5QWindowIcon
                UtilTime.WaitTime(2);
            }
            catch (Exception ex)
            {
                this.MainWindow_SW = new AT().GetElement(Name: this.Obj.Name_MainWidow, ClassName: this.Obj.ClassName_MainWidow, Timeout: this.Timeout);
                this.HandleWrongStepResult("Cannot find App.", this.LaunchTimes);
            }
        }

        public void IsSWCrash(int checkTime = 0, int checkInternal = 0)
        {
            if (checkInternal > 0)
            {
                UtilTime.WaitTime(checkInternal);
                this.WriteConsoleTitle(this.LaunchTimes, $"Waits ({checkInternal}s)", checkInternal);
            }
            checkTime = 10;
            this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for checking crash. ({checkTime}s)", checkTime);
            AT Crash_Window = new AT().GetElement(Name: this.Obj.Name_CrashMainWidow, Timeout: checkTime, returnNullWhenException: true);
            if (Crash_Window != null)
            {
                this.HandleWrongStepResult(SW.msg.CRASH, this.LaunchTimes);
                throw new Exception(SW.msg.CRASH);
            }
        }
        public void CloseSW()
        {
            try
            {
                AT button_Close = this.MainWindow_SW.GetElement(Name: this.Obj.Button_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                button_Close.DoClick();
                Timeout = 3;
                this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing1. ({Timeout}s)", Timeout);
                UtilTime.WaitTime(Timeout);
                try
                {
                    Timeout = 3;
                    this.WriteConsoleTitle(this.LaunchTimes, $"Waiting for closing2. ({Timeout}s)", Timeout);
                    button_Close = this.MainWindow_SW.GetElement(Name: this.Obj.Button_CloseMainWindow, ControlType: AT.ControlType.Button, TreeScope: AT.TreeScope.Descendants);
                    //button_Close.DoClickWithNewThread();
                    button_Close.DoClickPoint();//Don't know why sometimes "button_Close.DoClick();" does not work
                    UtilTime.WaitTime(Timeout);
                }
                catch (Exception)
                {

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ProfilesSimpleSwitch(long TEST_TIMES)
        {
            AT TabItem_Profiles = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.TabItem_PROFILES, TreeScope: AT.TreeScope.Descendants);
            TabItem_Profiles.DoClickPoint();//"DoClick();" does not work
            UtilTime.WaitTime(1);
            AT Profile_tmp = null;
            AT Loading_tmp = null;
            for (int i = 1; i < TEST_TIMES; i++)
            {
                this.ProfilesSimpleSwitch(PortalObj.Profile_1, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_2, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_3, Profile_tmp, Loading_tmp);
                this.ProfilesSimpleSwitch(PortalObj.Profile_4, Profile_tmp, Loading_tmp);
            }
        }
        private void ProfilesSimpleSwitch(ATElementStruct WhichProfile, AT Profile, AT Loading)
        {
            Profile = this.MainWindow_SW.GetElement(ATElementStruct: WhichProfile, TreeScope: AT.TreeScope.Descendants);
            Profile.DoClickPoint();
            UtilTime.WaitTime(3);
            try
            {
                while (true)
                {
                    Loading = this.MainWindow_SW.GetElement(ATElementStruct: PortalObj.Lable_LOADING, TreeScope: AT.TreeScope.Descendants);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
