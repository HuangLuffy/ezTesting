using System;
using System.Collections.Generic;
using System.IO;

namespace CommonLib.Util.IO
{
    public static class UtilFile
    {
        public static List<string> GetListByFuzzyLine(string fileFullPath, string wildcard)
        {
            var lineList = new List<string>();
            try
            {
                using (var streamReader = new StreamReader(fileFullPath))
                {

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.ToLower().Contains(wildcard.ToLower()))
                        {
                            lineList.Add(line);
                        }
                    }
                    return lineList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<string> GetListByLine(string fileFullPath)
        {
            var lineList = new List<string>();
            try
            {
                using (var streamReader = new StreamReader(fileFullPath))
                {
                    
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineList.Add(line);
                    }
                    return lineList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void WriteFileWhenNotExists(string fileFullPath, string content = "", bool append = true)
        {
            if (!File.Exists(fileFullPath))
            {
                WriteFile(fileFullPath, content, append);
            } 
        }
        public static bool Exists(string fileFullPath)
        {
            return File.Exists(fileFullPath);
        }
        public static void WriteFile(string fileFullPath, string content = "", bool append = true)
        {
            UtilFolder.CreateDirectory(Path.GetDirectoryName(fileFullPath));
            using (var streamWriter = new StreamWriter(fileFullPath, append))
            {
                streamWriter.WriteLine(content);
            }
        }
        public static List<string> ReadFileByLine(string fileFullPath)
        {
            //1、StreamReader只用来读字符串。
            //2、StreamReader可以用来读任何Stream，包括FileStream也包括NetworkStream，MemoryStream等等。
            //3、FileStream用来操作文件流。可读写，可字符串，也可二进制。
            //重要的区别是，StreamReader是读者，用来读任何输入流；FileStream是文件流，可以被读，被写
            var resultList = new List<string>();
            FileStream aFile = null;
            StreamReader sr = null;
            try
            {
                aFile = new FileStream(fileFullPath, FileMode.Open);
                sr = new StreamReader(aFile);
                var strLine = sr.ReadLine();
                while (strLine != null)
                {
                    resultList.Add(strLine);
                    strLine = sr.ReadLine();
                }
                return resultList;
            }
            catch (IOException)
            {
                return resultList;
            }
            finally
            {
                try
                {
                    sr?.Close();
                    aFile?.Close();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}

