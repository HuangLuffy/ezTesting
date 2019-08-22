using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CommonLib.Util
{
    public class UtilCapturer
    {
        public enum ImageType
        {
            PNG = 1,
            BMP = 2,
            JPG = 3
        }
        public static void Capture(string pathSave, ImageType ImageType = ImageType.PNG)
        {
            ImageFormat imgf = ImageFormat.Png;
            if (ImageType == ImageType.BMP)
            {
                imgf = ImageFormat.Bmp;
                pathSave += ".bmp";
            }
            else if(ImageType == ImageType.JPG)
            {
                imgf = ImageFormat.Jpeg;
                pathSave += ".jpg";
            }
            else
            {
                pathSave += ".png";
            }
            int iWidth = Screen.PrimaryScreen.Bounds.Width;
            int iHeight = Screen.PrimaryScreen.Bounds.Height;
            Image img = new Bitmap(iWidth, iHeight);
            Graphics gc = Graphics.FromImage(img);
            gc.CompositingQuality = CompositingQuality.HighSpeed;
            gc.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            img.Save(pathSave, imgf);
            //Guid.NewGuid().ToString()
            gc.Dispose();
        }
    }
}
