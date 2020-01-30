using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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

            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(51, 51);

            bool iswit1 = Util.IsWhite(comicPage, coord1);
            bool iswit50 = Util.IsWhite(comicPage, coord2);


            panel1.Save(outputfilePath, ImageFormat.Jpeg);

            Console.WriteLine("Hello World!");
        }


       
    }

}
