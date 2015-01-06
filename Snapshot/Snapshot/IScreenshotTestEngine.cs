namespace Snapshot
{
    public interface IScreenshotTestEngine
    {
        IScreenshotTestResult Run(IScreenshotDefinition outline);
    }
}