using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CMTest.Xml;
using CommonLib.Util;
using CommonLib.Util.OS;
using CommonLib.Util.Project;
using ReportLib;
using static CommonLib.Util.UtilCapturer;

namespace CMTest.Project.MasterPlus
{
    public class MasterPlusTestFlows
    {
        public struct TestNames
        {
            public const string OPTION_LAUNCH_TEST = "Launch Test";
            public const string OPTION_LAUNCH_CHECK_CRASH = "Launch and check crash";
            public const string OPTION_TEST = "Test";
        }
        
        private static readonly long TEST_TIMES = 99999999;
        private IReporter _iReporter;
        public Reporter.ResultTestInfo resultTestInfo;
        public readonly MasterPlusTestActions MpActions = new MasterPlusTestActions();
        //private string _manualCheckLink = Reporter.DefaultContent;
        private void Capture(Reporter.ResultTestCase r = null, string commentOnWeb = "Step_End", string name = "", ImageType imageType = ImageType.PNG)
        {
            if (name.Equals(""))
            {
                name = UtilTime.GetLongTimeString();
            }
            var t = Path.Combine(MpActions.ScreenshotsRelativePath, name);
            var manualCheckLink = _iReporter.SetNeedToCheck(commentOnWeb, Path.Combine(AbsResult.Const.Screenshots, name + "." + imageType));
            manualCheckLink = _iReporter.SetAsLink(manualCheckLink);
            UtilCapturer.Capture(t, imageType);
            if (r != null)
            {
                r.NodeNeedToCheck += manualCheckLink;
            }
        }
        public MasterPlusTestFlows()
        {
            MpActions.Initialize();
            _iReporter = new ReporterXsl(Path.Combine(MpActions.ResultTimePath, "MasterPlusTestFlows.xml"), ProjectPath.GetProjectFullPath());
            resultTestInfo = new Reporter.ResultTestInfo
            {
                AttrProject = "Cooler Master",
                AttrOs = UtilOs.GetOsVersion(),
                AttrLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name.Replace("-", "_"),
                AttrRegion = System.Globalization.CultureInfo.InstalledUICulture.Name.Split('-')[1],
                AttrDeviceModel = Reporter.DefaultContent,
                AttrDeviceName = Reporter.DefaultContent,
                AttrVersion = UtilOs.GetOsProperty(),
                AttrTotalCases = 0,
                AttrPasses = 0,
                AttrFailures = 0,
                AttrTbds = 0,
                AttrBlocks = 0
            };
        }
        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TEST_TIMES; i++)
            {
                MpActions.LaunchTimes = i;
                MpActions.LaunchMasterPlus("",11);
                MpActions.CloseMasterPlus();
            }
        }

        public void Flow_RestartSystemAndCheckDeviceRecognition(XmlOps xmlOps)
        {
            MpActions.RestartSystemAndCheckDeviceRecognitionFlow(xmlOps);
        }

        public void Flow_LaunchAndCheckCrash()
        {
            MpActions.LaunchAndCheckCrash(TEST_TIMES);
        }
        //Common cases
        public void Case_LaunchMasterPlus()
        {
            var r = new Reporter.ResultTestCase()
            {
                NodeDescription = $"Launch MasterPlus from {MpActions.SwLnkPath}. Timeout = 15s",
                NodeExpectedResult = "MasterPlus+ launched successfully.",
            };
            try
            {
                //var swMainWindow = MasterPlusTestActions.LaunchMasterPlus(SwLnkPath,15);
            }
            catch (Exception)
            {
                r.NodeResult = Reporter.Result.FAIL;
                r.AttrMessage = "Failed to launch MP+.";
            }
            Capture(r);
            _iReporter.AddTestStep(r, resultTestInfo);
        }
        public void Case_SelectDevice(string deviceName)
        {
            var r = new Reporter.ResultTestCase()
            {
                NodeDescription = $"Select {deviceName} from MasterPlus.",
                NodeExpectedResult = $"{deviceName} can be found.",
            };
            try
            {
                var swMainWindow = MpActions.GetMasterPlusMainWindow();
                var dut = MpActions.GetTestDevice(deviceName, swMainWindow);
                dut.DoClickPoint();
            }
            catch (Exception)
            {
                r.NodeResult = Reporter.Result.FAIL;
                r.AttrMessage = "Failed to find the device.";
            }
            Capture(r);
            _iReporter.AddTestStep(r, resultTestInfo);
        }
        //KeymappingTest
    }
}
