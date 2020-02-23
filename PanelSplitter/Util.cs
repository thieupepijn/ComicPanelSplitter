using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace PanelSplitter
{
    public class Util
    {


        public static string GetOutputFilePath(string outputDirectory, int counter)
        {
            string filename = string.Format("panel{0}.jpg", counter);
            return Path.Combine(outputDirectory, filename);
        }


        public static Coordinate[,] PixelsToCoordinates(Bitmap image)
        {
            Coordinate[,] coords = new Coordinate[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    {
                        Coordinate coordinate = new Coordinate(x, y, image.GetPixel(x, y));
                        coords[x, y] = coordinate;
                    }
                }
            }
            return coords;
        }

        public static Bitmap Resize(Bitmap bitmap, int factor)
        {
            int width = bitmap.Width / factor;
            int height = bitmap.Height / factor;
            return new Bitmap(bitmap, new Size(width, height));
        }

        public static void CutandWriteToFile(List<FloodFilledRegion> regions, Bitmap bitmap, string exportpath)
        {
            int counter = 1;
            foreach (FloodFilledRegion region in regions)
            {
                string outputFilePath = Util.GetOutputFilePath(exportpath, counter);
                CutAndWriteToFile(bitmap, region.Left,  region.Top, region.Right, region.Down, outputFilePath);
                counter++;
            }
        }

        public static void CutAndWriteToFile(Bitmap bitmap, int left, int top, int right, int bottom, string outputfilePath)
        {
            int width = right - left;
            int height = bottom - top;
            Rectangle region = new Rectangle(left, top, width, height);
            Bitmap panel = bitmap.Clone(region, PixelFormat.DontCare);
            panel.Save(outputfilePath, ImageFormat.Jpeg);
        }


    }
}
