using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class UtilFile
    {
        public static List<string> GetListByLine(string fileFullPath)
        {
            List<string> lineList = new List<string>();
            try
            {
                using (StreamReader _StreamReader = new StreamReader(fileFullPath))
                {
                    
                    string line;
                    while ((line = _StreamReader.ReadLine()) != null)
                    {
                        lineList.Add(line);
                    }
                    return lineList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException(string.Format("Failed to read file [{0}].", fileFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        public static void WriteFileWhenNotExists(string fileFullPath, string content = "", Boolean append = true)
        {
            if (!File.Exists(fileFullPath))
            {
                WriteFile(fileFullPath, content, append);
            } 
        }
        public static void WriteFile(string fileFullPath, string content = "", Boolean append = true)
        {
            try
            {
                UtilFolder.CreateDirectory(Path.GetDirectoryName(fileFullPath));
                using (StreamWriter _StreamWriter = new StreamWriter(fileFullPath, append))
                {
                    _StreamWriter.Write(content);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException(string.Format("Failed to write file [{0}].", fileFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public static List<string> ReadFileByLine(string fileFullPath)
        {
            //1、StreamReader只用来读字符串。
            //2、StreamReader可以用来读任何Stream，包括FileStream也包括NetworkStream，MemoryStream等等。
            //3、FileStream用来操作文件流。可读写，可字符串，也可二进制。
            //重要的区别是，StreamReader是读者，用来读任何输入流；FileStream是文件流，可以被读，被写
            List<string> resultList = new List<string>();
            string strLine = "";
            FileStream aFile = null;
            StreamReader sr = null;
            try
            {
                aFile = new FileStream(fileFullPath, FileMode.Open);
                sr = new StreamReader(aFile);
                strLine = sr.ReadLine();
                while (strLine != null)
                {
                    resultList.Add(strLine);
                    strLine = sr.ReadLine();
                }
                return resultList;
            }
            catch (IOException ex)
            {
                Logger.LogThrowMessage(string.Format("Failed to read file [{0}].", fileFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    sr.Close();
                    aFile.Close();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}

