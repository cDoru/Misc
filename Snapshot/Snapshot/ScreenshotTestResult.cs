namespace Snapshot
{
    public class ScreenshotTestResult : IScreenshotTestResult
    {
        public bool Passed { get; private set; }

        public ScreenshotTestResult(bool passed)
        {
            Passed = passed;
        }
    }
}