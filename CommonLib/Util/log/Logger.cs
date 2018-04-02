
using CommonLib.Util.msg;

namespace CommonLib.Util
{
    public class Logger : Msg
    {
        static ILog _ILog;
        public static void Load(ILog _ILogIn)
        {
            _ILog = _ILogIn;
        }
        public static void LogDebug(string message, string methodName = "")
        {
            _ILog.LogDebug(message, methodName);
        }
        public static void LogInfo(string message, string methodName = "")
        {
            _ILog.LogInfo(message, methodName);
        }
        public static void LogError(string message, string methodName = "", string exception = "")
        {
            _ILog.LogError(message, methodName, exception);
        }
        public static void LogThrowMessage(string message, string methodName = "", string exception = "")
        {
            _ILog.LogThrowMessage(message, methodName, exception);
        }
        public static void LogThrowException(string message, string methodName = "", string exception = "")
        {
            _ILog.LogThrowException(message, methodName, exception);
        } 
    }
}
