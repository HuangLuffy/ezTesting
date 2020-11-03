using CommonLib.Util;
using System;
using System.Windows.Automation;
using static System.String;

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
            AutomationElement = elePara;
        }
        public AT GetRootElement()
        {
            return new AT(AutomationElement.RootElement);
        }
        public ATS GetElementsFromChild(ATElementStruct ATElementStruct, int Timeout = -1, bool ReturnNullWhenException = false)
        {
            return GetElements(ATElementStruct, Timeout, TreeScope.Children, ReturnNullWhenException);
        }
        public ATS GetElementsFromDescendants(ATElementStruct ATElementStruct, int Timeout = -1, bool ReturnNullWhenException = false)
        {
            return GetElements(ATElementStruct, Timeout, TreeScope.Descendants, ReturnNullWhenException);
        }
        public AT GetElementFromChild(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return GetElement(ATElementStruct, Timeout, TreeScope.Children, CheckEnabled, ReturnNullWhenException);
        }
        public AT GetElementFromDescendants(ATElementStruct ATElementStruct, int Timeout = -1, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return GetElement(ATElementStruct, Timeout, TreeScope.Descendants, CheckEnabled, ReturnNullWhenException);
        }
        public ATS GetElements(ATElementStruct ATElementStruct, int Timeout = -1, string treeScope = TreeScope.Children, bool ReturnNullWhenException = false)
        {
            return GetElements(treeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ReturnNullWhenException);
        }
        public AT GetElementIA(ATElementStruct ATElementStruct, int Timeout = -1, string treeScope = TreeScope.Children, bool ReturnNullWhenException = false)
        {
            var managedATS = GetElements(ATElementStruct, Timeout, treeScope, ReturnNullWhenException);
            if (managedATS == null)
            {
                return null;
            }
            if (ATElementStruct.IADescription != null)
            {
                foreach (var at in managedATS.GetATCollection())
                {
                    if (ATElementStruct.IADescription.Equals(at.GetIAccessible().Description()))
                    {
                        return at;
                    }
                }
            }
            if (ReturnNullWhenException)
            {
                return null;
            }
            throw new Exception($"No element with Description [{ATElementStruct.IADescription}].");
        }
        public AT GetElement(ATElementStruct ATElementStruct, int Timeout = -1, string TreeScope = TreeScope.Descendants, bool CheckEnabled = false, bool ReturnNullWhenException = false)
        {
            return GetElement(TreeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ATElementStruct.Index, Timeout, CheckEnabled, ReturnNullWhenException);
        }
        public void WaitForDisappearedBySearch(ATElementStruct ATElementStruct, int Timeout = -1, string TreeScope = TreeScope.Descendants)
        {
            try
            {
                UtilWait.ForTrue(() => (GetElement(TreeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ATElementStruct.Index, -1, false, true) == null), Timeout);
                //UtilWait.ForTrue(() =>
                //{
                //    return (GetElement(TreeScope, ATElementStruct.Name, ATElementStruct.AutomationId, ATElementStruct.ClassName, ATElementStruct.FrameworkId, ATElementStruct.ControlType, ATElementStruct.Index, -1, false, true) == null);

                //}, Timeout);
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
                AutomationElement = AutomationElement ?? AutomationElement.RootElement;
                var treeScope = GetTreeScope(TreeScope);
                var condition = GetCondition(Name, AutomationId, ClassName, FrameworkId, ControlType);
                var atObj = Timeout <= 0 ? GetElementByHandler(AutomationElement, treeScope, condition, Name, AutomationId, ClassName, Index) : UtilWait.ForAnyResult(() => GetElementByHandler(AutomationElement, treeScope, condition, Name, AutomationId, ClassName, Index), Timeout);
                if (CheckEnabled != true) return atObj;
                if (!atObj.GetElementInfo().IsEnabled())
                {
                    throw new Exception("This element is not enabled.");
                }
                return atObj;
            }
            catch (Exception ex)
            {
                if (ReturnNullWhenException)
                {
                    return null;
                }
                throw new Exception("[ERROR]: GetElement. " + ex.Message + $"TreeScope:{(TreeScope ?? "")} Name:{(Name ?? "")} ControlType:{(ControlType ?? "")} ClassName:{(ClassName ?? "")} AutomationId:{(AutomationId ?? "")}");
            }
        }
        public ATS GetElements(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, bool returnNullWhenException = false)
        {
            try
            {
                var treeScope = GetTreeScope(TreeScope);
                var condition = GetCondition(Name, AutomationId, ClassName, FrameworkId, ControlType);
                AutomationElement = AutomationElement ?? AutomationElement.RootElement;
                var aec = AutomationElement.FindAll(treeScope, condition);
                var at = new AT[aec.Count];
                for (var i = 0; i < aec.Count; i++)
                {
                    at[i] = new AT(aec[i]);
                }
                return new ATS(at);
            }
            catch (Exception ex)
            {
                throw new Exception("[ERROR]: GetElement. " + ex.Message + $"TreeScope:{(TreeScope ?? "")} Name:{(Name ?? "")} ControlType:{(ControlType ?? "")} ClassName:{(ClassName ?? "")} AutomationId:{(AutomationId ?? "")}");
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
                //AT atObj = null;
                AutomationElement = AutomationElement ?? AutomationElement.RootElement;  //System.Windows.Automation.Condition.TrueCondition
                var t = AutomationElement.FindAll(System.Windows.Automation.TreeScope.Descendants, Condition.TrueCondition);
                Console.WriteLine(t.Count);
                //AutomationElementCollection t = this.me.FindAll(System.Windows.Automation.TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.Pane));
                foreach (AutomationElement item in t)
                {
                    try
                    {
                        Console.WriteLine(
                            $"[{item.Current.Name.ToString()}] [{item.Current.ControlType.ProgrammaticName.ToString()}] [{item.Current.ClassName.ToString()}] [{item.Current.AutomationId}]");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{ex.Message}]");
                    }
                }
                return null;
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
                elePara = elePara ?? (this);
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
            UtilWait.ForTrue(() => !GetElementInfo().Exists(), timeout, interval);
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
            UtilWait.ForTrue(() => GetElementInfo().IsEnabled(), timeout, interval);
        }
        private bool ContainsAndOrWildcard(string which)
        {
            if (IsNullOrEmpty(which)) return false;
            return which.Contains(Var.Mark.And) || which.Contains(Var.Mark.Or) || which.Contains(Var.Mark.Wildcard);
        }
        private AT GetElementByHandler(AutomationElement parentElement, System.Windows.Automation.TreeScope TreeScope = System.Windows.Automation.TreeScope.Children, Condition Condition = null, string Name = null, string AutomationId = null, string ClassName = null, int? Index = null)
        {
            try
            {
                AutomationElement resultEle = null;
                AutomationElementCollection resultEles = null;
                if (TreeScope.ToString().Equals(AT.TreeScope.Element))
                {
                    resultEle = parentElement;
                }
                else
                {
                    if (ContainsAndOrWildcard(Name) || ContainsAndOrWildcard(ClassName) || ContainsAndOrWildcard(AutomationId) || Index != null)
                    {
                        if (Condition == null)
                        {
                            Condition = Condition.TrueCondition;
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
                        if (Index != null && Index > 0)
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
                                if (IsElementsMatch(atObj: new AT(item), name: Name, className: ClassName, automationId: AutomationId))
                                {
                                    return new AT(item);
                                }
                            }
                        }
                    }
                }
                var atObj = new AT(resultEle);
                if (resultEle != null && IsElementsMatch(atObj: atObj, name: Name, className: ClassName, automationId: AutomationId))
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
        /// <param name="element"></param>
        /// <returns></returns>
        private AutomationElement GetTopLevelWindow(AutomationElement element)
        {
            var walker = TreeWalker.ControlViewWalker;
            var node = element;
            do
            {
                var elementParent = walker.GetParent(node);
                if (elementParent == AutomationElement.RootElement) break;
                node = elementParent;
            }
            while (true);
            return node;
        }
    }
}
