using Moq;
using NUnit.Framework;

namespace Snapshot.Tests
{
    public class ScreenshotTestRunnerTests
    {
        [Test]
        public void RunsTest()
        {
            var mock = new Mock<IScreenshotTest>();

            var tests = new[]
            {
                mock.Object,
                mock.Object
            };

            var sut = new ScreenshotTestRunner(tests);
            sut.Run();

            mock.Verify(x => x.Run(), Times.Exactly(2));
        }
    }
}