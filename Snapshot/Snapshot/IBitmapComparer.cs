using System.Drawing;

namespace Snapshot
{
    public interface IBitmapComparer
    {
        bool AreSame(string filePath1, string filePath2);
    }
}