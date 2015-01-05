using System.Drawing;

namespace Snapshot
{
    public class BitmapComparer : IBitmapComparer
    {
        public bool AreSame(string filePath1, string filePath2)
        {
            var bitmap1 = new Bitmap(filePath1);
            var bitmap2 = new Bitmap(filePath2);

            var result = AreSame(bitmap1, bitmap2);

            bitmap1.Dispose();
            bitmap2.Dispose();

            return result;
        }

        private bool AreSame(Bitmap bmp1, Bitmap bmp2)
        {
            return AreSameSize(bmp1, bmp2) &&
                   ArePixelsTheSame(bmp1, bmp2);
        }

        private static bool ArePixelsTheSame(Bitmap bmp1, Bitmap bmp2)
        {
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

        private static bool AreSameSize(Image bmp1, Image bmp2)
        {
            return bmp1.Size == bmp2.Size;
        }
    }
}