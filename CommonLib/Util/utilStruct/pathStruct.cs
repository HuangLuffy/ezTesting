using System.IO;

namespace CommonLib.Util.utilStruct
{
    public struct PathStruct
    {
        public string ParentFolderPath { get; set; }
        public string Name { get; set; }
        public string FullPath
        {
            get => Path.Combine(ParentFolderPath, Name);

            set => Name = value;
        }
        public PathStruct(string parentFolderPath, string name)
        {
            ParentFolderPath = parentFolderPath;
            Name = name;
        }
    }
}
