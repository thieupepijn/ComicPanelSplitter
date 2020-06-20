using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using PanelSplitter;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

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
                WriteMessageToConsole(string.Format("Exportdirectory {0} does not exist", exportPath));
                return;
            }

            int numberOfPanels = SplitInPanels(imageFilePath, exportPath);         
        }        

        private static int SplitInPanels(string path, string exportPath)
        {
            if (File.Exists(path))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(path);
                    int numberOfPanels = SplitInPanels(fileInfo, exportPath);
                    Console.WriteLine(string.Format("Split {0} into {1} panels", fileInfo.Name, numberOfPanels));
                    return numberOfPanels;
                }
                catch
                {
                    string message = string.Format("Couldn't process file {0}", new FileInfo(path).Name);
                    Console.WriteLine(message);
                    return 0;
                }
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
            int counter = 0;
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();
            Parallel.ForEach(fileInfos, f =>
            {
                {
                    try
                    {
                        numberOfPanels += SplitInPanels(f, exportPath);
                        counter++;
                        string message = string.Format("processed {0} of {1} files", counter, fileInfos.Count);
                        Console.WriteLine(message);

                    }
                    catch
                    {
                       // string message = string.Format("Couldn't process file {0}", f.Name);
                       // Console.WriteLine(message);
                        //continue;
                    }
                }
            });
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

                    if (regions.Count > Constants.MAXPANELSPERPAGE)
                    {
                      //  Console.WriteLine(string.Format("{0} has too many panels", fileInfo.Name));
                        return 0;
                    }
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
