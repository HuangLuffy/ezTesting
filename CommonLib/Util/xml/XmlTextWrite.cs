using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.xml
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
                Logger.LogThrowException(String.Format("Failed to get Elements List."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
    }
}
