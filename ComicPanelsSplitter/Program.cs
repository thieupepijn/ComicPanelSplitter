using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using PanelSplitter;
using System.Text;
using System.Linq;

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

            if ((!File.Exists(imageFilePath)) && (!Directory.Exists(imageFilePath)))
            {
                WriteMessageToConsole(string.Format("{0} is neither an existing file nor directory", imageFilePath));
                return;
            }

            if (!Directory.Exists(exportPath))
            {
                WriteMessageToConsole(string.Format("Directory {0} does not exist", exportPath));
                return;
            }

            int numberOfPanels = 0;
            try
            {
                numberOfPanels = SplitInPanels(imageFilePath, exportPath);
            }
            catch(Exception ex)
            {
#if DEBUG
                WriteMessageToConsole(ex.Message);
#else
                WriteMessageToConsole(string.Format("Something went wrong, cannot process image {0}", imageFilePath));
#endif

                return;
            }
           // WriteMessageToConsole(string.Format("Image {0} splitted in {1} panels which were written to {2}", imageFilePath, numberOfPanels, exportPath)); 
        }

        private static int SplitInPanels(string path, string exportPath)
        {
            if (File.Exists(path))
            {
                return SplitInPanels(new FileInfo(path), exportPath);
            }
            else if (Directory.Exists(path))
            {
                return SplitInPanels(new DirectoryInfo(path), exportPath);
            }
            else
            {
                return 0;
            }
        }




        private static int SplitInPanels(DirectoryInfo directoryInfo, string exportPath)
        {
            int numberOfPanels = 0;
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();
            foreach(FileInfo fileInfo in fileInfos)
            {
                numberOfPanels += SplitInPanels(fileInfo, exportPath);
            }
            return numberOfPanels;
        }

        private static int SplitInPanels(FileInfo fileInfo, string exportPath)
        {
            List<FloodFilledRegion> regions = new List<FloodFilledRegion>();
            using (Bitmap comicPage = (Bitmap)Image.FromFile(fileInfo.FullName))
            {
                Coordinate[,] coords = Util.PixelsToCoordinates(comicPage);
                while (Coordinate.GetSuitable(coords).Count > 0)
                {

                    FloodFilledRegion region = new FloodFilledRegion(coords);
                    region.ResetFloodedCoords(coords, region.Left, region.Top, region.Right, region.Down);
                    regions.Add(region);
                }

                FloodFilledRegion.RemoveSmallRegions(regions);
                int maxX = coords.GetLength(0);
                int maxY = coords.GetLength(1);
                regions = FloodFilledRegion.SortRegions(regions, maxX, maxY);
                Util.CutandWriteToFile(regions, comicPage, exportPath, fileInfo.Name);
            }
            return regions.Count;
        }

        private static void ShowUsage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("ComicPanelSplitter, small tool that splits a comic-page into it's panels");
            builder.AppendLine();
            builder.AppendLine("Usage: ComicPanelSplitter.exe <ComicImageFile> <ExportPath>");
            builder.AppendLine();
            builder.AppendLine("or: ComicPanelSplitter.exe <ComicImageFilesDirectory> <ExportPath>");
            WriteMessageToConsole(builder.ToString());
        }

        private static void WriteMessageToConsole(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }
        

    }

}
