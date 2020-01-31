using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace ComicPanelsSplitter
{
    public class Coordinate
    {

        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

     
        public bool IsWhite(Bitmap bitmap)
        {
            return IsWhite(bitmap.GetPixel(X, Y));

        }

        private static bool IsWhite(Color color)
        {
            if ((color.A > 245) && (color.R > 245) && (color.G > 245) && (color.B > 245))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Coordinate other = (Coordinate)obj;
            if ((other.X == X) && (other.Y == Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public override int GetHashCode()
        {
            return new Point(X, Y).GetHashCode();
        }



    }
}
