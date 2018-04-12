using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ATLib
{
    public class AT : ATBase
    {
        public class WaitProcessArgs : EventArgs
        {
            /// <summary>
            /// 
            /// </summary>
            private string messsage = "";
            /// <summary>
            /// 
            /// </summary>
            /// <param name="messsage"></param>
            public void setMessage(string messsage)
            {
                this.messsage = messsage;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string getMessage()
            {
                return this.messsage;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public delegate AT WaitProcessDelegateHandler(object sender, WaitProcessArgs e);
        /// <summary>
        /// 
        /// </summary>
        public static event WaitProcessDelegateHandler WaitProcessEventDelegate;
        /// <summary>
        /// 
        /// </summary>
        public AT()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elePara"></param>
        public AT(AutomationElement elePara)
        {
            this.AutomationElement = elePara;
        }
        private struct Ele
        {
            public static AutomationElement me = null;
            public static System.Windows.Automation.Condition condition = null;
            public static System.Windows.Automation.TreeScope treeScope = System.Windows.Automation.TreeScope.Children;
            public static string Name = null;
            public static string AutomationId = null;
            public static string ClassName = null;
            public static string Index = null;
        }
        public AT GetRootElement()
        {
            return new AT(AutomationElement.RootElement);
        }
        public AT GetElement(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, string Timeout = null, string IsEnabled = null)
        {
            try
            {
                AT atObj = null;
                this.AutomationElement = (this.AutomationElement == null) ? AutomationElement.RootElement : this.AutomationElement;
                System.Windows.Automation.TreeScope treeScope = GetTreeScope(TreeScope);
                System.Windows.Automation.Condition condition = GetCondition(Name, AutomationId, ClassName, FrameworkId, ControlType);
                if (WaitProcessEventDelegate != null && !String.IsNullOrEmpty(Timeout))
                {
                    Timeout = null;
                }
                AT.Ele.me = this.AutomationElement; AT.Ele.condition = condition; AT.Ele.treeScope = treeScope; AT.Ele.Name = Name; AT.Ele.AutomationId = AutomationId; AT.Ele.ClassName = ClassName; AT.Ele.Index = Index;
                if (String.IsNullOrEmpty(Timeout))
                {
                    atObj = this.GetElementHandle();
                }
                else
                {
                    WaitProcessEventDelegate += new AT.WaitProcessDelegateHandler(EventDo_GetElementTimeout);
                    atObj = GetElementAndWaitProcess(new AT(), Timeout);
                }
                if (!String.IsNullOrEmpty(IsEnabled))
                {
                    if (!(atObj.GetElementInfo().IsEnabled()))
                    {
                        throw new Exception("Not Enabled. ");
                    }
                }
                return atObj;
            }
            catch (Exception ex)
            {
                throw new Exception("[ERROR]: GetElement. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="timeout"></param>
        /// <param name="waitEvent"></param>
        /// <param name="interval"></param>
        /// <param name="dateInterval"></param>
        /// <returns></returns>
        public static AT GetElementAndWaitProcess<T>(T sender, string timeout, string waitEvent = WaitEvent.Existed, string interval = "0.5", UtilTime.DateInterval dateInterval = UtilTime.DateInterval.Second)
        {
            try
            {
                DateTime dt = DateTime.Now;
                AT at = null;
                while (true)
                {
                    UtilTime.WaitTime(Convert.ToDouble(interval));
                    try
                    {
                        at = WaitProcessEventDelegate(sender, new WaitProcessArgs());
                        if (at != null && waitEvent.Equals(WaitEvent.Existed))
                        {
                            UtilTime.WaitTime(Convert.ToDouble(interval));
                            return at;
                        }
                        else if (at == null && waitEvent.Equals(WaitEvent.Disappeared))
                        {
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    if (UtilTime.DateDiff(dt, DateTime.Now, dateInterval) > Convert.ToDouble(timeout))
                    {
                        throw new Exception(string.Format("Timeout > {0}s.", timeout));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                foreach (Delegate item in WaitProcessEventDelegate.GetInvocationList())
                {
                    typeof(AT).GetEvent(nameof(WaitProcessEventDelegate)).RemoveEventHandler(new AT(), item);
                    //typeof(AT).GetEvent("WaitProcessEventDelegate").RemoveEventHandler(new AT(), item);
                }
            }
        }
        public ATS GetElements(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null)
        {
            try
            {
                System.Windows.Automation.TreeScope treeScope = GetTreeScope(TreeScope);
                System.Windows.Automation.Condition condition = GetCondition(Name, AutomationId, ClassName, FrameworkId, ControlType);
                this.AutomationElement = this.AutomationElement == null ? AutomationElement.RootElement : this.AutomationElement;
                AutomationElementCollection aec = this.AutomationElement.FindAll(treeScope, condition);
                AT[] at = new AT[aec.Count];
                for (int i = 0; i < aec.Count; i++)
                {
                    at[i] = new AT(aec[i]);
                }
                return new ATS(at);
            }
            catch (Exception ex)
            {
                throw new Exception("GetElements error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AT Spy()
        {
            try
            {
                AT atObj = null;
                this.AutomationElement = (this.AutomationElement == null) ? AutomationElement.RootElement : this.AutomationElement;  //System.Windows.Automation.Condition.TrueCondition
                AutomationElementCollection t = this.AutomationElement.FindAll(System.Windows.Automation.TreeScope.Descendants, System.Windows.Automation.Condition.TrueCondition);
                //AutomationElementCollection t = this.me.FindAll(System.Windows.Automation.TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.Pane));
                foreach (AutomationElement item in t)
                {
                    try
                    {
                        Console.WriteLine(string.Format("[{0}] [{1}] [{2}] [{3}]", item.Current.Name.ToString(), item.Current.ControlType.ProgrammaticName.ToString(), item.Current.ClassName.ToString(), item.Current.AutomationId));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("[{0}]", ex.Message));
                    }

                }
                return atObj;
            }
            catch (Exception ex)
            {
                throw new Exception("GetElement error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elePara"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public CurrentElement GetElementInfo(AT elePara = null, string status = "")
        {
            try
            {
                elePara = elePara == null ? this : elePara;
                //AT eleName = elePara.GetCurrentPropertyValue(AutomationElement.NameProperty).ToString();
                return new CurrentElement(elePara);
            }
            catch (Exception ex)
            {
                throw new Exception("GetElementInfo error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WaitForDisappeared(string timeout = "1")
        {
            try
            {
                AT.WaitProcessEventDelegate += new AT.WaitProcessDelegateHandler(this.EventDo_IsDisappeared);
                AT t = AT.GetElementAndWaitProcess(this, timeout, waitEvent: WaitEvent.Disappeared);
                return t == null ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AT EventDo_IsDisappeared(object sender, WaitProcessArgs e)
        {
            try
            {
                return this.GetElementInfo().Exists() ? this : null;
            }
            catch (Exception)
            {
                return this;
            }
        }
        private Boolean ContainsAndOrWildcard(String which)
        {
            if (!String.IsNullOrEmpty(which))
            {
                if ((which.Contains(Var.Mark.And) || which.Contains(Var.Mark.Or) || which.Contains(Var.Mark.Wildcard)))
                {
                    return true;
                }
            }
            return false;
        }
        private AT GetElementHandle()
        {
            try
            {
                AutomationElement resultEle = null;
                AutomationElementCollection resultEles = null;
                AT atObj;
                if (AT.Ele.treeScope.ToString().Equals(AT.TreeScope.Element))
                {
                    resultEle = AT.Ele.me;
                }
                else
                {
                    if (this.ContainsAndOrWildcard(AT.Ele.Name) || this.ContainsAndOrWildcard(AT.Ele.ClassName) || this.ContainsAndOrWildcard(AT.Ele.AutomationId) || !String.IsNullOrEmpty(AT.Ele.Index))
                    {
                        if (AT.Ele.condition == null)
                        {
                            AT.Ele.condition = System.Windows.Automation.Condition.TrueCondition;
                        }
                        resultEles = AT.Ele.me.FindAll(AT.Ele.treeScope, AT.Ele.condition);
                    }
                    else
                    {
                        if (AT.Ele.condition == null)
                        {
                            return new AT(null);
                        }
                        resultEle = AT.Ele.me.FindFirst(AT.Ele.treeScope, AT.Ele.condition);
                    }
                    if (resultEle == null)
                    {
                        if (!String.IsNullOrEmpty(AT.Ele.Index))
                        {
                            resultEle = resultEles[Convert.ToInt16(AT.Ele.Index)];
                        }
                        else if (resultEles == null) {
                            throw new Exception("Can not find the element.");
                        }
                        else
                        {
                            foreach (AutomationElement item in resultEles)
                            {
                                if (ATElement.IsElementsMatch(atObj: new AT(item), Name: AT.Ele.Name, ClassName: AT.Ele.ClassName, AutomationId: AT.Ele.AutomationId))
                                {
                                    return new AT(item);
                                }
                            }
                        }
                    }
                }
                atObj = new AT(resultEle);
                if (resultEle != null && IsElementsMatch(atObj: atObj, Name: AT.Ele.Name, ClassName: AT.Ele.ClassName, AutomationId: AT.Ele.AutomationId))
                {
                    return atObj;
                }
                throw new Exception("");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private AT EventDo_GetElementTimeout(object sender, WaitProcessArgs e)
        {
            try
            {
                return GetElementHandle();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        public AT GetElementFromHwnd(IntPtr intPtr)
        {
            try
            {
                return new AT(AutomationElement.FromHandle(intPtr));
            }
            catch (Exception ex)
            {
                throw new Exception("GetElementFromHwnd error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void test(AT atObj)
        {
            try
            {
                AutomationElement aaa = GetTopLevelWindow(atObj.AutomationElement);
            }
            catch (Exception ex)
            {
                throw new Exception("GetElementFromHwnd error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private AutomationElement GetTopLevelWindow(AutomationElement element)
        {
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement elementParent;
            AutomationElement node = element;
            do
            {
                elementParent = walker.GetParent(node);
                if (elementParent == AutomationElement.RootElement) break;
                node = elementParent;
            }
            while (true);
            return node;
        }
    }
}
