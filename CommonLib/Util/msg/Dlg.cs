using System.Windows.Forms;

namespace CommonLib.Util
{
    public class Dlg : Msg
    {

        private struct CaptionName
        {
            public static string captionError = "Error";
            public static string captionNull = "";
            public static string captionInfo = "Info";
            public static string captionWarning = "Warning";
            public static string captionQuestion = "Question";
        }

        public static void ShowInfoDialog(string content)
        {
            ShowDialog(content, CaptionName.captionInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowErrorDialog(string content)
        {
            ShowDialog(content, CaptionName.captionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowWarningDialog(string content)
        {
            ShowDialog(content, CaptionName.captionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowQuestionDialog(string content)
        {
            ShowDialog(content, CaptionName.captionQuestion, MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        public static void ShowDialog(string content, string caption = "", MessageBoxButtons mbb = MessageBoxButtons.OK, MessageBoxIcon mbi = MessageBoxIcon.Error)
        {
            MessageBox.Show(content, caption, mbb, mbi);
        }
    }
}
