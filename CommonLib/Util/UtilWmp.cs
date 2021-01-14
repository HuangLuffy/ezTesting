using System.Diagnostics;

namespace CommonLib.Util
{
    public class UtilWmp
    {
        public static Process StartWmpWithMedias(params string[] medias)
        {
            var para = "";
            for (int i = 0; i < medias.Length; i++)
            {
                para += medias[0];
                if (medias.Length - 1 > i)
                {
                    para += " / ";
                }
            }
            foreach (var m in medias)
            {
                para += m;
            }
            return UtilProcess.StartProcessReturn("wmplayer.exe", $"{para}");
        }
        public void As()
        {
            UtilProcess.StartProcess("wmplayer.exe", @"C:\Users\Administrator\Desktop\music\2\1.mp3 / C:\Users\Administrator\Desktop\music\2\2.mp3  / C:\Users\Administrator\Desktop\music\2\3.mp3");

            UtilProcess.StartProcess("mswindowsmusic:", "start");
            //explorer.exe shell:AppsFolder\Microsoft.ZuneMusic_8wekyb3d8bbwe!Microsoft.ZuneMusic
            //"start mswindowsmusic: "C:\Users\Administrator\Desktop\music\2\1.mp3""
            //cmd /c start "explorer.exe shell:C:\Program Files\WindowsApps\Microsoft.ZuneMusic_3.6.25021.0_x64__8wekyb3d8bbwe!Microsoft.ZuneMusic" "C:\Users\Administrator\Desktop\music\2\1.mp3"
            UtilProcess.StartProcess("start mswindowsmusic:", @"C:\Users\Administrator\Desktop\music\2\1.mp3");
        }
    }
}
