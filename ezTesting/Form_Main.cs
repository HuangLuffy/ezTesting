using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ezTesting
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }
        public bool aaa()
        {
            return true;
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //TestStep testStep = new TestStep();
            //testStep.Run(testStep.firstStep);

            try
            {
                //ReportXsl reportXsl = new ReportXsl(@"D:\Dev\DevicePass\results\1.xml");
                //reportXsl.CreateResultXml();
                //reportXsl.AddTestCase();
                //AT window_VirtualBox = new AT();
                //window_VirtualBox = window_VirtualBox.GetElement(Name: "Oracle VM VirtualBox Manager");
                ////window_VirtualBox.Spy();
                //AT toolBar = window_VirtualBox.GetElement(ControlType: AT.ControlType.ToolBar, TreeScope: AT.TreeScope.Descendants);
                ////AT button_new = toolBar.GetElement(Name: "New");
                ////toolBar.GetElement();
                ////button_new.DoClick();
                ////Welcome to VirtualBox
                //AT sss = window_VirtualBox.GetElement(Name: "Welcome to VirtualBox.*", TreeScope: AT.TreeScope.Descendants);
                ////sss.Spy();
               //Debug.WriteLine(sss.GetElementInfo().Name());
                //sss.WaitForDisappeared(50);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //Debug.WriteLine(aa.GetElementInfo().Exists());
            Application.Exit();
            //UtilWait.intervalInSec = 88;
            //var bb = UtilWait.ForTrue(() =>
            //{
            //    //if (true)
            //    //{
            //    //    return false;
            //    //}
            //    return aaa();
            //}, 1);
            //Console.WriteLine("xxx" + bb + "ccc");




            //Console.ReadKey();
        }
    }
}
