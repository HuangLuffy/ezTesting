using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATLib.API
{
    public class API : APIBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IntPtr _container;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public API(IntPtr container)
        {
            //container = new IntPtr(0);
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIntPtr"></param>
        /// <param name="Name"></param>
        /// <param name="AutomationId"></param>
        /// <param name="ClassName"></param>
        /// <param name="FrameworkId"></param>
        /// <param name="ControlType"></param>
        /// <param name="Index"></param>
        /// <param name="Timeout"></param>
        /// <param name="IsEnabled"></param>
        /// <returns></returns>
        public IntPtr GetHwnd(List<IntPtr> listIntPtr = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, string Timeout = null, string IsEnabled = null)
        {
            try
            {
                if (ClassName.Equals(null))
                {
                    ClassName = null;
                }
                int num = 1;
                if (listIntPtr != null)
                {
                    num = listIntPtr.Count;
                }
                for (int i = 0; i < num; i++)
                {
                    IntPtr intPtrLoop = IntPtr.Zero;
                    do
                    {
                        _container = listIntPtr[i];
                        intPtrLoop = FindWindowEx(_container, intPtrLoop, ClassName, null);
                        if (IsHWNDMatched(intPtrLoop, Name, AutomationId))
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
        /// <param name="Name"></param>
        /// <param name="AutomationId"></param>
        /// <param name="ClassName"></param>
        /// <param name="FrameworkId"></param>
        /// <param name="ControlType"></param>
        /// <param name="Index"></param>
        /// <param name="Timeout"></param>
        /// <param name="IsEnabled"></param>
        /// <returns></returns>
        public List<IntPtr> GetHwnds(string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, string Timeout = null, string IsEnabled = null)
        {
            try
            {
                List<IntPtr> list_IntPtr = new List<IntPtr>();
                if (ClassName.Equals(null))
                {
                    ClassName = null;
                }
                IntPtr intPtr = IntPtr.Zero;
                do
                {
                    intPtr = FindWindowEx(_container, intPtr, ClassName, null);
                    if (IsHWNDMatched(intPtr, Name, AutomationId))
                    {
                        list_IntPtr.Add(intPtr);
                    }
                }
                while (!intPtr.Equals(IntPtr.Zero));
                if (list_IntPtr.Count == 0)
                {
                    throw new Exception();
                }
                return list_IntPtr;
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
        public void test(string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, string Timeout = null, string IsEnabled = null)
        {
            try
            {
                StringBuilder s = new StringBuilder(512);
                StringBuilder b = new StringBuilder(512);
                IntPtr intPtr = IntPtr.Zero;
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
        /// <param name="Name"></param>
        /// <param name="AutomationId"></param>
        /// <returns></returns>
        public bool IsHWNDMatched(IntPtr intPtr, string Name = null, string AutomationId = null)
        {
            try
            {
                int i = 0;
                if (!Name.Equals(null))
                {
                    StringBuilder s = new StringBuilder(512);
                    i = GetWindowText(intPtr, s, s.Capacity);
                    if (!s.ToString().ToLower().Equals(Name.ToLower()))
                    {
                        return false;
                    }
                }
                if (!AutomationId.Equals(null))
                {
                    if (!GetDlgCtrlID(intPtr).ToString().ToLower().Equals(AutomationId.ToLower()))
                    {
                        return false;
                    }
                }
                return true;
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
            int ret = SendMessage(intPtr, Status.BM_GETCHECK, 0, IntPtr.Zero);
            if (ret == Status.BST_CHECKED)
            {
                return true;
            }
            return false;
        }
    }
}
