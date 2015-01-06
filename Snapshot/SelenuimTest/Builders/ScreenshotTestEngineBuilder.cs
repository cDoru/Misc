using Moq;

namespace Snapshot.Tests.Builders
{
    public class ScreenshotTestEngineBuilder
    {
        private readonly Mock<IFilePathProvider> _mockOldFilePathProvider;
        private readonly Mock<IFilePathProvider> _mockNewFilePathProvider;
        private readonly Mock<IScreenshotProvider> _mockScreenshotProvider;
        private readonly Mock<IBitmapComparer> _mockBitmapComparer;

        public ScreenshotTestEngineBuilder()
        {
            _mockOldFilePathProvider = new Mock<IFilePathProvider>();
            _mockNewFilePathProvider = new Mock<IFilePathProvider>();
            _mockScreenshotProvider = new Mock<IScreenshotProvider>();
            _mockBitmapComparer = new Mock<IBitmapComparer>();
        }

        public Mock<IScreenshotProvider> MockScreenshotProvider
        {
            get { return _mockScreenshotProvider; }
        }

        public ScreenshotTestEngineBuilder WithOldFilePath(string filePath)
        {
            _mockOldFilePathProvider.Setup(x => x.GetFilePath(It.IsAny<string>()))
                .Returns(filePath);
            return this;
        }

        public ScreenshotTestEngineBuilder WithNewFilePath(string filePath)
        {
            _mockNewFilePathProvider.Setup(x => x.GetFilePath(It.IsAny<string>()))
                .Returns(filePath);
            return this;
        }

        public ScreenshotTestEngineBuilder WhereImagesDontMatch()
        {
            _mockBitmapComparer.Setup(x => x.AreSame(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
            return this;
        }

        public ScreenshotTestEngineBuilder WhereImagesMatch()
        {
            _mockBitmapComparer.Setup(x => x.AreSame(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            return this;
        }

        public IScreenshotTestEngine Build()
        {
            return new ScreenshotTestEngine(
                _mockOldFilePathProvider.Object, 
                _mockNewFilePathProvider.Object, 
                MockScreenshotProvider.Object,
                _mockBitmapComparer.Object);
        }
    }
}