using CommonLib.Util.msg;

namespace CommonLib.Util.log
{
    public class Logger : Msg
    {
        private static ILog _iLog;
        public static void Load(ILog iLog)
        {
            _iLog = iLog;
        }
        public static void LogDebug(string message, string methodName = "")
        {
            _iLog.LogDebug(message, methodName);
        }
        public static void LogInfo(string message, string methodName = "")
        {
            _iLog.LogInfo(message, methodName);
        }
        public static void LogError(string message, string methodName = "", string exception = "")
        {
            _iLog.LogError(message, methodName, exception);
        }
        public static void LogThrowMessage(string message, string methodName = "", string exception = "")
        {
            _iLog.LogThrowMessage(message, methodName, exception);
        }
        public static void LogThrowException(string message, string methodName = "", string exception = "")
        {
            _iLog.LogThrowException(message, methodName, exception);
        } 
    }
}
