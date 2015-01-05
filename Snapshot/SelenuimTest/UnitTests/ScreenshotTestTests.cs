using System.IO;
using Moq;
using NUnit.Framework;

namespace Snapshot.Tests.UnitTests
{
    public class ScreenshotTestTests
    {
        [Test]
        public void RunsTest()
        {
            var mockFilePathProvider = new Mock<IFilePathProvider>();

            var url = Directory.GetCurrentDirectory() + "/WebPages/DemoPage.html";
            var fileName = "pic.png";

            mockFilePathProvider.Setup(x => x.GetNewFilePath(It.IsAny<string>()))
                .Returns(@"NewImages/pic.png");

            mockFilePathProvider.Setup(x => x.GetOldFilePath(It.IsAny<string>()))
                .Returns(@"OldImages/pic.png");

            if (!Directory.Exists(@"NewImages/"))
            {
                Directory.CreateDirectory(@"NewImages/");
            }

            var sut = new ScreenshotTest(url, fileName, mockFilePathProvider.Object);
            sut.Run();

            Assert.IsTrue(sut.Result);
        }
    }
}
