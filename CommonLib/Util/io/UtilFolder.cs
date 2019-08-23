using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CommonLib.Util.io
{
    public static class UtilFolder
    {
        public static void CreateDirectory(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (Exception)
            {
                throw new Exception($"Failed to CreateDirectory [{folderPath}].");
            }
        }
        private static void DeleteFilesAndFoldersRecursively(string targetDir)
        {
            foreach (var file in Directory.GetFiles(targetDir))
            {
                File.Delete(file);
            }
            foreach (var subDir in Directory.GetDirectories(targetDir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }
            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) was not enough.
            Directory.Delete(targetDir);
        }
        public static void DeleteDirectory(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    DeleteFilesAndFoldersRecursively(folderPath);
                    // Directory.Delete(folderPath, true);
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Failed to Delete folder [{0}]. " + ex.Message, folderPath));
            }
        }
        public static void RecreateDirectory(string folderPath)
        {
            try
            {
                DeleteDirectory(folderPath);
                Thread.Sleep(100);
                CreateDirectory(folderPath);
            }
            catch (Exception)
            {
                Logger.LogThrowException($"Failed to recreate folder [{folderPath}].");
            }
        }
        public static string[] GetDirectoryFiles(string folderPath)
        {
            try
            {
                return Directory.GetFiles(folderPath, "*", SearchOption.TopDirectoryOnly);
            }
            catch (Exception)
            {
                throw new Exception($"Failed to get files in folder [{folderPath}].");
            }
        }
        public static string[] GetSubDirectories(string folderPath)
        {
            try
            {
                return Directory.GetDirectories(folderPath, "*" , SearchOption.TopDirectoryOnly);
            }
            catch (Exception)
            {
                throw new Exception($"Failed to get subfolders [{folderPath}].");
            }
        }
        public static void MoveDirectory(string source, string destination)
        {
            try
            {
                //need destination folder does not exist.
                Directory.Move(source, destination);
            }
            catch (Exception)
            {
                throw new Exception($"Failed to move folder from [{source}] to [{destination}].");
            }
        }
        private static void CopyDirectoryAndSubDirectoriesFiles(DirectoryInfo oldDirectory, DirectoryInfo newDirectory)
        {
            var newDirectoryFullName = newDirectory.FullName ;
            //string NewDirectoryFullName = NewDirectory.FullName + @"\" + OldDirectory.Name;
            if (!Directory.Exists(newDirectoryFullName)) Directory.CreateDirectory(newDirectoryFullName);
            var oldFileAry = oldDirectory.GetFiles();
            foreach (var aFile in oldFileAry)
                File.Copy(aFile.FullName, newDirectoryFullName + @"\" + aFile.Name, true);

            var oldDirectoryAry = oldDirectory.GetDirectories();
            foreach (var aOldDirectory in oldDirectoryAry)
            {
                var aNewDirectory = new DirectoryInfo(newDirectoryFullName);
                CopyDirectoryAndSubDirectoriesFiles(aOldDirectory, aNewDirectory);
            }
        }
        public static void MoveDirectoryTo(string source, string destination)
        {
            try
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);
                var directoryInfo = new DirectoryInfo(source);
                var files = directoryInfo.GetFiles();
                foreach (var file in files)
                {
                    //if (File.Exists(Path.Combine(directoryTarget, file.Name)))
                    //{
                    //    if (File.Exists(Path.Combine(directoryTarget, file.Name + ".bak")))
                    //    {
                    //        File.Delete(Path.Combine(directoryTarget, file.Name + ".bak"));
                    //    }
                    //    File.Move(Path.Combine(directoryTarget, file.Name), Path.Combine(directoryTarget, file.Name + ".bak"));
                    //}
                    file.MoveTo(Path.Combine(destination, file.Name));

                }
                var directoryInfoArray = directoryInfo.GetDirectories();
                foreach (var dir in directoryInfoArray)
                    MoveDirectoryTo(Path.Combine(source, dir.Name), Path.Combine(destination, dir.Name));
            }
            catch (Exception)
            {
                throw new Exception($"Failed to move directory from [{source}] to [{destination}].");
            }
        }
        public static void CopyDirectoryAndSubDirectoriesFiles(string source, string destination)
        {
            try
            {
                var oldDirectory = new DirectoryInfo(source);
                var newDirectory = new DirectoryInfo(destination);
                CopyDirectoryAndSubDirectoriesFiles(oldDirectory, newDirectory);
            }
            catch (Exception)
            {
                throw new Exception(
                    $"Failed to CopyDirectoryAndSubDirectoriesFiles from [{source}] to [{destination}].");
            }
        }
        public static void CopyDirectoryTo(string source, string destination)
        {
            try
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);
                var directoryInfo = new DirectoryInfo(source);
                var files = directoryInfo.GetFiles();
                foreach (var file in files)
                    file.CopyTo(Path.Combine(destination, file.Name));
                var directoryInfoArray = directoryInfo.GetDirectories();
                foreach (var dir in directoryInfoArray)
                    CopyDirectoryTo(Path.Combine(source, dir.Name), Path.Combine(destination, dir.Name));
            }
            catch (Exception)
            {
                throw new Exception($"Failed to copy directory from [{source}] to [{destination}].");
            }

        }
       
        public static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            var listFiles = new List<FileInfo>();
            var directory = new DirectoryInfo(strDirectory);
            var directoryArray = directory.GetDirectories();
            var fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (var directoryInfo in directoryArray)
            {
                var directoryA = new DirectoryInfo(directoryInfo.FullName);
                //var directoryArrayA = directoryA.GetDirectories();
                var fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                GetAllFilesInDirectory(directoryInfo.FullName);
            }
            return listFiles;
        }
    }
}
