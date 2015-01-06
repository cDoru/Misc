using System.Collections.Generic;
using System.Linq;

namespace Snapshot
{
    public class ScreenshotTestRunner
    {
        private readonly IEnumerable<IScreenshotDefinition> _tests;
        private readonly IScreenshotTestEngine _screenshotTestEngine;

        public ScreenshotTestRunner(IScreenshotTestEngine screenshotTestEngine, IEnumerable<IScreenshotDefinition> tests)
        {
            _screenshotTestEngine = screenshotTestEngine;
            _tests = tests;
        }

        public IEnumerable<IScreenshotTestResult> Run()
        {
            return _tests.Select(test => _screenshotTestEngine.Run(test))
                .ToList();
        }
    }
}