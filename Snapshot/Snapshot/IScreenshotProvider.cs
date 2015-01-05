namespace Snapshot
{
    public interface IScreenshotProvider
    {
        void GetScreenshot(string url, string filePath);
    }
}