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
    public partial class Hook : Form
    {
        public Hook()
        {
            InitializeComponent();
        }

        private void richTextBox1_KeyPressed(object sender, KeyPressEventArgs e)
        {
            var aa = e;
        }
    }
}
