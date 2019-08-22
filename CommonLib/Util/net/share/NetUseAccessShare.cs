using CommonLib.Util.net.share;
using System;

namespace CommonLib.Util.net
{
    public class NetUseAccessShare : AccessShare, IAccessShare
    {
        public NetUseAccessShare(string shareFolderFullPath, string userName = "", string password = "") : base(shareFolderFullPath, userName, password)
        {
            
        }
        public NetUseAccessShare(string shareFolderFullPath) : base(shareFolderFullPath)
        {

        }
        public void ConnectShare(bool isPersistent = true)
        {
            string persistentValue = "YES";
            if (!isPersistent)
            {
                persistentValue = "NO";
            }
            string para = string.Format(@"use {0} {1} {2} /PERSISTENT:{3}", ShareFolderFullPath, UserName.Equals("") ? "" : "/User:" + UserName, Password, persistentValue);
            try
            {
                UtilProcess.StartProcessGetString("net", para);
            }
            catch (Exception)
            {
                //Logger.LogThrowMessage(string.Format(@"Failed to execute [net {0}]", para), new StackFrame(0).GetMethod().Name, ex.Message);
                throw new Exception(string.Format(@"Failed to execute [net {0}]", para));
            }
        }

        public void DeleteShareConnect(string ipOrName, string folderName = "")
        {
            string para = string.Format(@"use /delete {0}", ShareFolderFullPath);
            try
            {
                UtilProcess.StartProcessGetString("net", para);
            }
            catch (Exception)
            {
                //Logger.LogThrowMessage(string.Format(@"Failed to execute [net {0}]", para), new StackFrame(0).GetMethod().Name, ex.Message);
                throw new Exception(string.Format(@"Failed to execute [net {0}]", para));
            }
        }

        public string GetShareFolderFullPath()
        {
            return ShareFolderFullPath;
        }
    } 
}
