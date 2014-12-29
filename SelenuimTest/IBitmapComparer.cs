using System.Drawing;

namespace SelenuimTest
{
    public interface IBitmapComparer
    {
        bool Compare(Bitmap bmp1, Bitmap bmp2);
    }
}