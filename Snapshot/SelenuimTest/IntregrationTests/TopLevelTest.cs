using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Snapshot.Tests.IntregrationTests
{
    public class TopLevelTests
    {
        [Test]
        public void RunTests()
        {
            var screenshots = new[]
            {
                new ScreenshotDefinition(
                    @"http://www.thelondonclinic.co.uk/patient-care/your-stay-at-the-london-clinic", "Your_Stay.png"),
                new ScreenshotDefinition(@"http://www.thelondonclinic.co.uk/eye-centre", "Eye_Centre.png")
            };

            var oldFilePath = new FilePathProvider("Original");
            var newFilePath = new FilePathProvider("New");
            var screenshotProvider = new ScreenshotProvider(new ChromeDriver(@"Drivers/"));

            var runner = new ScreenshotRunner(screenshotProvider, oldFilePath);
            runner.CreateScreenshots(screenshots);

            var screenshotEngine = new ScreenshotTestEngine(oldFilePath, newFilePath, screenshotProvider);

            var testRunner = new ScreenshotTestRunner(screenshotEngine, screenshots);
            var results = testRunner.Run();



        }


    }
}
