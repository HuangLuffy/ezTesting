using System;
using System.Collections.Generic;
using System.Linq;

namespace ATLib
{
    public class ATS : ATElement
    {
        private AT[] ats;
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
        private ATS GetMatchedElements(string treeScope = null, string name = null, string automationId = null, string className = null, string frameworkId = null, string controlType = null, string index = null, SelectNum selectNumber = SelectNum.Single)
        {
            var eleList = new List<AT>();
            foreach (var item in GetATCollection())
            {
                try
                {
                    if (IsElementsMatch(atObj: item, name: name, className: className, automationId: automationId))
                    {
                        item.GetElement(treeScope: treeScope, name: name, automationId: automationId, className: className, frameworkId: frameworkId, controlType: controlType);
                        eleList.Add(item);
                        if (selectNumber.Equals(AT.SelectNum.Single))
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
                AT ele;
                if (!string.IsNullOrEmpty(name))
                {
                    try
                    {
                        ele = GetMatchedElements(name: name, treeScope: TreeScope.Element).GetATCollection()[0];
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
                var tName = "Can not get name";
                try
                {
                    tName = ele.GetElementInfo().Name();
                }
                catch (Exception)
                {
                    //ignored
                }
                return tName;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AT GetElementByIA(ATElementStruct iAElementStruct, bool returnNullWhenException = false)
        {
            if (iAElementStruct.IADescription != null)
            {
                return this.ats.ToList().Find(d => iAElementStruct.IADescription.Equals(d.GetIAccessible().Description()));
            }
            if (returnNullWhenException)
            {
                return null;
            }
            throw new Exception($"No element with Description [{iAElementStruct.IADescription}].");
        }
    }
}
