namespace Snapshot
{
    public interface IScreenshotProvider
    {
        void SaveScreenshot(string url, string filePath);
    }
}