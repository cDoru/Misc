namespace Snapshot.Tests.Builders
{
    public class ScreenshotTestBuilder
    {
        private string _url = "DemoPage.html";
        private string _fileName = "pic.png";

        public string Url
        {
            get { return _url; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public ScreenshotTestBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public ScreenshotTestBuilder WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public IScreenshotDefinition Build()
        {
            return new ScreenshotDefinition(Url, FileName);
        }
    }
}