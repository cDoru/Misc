using System.Collections.Generic;

namespace Snapshot
{
    public class ScreenshotTestRunner
    {
        private readonly IEnumerable<IScreenshotTest> _tests;

        public ScreenshotTestRunner(IEnumerable<IScreenshotTest> tests )
        {
            _tests = tests;
        }

        public void Run()
        {
            foreach (var test in _tests)
            {
                test.Run();
            }
        }
    }
}