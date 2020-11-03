using System;
using System.Collections.Generic;

namespace ATLib
{
    public class ATS : ATElement
    {
        private AT[] ats = null;
        public ATS(AT[] ats)
        {
            this.ats = ats;
        }
        public AT[] GetATCollection()
        {
            return ats;
        }
        public int Length()
        {
            try
            {
                return GetATCollection().Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private ATS GetMatchedElements(string TreeScope = null, string Name = null, string AutomationId = null, string ClassName = null, string FrameworkId = null, string ControlType = null, string Index = null, SelectNum SelectNumber = SelectNum.Single)
        {
            var eleList = new List<AT>();
            foreach (var item in GetATCollection())
            {
                try
                {
                    if (IsElementsMatch(atObj: item, Name: Name, ClassName: ClassName, AutomationId: AutomationId))
                    {
                        item.GetElement(TreeScope: TreeScope, Name: Name, AutomationId: AutomationId, ClassName: ClassName, FrameworkId: FrameworkId, ControlType: ControlType);
                        eleList.Add(item);
                        if (SelectNumber.Equals(AT.SelectNum.Single))
                            break;
                    }
                }
                catch (Exception) { 
                    //ignored
                }
            }
            if (eleList.Count == 0)
            {
                throw new Exception("There is no any item matching.");
            }
            var arrAutomationElement = eleList.ToArray();
            return new ATS(arrAutomationElement);
        }
        public string SelectItemFromCollection(string strIndex = null, string name = null, DoMode doMode = DoMode.Point)
        {
            try
            {
                AT ele = null;
                if (!string.IsNullOrEmpty(name))
                {
                    try
                    {
                        ele = GetMatchedElements(Name: name, TreeScope: TreeScope.Element).GetATCollection()[0];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("The item {0} does not exist.", name, ex.Message));
                    }
                }
                else
                {
                    try
                    {
                        ele = GetATCollection()[Convert.ToInt16(strIndex)];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("The item index {0} does not exist", strIndex, ex.Message));
                    }
                }
                ele.DoByMode(doMode);
                var t_name = "Can not get name";
                try { t_name = ele.GetElementInfo().Name(); }
                catch (Exception) { }
                return t_name;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
