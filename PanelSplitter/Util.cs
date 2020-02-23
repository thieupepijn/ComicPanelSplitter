using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace PanelSplitter
{
    public class Util
    {


        public static string GetOutputFilePath(string inputFilePath, string outputDirectory)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath) + "FloodFilled" + Path.GetExtension(inputFilePath);
            return Path.Combine(outputDirectory, outputFileName);
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

    }
}
