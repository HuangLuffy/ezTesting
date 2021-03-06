﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommonLib.Util.Log;

namespace CommonLib.Util
{
    public static class UtilProcess
    {
        //UtilProcess.KillProcessByName(System.IO.Path.GetFileName(_appPath));

        public static void KillAllProcessesByName(params string[] ps)
        {
            foreach (var p in ps)
            {
                KillProcessByName(p); UtilTime.WaitTime(0.2);
            }
        }
        public static bool IsProcessExistedByName(string name)
        {
            return Process.GetProcessesByName(name).Any();
            //foreach (var p in Process.GetProcessesByName(name))
            //{
            //    return true;
            //}
            //return false;
        }
        public static void KillProcessByName(string name)
        {
            name = name.Contains(".exe") ? name.Replace(".exe", "") : name;
            foreach (var p in Process.GetProcessesByName(name))
            {
                p.Kill();
            }
        }
        public static void KillProcessByFuzzyName(string name)
        {
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName.ToLower().Contains(name.ToLower()))
                {
                    p.Kill();
                }
            } 
        }
        //UtilProcess.ExecuteCmd(uninstallerPath + " /silent");     does not work
        //UtilProcess.StartProcessGetString(uninstallerPath, "/silent");   work
        public static void ExecuteCmd(string command = "shutdown -f -r -t 0")
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";//启动cmd命令
                p.StartInfo.UseShellExecute = false;//是否使用系统外壳程序启动进程
                p.StartInfo.RedirectStandardInput = true;//是否从流中读取
                p.StartInfo.RedirectStandardOutput = true;//是否写入流
                p.StartInfo.RedirectStandardError = true;//是否将错误信息写入流
                p.StartInfo.CreateNoWindow = true;//是否在新窗口中启动进程
                p.Start();
                p.StandardInput.WriteLine(command);
                var a = p.StandardOutput.ReadToEnd();
            }
        }
        public static void SingletonUi()
        {
            try
            {
                using (var currentP = Process.GetCurrentProcess())
                { 
                    foreach (var p in Process.GetProcessesByName(currentP.ProcessName))
                    {
                        if (p.Id == currentP.Id) continue;
                        UtilWinApi.SetForegroundWindow(p.MainWindowHandle);
                        UtilWinApi.ShowWindowAsync(p.MainWindowHandle, UtilWinApi.SwRestore);
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
                    p?.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to start process [{targetFullPath} {para}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static void StartProcess(string targetFullPath, string para = "")
        {
            using (Process.Start(targetFullPath, para))
            {
                //p.Start();
            }
        }
        public static Process StartProcessReturn(string targetFullPath, string para = "")
        {
            return Process.Start(targetFullPath, para);
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

        public static IEnumerable<string> StartProcessGetStrings(string targetFullPath, string para = "")
        {
            var strList = StartProcessGetString(targetFullPath, para);
            return strList.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
