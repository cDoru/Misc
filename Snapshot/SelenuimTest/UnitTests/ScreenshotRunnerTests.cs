using Moq;
using NUnit.Framework;

namespace Snapshot.Tests.UnitTests
{
    public class ScreenshotRunnerTests
    {
        [Test]
        [Category("UnitTest")]
        public void WhenRunIsCalledThenScreenshotEngineTakesASnapshotForEachUrl()
        {
            var mockScreenshotProvider = new Mock<IScreenshotProvider>();
            var filePathProvider = new FilePathProvider("TestDirectory");

            var sut = new ScreenshotRunner(mockScreenshotProvider.Object, filePathProvider);

            var screenshotDefinitions = new[]
            {
                new ScreenshotDefinition("url1", "fileName1"),
                new ScreenshotDefinition("url2", "fileName2")
            };

            sut.CreateScreenshots(screenshotDefinitions);

            mockScreenshotProvider.Verify(x => x.SaveScreenshot("url1", @"TestDirectory\fileName1"), Times.Once);
            mockScreenshotProvider.Verify(x => x.SaveScreenshot("url2", @"TestDirectory\fileName2"), Times.Once);
        }
    }
}