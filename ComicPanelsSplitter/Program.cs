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

            int resizeFactor = 1;

            if (args.Length > 2)
            {
                resizeFactor = Convert.ToInt32(args[2]);
            }


            List<FloodFilledRegion> regions = new List<FloodFilledRegion>();
            using (Bitmap comicPage = (Bitmap)Image.FromFile(imageFilePath))
            {
                
                using (Bitmap comicPageResized = Util.Resize(comicPage, resizeFactor))
                {
                    Coordinate[,] coords = Util.PixelsToCoordinates(comicPageResized);

                    while (Coordinate.GetSuitable(coords).Count > 0)
                    {

                        FloodFilledRegion region = new FloodFilledRegion(coords);
                        region.ResetFloodedCoords(coords, region.Left, region.Top, region.Right, region.Down);
                        regions.Add(region);
                    }
                }

                FloodFilledRegion.RemoveSmallRegions(regions);
                FloodFilledRegion.MultiplyBorders(regions, resizeFactor);
                Util.CutandWriteToFile(regions, comicPage, exportpath);
            }
        }


        
        

    }

}
