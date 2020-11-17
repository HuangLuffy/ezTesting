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
        public enum WaitEvent
        {
            /// <summary>
            /// 
            /// </summary>
            Existed = 0,
            /// <summary>
            /// 
            /// </summary>
            Disappeared = 1
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
        public enum SelectNum
        {
            /// <summary>
            /// 
            /// </summary>
            Single = 0,
            /// <summary>
            /// 
            /// </summary>
            All = 1
        }
        /// <summary>
        /// 
        /// </summary>
        public enum WindowMode
        {
            /// <summary>
            /// 
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 
            /// </summary>
            Maximized = 1,
            /// <summary>
            /// 
            /// </summary>
            Minimized = 2,
            /// <summary>
            /// 
            /// </summary>
            Close = 3
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
        public enum ClickMode
        {
            /// <summary>
            /// 
            /// </summary>
            Invoke = 0,
            /// <summary>
            /// 
            /// </summary>
            Point = 1
        }
        /// <summary>
        /// 
        /// </summary>
        public enum SelectMode
        {
            /// <summary>
            /// 
            /// </summary>
            Select = 0,
            /// <summary>
            /// 
            /// </summary>
            Point = 1
        }
        /// <summary>
        /// 
        /// </summary>
        public enum DoMode
        {
            /// <summary>
            /// 
            /// </summary>
            Select = 0,
            /// <summary>
            /// 
            /// </summary>
            Invoke = 1,
            /// <summary>
            /// 
            /// </summary>
            Point = 2
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="atObj"></param>
        /// <param name="treeScope"></param>
        /// <param name="name"></param>
        /// <param name="automationId"></param>
        /// <param name="className"></param>
        /// <param name="frameworkId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        protected static bool IsElementsMatch(AT atObj = null, string name = null, string treeScope = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null)
        {
            try
            {
                var t = "";
                try
                {
                    t = atObj?.GetElementInfo().Name();
                }
                catch (Exception) { }
                IsElementMatch(t, name);

                try { t = atObj?.GetElementInfo().ClassName(); }
                catch (Exception) { }
                IsElementMatch(t, className);

                try { t = atObj?.GetElementInfo().AutomationId(); }
                catch (Exception) { }
                IsElementMatch(t, automationId);

                try { t = atObj?.GetElementInfo().ControlType(); }
                catch (Exception) { }
                IsElementMatch(t, controlType);

                try { t = atObj?.GetElementInfo().FrameworkId(); }
                catch (Exception) { }
                IsElementMatch(t, frameworkId);

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
            if (!string.IsNullOrEmpty(matchName))
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
            if (ctrlType.Equals(ControlType.List))
            {
                return System.Windows.Automation.ControlType.List;
            }
            if (ctrlType.Equals(ControlType.TreeItem))
            {
                return System.Windows.Automation.ControlType.TreeItem;
            }
            if (ctrlType.Equals(ControlType.Button))
            {
                return System.Windows.Automation.ControlType.Button;
            }
            if (ctrlType.Equals(ControlType.Hyperlink))
            {
                return System.Windows.Automation.ControlType.Hyperlink;
            }
            if (ctrlType.Equals(ControlType.Custom))
            {
                return System.Windows.Automation.ControlType.Custom;
            }
            if (ctrlType.Equals(ControlType.Pane))
            {
                return System.Windows.Automation.ControlType.Pane;
            }
            if (ctrlType.Equals(ControlType.Edit))
            {
                return System.Windows.Automation.ControlType.Edit;
            }
            if (ctrlType.Equals(ControlType.Tab))
            {
                return System.Windows.Automation.ControlType.Tab;
            }
            if (ctrlType.Equals(ControlType.TabItem))
            {
                return System.Windows.Automation.ControlType.TabItem;
            }
            if (ctrlType.Equals(ControlType.Window))
            {
                return System.Windows.Automation.ControlType.Window;
            }
            if (ctrlType.Equals(ControlType.Text))
            {
                return System.Windows.Automation.ControlType.Text;
            }
            if (ctrlType.Equals(ControlType.Tree))
            {
                return System.Windows.Automation.ControlType.Tree;
            }
            if (ctrlType.Equals(ControlType.ComboBox))
            {
                return System.Windows.Automation.ControlType.ComboBox;
            }
            if (ctrlType.Equals(ControlType.DataItem))
            {
                return System.Windows.Automation.ControlType.DataItem;
            }
            if (ctrlType.Equals(ControlType.DataGrid))
            {
                return System.Windows.Automation.ControlType.DataGrid;
            }
            if (ctrlType.Equals(ControlType.RadioButton))
            {
                return System.Windows.Automation.ControlType.RadioButton;
            }
            if (ctrlType.Equals(ControlType.Hyperlink))
            {
                return System.Windows.Automation.ControlType.Hyperlink;
            }
            if (ctrlType.Equals(ControlType.CheckBox))
            {
                return System.Windows.Automation.ControlType.CheckBox;
            }
            if (ctrlType.Equals(ControlType.Text))
            {
                return System.Windows.Automation.ControlType.Text;
            }
            if (ctrlType.Equals(ControlType.Menu))
            {
                return System.Windows.Automation.ControlType.Menu;
            }
            if (ctrlType.Equals(ControlType.MenuItem))
            {
                return System.Windows.Automation.ControlType.MenuItem;
            }
            if (ctrlType.Equals(ControlType.ToolBar))
            {
                return System.Windows.Automation.ControlType.ToolBar;
            }
            if (ctrlType.Equals(ControlType.ToolTip))
            {
                return System.Windows.Automation.ControlType.ToolTip;
            }
            if (ctrlType.Equals(ControlType.MenuBar))
            {
                return System.Windows.Automation.ControlType.MenuBar;
            }
            throw new Exception("Failed to get ControlType " + ctrlType + ".");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeScope"></param>
        /// <returns></returns>
        protected static System.Windows.Automation.TreeScope GetTreeScope(string treeScope)
        {
            if (string.IsNullOrEmpty(treeScope))
            {
                return System.Windows.Automation.TreeScope.Children;
            }
            if(treeScope.Equals(TreeScope.Children))
            {
                return System.Windows.Automation.TreeScope.Children;
            }
            if (treeScope.Equals(TreeScope.Descendants))
            {
                return System.Windows.Automation.TreeScope.Descendants;
            }
            if (treeScope.Equals(TreeScope.Parent))
            {
                return System.Windows.Automation.TreeScope.Parent;
            }
            if (treeScope.Equals(TreeScope.Element))
            {
                return System.Windows.Automation.TreeScope.Element;
            }
            if (treeScope.Equals(TreeScope.Subtree))
            {
                return System.Windows.Automation.TreeScope.Subtree;
            }
            if (treeScope.Equals(TreeScope.Ancestors))
            {
                return System.Windows.Automation.TreeScope.Ancestors;
            }
            throw new Exception("Failed to get TreeScope. Current treeScope = " + treeScope + " .");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="automationId"></param>
        /// <param name="className"></param>
        /// <param name="frameworkId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        protected static Condition GetCondition(string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string fullDescription = null)
        {
            var conditionList = new List<Condition>();
            try
            {
                if (!string.IsNullOrEmpty(name) && !name.Contains(Var.Mark.Wildcard) && !name.Contains(Var.Mark.Or) && !name.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.NameProperty, name));
                }
                if (!string.IsNullOrEmpty(automationId) && !automationId.Contains(Var.Mark.Wildcard) && !automationId.Contains(Var.Mark.Or) && !automationId.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
                }
                if (!string.IsNullOrEmpty(className) && !className.Contains(Var.Mark.Wildcard) && !className.Contains(Var.Mark.Or) && !className.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.ClassNameProperty, className));
                }
                if (!string.IsNullOrEmpty(fullDescription) && !fullDescription.Contains(Var.Mark.Wildcard) && !fullDescription.Contains(Var.Mark.Or) && !fullDescription.Contains(Var.Mark.And))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.FullDescription, fullDescription));
                }
                if (!string.IsNullOrEmpty(frameworkId))
                {
                    conditionList.Add(new PropertyCondition(AutomationElement.FrameworkIdProperty, frameworkId));
                }
                if (!string.IsNullOrEmpty(controlType))
                {
                    System.Windows.Automation.ControlType ctrlType = GetControlType(controlType);
                    conditionList.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, ctrlType));
                }
                var condition = new Condition[conditionList.Count];
                //No conditions in, give True Condition
                if (conditionList.Count == 0)
                {
                    return Condition.TrueCondition;
                }
                for (var i = 0; i < conditionList.Count; i++)
                {
                    condition[i] = conditionList[i];
                }
                if (conditionList.Count == 1)
                {
                    return condition[0];
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
