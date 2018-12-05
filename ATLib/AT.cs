using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ATLib
{
    public class AT : ATAction
    {
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
        public AT GetRootElement()
        {
            return new AT(AutomationElement.RootElement);
        }
        public ATS GetElementsFromChild(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return this.GetElements(ATElementStruct, Timeout, AT.TreeScope.Children, ReturnNullWhenException);
        }
        public ATS GetElementsFromDescendants(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return this.GetElements(ATElementStruct, Timeout, AT.TreeScope.Descendants, ReturnNullWhenException);
        }
        public AT GetElementFromChild(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return this.GetElement(ATElementStruct, Timeout, AT.TreeScope.Children, CheckEnabled, ReturnNullWhenException);
        }
        public AT GetElementFromDescendants(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return this.GetElement(ATElementStruct, Timeout, AT.TreeScope.Descendants, CheckEnabled, ReturnNullWhenException);
        }

        public ATS GetElements(ATElementStruct ATElementStruct, int Timeout = -1, string treeScope = AT.TreeScope.Children, bool ReturnNullWhenException = false)
        {
            return this.GetElements(treeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ReturnNullWhenException);
        }
        public AT GetElement(ATElementStruct ATElementStruct, int Timeout = -1, string TreeScope = AT.TreeScope.Descendants, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return this.GetElement(TreeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ATElementStruct.Index, Timeout, CheckEnabled, ReturnNullWhenException);
        }
        public void WaitForDisappearedBySearch(ATElementStruct ATElementStruct, int Timeout = -1, string TreeScope = AT.TreeScope.Descendants)
        {
            try
            {
                UtilWait.ForTrue(() =>
                {
                    return (this.GetElement(TreeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ATElementStruct.Index, -1, false, true) == null);

                }, Timeout);
            }
            catch (Exception ex)
            {
                throw new Exception($"This element still exists. {ex.Message}" );
            }
        }
        public AT GetElement(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, int? Index = null, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            try
            {
                AT atObj = null;
                this.AutomationElement = (this.AutomationElement == null) ? AutomationElement.RootElement : this.AutomationElement;
                System.Windows.Automation.TreeScope treeScope = GetTreeScope(TreeScope);
                System.Windows.Automation.Condition condition = GetCondition(Name, AutomationId, ClassName, FrameworkId, ControlType);
                if (Timeout <= 0)
                {
                    atObj = this.GetElementByHandler(this.AutomationElement, treeScope, condition, Name, AutomationId, ClassName, Index);
                }
                else
                {
                    atObj = UtilWait.ForResult(() =>
                    {
                        return GetElementByHandler(this.AutomationElement, treeScope, condition, Name, AutomationId, ClassName, Index);
                    }, Timeout);
                }
                if (CheckEnabled == true)
                {
                    if (!(atObj.GetElementInfo().IsEnabled()))
                    {
                        throw new Exception("This element is not enabled. ");
                    }
                }
                return atObj;
            }
            catch (Exception ex)
            {
                if (ReturnNullWhenException)
                {
                    return null;
                }
                throw new Exception("[ERROR]: GetElement. " + ex.Message + $"TreeScope: {(TreeScope == null ? "" : TreeScope)} Name: {(Name == null ? "" : Name)} ControlType: {(ControlType == null ? "" : Name)} ClassName: {(ClassName == null ? "" : Name)} AutomationId: {(AutomationId == null ? "" : AutomationId)}");
            }
        }
        public ATS GetElements(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, bool returnNullWhenException = false)
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
                throw new Exception("[ERROR]: GetElements. " + ex.Message);
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
                throw new Exception("[ERROR]: GetElementInfo. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void WaitForDisappeared(int timeout = 1, int interval = -1)
        {
            UtilWait.ForTrue(() =>
            {
               return !this.GetElementInfo().Exists();
            }, timeout, interval);
        }
        //public void WaitForExisted(int timeout = 1)
        //{
        //    UtilWait.ForTrue(() =>
        //    {
        //        return this.GetElementInfo().Exists();
        //    }, timeout);
        //}
        public void WaitForEnabled(int timeout = 1, int interval = -1)
        {
            UtilWait.ForTrue(() =>
            {
                return this.GetElementInfo().IsEnabled();
            }, timeout, interval);
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
        private AT GetElementByHandler(AutomationElement parentElement, System.Windows.Automation.TreeScope TreeScope = System.Windows.Automation.TreeScope.Children, System.Windows.Automation.Condition Condition = null, string Name = null, string AutomationId = null, string ClassName = null, int? Index = null)
        {
            try
            {
                AutomationElement resultEle = null;
                AutomationElementCollection resultEles = null;
                AT atObj;
                if (TreeScope.ToString().Equals(AT.TreeScope.Element))
                {
                    resultEle = parentElement;
                }
                else
                {
                    if (this.ContainsAndOrWildcard(Name) || this.ContainsAndOrWildcard(ClassName) || this.ContainsAndOrWildcard(AutomationId) || Index != null)
                    {
                        if (Condition == null)
                        {
                            Condition = System.Windows.Automation.Condition.TrueCondition;
                        }
                        resultEles = parentElement.FindAll(TreeScope, Condition);
                    }
                    else
                    {
                        if (Condition == null)
                        {
                            return new AT(null);
                        }
                        resultEle = parentElement.FindFirst(TreeScope, Condition);
                    }
                    if (resultEle == null)
                    {
                        if (Index != null || Index > 0)
                        {
                            resultEle = resultEles[Convert.ToInt16(Index)];
                        }
                        else if (resultEles == null) {
                            throw new Exception("Can not find the element.");
                        }
                        else
                        {
                            foreach (AutomationElement item in resultEles)
                            {
                                if (ATElement.IsElementsMatch(atObj: new AT(item), Name: Name, ClassName: ClassName, AutomationId: AutomationId))
                                {
                                    return new AT(item);
                                }
                            }
                        }
                    }
                }
                atObj = new AT(resultEle);
                if (resultEle != null && IsElementsMatch(atObj: atObj, Name: Name, ClassName: ClassName, AutomationId: AutomationId))
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
