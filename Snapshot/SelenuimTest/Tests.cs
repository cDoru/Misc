using System;
using System.Drawing;
using System.Drawing.Imaging;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Snapshot;

namespace Snapshot.Tests
{

    public class Tests
    {
        [Test]
        public void Test1()
        {
            IBitmapComparer bitmapComparer = new BitmapComparer();

            var driver = new ChromeDriver(@"Drivers/");

            //var driver = new InternetExplorerDriver(@"Drivers/");

            driver.Navigate().GoToUrl("http://www.thelondonclinic.co.uk");

            WaitForElementToAppear(driver, 2000, By.Id("patient"));

            driver.FindElement(By.Id("patient")).Click();

            WaitForElementToAppear(driver, 2000, By.LinkText("Why choose The London Clinic?"));

            driver.FindElementByLinkText("Why choose The London Clinic?").Click();

            var s = driver.GetScreenshot();
            s.SaveAsFile(@"pic.png", ImageFormat.Png);
            s.SaveAsFile(@"pic2.png", ImageFormat.Png);

            var result = bitmapComparer.AreSame(@"pic.png", @"pic2.png");

            Assert.IsTrue(result);
        }

        public static IWebElement WaitForElementToAppear(IWebDriver driver, int waitTime, By waitingElement)
        {
            var wait =
                new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime)).Until(
                    ExpectedConditions.ElementExists(waitingElement));
            return wait;
        }
    }
}
