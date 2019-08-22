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
                this.ShareFolderFullPath = shareFolderFullPath.Split('@')[1].Trim();
                this.IpOrName = alterPath(this.ShareFolderFullPath).Split('\\')[0].Trim();
                string userAndPass = alterPath(shareFolderFullPath.Split('@')[0]).Trim();
                this.UserName = userAndPass.Split(':')[0].Trim();
                this.Password = userAndPass.Split(':')[1].Trim();
                this.ShareFolderFullPath = @"\\" + alterPath(this.ShareFolderFullPath);
            }
            else
            {
                this.ShareFolderFullPath = @"\\" + alterPath(shareFolderFullPath).Trim();
                this.IpOrName = alterPath(this.ShareFolderFullPath).Split('\\')[0].Trim();
            }
        }
        public AccessShare(string shareFolderFullPath, string userName, string password)
        {
            this.ShareFolderFullPath = @"\\" + alterPath(shareFolderFullPath).Trim();
            this.IpOrName = alterPath(this.ShareFolderFullPath).Split('\\')[0].Trim();
            this.UserName = userName.Trim();
            this.Password = password.Trim();
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
                if (Path.HasExtension(this.ShareFolderFullPath))
                {
                    return File.Exists(this.ShareFolderFullPath);
                }
                else
                {
                    return Directory.Exists(this.ShareFolderFullPath);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
