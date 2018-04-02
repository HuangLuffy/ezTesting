using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.msg
{
    public interface ILog
    {
        void LogDebug(string message, string methodName = "");
        void LogInfo(string message, string methodName = "");
        void LogError(string message, string methodName = "", string exception = "");
        void LogThrowMessage(string message, string methodName = "", string exception = "");
        void LogThrowException(string message, string methodName = "", string exception = "");
    }
}
