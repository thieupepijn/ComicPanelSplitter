using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ComicPanelsSplitter
{
    public class Util
    {


        public static List<Coordinate> NonPanelCoordinates(Bitmap bitmap)
        {
            Coordinate startCoordinate = new Coordinate(0, 0);
            List<Coordinate> coordinates = new List<Coordinate>();
            NonPanelCoordinates(bitmap, startCoordinate, ref coordinates);
            return coordinates;
        }

        public static void NonPanelCoordinates(Bitmap bitmap, Coordinate startCoordinate, ref List<Coordinate> coordinates)
        {
            if ((startCoordinate.IsWhite(bitmap)) && (!coordinates.Contains(startCoordinate)))
            {
                coordinates.Add(startCoordinate);
                foreach (Coordinate whiteNeighbour in startCoordinate.WhiteNeighbours(bitmap))
                {
                        NonPanelCoordinates(bitmap, whiteNeighbour, ref coordinates);                    
                }
            }
        }
    
    
    }
}
