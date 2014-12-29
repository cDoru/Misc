using System.Drawing;

namespace SelenuimTest
{
    public class BitmapComparer : IBitmapComparer
    {
        public bool Compare(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Size != bmp2.Size)
            {
                return false;
            }

            for (var x = 0; x < bmp1.Width; ++x)
            {
                for (var y = 0; y < bmp1.Height; ++y)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}