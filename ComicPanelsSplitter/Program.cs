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
                regions = FloodFilledRegion.SortRegions(regions);
                Util.CutandWriteToFile(regions, comicPage, exportpath);
            }
        }
        

    }

}
