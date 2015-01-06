namespace Snapshot
{
    public class ScreenshotDefinition : IScreenshotDefinition
    {
        private readonly string _url;
        private readonly string _fileName;

        public ScreenshotDefinition(string url, string fileName)
        {
            _fileName = fileName;
            _url = url;
        }

        public string Url
        {
            get { return _url; }
        }

        public string FileName
        {
            get { return _fileName; }
        }
    }
}