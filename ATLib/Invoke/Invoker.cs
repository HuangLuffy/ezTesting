using System;

namespace ATLib.Invoke
{
    public class Invoker : BaseInvoker
    {
        public Invoker(string[] args) :base(args)
        {
        }
        public string HandleEvent()
        {
            try
            {
                InitParameters();
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (functionName.ToLower().Equals(StructFunctionName.CLICKButtonByIndexOnWindowByClassName.ToLower()))
                {
                    ClickElement(StructPropertyType.index, targetPropertyValue, ATElement.ControlType.Button, StructPropertyType.className, containerPropertyValue, ATElement.ControlType.Window);
                }
                else if (functionName.ToLower().Equals(StructFunctionName.CLICKHyperLinkByNameOnWindowByClassName.ToLower()))
                {
                    ClickElement(StructPropertyType.name, targetPropertyValue, ATElement.ControlType.Hyperlink, StructPropertyType.className, containerPropertyValue, ATElement.ControlType.Window);
                }
                else if (functionName.ToLower().Equals(StructFunctionName.EXISTContext.ToLower()))
                {
                    IsElementExisted(StructPropertyType.name, "Context", ATElement.ControlType.Menu);
                }
                else
                {
                    throw new Exception($"Unknown function:[{functionName}].");
                }
                return ReturnResult.PASSED;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void ClickElement(string targetPropertyType, string targetPropertyValue, string targetControlType, string containerPropertyType, string containerPropertyValue, string containerControlType)
        {
            var parent = GetInvokerElement(containerPropertyType, containerPropertyValue, containerControlType);
            var element = GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType, parent);
            element.DoClick();
        }
        private bool IsElementExisted(string targetPropertyType, string targetPropertyValue, string targetControlType)
        {
            var element = GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType);
            if (element.GetElementInfo().Exists())
            {
                return true;
            }
            throw new Exception("Not exist.");
        }
    }
}
