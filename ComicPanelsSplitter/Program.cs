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

            Rectangle region = new Rectangle(0, 0, 890, 250);
            Bitmap panel1 = comicPage.Clone(region, System.Drawing.Imaging.PixelFormat.DontCare);

            string outputfilePath = Path.Join(exportpath, "panel1.jpg");

            List<Coordinate> nonPanelCoordinates = Util.NonPanelCoordinates(comicPage);

            panel1.Save(outputfilePath, ImageFormat.Jpeg);

            foreach(Coordinate coordinate in nonPanelCoordinates)
            {
                string message = string.Format("{0} , [1}", coordinate.X, coordinate.Y);
                Console.WriteLine(message);
            }

           
        }


       
    }

}
