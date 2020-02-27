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
            string exportPath = args[1];

            if (!File.Exists(imageFilePath))
            {
                Console.WriteLine();
                string errorMessage = string.Format("File {0} does not exist", imageFilePath);
                Console.WriteLine(errorMessage);
                return;
            }

            if (!Directory.Exists(exportPath))
            {
                Console.WriteLine();
                string errorMessage = string.Format("Directory {0} does not exist", exportPath);
                Console.WriteLine(errorMessage);
                return;
            }

            List<FloodFilledRegion> regions = new List<FloodFilledRegion>();
            try
            {
                SplitInPanels(imageFilePath, exportPath, regions);
            }
            catch
            {
                Console.WriteLine();
                string errorMessage = string.Format("Something went wrong, cannot process image {0}", imageFilePath);
                Console.WriteLine(errorMessage);
                return;
            }

            Console.WriteLine();
            string message = string.Format("Image {0} splitted in {1} panels which were written to {2}", imageFilePath, regions.Count, exportPath);
            Console.WriteLine(message);
        }

        private static void SplitInPanels(string imageFilePath, string exportPath, List<FloodFilledRegion> regions)
        {
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
                regions = FloodFilledRegion.SortRegions(regions);
                Util.CutandWriteToFile(regions, comicPage, exportPath);
            }
        }
        

    }

}
