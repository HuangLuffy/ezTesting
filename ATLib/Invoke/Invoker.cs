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
                    existElement(StructPropertyType.name, "Context", ATElement.ControlType.Menu);
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
            AT _Parent = GetInvokerElement(containerPropertyType, containerPropertyValue, containerControlType);
            AT _Element = GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType, _Parent);
            _Element.DoClick();
        }
        private Boolean existElement(string targetPropertyType, string targetPropertyValue, string targetControlType)
        {
            AT _Element = GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType);
            if (_Element.GetElementInfo().Exists())
            {
                return true;
            }
            throw new Exception("Not exist.");
        }
    }
}
