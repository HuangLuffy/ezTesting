using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATLib.API
{
    public class WinApi : APIBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IntPtr _container;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public WinApi(IntPtr container)
        {
            //container = new IntPtr(0);
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIntPtr"></param>
        /// <param name="name"></param>
        /// <param name="automationId"></param>
        /// <param name="className"></param>
        /// <param name="frameworkId"></param>
        /// <param name="controlType"></param>
        /// <param name="index"></param>
        /// <param name="timeout"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public IntPtr GetHwnd(List<IntPtr> listIntPtr = null, string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string index = null, string timeout = null, string isEnabled = null)
        {
            try
            {
                var num = 1;
                if (listIntPtr != null)
                {
                    num = listIntPtr.Count;
                }
                for (var i = 0; i < num; i++)
                {
                    var intPtrLoop = IntPtr.Zero;
                    do
                    {
                        _container = listIntPtr[i];
                        intPtrLoop = FindWindowEx(_container, intPtrLoop, className, null);
                        if (IsHwndMatched(intPtrLoop, name, automationId))
                        {
                            return intPtrLoop;
                        }
                    }
                    while (!intPtrLoop.Equals(IntPtr.Zero));
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception("GetHWND error. " + ex.Message);
            }
            /*
            IntPtr hWnd = FindWindowEx(new IntPtr(0xD002BE), IntPtr.Zero, "Button", null);
            StringBuilder s = new StringBuilder(512);
            int i = GetWindowText(hWnd, s, s.Capacity);
            //MessageBox.Show(s.ToString());
            //"Show Viewer After Scan"
            IntPtr intPtr = IntPtr.Zero;
            do
            {//new IntPtr(0xD002BE)
                intPtr = FindWindowEx(new IntPtr(0xA10A5E), intPtr, "Button", null);
                //intPtr = GetWindow(intPtr, 1);
                Console.WriteLine(GetDlgCtrlID(intPtr));
                if (!intPtr.Equals(IntPtr.Zero))
                {
                    i = GetWindowText(intPtr, s, s.Capacity);
                    Console.WriteLine(intPtr);
                    int ret = SendMessage(intPtr, BM_GETCHECK, 0, IntPtr.Zero);
                    //Send the BM_GETCHECK message
                    //If ret = BST_CHECKED or 1 then the checkbox is checked
                    if (ret == BST_CHECKED)
                    {
                        MessageBox.Show("CheckBox Is Checked");
                    }
                }       
            } while (!intPtr.Equals(IntPtr.Zero));
             */
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="automationId"></param>
        /// <param name="className"></param>
        /// <param name="frameworkId"></param>
        /// <param name="controlType"></param>
        /// <param name="index"></param>
        /// <param name="timeout"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public List<IntPtr> GetHwnds(string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string index = null, string timeout = null, string isEnabled = null)
        {
            try
            {
                var listIntPtr = new List<IntPtr>();
                var intPtr = IntPtr.Zero;
                do
                {
                    intPtr = FindWindowEx(_container, intPtr, className, null);
                    if (IsHwndMatched(intPtr, name, automationId))
                    {
                        listIntPtr.Add(intPtr);
                    }
                }
                while (!intPtr.Equals(IntPtr.Zero));
                if (listIntPtr.Count == 0)
                {
                    throw new Exception();
                }
                return listIntPtr;
            }
            catch (Exception ex)
            {
                throw new Exception("GetHWNDs error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="AutomationId"></param>
        /// <param name="ClassName"></param>
        /// <param name="FrameworkId"></param>
        /// <param name="ControlType"></param>
        /// <param name="Index"></param>
        /// <param name="Timeout"></param>
        /// <param name="IsEnabled"></param>
        /// <returns></returns>
        public void Test(string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, string Timeout = null, string IsEnabled = null)
        {
            try
            {
                var s = new StringBuilder(512);
                var b = new StringBuilder(512);
                var intPtr = IntPtr.Zero;
                do
                {
                    intPtr = FindWindowEx(new IntPtr(0x9B0A5A), intPtr, "#32770", null);
                    GetWindowText(intPtr, s, s.Capacity);
                    GetClassName(intPtr, b, b.Capacity);
                    Console.WriteLine(
                        $"[{UtilString.ConvertIt.ConvertHex(intPtr.ToString())}][{GetDlgCtrlID(intPtr)}][{s}][{b}]");
                }
                while (!intPtr.Equals(IntPtr.Zero));
            }
            catch (Exception ex)
            {
                throw new Exception("GetHWNDs error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="name"></param>
        /// <param name="automationId"></param>
        /// <returns></returns>
        public bool IsHwndMatched(IntPtr intPtr, string name = null, string automationId = null)
        {
            try
            {
                if (name == null)
                    return automationId == null || GetDlgCtrlID(intPtr).ToString().ToLower().Equals(automationId.ToLower());
                var s = new StringBuilder(512);
                GetWindowText(intPtr, s, s.Capacity);
                if (!s.ToString().ToLower().Equals(name.ToLower()))
                {
                    return false;
                }
                return automationId == null || GetDlgCtrlID(intPtr).ToString().ToLower().Equals(automationId.ToLower());
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <returns></returns>
        public bool IsChecked(IntPtr intPtr)
        {
            var ret = SendMessage(intPtr, Status.BM_GETCHECK, 0, IntPtr.Zero);
            return ret == Status.BST_CHECKED;
        }
    }
}
