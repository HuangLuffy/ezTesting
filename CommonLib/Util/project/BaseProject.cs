using System;
using System.IO;
using System.Windows.Forms;

namespace CommonLib.Util.project
{
    public class ProjectPath
    {
        public static String getProjectFullPath()
        {
            return Application.StartupPath;
        }
        public static String getProjectParentPath()
        {
            return new DirectoryInfo(getProjectFullPath()).Parent.ToString();
        }
        //Process.GetCurrentProcess().MainModule.FileName;    //可获得当前执行的exe的文件名。  
        //Environment.CurrentDirectory;          //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
        //备注 按照定义，如果该进程在本地或网络驱动器的根目录中启动，则此属性的值为驱动器名称后跟一个尾部反斜杠（如“C:/”）。如果该进程在子目录中启动，则此属性的值为不带尾部反斜杠的驱动器和子目录路径（如“C:/mySubDirectory”）。
        //Directory.GetCurrentDirectory();                      //获取应用程序的当前工作目录。
        //AppDomain.CurrentDomain.BaseDirectory;    //获取基目录，它由程序集冲突解决程序用来探测程序集。
        //Application.StartupPath;                                   //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。（如：D:/project/集团客户短信服务端/bin/Debug）
        //Application.ExecutablePath;         //获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
        //AppDomain.CurrentDomain.SetupInformation.ApplicationBase;  //获取或设置包含该应用程序的目录的名称。 
    }
}
