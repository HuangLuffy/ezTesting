using System;
using System.Globalization;
using System.IO;
using CommonLib.Util.msg;
using CommonLib.Util.project;

namespace CommonLib.Util.log
{
    public class LogSimple : ILog
    {
        private readonly string _logName = "Log";
        public readonly string LogFolderPath;
        private readonly string _logFileFullPath;
        public LogSimple(string logFolderPath, string logName )
        {
            LogFolderPath = logFolderPath;
            _logName = logName;
            LogFolderPath = logFolderPath.Equals("") ? ProjectPath.GetProjectFullPath() : logFolderPath;
            _logFileFullPath = LogFolderPath + @"\" + _logName + ".log";
        }
        public LogSimple(string logFolderPath)
        {
            LogFolderPath = logFolderPath;
            _logFileFullPath = LogFolderPath + @"\" + _logName + ".log";
        }
        public LogSimple()
        {
            _logFileFullPath = Path.Combine(ProjectPath.GetProjectFullPath(), _logName + ".log");
        }
        private struct LogLevel
        {
            public const string Info = "Info";
            public const string Error = "Error";
            public const string Log = "Log";
            public const string Debug = "Debug";
            public const string Exception = "Exception";
        }
        private struct LogLevelRecord
        {
            public static bool Info = true;
            public static bool Error = true;
            public static bool Log = true;
            public static bool Debug = true;
            public static bool Exception = true;
        }

        private void Log(string message, string logLevel = "", string methodName = "", string exception = "")
        {

            if (LogLevelRecord.Log == false) return;
            methodName = methodName.Equals("") ? "- {NA}" : "- {" + methodName + "}";
            var fileInfo = new FileInfo(_logFileFullPath);
            var fileMode = File.Exists(_logFileFullPath) ? FileMode.Append : FileMode.Create;
            var fileStream = new FileStream(_logFileFullPath, fileMode);
            //content = streamReader.ReadToEnd();
            var streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(
                $"{DateTime.Now:yyyy_MM_dd hh:mm:ss} > [{logLevel}] {message} {methodName} {exception}");
            streamWriter.Close();
            fileStream.Close();
            if (fileInfo.Length < 1024 * 1024 * 200) return;
            var newName = _logFileFullPath + @"\" + _logName + Time() + ".txt";
            File.Move(_logFileFullPath, newName);

        }
        public void LogDebug(string message, string methodName = "")
        {
            if (LogLevelRecord.Debug) Log(message, LogLevel.Debug, methodName);
        }
        public void LogInfo(string message, string methodName = "")
        {
            if (LogLevelRecord.Info) Log(message, LogLevel.Info, methodName);
        }
        public void LogError(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Error) Log(message, LogLevel.Error, methodName, exception);
        }
        public void LogThrowMessage(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Exception) Log(message, LogLevel.Exception, methodName, exception);
            throw new Exception(message);
        }
        public void LogThrowException(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Exception) Log(message, LogLevel.Exception, methodName, exception);
            throw new Exception(exception);
        }
        /// <summary>
        /// time
        /// </summary>
        /// <returns></returns>
        private string Time()
        {
            var dNow = DateTime.Now.ToString(CultureInfo.InvariantCulture).Trim().Replace("/", "").Replace(":", "");
            var logFullPath = dNow;
            return logFullPath;
        }
    }
}
