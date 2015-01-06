using OpenQA.Selenium.Chrome;

namespace Snapshot
{
    public class ScreenshotTestEngine : IScreenshotTestEngine
    {
        private readonly IFilePathProvider _oldFilePathProvider;
        private readonly IFilePathProvider _newFilePathProvider;
        private readonly IScreenshotProvider _screenshotProvider;
        private readonly IBitmapComparer _bitmapComparer;

        public ScreenshotTestEngine(IFilePathProvider oldFilePathProvider, IFilePathProvider newFilePathProvider,
            IScreenshotProvider screenshotProvider)
            :this(oldFilePathProvider, newFilePathProvider,screenshotProvider, new BitmapComparer())
        { }

        public ScreenshotTestEngine(IFilePathProvider oldFilePathProvider, IFilePathProvider newFilePathProvider, IScreenshotProvider screenshotProvider, IBitmapComparer bitmapComparer)
        {
            _bitmapComparer = bitmapComparer;
            _screenshotProvider = screenshotProvider;
            _newFilePathProvider = newFilePathProvider;
            _oldFilePathProvider = oldFilePathProvider;
        }

        public IScreenshotTestResult Run(IScreenshotDefinition outline)
        {
            var newFilePath = _newFilePathProvider.GetFilePath(outline.FileName);
            var oldFilePath = _oldFilePathProvider.GetFilePath(outline.FileName);

            _screenshotProvider.SaveScreenshot(outline.Url, newFilePath);
            var result = _bitmapComparer.AreSame(oldFilePath, newFilePath);
            return new ScreenshotTestResult(result);
        }
    }
}