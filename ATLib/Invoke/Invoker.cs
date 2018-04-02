using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATLib.Invoke
{
    public class Invoker : BaseInvoker, IInvoker
    {
        public Invoker(string[] args) :base(args)
        {
        }
        public string HandleEvent()
        {
            try
            {
                base.InitParameters();
            }
            catch (Exception)
            {

            }
            try
            {
                if (base.functionName.ToLower().Equals(StructFunctionName.CLICKButtonByIndexOnWindowByClassName.ToLower()))
                {
                    this.clickElement(StructPropertyType.index, targetPropertyValue, AT.ControlType.Button, StructPropertyType.className, containerPropertyValue, AT.ControlType.Window);
                }
                else if (base.functionName.ToLower().Equals(StructFunctionName.CLICKHyperLinkByNameOnWindowByClassName.ToLower()))
                {
                    this.clickElement(StructPropertyType.name, targetPropertyValue, AT.ControlType.Hyperlink, StructPropertyType.className, containerPropertyValue, AT.ControlType.Window);
                }
                else if (base.functionName.ToLower().Equals(StructFunctionName.EXISTContext.ToLower()))
                {
                    this.existElement(StructPropertyType.name, "Context", AT.ControlType.Menu);
                }
                else
                {
                    throw new Exception(string.Format("Unknown function:[{0}].", base.functionName));
                }
                return ReturnResult.PASSED;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void clickElement(string targetPropertyType, string targetPropertyValue, string targetControlType, string containerPropertyType, string containerPropertyValue, string containerControlType)
        {
            AT _Parent = base.GetInvokerElement(containerPropertyType, containerPropertyValue, containerControlType);
            AT _Element = base.GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType, _Parent);
            _Element.DoClick();
        }
        private Boolean existElement(string targetPropertyType, string targetPropertyValue, string targetControlType)
        {
            AT _Element = base.GetInvokerElement(targetPropertyType, targetPropertyValue, targetControlType);
            if (_Element.GetElementInfo().Exists())
            {
                return true;
            }
            throw new Exception(string.Format("Not exist."));
        }
    }
}
