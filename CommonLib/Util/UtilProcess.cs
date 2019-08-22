using System;
using System.Diagnostics;

namespace CommonLib.Util
{
    public class UtilProcess
    {
        public static bool IsProcessExistedByName(string name)
        {
            foreach (Process p in Process.GetProcessesByName(name))
            {
                return true;
            }
            return false;
        }
        public static void KillProcessByName(string name)
        {
            foreach (Process p in Process.GetProcessesByName(name))
            {
                p.Kill();
            }
        }
        public static void SingletonUI()
        {
            try
            {
                using (Process currentP = Process.GetCurrentProcess())
                { 
                    foreach (Process p in Process.GetProcessesByName(currentP.ProcessName))
                    {
                        if (p.Id != currentP.Id)
                        {
                            WinApi.SetForegroundWindow(p.MainWindowHandle);
                            WinApi.ShowWindowAsync(p.MainWindowHandle, WinApi.SW_RESTORE);
                            currentP.Kill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage(string.Format("Failed to leave unique UI."), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static void StartProcessWaitForExit(string targetFullPath, string para = "")
        {
            try
            {
                using (Process p = Process.Start(targetFullPath, para))
                {
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage(string.Format("Failed to start process [{0} {1}].", targetFullPath, para), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static void StartProcess(string targetFullPath, string para = "")
        {
            try
            {
                using (Process p = Process.Start(targetFullPath, para))
                {
                    //p.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage(string.Format("Failed to start process [{0} {1}].", targetFullPath, para), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static int StartProcessGetInt(string targetFullPath, string para = "")
        {
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(targetFullPath, para);
                    p.StartInfo = processStartInfo;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    while (!p.HasExited)
                    {
                        p.WaitForExit();
                    }
                    return p.ExitCode;
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage(string.Format("Failed to start [{0} {1}].", targetFullPath, para), new StackFrame(0).GetMethod().Name, ex.Message);
                return -1;
            }
        }
        public static string StartProcessGetString(string targetFullPath, string para = "")
        {
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = targetFullPath;
                    p.StartInfo.Arguments = para;
                    p.Start();
                    //string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                    //proc.StandardInput.WriteLine(dosLine);
                    //proc.StandardInput.WriteLine("exit");
                    p.WaitForExit();
                    string errormsg = "";
                    if (p.ExitCode != 0)
                    {
                        errormsg = p.StandardError.ReadToEnd();
                        if (!string.IsNullOrEmpty(errormsg))
                        {
                            throw new Exception(errormsg.Replace(System.Environment.NewLine, " "));
                        }        
                    }
                    p.StandardError.Close();
                    return p.StandardOutput.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string[] StartProcessGetStrings(string targetFullPath, string para = "")
        {
            try
            {
                string strlist = UtilProcess.StartProcessGetString(targetFullPath, para);
                return strlist.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
