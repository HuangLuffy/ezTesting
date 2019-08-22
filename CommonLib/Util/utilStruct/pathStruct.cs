﻿using System.IO;

namespace CommonLib.Util.utilStruct
{
    public struct PathStruct
    {
        string parentFolderPath;
        string name;
        public string ParentFolderPath
        {
            get
            {
                return parentFolderPath;
            }

            set
            {
                parentFolderPath = value;
            }
        }

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
        public string FullPath
        {
            get
            {
                return Path.Combine(parentFolderPath, name);
            }

            set
            {
                name = value;
            }
        }
        public PathStruct(string parentFolderPath, string name)
        {
            this.parentFolderPath = parentFolderPath;
            this.name = name;
        }
    }
}
