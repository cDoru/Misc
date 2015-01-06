using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Snapshot.Tests.IntregrationTests
{
    public class ScreenshotProviderTest
    {
        [Test]
        public void TakesAScreenshotForAWebPage()
        {
            var url = Directory.GetCurrentDirectory() + "/WebPages/DemoPage.html";
            var filePath = "pic.png";

            using (var driver = new ChromeDriver(@"Drivers/"))
            {
                var sut = new ScreenshotProvider(driver);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                sut.SaveScreenshot(url, filePath);
            }

            var bitmapComparer = new BitmapComparer();
            var result = bitmapComparer.AreSame(@"pic.png", @"OldImages/pic.png");

            Assert.IsTrue(result);
            File.Exists(filePath);
        }

        [Test]
        public void TakesAScreenshotForAWebPageNonMatch()
        {
            var url = Directory.GetCurrentDirectory() + "/WebPages/DemoPage2.html";
            var filePath = "pic.png";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var driver = new ChromeDriver(@"Drivers/"))
            {
                var sut = new ScreenshotProvider(driver);
                sut.SaveScreenshot(url, filePath);
            }

            var bitmapComparer = new BitmapComparer();
            var result = bitmapComparer.AreSame(@"pic.png", @"OldImages/pic.png");

            Assert.IsFalse(result);
        }

    }
}
