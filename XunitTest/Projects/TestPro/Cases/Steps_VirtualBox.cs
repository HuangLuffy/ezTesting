using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestLib;
using XunitTest.Projects.TestPro.PO.Models;
using XunitTest.Handler;

namespace XunitTest.Projects.TestPro.Cases
{
    public class Steps_VirtualBox : Steps_Common
    {
        public Steps_VirtualBox(string pathReportXml = "") : base(pathReportXml)
        {
        }

        Model_VirtualBox _Model_VirtualBox = new Model_VirtualBox();

        [Descriptions("1. Open VirtualBox.", "2. 123")]
        [ExpectedResults("NA")]
        public void OpenVirtualBox()
        {
            _TestStepHandler.Exec(() => 
                {
                    UtilProcess.StartProcess(@"D:\Program Files\Oracle\VirtualBox\VirtualBox.exe");
                }
            );
        }
        [Descriptions("Follow previous step.")]
        [ExpectedResults("VirtualBox launched successfully")]
        public void verifyIfVirtualBoxLaunchedSuccessfully()
        {
            _TestStepHandler.Exec(() =>
                {
                    AT Window_VirtualBox = new AT().GetElementFromChild(_Model_VirtualBox.main_Window, 10);
                }
            );
        }
    }
}
