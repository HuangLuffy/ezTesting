using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hook.Hook;

namespace Hook
{
    public partial class KeysSpy : Form
    {
        public KeysSpy()
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
        private IEnumerable<string> ignoreEventList = new List<string>(){ "LMenu", "RMenu", "LWin", "RWin", "Capital", "Return", "Space", "MediaPlayPause"
            , "MediaStop", "MediaNextTrack", "MediaPreviousTrack", "VolumeUp", "VolumeDown", "VolumeMute", "LaunchMail", "LaunchApplication2", "BrowserHome"};

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            var k = e.KeyCode.ToString();
            KeyboardHookStruct _KeyboardHookStruct = (KeyboardHookStruct)sender;
            if (_KeyboardHookStruct.VirtualKeyCode == 13)
            {
                k = _KeyboardHookStruct.Flags == 0 ? k : "NumPadReturn";
            }
            tb.AppendText(k + "\n");
            e.Handled = true;
            //foreach (var item in ignoreEventList)
            //{
            //    if (e.KeyCode.ToString().Equals(item))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb.Clear();
            labelHide.Text = "";
            tb.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
