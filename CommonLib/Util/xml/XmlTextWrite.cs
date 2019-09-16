using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommonLib.Util.Log;

namespace CommonLib.Util.Xml
{
    public class XmlTextWrite
    {
        public List<string> GetElementsList()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogThrowException("Failed to get Elements List.", new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
    }
}
