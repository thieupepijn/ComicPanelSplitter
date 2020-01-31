using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

namespace ComicPanelsSplitter
{
    class Program
    {
        static void Main(string[] args)
        {


            string imageFilePath = args[0];
            string exportpath = args[1];

            Bitmap comicPage = (Bitmap)Image.FromFile(imageFilePath);
            Console.WriteLine("Width: " + comicPage.Width.ToString());
            Console.WriteLine("Height: " + comicPage.Height.ToString());

           
            Coordinate topleft = Util.FindTopLeftNonWhitePixel(comicPage);

           
           if (topleft != null)
            {
                string message = string.Format("topleft; {0} , {1}", topleft.X, topleft.Y);
                Console.WriteLine(message);
            }


            string outputfilePath = Path.Join(exportpath, "panel1.jpg");
            //CutAndWriteToFile(comicPage, 50, 50, outputfilePath);

            Graphics graafix = Graphics.FromImage(comicPage);
            graafix.DrawEllipse(new Pen(Brushes.Red, 5), topleft.X - 5, topleft.Y - 5, 10, 10);
            comicPage.Save(outputfilePath, ImageFormat.Jpeg);

        }


        private static void CutAndWriteToFile(Bitmap bitmap, int width, int height, string outputfilePath)
        {
             Rectangle region = new Rectangle(0, 0, width, height);
             Bitmap panel = bitmap.Clone(region, PixelFormat.DontCare);

           
            panel.Save(outputfilePath, ImageFormat.Jpeg);

        }

    }

}
