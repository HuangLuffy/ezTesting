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
using XunitTest.Wrapper;

namespace XunitTest.Projects.TestPro.Cases
{
    public class Steps_VirtualBox : DetailedTestStep
    {
        Model_VirtualBox _Model_VirtualBox = new Model_VirtualBox();

        public Steps_VirtualBox(string pathReportXml = "") : base(pathReportXml)
        {
            this.pathReportXml = pathReportXml;
        }

        [Descriptions("Open VirtualBox.")]
        [ExpectedResults("NA")]
        public void OpenVirtualBox()
        {
             this.Rec(() => 
                {
                    UtilProcess.StartProcess(@"D:\Program Files\Oracle\VirtualBox\VirtualBox.exe");
                }
             );
        }
        [Descriptions("Follow previous step.")]
        [ExpectedResults("VirtualBox launched successfully")]
        public void verifyIfVirtualBoxLaunchedSuccessfully()
        {
            this.Rec(() =>
                {
                    AT Window_VirtualBox = new AT().GetElementFromChild(_Model_VirtualBox.main_Window, 10);
                }
            );
        }
    }
}
