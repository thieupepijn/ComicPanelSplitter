using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using PanelSplitter;

namespace ComicPanelsSplitter
{
    class Program
    {
        static void Main(string[] args)
        {


            string imageFilePath = args[0];
            string exportpath = args[1];

            List<FloodFilledRegion> regions = new List<FloodFilledRegion>();
            using (Bitmap comicPage = (Bitmap)Image.FromFile(imageFilePath))
            {
                Coordinate[,] coords = Util.PixelsToCoordinates(comicPage);

                while (Coordinate.GetSuitable(coords).Count > 0)
                {

                    FloodFilledRegion region = new FloodFilledRegion(coords);
                    region.ResetFloodedCoords(coords, region.Left, region.Top, region.Right, region.Down);
                    regions.Add(region);
                }

                FloodFilledRegion.RemoveSmallRegions(regions);

                int counter = 1;
                foreach (FloodFilledRegion region in regions)
                {
                    string outputfilename = string.Format("panel{0}.jpg", counter);
                    string outputfilePath = Path.Join(exportpath, outputfilename);
                    CutAndWriteToFile(comicPage, region.Left, region.Top, region.Right, region.Down, outputfilePath);
                    counter++;
                }
            }

        }


        private static void CutAndWriteToFile(Bitmap bitmap, int left, int top, int right, int bottom, string outputfilePath)
        {
            int width = right - left;
            int height = bottom - top;
             Rectangle region = new Rectangle(left, top, width, height);
             Bitmap panel = bitmap.Clone(region, PixelFormat.DontCare); 
            panel.Save(outputfilePath, ImageFormat.Jpeg);

        }

    }

}
