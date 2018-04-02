using CommonLib.Util.project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.msg
{
    public class LogSimple : ILog
    {
        private string logName = "Log";
        private string logFolderPath;
        private string logFileFullPath;
        public LogSimple(string logFolderPath, string logName )
        {
            this.logFolderPath = logFolderPath;
            this.logName = logName;
            this.logFolderPath = logFolderPath.Equals("") ? ProjectPath.getProjectFullPath() : logFolderPath;
            this.logFileFullPath = this.logFolderPath + @"\" + this.logName + ".log";
        }
        public LogSimple(string logFolderPath)
        {
            this.logFolderPath = logFolderPath;
            this.logFileFullPath = this.logFolderPath + @"\" + this.logName + ".log";
        }
        public LogSimple()
        {
            this.logFileFullPath = Path.Combine(ProjectPath.getProjectFullPath(), logName + ".log");
        }
        private struct LogLevel
        {
            public const string INFO = "Info";
            public const string ERROR = "Error";
            public const string LOG = "Log";
            public const string DEBUG = "Debug";
            public const string EXCEPTION = "Exception";
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
            try
            {
                if (LogLevelRecord.Log == false) return;
                methodName = methodName.Equals("") ? "- {NA}" : "- {" + methodName + "}";
                FileInfo fileInfo = new FileInfo(logFileFullPath);
                FileMode fileMode;
                fileMode = File.Exists(logFileFullPath) ? FileMode.Append : FileMode.Create;
                FileStream fileStream = new FileStream(logFileFullPath, fileMode);
                //content = streamReader.ReadToEnd();
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(string.Format("{0} > [{1}] {2} {3} {4}", DateTime.Now.ToString("yyyy_MM_dd hh:mm:ss"), logLevel, message, methodName, exception));
                streamWriter.Close();
                fileStream.Close();
                if (fileInfo.Length >= 1024 * 1024 * 200)
                {
                    string NewName = logFileFullPath + @"\" + logName + this.time() + ".txt";
                    File.Move(logFileFullPath, NewName);
                }
            }
            catch (Exception ex)
            {
                //Dlg.ShowErrorDialog(Dlg.INFO.UnknowError); 
                throw ex;
            }
        }
        public void LogDebug(string message, string methodName = "")
        {
            if (LogLevelRecord.Debug) Log(message, LogLevel.DEBUG, methodName);
        }
        public void LogInfo(string message, string methodName = "")
        {
            if (LogLevelRecord.Info) Log(message, LogLevel.INFO, methodName);
        }
        public void LogError(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Error) Log(message, LogLevel.ERROR, methodName, exception);
        }
        public void LogThrowMessage(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Exception) Log(message, LogLevel.EXCEPTION, methodName, exception);
            throw new Exception(message);
        }
        public void LogThrowException(string message, string methodName = "", string exception = "")
        {
            if (LogLevelRecord.Exception) Log(message, LogLevel.EXCEPTION, methodName, exception);
            throw new Exception(exception);
        }
        /// <summary>
        /// time
        /// </summary>
        /// <returns></returns>
        private string time()
        {
            string dNow = DateTime.Now.ToString().Trim().Replace("/", "").Replace(":", "");
            string logFullPath = dNow.ToString();
            return logFullPath;
        }
    }
}
