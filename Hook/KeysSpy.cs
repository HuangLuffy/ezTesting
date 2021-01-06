using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hook
{
    public partial class KeysTest : Form
    {
        public KeysTest()
        {
            InitializeComponent();
            Hook.KeyDown += HookManager_KeyDown;
        }

        private void richTextBox1_KeyPressed(object sender, KeyPressEventArgs e)
        {
            var aa = e;
        }
        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            tb.AppendText(e.KeyChar.ToString());
            tb.ScrollToCaret();
        } 

        private void AddValueToLabel(string keyValue)
        {
            labelHide.Text += labelHide.Text.Equals("") ? keyValue : " * " + keyValue;
        }
        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            var k = e.KeyCode.ToString() + "\n";
            tb.AppendText(k);
            //AddValueToLabel(k);
            //tb.AppendText(e.KeyValue.ToString());
            tb.ScrollToCaret();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb.Clear();
            labelHide.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
