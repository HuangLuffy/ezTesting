﻿using Microsoft.Win32;
using System;

namespace CommonLib.Util
{
    public class UtilSysEnv
    {
        /// <summary>
        /// 获取系统环境变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSysEnvironmentByName(string name)
        {
            string result;
            try
            {
                result = OpenSysEnvironment().GetValue(name).ToString();//读取
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 打开系统环境变量注册表
        /// </summary>
        /// <returns>RegistryKey</returns>
        private static RegistryKey OpenSysEnvironment()
        {
            var regLocalMachine = Registry.LocalMachine;
            var regSystem = regLocalMachine.OpenSubKey("SYSTEM", true);//打开HKEY_LOCAL_MACHINE下的SYSTEM 
            var regControlSet001 = regSystem.OpenSubKey("ControlSet001", true);//打开ControlSet001 
            var regControl = regControlSet001.OpenSubKey("Control", true);//打开Control 
            var regManager = regControl.OpenSubKey("Session Manager", true);//打开Control 
            var regEnvironment = regManager.OpenSubKey("Environment", true);
            return regEnvironment;
        }

        /// <summary>
        /// 设置系统环境变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="strValue">值</param>
        public static void SetSysEnvironment(string name, string strValue)
        {
            OpenSysEnvironment().SetValue(name, strValue);
        }

        /// <summary>
        /// 检测系统环境变量是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckSysEnvironmentExist(string name)
        {
            return !string.IsNullOrEmpty(GetSysEnvironmentByName(name));
        }

        /// <summary>
        /// 添加到PATH环境变量（会检测路径是否存在，存在就不重复）
        /// </summary>
        /// <param name="strHome"></param>
        public static void SetPathAfter(string strHome)
        {
            var pathList = GetSysEnvironmentByName("PATH");
            //检测是否以;结尾
            if (pathList.Substring(pathList.Length - 1, 1) != ";")
            {
                SetSysEnvironment("PATH", pathList + ";");
                pathList = GetSysEnvironmentByName("PATH");
            }
            var list = pathList.Split(';');
            var isPathExist = false;

            foreach (var item in list)
            {
                if (item == strHome)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetSysEnvironment("PATH", pathList + strHome + ";");
            }
        }

        public static void SetPathBefore(string strHome)
        {
            var pathList = GetSysEnvironmentByName("PATH");
            var list = pathList.Split(';');
            var isPathExist = false;
            foreach (var item in list)
            {
                if (item == strHome)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetSysEnvironment("PATH", strHome + ";" + pathList);
            }
        }

        public static void SetPath(string strHome)
        {
            var pathList = GetSysEnvironmentByName("PATH");
            var list = pathList.Split(';');
            var isPathExist = false;
            foreach (var item in list)
            {
                if (item == strHome)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetSysEnvironment("PATH", pathList + strHome + ";");
            }
        }
    }
}
