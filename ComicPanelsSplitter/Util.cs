using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ComicPanelsSplitter
{
    public class Util
    {


       public static Coordinate FindTopLeftNonWhitePixel(Bitmap bitmap)
        {
            for(int x=0; x<bitmap.Width; x++)
            {
                for(int y=0; y<bitmap.Height; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    if (!coordinate.IsWhite(bitmap))
                    {
                        return coordinate;
                    }
                }
            }
            return null;
        }

    }
}
