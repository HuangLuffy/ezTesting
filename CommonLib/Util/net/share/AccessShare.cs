using System;
using System.Collections.Generic;
using System.IO;

namespace CommonLib.Util.net.share
{
    public abstract class AccessShare
    {
        private string shareFolderFullPath = "";
        private string userName = "";
        private string password = "";
        private string ipOrName = "";
        protected List<ShareFolderProperties> _ShareFolderPropertiesList = new List<ShareFolderProperties>();
        protected ShareFolderProperties _ShareFolderProperties;

        public string ShareFolderFullPath
        {
            get
            {
                return shareFolderFullPath;
            }

            set
            {
                shareFolderFullPath = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string IpOrName
        {
            get
            {
                return ipOrName;
            }

            set
            {
                ipOrName = value;
            }
        }
        public AccessShare(string shareFolderFullPath)
        {
            if (shareFolderFullPath.Contains("@"))
            {
                ShareFolderFullPath = shareFolderFullPath.Split('@')[1].Trim();
                IpOrName = alterPath(ShareFolderFullPath).Split('\\')[0].Trim();
                string userAndPass = alterPath(shareFolderFullPath.Split('@')[0]).Trim();
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
        public AccessShare(string shareFolderFullPath, string userName, string password)
        {
            ShareFolderFullPath = @"\\" + alterPath(shareFolderFullPath).Trim();
            IpOrName = alterPath(ShareFolderFullPath).Split('\\')[0].Trim();
            UserName = userName.Trim();
            Password = password.Trim();
        }
        private string alterPath(string ipOrName)
        {
            for (int i = 0; i < ipOrName.Length; i++)
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
                if (Path.HasExtension(ShareFolderFullPath))
                {
                    return File.Exists(ShareFolderFullPath);
                }
                else
                {
                    return Directory.Exists(ShareFolderFullPath);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
