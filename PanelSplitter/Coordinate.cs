using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace PanelSplitter
{
    public class Coordinate
    {

        public int X { get; private set; }
        public int Y { get; private set; }
        public Enumerations.FloodFillStatus FloodFillStatus { get; set; }

        public Coordinate(int x, int y, Color color)
        {
            X = x;
            Y = y;

            //pixel is not white or almost white
            if ((color.R < 100) || (color.G < 100) || (color.B < 100))
            {
                FloodFillStatus = Enumerations.FloodFillStatus.Suitable;
            }
            else
            {
                FloodFillStatus = Enumerations.FloodFillStatus.NotSuitable;
            }
        }

        public Coordinate Left(Coordinate[,] coords)
        {
            if (X > 0)
            {
                return coords[X - 1, Y];
            }
            else
            {
                return null;
            }
        }


        public Coordinate Up(Coordinate[,] coords)
        {
            if (Y > 0)
            {
                return coords[X, Y - 1];
            }
            else
            {
                return null;
            }
        }

        public Coordinate Right(Coordinate[,] coords)
        {
            if (X < coords.GetLength(0) - 1)
            {
                return coords[X + 1, Y];
            }
            else
            {
                return null;
            }
        }

        public Coordinate Down(Coordinate[,] coords)
        {
            if (Y < coords.GetLength(1) - 1)
            {
                return coords[X, Y + 1];
            }
            else
            {
                return null;
            }
        }



        public List<Coordinate> Neigbours(Coordinate[,] coords)
        {
            List<Coordinate> neighbours = new List<Coordinate>();
            Coordinate left = Left(coords);
            Coordinate up = Up(coords);
            Coordinate right = Right(coords);
            Coordinate down = Down(coords);

            if (left != null)
            {
                neighbours.Add(left);
            }

            if (up != null)
            {
                neighbours.Add(up);
            }

            if (right != null)
            {
                neighbours.Add(right);
            }

            if (down != null)
            {
                neighbours.Add(down);
            }

            return neighbours;
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

        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            if ((object.ReferenceEquals(coordinate1, null)) && (object.ReferenceEquals(coordinate2, null)))
            {
                return true;
            }
            else
            {
                return coordinate1.Equals(coordinate2);
            }
        }

        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return !coordinate1.Equals(coordinate2);
        }



        public override int GetHashCode()
        {
            return new Point(X, Y).GetHashCode();
        }


        public static Coordinate GetStartCoordinate(Coordinate[,] coords)
        {
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    {
                        Coordinate coordinate = coords[x, y];
                        if (coordinate.FloodFillStatus == Enumerations.FloodFillStatus.Suitable)
                        {
                            return coordinate;
                        }
                    }
                }
            }
            return null;
        }


        public static List<Coordinate> GetSuitable(Coordinate[,] coords)
        {
            List<Coordinate> suitable = new List<Coordinate>();
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    if (coords[x, y].FloodFillStatus == Enumerations.FloodFillStatus.Suitable)
                    {
                        suitable.Add(coords[x, y]);
                    }
                }
            }
            return suitable;
        }

    }
}
