using CommonLib.Util.net.share;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string para = string.Format(@"use {0} {1} {2} /PERSISTENT:{3}", this.ShareFolderFullPath, UserName.Equals("") ? "" : "/User:" + this.UserName, this.Password, persistentValue);
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
            string para = string.Format(@"use /delete {0}", this.ShareFolderFullPath);
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
            return this.ShareFolderFullPath;
        }
    } 
}
