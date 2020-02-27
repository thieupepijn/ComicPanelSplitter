using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using PanelSplitter;
using System.Text;

namespace ComicPanelsSplitter
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                ShowUsage();
                return;
            }

            string imageFilePath = args[0];
            string exportPath = args[1];

            if (!File.Exists(imageFilePath))
            {
                WriteMessageToConsole(string.Format("File {0} does not exist", imageFilePath));
                return;
            }

            if (!Directory.Exists(exportPath))
            {
                WriteMessageToConsole(string.Format("Directory {0} does not exist", exportPath));
                return;
            }

            List<FloodFilledRegion> regions = new List<FloodFilledRegion>();
            try
            {
                SplitInPanels(imageFilePath, exportPath, regions);
            }
            catch(Exception ex)
            {
                WriteMessageToConsole(string.Format("Something went wrong, cannot process image {0}", imageFilePath));
                return;
            }
            WriteMessageToConsole(string.Format("Image {0} splitted in {1} panels which were written to {2}", imageFilePath, regions.Count, exportPath)); 
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

        private static void ShowUsage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("ComicPanelSplitter, small tool that splits a comic-page into it's panels");
            builder.AppendLine();
            builder.AppendLine("Usage: ComicPanelSplitter.exe <ComicImageFile> <ExportPath>");
            WriteMessageToConsole(builder.ToString());
        }

        private static void WriteMessageToConsole(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }
        

    }

}
