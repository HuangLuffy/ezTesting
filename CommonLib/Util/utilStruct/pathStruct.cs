using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return Path.Combine(this.parentFolderPath, this.name);
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
