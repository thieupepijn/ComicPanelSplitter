using System;
using System.Collections.Generic;
using System.Text;

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

            if(neighbourLeft != null)
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

        private Coordinate NeighbourLeft()
        {
            if (X == 0)
            {
                return null;
            }
            else
            {
                return new Coordinate(X-1, Y);
            }
        }

        private Coordinate NeighbourUp()
        {
            if (Y == 0)
            {
                return null;
            }
            else
            {
                return new Coordinate(X, Y-1);
            }
        }

        private Coordinate NeighbourRight(int maxX)
        {
            if (X == maxX)
            {
                return null;
            }
            else
            {
                return new Coordinate(X + 1, Y);
            }
        }

        private Coordinate NeighbourDown(int maxY)
        {
            if (Y == maxY)
            {
                return null;
            }
            else
            {
                return new Coordinate(X, Y+1);
            }
        }

    }
}
