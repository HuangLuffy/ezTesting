using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace ATLib
{
    public class ATElement
    {
        #region struct
        /// <summary>
        /// Z:\WinBlueSliceAutomation\WinBlueSliceAutomation\DeviceConfig\Public\AT_\ATElement.cs
        /// </summary>
        public struct WaitEvent
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Existed = "Existed";
            /// <summary>
            /// 
            /// </summary>
            public const string Disappeared = "Disappeared";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct FrameworkId
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Win32 = "Win32";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct SelectNum
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Single = "Single";
            /// <summary>
            /// 
            /// </summary>
            public const string All = "All";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct WindowMode
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Normal = "Normal";
            /// <summary>
            /// 
            /// </summary>
            public const string Maximized = "Maximized";
            /// <summary>
            /// 
            /// </summary>
            public const string Minimized = "Minimized";
            /// <summary>
            /// 
            /// </summary>
            public const string Close = "Close";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct ClassName
        {
            /// <summary>
            /// 
            /// </summary>
            public const string WordPadClass = "WordPadClass";
            /// <summary>
            /// 
            /// </summary>
            public const string Notepad = "Notepad";
            /// <summary>
            /// 
            /// </summary>
            public const string Photo_Lightweight_Viewer = "Photo_Lightweight_Viewer";
            /// <summary>
            /// 
            /// </summary>
            public const string NativeHWNDHost = "NativeHWNDHost";
            /// <summary>
            /// 
            /// </summary>
            public const string CabinetWClass = "CabinetWClass";
            /// <summary>
            /// 
            /// </summary>
            public const string Static = "Static";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string P32770 = "#32770";
            /// <summary>
            /// 
            /// </summary>
            public const string CCPushButton = "CCPushButton";
            /// <summary>
            /// 
            /// </summary>
            public const string Button = "Button";
            /// <summary>
            /// 
            /// </summary>
            public const string WFS_Main_Pane = "7a56577c-6143-43d9-bdcb-bcf234d86e98";
            /// <summary>
            /// 
            /// </summary>
            public const string AfxWnd42u = "AfxWnd42u";
            /// <summary>
            /// 
            /// </summary>
            public const string SysListView32 = "SysListView32";
            /// <summary>
            /// 
            /// </summary>
            public const string ReBarWindow32 = "ReBarWindow32";
            /// <summary>
            /// 
            /// </summary>
            public const string ToolbarWindow32 = "ToolbarWindow32";
            /// <summary>
            /// 
            /// </summary>
            public const string WiaPreviewControl = "WiaPreviewControl";
            /// <summary>
            /// 
            /// </summary>
            public const string WiaPreviewControlFrame = "WiaPreviewControlFrame";
            /// <summary>
            /// 
            /// </summary>
            public const string TW_App_MainWnd = "TW_App_MainWnd";
            /// <summary>
            /// 
            /// </summary>
            public const string IEFrame = "IEFrame";
            /// <summary>
            /// 
            /// </summary>
            public const string ListBox = "ListBox";
            /// <summary>
            /// 
            /// </summary>
            public const string SHELLDLL_DefView = "SHELLDLL_DefView";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct ControlType
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Tree = "Tree";
            /// <summary>
            /// 
            /// </summary>
            public const string TreeItem = "TreeItem";
            /// <summary>
            /// 
            /// </summary>
            public const string ComboBox = "ComboBox";
            /// <summary>
            /// 
            /// </summary>
            public const string List = "List";
            /// <summary>
            /// 
            /// </summary>
            public const string ListItem = "ListItem";
            /// <summary>
            /// 
            /// </summary>
            public const string Button = "Button";
            /// <summary>
            /// 
            /// </summary>
            public const string Hyperlink = "Hyperlink";
            /// <summary>
            /// 
            /// </summary>
            public const string Custom = "Custom";
            /// <summary>
            /// 
            /// </summary>
            public const string Pane = "Pane";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string Text = "Text";
            /// <summary>
            /// 
            /// </summary>
            public const string Window = "Window";
            /// <summary>
            /// 
            /// </summary>
            public const string Tab = "Tab";
            /// <summary>
            /// 
            /// </summary>
            public const string TabItem = "TabItem";
            /// <summary>
            /// 
            /// </summary>
            public const string RadioButton = "RadioButton";
            /// <summary>
            /// 
            /// </summary>
            public const string DataItem = "DataItem";
            /// <summary>
            /// 
            /// </summary>
            public const string DataGrid = "DataGrid";
            /// <summary>
            /// 
            /// </summary>
            public const string CheckBox = "CheckBox";
            /// <summary>
            /// 
            /// </summary>
            public const string Menu = "Menu";
            /// <summary>
            /// 
            /// </summary>
            public const string MenuItem = "MenuItem";
            /// <summary>
            /// 
            /// </summary>
            public const string ToolBar = "ToolBar";
            /// <summary>
            /// 
            /// </summary>
            public const string MenuBar = "MenuBar";
            /// <summary>
            /// 
            /// </summary>
            public const string ToolTip = "ToolTip";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct TreeScope
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Element = "Element";
            /// <summary>
            /// 
            /// </summary>
            public const string Children = "Children";
            /// <summary>
            /// 
            /// </summary>
            public const string Descendants = "Descendants";
            /// <summary>
            /// 
            /// </summary>
            public const string Parent = "Parent";
            /// <summary>
            /// 
            /// </summary>
            public const string Subtree = "Subtree";
            /// <summary>
            /// 
            /// </summary>
            public const string Ancestors = "Ancestors";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct ClickMode
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Invoke = "Invoke";
            /// <summary>
            /// 
            /// </summary>
            public const string Point = "Point";
        }
        /// <summary>
        /// 
        /// </summary>
        public struct SelectMode
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Select = "Select";
            /// <summary>
            /// 
            /// </summary>
            public const string Point = ClickMode.Point;
        }
        /// <summary>
        /// 
        /// </summary>
        public struct DoMode
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Select = "Select";
            /// <summary>
            /// 
            /// </summary>
            public const string Invoke = "Invoke";
            /// <summary>
            /// 
            /// </summary>
            public const string Point = "Point";
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="atObj"></param>
        /// <param name="TreeScope"></param>
        /// <param name="Name"></param>
        /// <param name="AutomationId"></param>
        /// <param name="ClassName"></param>
        /// <param name="FrameworkId"></param>
        /// <param name="ControlType"></param>
        /// <returns></returns>
        protected static bool IsElementsMatch(AT atObj = null, string Name = null, string TreeScope = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null)
        {
            try
            {
                var t = "";
                try
                {
                    t = atObj.GetElementInfo().Name();
                }
                catch (Exception) { }
                IsElementMatch(t, Name);

                try { t = atObj.GetElementInfo().ClassName(); }
                catch (Exception) { }
                IsElementMatch(t, ClassName);

                try { t = atObj.GetElementInfo().AutomationId(); }
                catch (Exception) { }
                IsElementMatch(t, AutomationId);

                try { t = atObj.GetElementInfo().ControlType(); }
                catch (Exception) { }
                IsElementMatch(t, ControlType);

                try { t = atObj.GetElementInfo().FrameworkId(); }
                catch (Exception) { }
                IsElementMatch(t, FrameworkId);

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
        /// <param name="targetName"></param>
        /// <param name="matchName"></param>
        private static bool IsElementMatch(string targetName, string matchName)
        {
            if (!String.IsNullOrEmpty(matchName))
            {
                if (!UtilMatch.NameMatch(targetName, matchName))
                {
                    throw new Exception(matchName + " Not Matched.");
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        private static System.Windows.Automation.ControlType GetControlType(string ctrlType)
        {
            if (ctrlType.Equals(ControlType.ListItem))
            {
                return System.Windows.Automation.ControlType.ListItem;
            }
            else if (ctrlType.Equals(ControlType.List))
            {
                return System.Windows.Automation.ControlType.List;
            }
            else if (ctrlType.Equals(ControlType.TreeItem))
            {
                return System.Windows.Automation.ControlType.TreeItem;
            }
            else if (ctrlType.Equals(ControlType.Button))
            {
                return System.Windows.Automation.ControlType.Button;
            }
            else if (ctrlType.Equals(ControlType.Hyperlink))
            {
                return System.Windows.Automation.ControlType.Hyperlink;
            }
            else if (ctrlType.Equals(ControlType.Custom))
            {
                return System.Windows.Automation.ControlType.Custom;
            }
            else if (ctrlType.Equals(ControlType.Pane))
            {
                return System.Windows.Automation.ControlType.Pane;
            }
            else if (ctrlType.Equals(ControlType.Edit))
            {
                return System.Windows.Automation.ControlType.Edit;
            }
            else if (ctrlType.Equals(ControlType.Tab))
            {
                return System.Windows.Automation.ControlType.Tab;
            }
            else if (ctrlType.Equals(ControlType.TabItem))
            {
                return System.Windows.Automation.ControlType.TabItem;
            }
            else if (ctrlType.Equals(ControlType.Window))
            {
                return System.Windows.Automation.ControlType.Window;
            }
            else if (ctrlType.Equals(ControlType.Text))
            {
                return System.Windows.Automation.ControlType.Text;
            }
            else if (ctrlType.Equals(ControlType.Tree))
            {
                return System.Windows.Automation.ControlType.Tree;
            }
            else if (ctrlType.Equals(ControlType.ComboBox))
            {
                return System.Windows.Automation.ControlType.ComboBox;
            }
            else if (ctrlType.Equals(ControlType.DataItem))
            {
                return System.Windows.Automation.ControlType.DataItem;
            }
            else if (ctrlType.Equals(ControlType.DataGrid))
            {
                return System.Windows.Automation.ControlType.DataGrid;
            }
            else if (ctrlType.Equals(ControlType.RadioButton))
            {
                return System.Windows.Automation.ControlType.RadioButton;
            }
            else if (ctrlType.Equals(ControlType.Hyperlink))
            {
                return System.Windows.Automation.ControlType.Hyperlink;
            }
            else if (ctrlType.Equals(ControlType.CheckBox))
            {
                return System.Windows.Automation.ControlType.CheckBox;
            }
            else if (ctrlType.Equals(ControlType.Text))
            {
                return System.Windows.Automation.ControlType.Text;
            }
            else if (ctrlType.Equals(ControlType.Menu))
            {
                return System.Windows.Automation.ControlType.Menu;
            }
            else if (ctrlType.Equals(ControlType.MenuItem))
            {
                return System.Windows.Automation.ControlType.MenuItem;
            }
            else if (ctrlType.Equals(ControlType.ToolBar))
            {
                return System.Windows.Automation.ControlType.ToolBar;
            }
            else if (ctrlType.Equals(ControlType.ToolTip))
            {
                return System.Windows.Automation.ControlType.ToolTip;
            }
            else if (ctrlType.Equals(ControlType.MenuBar))
            {
                return System.Windows.Automation.ControlType.MenuBar;
            }
            else
            {
                throw new Exception("Failed to get ControlType " + ctrlType + ".");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeScope"></param>
        /// <returns></returns>
        protected static System.Windows.Automation.TreeScope GetTreeScope(string treeScope)
        {
            try
            {
                if (String.IsNullOrEmpty(treeScope))
                {
                    return System.Windows.Automation.TreeScope.Children;
                }
                else if(treeScope.Equals(TreeScope.Children))
                {
                    return System.Windows.Automation.TreeScope.Children;
                }
                else if (treeScope.Equals(TreeScope.Descendants))
                {
                    return System.Windows.Automation.TreeScope.Descendants;
                }
                else if (treeScope.Equals(TreeScope.Parent))
                {
                    return System.Windows.Automation.TreeScope.Parent;
                }
                else if (treeScope.Equals(TreeScope.Element))
                {
                    return System.Windows.Automation.TreeScope.Element;
                }
                else if (treeScope.Equals(TreeScope.Subtree))
                {
                    return System.Windows.Automation.TreeScope.Subtree;
                }
                else if (treeScope.Equals(TreeScope.Ancestors))
                {
                    return System.Windows.Automation.TreeScope.Ancestors;
                }
                else
                {
                    throw new Exception("Failed to get TreeScope. Current treeScope = " + treeScope + " .");
                }
            }
            catch (Exception)
            {
                throw;
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
        /// <returns></returns>
        protected static Condition GetCondition(string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null)
        {
            List<Condition> conditionList = new List<Condition>();
            try
            {
                if (!String.IsNullOrEmpty(Name) && !Name.Contains(Var.Mark.Wildcard) && !Name.Contains(Var.Mark.Or) && !Name.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.NameProperty, Name));
                }
                if (!String.IsNullOrEmpty(AutomationId) && !AutomationId.Contains(Var.Mark.Wildcard) && !AutomationId.Contains(Var.Mark.Or) && !AutomationId.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.AutomationIdProperty, AutomationId));
                }
                if (!String.IsNullOrEmpty(ClassName) && !ClassName.Contains(Var.Mark.Wildcard) && !ClassName.Contains(Var.Mark.Or) && !ClassName.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.ClassNameProperty, ClassName));
                }
                if (!String.IsNullOrEmpty(FrameworkId))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.FrameworkIdProperty, FrameworkId));
                }
                if (!String.IsNullOrEmpty(ControlType))
                {
                    System.Windows.Automation.ControlType ctrlType = GetControlType(ControlType);
                    conditionList.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, ctrlType));
                }
                Condition[] condition = new Condition[conditionList.Count];
                for (int i = 0; i < conditionList.Count; i++)
                {
                    condition[i] = conditionList[i];
                }
                if (conditionList.Count == 1)
                {
                    return condition[0];
                }
                else if (conditionList.Count == 0)
                {
                    return null;
                }
                return new AndCondition(condition);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to GetCondition. " + ex.Message);
            }
        }
    }
}
