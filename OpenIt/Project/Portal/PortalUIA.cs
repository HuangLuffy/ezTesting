﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class PortalUIA
    {
        //private string name_MainWidow;
        //private string btn_CloseMainWindow;
        ////Personality
        //private string btn_Ok_PlugOut;

        public string Name_MainWidow
        {
            get { return "PORTAL"; }
            //set { name_MainWidow = value; }
        }
        public string ClassName_MainWidow
        {
            get { return "Qt5QWindowIcon"; }
            //set { name_MainWidow = value; }
        }
        
        public string Btn_CloseMainWindow
        {
            get { return "sw_close"; }
        }

        public string Btn_OK_PlugOut
        {
            get { return "plugout_ok"; }
        }

        public string Name_CrashMainWidow
        {
            get { return "PORTAL.exe"; }
        }
    }
}