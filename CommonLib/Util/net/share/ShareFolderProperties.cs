namespace CommonLib.Util.net.share
{
    public class ShareFolderProperties
    {
        private string name = "";
        private string parentPath = "";
        private string fullPath = "";
        private string localPath = "";
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string ParentPath
        {
            get
            {
                return parentPath;
            }

            set
            {
                parentPath = value;
            }
        }

        public string FullPath
        {
            get
            {
                return fullPath;
            }

            set
            {
                fullPath = value;
            }
        }

        public string LocalPath
        {
            get
            {
                return localPath;
            }

            set
            {
                localPath = value;
            }
        }
    }
}
