using System.Linq;
using Moq;
using NUnit.Framework;

namespace Snapshot.Tests.UnitTests
{
    public class ScreenshotTestRunnerTests
    {
        private static ScreenshotTestRunner CreateSut(IScreenshotDefinition screenshotDefinition, IScreenshotTestEngine screenshotTestEngine)
        {
            var tests = new[]
            {
                screenshotDefinition,
                screenshotDefinition
            };
            return new ScreenshotTestRunner(screenshotTestEngine, tests);
        }

        [Test]
        [Category("UnitTest")]  
        public void WhenRunIsCalledThenTestEngineRunIsCalledForEachTest()
        {
            var mockScreenshotTestEngine = new Mock<IScreenshotTestEngine>();

            var sut = CreateSut(Mock.Of<IScreenshotDefinition>(), mockScreenshotTestEngine.Object);
            sut.Run();

            mockScreenshotTestEngine.Verify(x => x.Run(It.IsAny<IScreenshotDefinition>()), Times.Exactly(2));
        }

        [Test]
        [Category("UnitTest")]
        public void WhenRunIsCalledThenItReturnsACollectionOfResults()
        {
            var sut = CreateSut(Mock.Of<IScreenshotDefinition>(), Mock.Of<IScreenshotTestEngine>());
            var results = sut.Run();

            Assert.AreEqual(2, results.Count());
        }

        [Test]
        [Category("UnitTest")]  
        public void WhenRunIsCalledAndEachTestPassedThenItReturnsACollectionOfResultsShowingTheTestsPassed()
        {
            var mockScreenshotTestEngine = new Mock<IScreenshotTestEngine>();

            mockScreenshotTestEngine.Setup(x => x.Run(It.IsAny<IScreenshotDefinition>()))
                .Returns(new ScreenshotTestResult(true));

            var sut = CreateSut(Mock.Of<IScreenshotDefinition>(), mockScreenshotTestEngine.Object);
            var results = sut.Run();

            Assert.IsTrue(results.FirstOrDefault().Passed);
        }

        [Test]
        [Category("UnitTest")]  
        public void WhenRunIsCalledAndEachTestFailsThenItReturnsACollectionOfResultsShowingTheTestsFailed()
        {
            var mockScreenshotTestEngine = new Mock<IScreenshotTestEngine>();

            mockScreenshotTestEngine.Setup(x => x.Run(It.IsAny<IScreenshotDefinition>()))
                .Returns(new ScreenshotTestResult(false));

            var sut = CreateSut(Mock.Of<IScreenshotDefinition>(), mockScreenshotTestEngine.Object);
            var results = sut.Run();

            Assert.IsFalse(results.FirstOrDefault().Passed);
        }

    }
}