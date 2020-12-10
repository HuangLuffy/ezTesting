using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hook
{
    public class MouseEventExtArgs : MouseEventArgs
    {
        /// <summary>
        /// 在初始化MouseEventArgs的类的新实例。
        /// </summary>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        { }

        /// <summary>
        /// 在初始化MouseEventArgs的类的新实例。
        /// </summary>
        internal MouseEventExtArgs(MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        { }

        private bool m_Handled;

        /// <summary>
        /// 将此属性设置为<b>true</b>您的事件处理程序中，以防止其它应用程序事件的进一步处理。
        /// </summary>
        public bool Handled
        {
            get { return m_Handled; }
            set { m_Handled = value; }
        }
    }
}
