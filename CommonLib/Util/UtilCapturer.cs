using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CommonLib.Util
{
    public static class UtilCapturer
    {
        public enum ImageType
        {
            PNG = 1,
            BMP = 2,
            JPG = 3
        }
        public static void Capture(string pathSave, ImageType imageType = ImageType.PNG)
        {
            var imgf = ImageFormat.Png;
            if (imageType == ImageType.BMP)
            {
                imgf = ImageFormat.Bmp;
                pathSave += ".bmp";
            }
            else if(imageType == ImageType.JPG)
            {
                imgf = ImageFormat.Jpeg;
                pathSave += ".jpg";
            }
            else
            {
                pathSave += ".png";
            }
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            Image img = new Bitmap(iWidth, iHeight);
            var gc = Graphics.FromImage(img);
            gc.CompositingQuality = CompositingQuality.HighSpeed;
            gc.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            img.Save(pathSave, imgf); // pathSave: full path will cause error?
            //Guid.NewGuid().ToString()
            gc.Dispose();
            img.Dispose();
        }
    }
}
