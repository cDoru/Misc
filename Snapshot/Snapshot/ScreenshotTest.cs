using System.Drawing;
using System.IO;
using OpenQA.Selenium.Chrome;

namespace Snapshot
{
    public class ScreenshotTest : IScreenshotTest
    {
        private readonly string _url;
        private readonly string _oldFilePath;
        private string _newFilePath;
        private IFilePathProvider _filePathProvider;
        public bool Result { get; set; }

        public ScreenshotTest(string url, string fileName, IFilePathProvider filePathProvider)
        {
            _filePathProvider = filePathProvider;
            _url = url;
            _newFilePath = _filePathProvider.GetNewFilePath(fileName);
            _oldFilePath = _filePathProvider.GetOldFilePath(fileName);
        }

        public void Run()
        {
            using (var driver = new ChromeDriver(@"Drivers/"))
            {
                var sut = new ScreenshotProvider(driver);
                sut.GetScreenshot(_url, _newFilePath);
            }

            var bitmapComparer = new BitmapComparer();

            bitmapComparer.AreSame(_oldFilePath, _newFilePath);
        }
    }

    public interface IFilePathProvider
    {
        string GetOldFilePath(string fileName);
        string GetNewFilePath(string fileName);
    }
}