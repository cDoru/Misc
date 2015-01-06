using System.IO;
using Moq;
using NUnit.Framework;
using Snapshot.Tests.Builders;

namespace Snapshot.Tests.UnitTests
{
    public class ScreenshotTestEngineTests
    {
        [Test]
        [Category("UnitTest")]
        public void WhenRunIsCalledThenItCallsSaveScreenshotOnTestEngineBuilder()
        {
            var screenshotTestEngineBuilder = new ScreenshotTestEngineBuilder()
                .WithOldFilePath(@"OldImages/pic.png")
                .WithNewFilePath(@"NewImages/pic.png");

            var sut = screenshotTestEngineBuilder.Build();

            var screenshotTestBuilder = new ScreenshotTestBuilder()
                .WithFileName("pic.png")
                .WithUrl("DemoPage.html");

            sut.Run(screenshotTestBuilder.Build());

            screenshotTestEngineBuilder.MockScreenshotProvider.Verify(x =>
                x.SaveScreenshot(screenshotTestBuilder.Url, @"NewImages/pic.png"), Times.Once);
        }

        [Test]
        [Category("UnitTest")]
        public void WhenRunIsCalledAndScreenshotsDontMatchThenItReturnsANegativeResult()
        {
            var screenshotTestEngineBuilder = new ScreenshotTestEngineBuilder()
                .WithOldFilePath(@"OldImages/pic.png")
                .WithNewFilePath(@"NewImages/pic.png")
                .WhereImagesDontMatch();

            var sut = screenshotTestEngineBuilder.Build();

            var screenshotTestBuilder = new ScreenshotTestBuilder()
                .WithFileName("pic.png")
                .WithUrl("DemoPage.html");

            var result = sut.Run(screenshotTestBuilder.Build());

            Assert.IsFalse(result.Passed);
        }

        [Test]
        [Category("UnitTest")]
        public void WhenRunIsCalledAndScreenshotsMatchThenItReturnsAPositiveResult()
        {
            var screenshotTestEngineBuilder = new ScreenshotTestEngineBuilder()
                .WithOldFilePath(@"OldImages/pic.png")
                .WithNewFilePath(@"NewImages/pic.png")
                .WhereImagesMatch();

            var sut = screenshotTestEngineBuilder.Build();

            var screenshotTestBuilder = new ScreenshotTestBuilder()
                .WithFileName("pic.png")
                .WithUrl("DemoPage.html");

            var result = sut.Run(screenshotTestBuilder.Build());

            Assert.IsTrue(result.Passed);
        }

    }
}
