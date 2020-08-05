using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace Framework.Src.Tools
{
    public abstract class ReportHandler
    {
        protected IWebDriver _driver { get; set; }
        protected ExtentTest _curTest { get; set; }

        public void TestFailed(string message)
        {
            Base.Report.TestFailed(message, _driver, _curTest);
        }

        public void StepPassedScreenShot(string message)
        {
            Base.Report.StepPassedWithScreenshot(message, _driver, _curTest);
        }
    }
}