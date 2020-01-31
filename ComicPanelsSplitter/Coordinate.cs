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

        public List<Coordinate> Neigbours(int maxX, int maxY)
        {
            List<Coordinate> neighbours = new List<Coordinate>();

            Coordinate neighbourLeft = NeighbourLeft();
            Coordinate neighbourUp = NeighbourUp();
            Coordinate neighbourRight = NeighbourRight(maxX);
            Coordinate neighbourDown = NeighbourDown(maxY);

            if (neighbourLeft != null)
            {
                neighbours.Add(neighbourLeft);
            }

            if (neighbourUp != null)
            {
                neighbours.Add(neighbourUp);
            }

            if (neighbourRight != null)
            {
                neighbours.Add(neighbourRight);
            }

            if (neighbourDown != null)
            {
                neighbours.Add(neighbourDown);
            }
            return neighbours;
        }

        public List<Coordinate> WhiteNeighbours(Bitmap bitmap)
        {
            return Neigbours(bitmap.Width, bitmap.Height).Where(n => n.IsWhite(bitmap)).ToList();
        }

        public bool IsWhite(Bitmap bitmap)
        {
            return IsWhite(bitmap.GetPixel(X, Y));

        }

        private static bool IsWhite(Color color)
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


        private Coordinate NeighbourLeft()
        {
            if (X > 0)
            {
                return new Coordinate(X - 1, Y);
            }
            else
            {
                return null;
            }
        }

        private Coordinate NeighbourUp()
        {
            if (Y > 0)
            {
                return new Coordinate(X, Y-1);
            }
            else
            {
                return null;
            }
        }

        private Coordinate NeighbourRight(int maxX)
        {
            if (X < maxX-1)
            {
                return new Coordinate(X + 1, Y);
            }
            else
            {
                return null;
            }
        }

        private Coordinate NeighbourDown(int maxY)
        {
            if (Y < maxY-1)
            {
                return new Coordinate(X, Y + 1);
            }
            else
            {
                return null;
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
