﻿using System;
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
        public readonly IReporter R;
        public readonly MasterPlusTestActions MpActions = new MasterPlusTestActions();
        public MasterPlusTestFlows()
        {
            MpActions.Initialize();
            R = new ReporterXsl(Path.Combine(MpActions.ResultTimePath, "MasterPlusTestFlows.xml"),
                ProjectPath.GetProjectFullPath(), 
                new Reporter.ResultTestInfo
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
                });
            R.SetCaptureRelativePath(MpActions.GetScreenshotsRelativePath());
        }

        public void LaunchTestReport()
        {
            Process.Start("IExplore.exe", R.GetResultFullPath());
        }

        public void Flow_LaunchTest()
        {
            for (var i = 1; i < TEST_TIMES; i++)
            {
                MpActions.LaunchTimes = i;
                MpActions.LaunchMasterPlus("", 11);
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
            R.Exec(() =>
                {
                    //var swMainWindow = MasterPlusTestActions.LaunchMasterPlus(SwLnkPath,15);
                }
                , $"Launch MasterPlus from {MpActions.SwLnkPath}. Timeout = 15s."
                , "MasterPlus+ launched successfully."
                , "Failed to launch MP+.");
        }
        public void Case_SelectDeviceFromList(string deviceName)
        {
            R.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    var dut = MpActions.GetTestDevice(deviceName, swMainWindow);
                    dut.DoClickPoint();
                }
                , $"Select {deviceName} from MasterPlus."
                , $"{deviceName} can be found."
                , "Failed to find the device.");
        }

        public void Case_SelectKeyMappingTab()
        {
            R.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();

    
                }
                , $"Select KeyMapping tab."
                , $"Select successfully."
                , "Failed to select KeyMapping tab.");
        }
        public void Case_OpenKey1()
        {
            R.Exec(() =>
                {
                    var swMainWindow = MpActions.GetMasterPlusMainWindow();
                    

                }
                , R.SetAsLines("Open Reassignment dialog for the Key 1.", 
                                           "Type 2 as the assigned key.")
                , $"Assign successfully."
                , "Failed to assign.");
        }



        //private void Capture(Reporter.ResultTestCase r = null, string commentOnWeb = "Step_End", string imageName = "",
        //    ImageType imageType = ImageType.PNG)
        //{
        //    if (imageName.Equals(""))
        //    {
        //        imageName = UtilTime.GetLongTimeString();
        //    }
        //    var t = Path.Combine(MpActions.GetScreenshotsRelativePath(), imageName);
        //    var manualCheckLink = _iReporter.SetNeedToCheck(commentOnWeb,
        //        Path.Combine(AbsResult.Const.Screenshots, imageName + "." + imageType));
        //    manualCheckLink = _iReporter.SetAsLink(manualCheckLink);
        //    UtilCapturer.Capture(t, imageType);
        //    if (r != null)
        //    {
        //        r.NodeNeedToCheck += manualCheckLink;
        //    }
        //}
        //public void Case_LaunchMasterPlus()
        //{
        //    var r = new Reporter.ResultTestCase()
        //    {
        //        NodeDescription = $"Launch MasterPlus from {MpActions.SwLnkPath}. Timeout = 15s",
        //        NodeExpectedResult = "MasterPlus+ launched successfully.",
        //    };
        //    if (isCaseFailed)
        //    {
        //        r.NodeResult = Reporter.Result.BLOCK;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            //var swMainWindow = MasterPlusTestActions.LaunchMasterPlus(SwLnkPath,15);
        //        }
        //        catch (Exception)
        //        {
        //            isCaseFailed = true;
        //            r.NodeResult = Reporter.Result.FAIL;
        //            r.AttrMessage = "Failed to launch MP+.";
        //        }

        //        Capture(r);
        //    }
        //    _iReporter.AddTestStep(r, resultTestInfo);
        //}
        //public void Case_SelectDevice1(string deviceName)
        //{
        //    var r = new Reporter.ResultTestCase()
        //    {
        //        NodeDescription = $"Select {deviceName} from MasterPlus.",
        //        NodeExpectedResult = $"{deviceName} can be found.",
        //    };
        //    if (isCaseFailed)
        //    {
        //        r.NodeResult = Reporter.Result.BLOCK;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var swMainWindow = MpActions.GetMasterPlusMainWindow();
        //            var dut = MpActions.GetTestDevice(deviceName, swMainWindow);
        //            dut.DoClickPoint();
        //        }
        //        catch (Exception)
        //        {
        //            isCaseFailed = true;
        //            r.NodeResult = Reporter.Result.FAIL;
        //            r.AttrMessage = "Failed to find the device.";
        //        }

        //        Capture(r);
        //    }

        //    _iReporter.AddTestStep(r, resultTestInfo);
        //}

        //KeymappingTest

    }
}