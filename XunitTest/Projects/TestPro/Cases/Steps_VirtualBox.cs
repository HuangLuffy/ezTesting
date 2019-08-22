using ATLib;
using CommonLib.Util;
using TestLib;
using XunitTest.Projects.TestPro.PO.Models;

namespace XunitTest.Projects.TestPro.Cases
{
    public class StepsVirtualBox : StepsCommon
    {
        public StepsVirtualBox(string pathReportXml = "") : base(pathReportXml)
        {
        }

        Model_VirtualBox _Model_VirtualBox = new Model_VirtualBox();

        [Descriptions("1. Open VirtualBox.", "2. 123")]
        [ExpectedResults("NA")]
        [DoNotBlock]
        public void OpenVirtualBox()
        {
            _TestStepHandler.Exec(() => 
                {
                    UtilTime.WaitTime(1);
                    UtilProcess.StartProcess(@"D:\Program Files\Oracle\VirtualBox\VirtualBox.exe");
                    _TestStepHandler.Capture("");
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
