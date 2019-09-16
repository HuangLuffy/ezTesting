using System;
using System.Collections.Generic;
using System.IO;

namespace CommonLib.Util.Net.Share
{
    public abstract class AccessShare
    {
        protected readonly List<ShareFolderProperties> ShareFolderPropertiesList = new List<ShareFolderProperties>();
        protected ShareFolderProperties ShareFolderProperties;

        protected string ShareFolderFullPath { get; }

        protected string UserName { get; } = "";

        protected string Password { get;  } = "";

        protected string IpOrName { get;  }

        protected AccessShare(string shareFolderFullPath)
        {
            if (shareFolderFullPath.Contains("@"))
            {
                ShareFolderFullPath = shareFolderFullPath.Split('@')[1].Trim();
                IpOrName = alterPath(ShareFolderFullPath).Split('\\')[0].Trim();
                var userAndPass = alterPath(shareFolderFullPath.Split('@')[0]).Trim();
                UserName = userAndPass.Split(':')[0].Trim();
                Password = userAndPass.Split(':')[1].Trim();
                ShareFolderFullPath = @"\\" + alterPath(ShareFolderFullPath);
            }
            else
            {
                ShareFolderFullPath = @"\\" + alterPath(shareFolderFullPath).Trim();
                IpOrName = alterPath(ShareFolderFullPath).Split('\\')[0].Trim();
            }
        }
        protected AccessShare(string shareFolderFullPath, string userName, string password)
        {
            ShareFolderFullPath = @"\\" + alterPath(shareFolderFullPath).Trim();
            IpOrName = alterPath(ShareFolderFullPath).Split('\\')[0].Trim();
            UserName = userName.Trim();
            Password = password.Trim();
        }
        private string alterPath(string ipOrName)
        {
            for (var i = 0; i < ipOrName.Length; i++)
            {
                if (ipOrName.StartsWith(@"\"))
                {
                    ipOrName = ipOrName.Substring(1);  
                }
            }
            return ipOrName;
        }
        public bool GetConnectState()
        {
            try
            {
                return Path.HasExtension(ShareFolderFullPath) ? File.Exists(ShareFolderFullPath) : Directory.Exists(ShareFolderFullPath);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
