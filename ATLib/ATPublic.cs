using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ATLib
{
    public class ATPublic
    {
        /// <summary>
        /// 
        /// </summary>
        public static void CloseIE()
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    AT IE = new AT().GetElement(ClassName: ATElement.ClassName.IEFrame);
                    IE.DoWindowEvents().Close();
                }
                catch (Exception)
                {

                }
                UtilTime.WaitTime(1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<string> GetCoordinateObj()
        {
            Point _Point = new Point();
            _Point.X = Control.MousePosition.X; 
            _Point.Y = Control.MousePosition.Y;
            AT at = new AT(AutomationElement.FromPoint(_Point));
            ATS items = at.GetElements(TreeScope: ATElement.TreeScope.Descendants, ControlType: ATElement.ControlType.ListItem);
            List<string> list = new List<string>();
            foreach (AT item in items.GetATCollection())
            {
                list.Add(item.GetElementInfo().Name());
            }
            return list;
        }

    }
}
