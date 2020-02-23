using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace PanelSplitter
{
    public class FloodFilledRegion
    {

        public List<Coordinate> Flooded { get; private set; }
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Right { get; private set; }
        public int Down { get; private set; }


        public FloodFilledRegion(Coordinate[,] coords)
        {
            Coordinate startCoordinate = Coordinate.GetStartCoordinate(coords);
            Stack<Coordinate> workStack = new Stack<Coordinate>();
            workStack.Push(startCoordinate);
            FloodFill(coords, workStack);
            Flooded = GetFlooded(coords);
            GetBorders(Flooded);
        }


        private void FloodFill(Coordinate[,] coords, Stack<Coordinate> workStack)
        {
            while (workStack.Count > 0)
            {
                Coordinate current = workStack.Pop();
                if (coords[current.X, current.Y].FloodFillStatus == Enumerations.FloodFillStatus.Suitable)
                {
                    coords[current.X, current.Y].FloodFillStatus = Enumerations.FloodFillStatus.Yes;

                    foreach (Coordinate neighbour in current.Neigbours(coords))
                    {
                        workStack.Push(neighbour);
                    }
                }
            }
        }


        private List<Coordinate> GetFlooded(Coordinate[,] coords)
        {
            List<Coordinate> floodFilled = new List<Coordinate>();
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    {
                        if (coords[x, y].FloodFillStatus == Enumerations.FloodFillStatus.Yes)
                        {
                            floodFilled.Add(coords[x, y]);
                        }
                    }
                }
            }
            return floodFilled;
        }

        public void GetBorders(List<Coordinate> flooded)
        {
            Left = flooded.Min(c => c.X);
            Top = flooded.Min(c => c.Y);
            Right = flooded.Max(c => c.X);
            Down = flooded.Max(c => c.Y);
        }

        public void ResetFloodedCoords(Coordinate[,] coords)
        {
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    Coordinate coordinate = coords[x, y];
                    if (coords[x, y].FloodFillStatus == Enumerations.FloodFillStatus.Yes)
                    {
                        coords[x, y].FloodFillStatus = Enumerations.FloodFillStatus.NotSuitable;
                    }
                }
            }
        }

        public void ResetFloodedCoords(Coordinate[,] coords, int left, int top, int right, int down)
        {
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    Coordinate coordinate = coords[x, y];
                    if ((x >= left) && (y >= top) && (x <= right) && (y <= down))
                    {
                        coords[x, y].FloodFillStatus = Enumerations.FloodFillStatus.NotSuitable;
                    }
                }
            }
        }

        public void MultiplyBorders(int factor)
        {
            Left = Left * factor;
            Top = Top * factor;
            Right = Right * factor;
            Down = Down * factor;
        }

        public static void RemoveSmallRegions(List<FloodFilledRegion> regions)
        {
            regions.RemoveAll(r => r.Right - r.Left < 10);
            regions.RemoveAll(r => r.Down - r.Top < 10);
        }

        public static void MultiplyBorders(List<FloodFilledRegion> regions, int factor)
        {
            regions.ForEach(r => r.MultiplyBorders(factor));
        }
    }
}
