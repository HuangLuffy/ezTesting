using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UtilWait.intervalInSec = 88;
            var bb = UtilWait.ForTrue(() =>
            {
                //if (true)
                //{
                //    return false;
                //}
                return aaa();
            }, 1);
            Console.WriteLine("xxx" + bb + "ccc");

            Application.Exit();

            //AT aa = new AT();
            //aa = aa.GetElement(Name: "Program Manager");
            //Console.ReadKey();
        }
    }
}
