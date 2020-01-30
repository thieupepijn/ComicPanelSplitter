using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ComicPanelsSplitter
{
    public class Util
    {

        public static bool IsWhite(Bitmap bitmap, Coordinate coordinate)
        {
            return IsWhite(bitmap.GetPixel(coordinate.X, coordinate.Y));

        }

        public static bool IsWhite(Color color)
        {
            if ((color.A == 255) && (color.R == 255) && (color.G == 255) && (color.B == 255))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static List<Coordinate> NonPanelCoordinates(Bitmap bitmap)
        {
            Coordinate startCoordinate = new Coordinate(0, 0);

            if (!IsWhite(bitmap, startCoordinate))
            {
                return new List<Coordinate>();
            }

            List<Coordinate> coordinates = new List<Coordinate>();
            coordinates.Add(startCoordinate);

            return coordinates;
        }


        


       

    }
}
