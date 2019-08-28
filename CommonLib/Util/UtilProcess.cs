﻿using System;
using System.Diagnostics;
using CommonLib.Util.log;

namespace CommonLib.Util
{
    public static class UtilProcess
    {
        public static bool IsProcessExistedByName(string name)
        {
            foreach (var p in Process.GetProcessesByName(name))
            {
                return true;
            }
            return false;
        }
        public static void KillProcessByName(string name)
        {
            foreach (var p in Process.GetProcessesByName(name))
            {
                p.Kill();
            }
        }
        public static void SingletonUI()
        {
            try
            {
                using (var currentP = Process.GetCurrentProcess())
                { 
                    foreach (var p in Process.GetProcessesByName(currentP.ProcessName))
                    {
                        if (p.Id == currentP.Id) continue;
                        WinApi.SetForegroundWindow(p.MainWindowHandle);
                        WinApi.ShowWindowAsync(p.MainWindowHandle, WinApi.SwRestore);
                        currentP.Kill();
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
                using (var p = Process.Start(targetFullPath, para))
                {
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to start process [{targetFullPath} {para}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static void StartProcess(string targetFullPath, string para = "")
        {
            try
            {
                using (var p = Process.Start(targetFullPath, para))
                {
                    //p.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to start process [{targetFullPath} {para}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static int StartProcessGetInt(string targetFullPath, string para = "")
        {
            try
            {
                using (var p = new Process())
                {
                    var processStartInfo = new ProcessStartInfo(targetFullPath, para);
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
                Logger.LogThrowMessage($"Failed to start [{targetFullPath} {para}].", new StackFrame(0).GetMethod().Name, ex.Message);
                return -1;
            }
        }

        public static string StartProcessGetString(string targetFullPath, string para = "")
        {
            using (var p = new Process())
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
                if (p.ExitCode != 0)
                {
                    var errorMsg = p.StandardError.ReadToEnd();
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        throw new Exception(errorMsg.Replace(Environment.NewLine, " "));
                    }
                }

                p.StandardError.Close();
                return p.StandardOutput.ReadToEnd();
            }
        }

        public static string[] StartProcessGetStrings(string targetFullPath, string para = "")
        {
            var strList = StartProcessGetString(targetFullPath, para);
            return strList.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
