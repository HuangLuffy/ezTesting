using System;

namespace ATLib.Invoke
{
    public class BaseInvoker
    {
        protected string[] args = null;
        protected string functionName = null;
        protected string targetPropertyValue = null;
        protected string containerPropertyValue = null;
        public BaseInvoker(string[] args)
        {
            this.args = args;
        }
        protected void InitParameters()
        {
            try { functionName = args[0].Replace("\"", ""); } catch (Exception) { }
            try { targetPropertyValue = args[1].Replace("\"", ""); } catch (Exception) { }
            try { containerPropertyValue = args[2].Replace("\"", ""); } catch (Exception) { }
        }
        protected struct ReturnResult
        {
            public const string PASSED = "Passed";
        }
        protected struct StructPropertyType
        {
            public const string index = "index";
            public const string name = "name";
            public const string className = "className";
            public const string id = "id";
            public const string hwnd = "hwnd";
        }
        protected struct StructFunctionName
        {
            public const string CLICKButtonByIndexOnWindowByClassName = "clickButtonByIndexOnWindowByClassName";
            public const string CLICKHyperLinkByNameOnWindowByClassName = "clickHyperLinkByNameOnWindowByClassName";
            public const string EXISTContext = "existContext";
        }
        private AT GetInvokerElementByPropertyType(AT parent, string propertyType, string propertyValue, string controlTypeValue)
        { 
            if (propertyType.Equals(StructPropertyType.index))
            {
                return parent.GetElement(TreeScope: ATElement.TreeScope.Descendants, Index: int.Parse(propertyValue), ControlType: controlTypeValue);
            }
            else if (propertyType.Equals(StructPropertyType.name))
            {
                return parent.GetElement(TreeScope: ATElement.TreeScope.Descendants, Name: propertyValue, ControlType: controlTypeValue);
            }
            else if (propertyType.Equals(StructPropertyType.className))
            {
                return parent.GetElement(TreeScope: ATElement.TreeScope.Descendants, ClassName: propertyValue, ControlType: controlTypeValue);
            }
            else if (propertyType.Equals(StructPropertyType.id))
            {
                return parent.GetElement(AutomationId: propertyValue, ControlType: controlTypeValue);
            }
            else if (propertyType.Equals(StructPropertyType.hwnd))
            {
                return parent.GetElementFromHwnd(new IntPtr(Convert.ToInt32(propertyValue)));
            }
            return null;
        }
        protected AT GetInvokerElement(string propertyType, string propertyValue, string controlTypeValue, AT parent = null)
        {
            try
            {
                parent = parent == null ? new AT().GetRootElement() : parent;
                return GetInvokerElementByPropertyType(parent, propertyType, propertyValue, controlTypeValue);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Failed to get [{controlTypeValue}]. propertyType:[{propertyType}] propertyValue:[{propertyValue}]");
            }
        } 
    }
}
