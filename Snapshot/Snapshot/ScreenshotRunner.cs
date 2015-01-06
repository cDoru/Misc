using System.Collections.Generic;

namespace Snapshot
{
    public class ScreenshotRunner
    {
        private readonly IScreenshotProvider _screenshotProvider;
        private readonly IFilePathProvider _filePathProvider;

        public ScreenshotRunner(IScreenshotProvider screenshotProvider, IFilePathProvider filePathProvider)
        {
            _filePathProvider = filePathProvider;
            _screenshotProvider = screenshotProvider;
        }

        public void CreateScreenshots(IEnumerable<IScreenshotDefinition> screenshotDefinitions)
        {
            foreach (var screenshotDefinition in screenshotDefinitions)
            {
                var filePath = _filePathProvider.GetFilePath(screenshotDefinition.FileName);
                _screenshotProvider.SaveScreenshot(screenshotDefinition.Url, filePath);
            }
        }
    }
}