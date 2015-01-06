using System.IO;

namespace Snapshot
{
    public class FilePathProvider : IFilePathProvider
    {
        private readonly string _filePath;

        public FilePathProvider(string filePath)
        {
            _filePath = filePath;
        }

        public string GetFilePath(string fileName)
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }
            return Path.Combine(_filePath, fileName);
        }
    }
}