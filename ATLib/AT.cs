using CommonLib.Util;
using System;
using System.Linq;
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
        public ATS GetElementsFromChild(ATElementStruct aTElementStruct, int timeout = -1, bool returnNullWhenException = false)
        {
            return GetElements(aTElementStruct, timeout, TreeScope.Children, returnNullWhenException);
        }
        public ATS GetElementsAllChild(int timeout = -1, bool returnNullWhenException = false)
        {
            return GetElements(new ATElementStruct(), timeout, TreeScope.Children, returnNullWhenException);
        }
        public ATS GetElementsFromDescendants(ATElementStruct aTElementStruct, int timeout = -1, bool returnNullWhenException = false)
        {
            return GetElements(aTElementStruct, timeout, TreeScope.Descendants, returnNullWhenException);
        }
        public AT GetElementFromChild(ATElementStruct aTElementStruct, int timeout = -1, bool checkEnabled = false, bool returnNullWhenException = false)
        {
            return GetElement(aTElementStruct, timeout, TreeScope.Children, checkEnabled, returnNullWhenException);
        }
        public AT GetElementFromDescendants(ATElementStruct aTElementStruct, int timeout = -1, bool checkEnabled = false, bool returnNullWhenException = false)
        {
            return GetElement(aTElementStruct, timeout, TreeScope.Descendants, checkEnabled, returnNullWhenException);
        }
        public ATS GetElements(ATElementStruct aTElementStruct, int timeout = -1, string treeScope = TreeScope.Children, bool returnNullWhenException = false)
        {
            return GetElements(treeScope, aTElementStruct.Name, aTElementStruct.AutomationId, aTElementStruct.ClassName, aTElementStruct.FrameworkId, aTElementStruct.ControlType, aTElementStruct.FullDescriton, returnNullWhenException);
        }
        public AT GetIAElementFromATS(ATElementStruct iAElementStruct, ATS ats, bool returnNullWhenException = false)
        {
            if (iAElementStruct.IADescription != null)
            {
                return ats.GetATCollection().ToList().Find(d => iAElementStruct.IADescription.Equals(d.GetIAccessible().Description()));
                //foreach (var at in ats.GetATCollection())
                //{
                //    if (aTElementStruct.IADescription.Equals(at.GetIAccessible().Description()))
                //    {
                //        return at;
                //    }
                //}
            }
            if (returnNullWhenException)
            {
                return null;
            }
            throw new Exception($"No element with Description [{iAElementStruct.IADescription}].");
        }
        public AT GetElement(ATElementStruct aTElementStruct, int timeout = -1, string treeScope = TreeScope.Descendants, bool checkEnabled = false, bool returnNullWhenException = false)
        {
            return GetElement(treeScope, aTElementStruct.Name, aTElementStruct.AutomationId, aTElementStruct.ClassName, aTElementStruct.FrameworkId, aTElementStruct.ControlType, aTElementStruct.FullDescriton, aTElementStruct.Index, timeout, checkEnabled, returnNullWhenException);
        }
        public void WaitForVanishedBySearch(ATElementStruct aTElementStruct, int timeout = -1, string treeScope = TreeScope.Descendants)
        {
            try
            {
                UtilWait.ForTrue(() => (GetElement(treeScope, aTElementStruct.Name, aTElementStruct.AutomationId, aTElementStruct.ClassName, aTElementStruct.FrameworkId, aTElementStruct.ControlType, aTElementStruct.FullDescriton, aTElementStruct.Index, -1, false, true) == null), timeout);
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
        public AT GetElement(string treeScope = null, string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string fullDescription = null, int? index = null, int timeout = -1, bool checkEnabled = false, bool returnNullWhenException = false)
        {
            try
            {
                AutomationElement = AutomationElement ?? AutomationElement.RootElement;
                var treeScopeVar = GetTreeScope(treeScope);
                var condition = GetCondition(name, automationId, className, frameworkId, controlType, fullDescription);
                var atObj = timeout <= 0 ? GetElementByHandler(AutomationElement, treeScopeVar, condition, name, automationId, className, index) : UtilWait.ForAnyResult(() => GetElementByHandler(AutomationElement, treeScopeVar, condition, name, automationId, className, index), timeout);
                if (checkEnabled != true) return atObj;
                if (!atObj.GetElementInfo().IsEnabled())
                {
                    throw new Exception("This element is not enabled.");
                }
                return atObj;
            }
            catch (Exception ex)
            {
                if (returnNullWhenException)
                {
                    return null;
                }
                throw new Exception("[ERROR]: GetElement. " + ex.Message + $"TreeScope:{(treeScope ?? "")} Name:{(name ?? "")} ControlType:{(controlType ?? "")} fullDescription:{(fullDescription ?? "")} ClassName:{(className ?? "")} AutomationId:{(automationId ?? "")}");
            }
        }
        public ATS GetElements(string treeScope = null, string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string fullDescription = null, bool returnNullWhenException = false)
        {
            try
            {
                var treeScopeVar = GetTreeScope(treeScope);
                var condition = GetCondition(name, automationId, className, frameworkId, controlType, fullDescription);
                AutomationElement = AutomationElement ?? AutomationElement.RootElement;
                var aec = AutomationElement.FindAll(treeScopeVar, condition);
                var at = new AT[aec.Count];
                for (var i = 0; i < aec.Count; i++)
                {
                    at[i] = new AT(aec[i]);
                }
                return new ATS(at);
            }
            catch (Exception ex)
            {
                throw new Exception("[ERROR]: GetElement. " + ex.Message + $"TreeScope:{(treeScope ?? "")} Name:{(name ?? "")} ControlType:{(controlType ?? "")} fullDescription:{(fullDescription ?? "")} ClassName:{(className ?? "")} AutomationId:{(automationId ?? "") }");
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
        public void WaitForVanished(int timeout = 1, int interval = -1)
        {
            UtilWait.ForTrue(() => !GetElementInfo().Exists(), timeout, interval);
        }
        //public void WaitForPresent(int timeout = 1)
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
        private AT GetElementByHandler(AutomationElement parentElement, System.Windows.Automation.TreeScope treeScope = System.Windows.Automation.TreeScope.Children, Condition condition = null, string name = null, string automationId = null, string className = null, int? index = null)
        {
            try
            {
                AutomationElement resultEle = null;
                AutomationElementCollection resultEles = null;
                if (treeScope.ToString().Equals(AT.TreeScope.Element))
                {
                    resultEle = parentElement;
                }
                else
                {
                    if (ContainsAndOrWildcard(name) || ContainsAndOrWildcard(className) || ContainsAndOrWildcard(automationId) || index != null)
                    {
                        resultEles = parentElement.FindAll(treeScope, Condition.TrueCondition);
                    }
                    else
                    {
                        //if (condition == null)
                        //{
                        //    return new AT(null);
                        //}
                        if (condition == null)
                        {
                            condition = Condition.TrueCondition;
                        }
                        resultEle = parentElement.FindFirst(treeScope, condition);
                    }
                    if (resultEle == null)
                    {
                        if (index != null && index > 0)
                        {
                            resultEle = resultEles[Convert.ToInt16(index)];
                        }
                        else if (resultEles == null) {
                            throw new Exception("Can not find the element.");
                        }
                        else
                        {
                            foreach (AutomationElement item in resultEles)
                            {
                                if (IsElementsMatch(atObj: new AT(item), name: name, className: className, automationId: automationId))
                                {
                                    return new AT(item);
                                }
                            }
                        }
                    }
                }
                var atObj = new AT(resultEle);
                if (resultEle != null && IsElementsMatch(atObj: atObj, name: name, className: className, automationId: automationId))
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
