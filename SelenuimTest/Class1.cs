using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace SelenuimTest
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
            s.SaveAsFile(@"C:\Users\Daniel\Documents\CVs\pic2.png", ImageFormat.Png);

            var bitmap1 = new Bitmap(@"C:\Users\Daniel\Documents\CVs\pic.png");
            var bitmap2 = new Bitmap(@"C:\Users\Daniel\Documents\CVs\pic2.png");

            var result = bitmapComparer.Compare(bitmap1, bitmap2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Test2()
        {
            var bitmapComparer = new BitmapComparer();

            var driver = new ChromeDriver(@"Drivers/");

            driver.Navigate().GoToUrl("http://www.thelondonclinic.co.uk/patient-care/why-choose-the-london-clinic");

            Thread.Sleep(2000);

            var s = driver.GetScreenshot();
            s.SaveAsFile(@"C:\Users\Daniel\Documents\CVs\pic2.png", ImageFormat.Png);

            var bitmap1 = new Bitmap(@"C:\Users\Daniel\Documents\CVs\pic.png");
            var bitmap2 = new Bitmap(@"C:\Users\Daniel\Documents\CVs\pic2.png");

            var result = bitmapComparer.Compare(bitmap1, bitmap2);

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
