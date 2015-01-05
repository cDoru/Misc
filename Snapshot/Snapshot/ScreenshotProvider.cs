using System;
using System.Drawing.Imaging;
using OpenQA.Selenium.Remote;

namespace Snapshot
{
    public class ScreenshotProvider : IScreenshotProvider
    {
        private readonly RemoteWebDriver _remoteWebDriver;

        public ScreenshotProvider(RemoteWebDriver remoteWebDriver)
        {
            _remoteWebDriver = remoteWebDriver;
            _remoteWebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(15));
        }

        public void GetScreenshot(string url, string filePath)
        {
            _remoteWebDriver.Navigate().GoToUrl(url);
            var s = _remoteWebDriver.GetScreenshot();
            s.SaveAsFile(filePath, ImageFormat.Png);
        }
    }
}