using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class UtilFolder
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
                throw new Exception(string.Format("Failed to CreateDirectory [{0}].", folderPath));
            }
        }
        private static void DeleteFilesAndFoldersRecursively(string target_dir)
        {
            foreach (string file in Directory.GetFiles(target_dir))
            {
                File.Delete(file);
            }
            foreach (string subDir in Directory.GetDirectories(target_dir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }
            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) was not enough.
            Directory.Delete(target_dir);
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
                Logger.LogThrowException(string.Format("Failed to recreate folder [{0}].", folderPath));
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
                throw new Exception(string.Format("Failed to get files in folder [{0}].", folderPath));
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
                throw new Exception(string.Format("Failed to get subfolders [{0}].", folderPath));
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
                throw new Exception(string.Format("Failed to move folder from [{0}] to [{1}].", source, destination));
            }
        }
        private static void CopyDirectoryAndSubDirectoriesFiles(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
        {
            string NewDirectoryFullName = NewDirectory.FullName ;
            //string NewDirectoryFullName = NewDirectory.FullName + @"\" + OldDirectory.Name;
            if (!Directory.Exists(NewDirectoryFullName)) Directory.CreateDirectory(NewDirectoryFullName);
            FileInfo[] OldFileAry = OldDirectory.GetFiles();
            foreach (FileInfo aFile in OldFileAry)
                File.Copy(aFile.FullName, NewDirectoryFullName + @"\" + aFile.Name, true);

            DirectoryInfo[] OldDirectoryAry = OldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in OldDirectoryAry)
            {
                DirectoryInfo aNewDirectory = new DirectoryInfo(NewDirectoryFullName);
                CopyDirectoryAndSubDirectoriesFiles(aOldDirectory, aNewDirectory);
            }
        }
        public static void MoveDirectoryTo(string source, string destination)
        {
            try
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);
                DirectoryInfo directoryInfo = new DirectoryInfo(source);
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
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
                DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dir in directoryInfoArray)
                    MoveDirectoryTo(Path.Combine(source, dir.Name), Path.Combine(destination, dir.Name));
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Failed to move directory from [{0}] to [{1}].", source, destination));
            }
        }
        public static void CopyDirectoryAndSubDirectoriesFiles(string source, string destination)
        {
            try
            {
                DirectoryInfo OldDirectory = new DirectoryInfo(source);
                DirectoryInfo NewDirectory = new DirectoryInfo(destination);
                CopyDirectoryAndSubDirectoriesFiles(OldDirectory, NewDirectory);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Failed to CopyDirectoryAndSubDirectoriesFiles from [{0}] to [{1}].", source, destination));
            }
        }
        public static void CopyDirectoryTo(string source, string destination)
        {
            try
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);
                DirectoryInfo directoryInfo = new DirectoryInfo(source);
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                    file.CopyTo(Path.Combine(destination, file.Name));
                DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dir in directoryInfoArray)
                    CopyDirectoryTo(Path.Combine(source, dir.Name), Path.Combine(destination, dir.Name));
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Failed to copy directory from [{0}] to [{1}].", source, destination));
            }

        }
       
        public static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            try
            {
                List<FileInfo> listFiles = new List<FileInfo>();
                DirectoryInfo directory = new DirectoryInfo(strDirectory);
                DirectoryInfo[] directoryArray = directory.GetDirectories();
                FileInfo[] fileInfoArray = directory.GetFiles();
                if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
                foreach (DirectoryInfo _directoryInfo in directoryArray)
                {
                    DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                    DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                    FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                    if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                    GetAllFilesInDirectory(_directoryInfo.FullName);
                }
                return listFiles;
            }
            catch (Exception)
            {
                throw;
            }
        }    
        
        
    }
}
