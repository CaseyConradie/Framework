
using Framework.Src.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.WayToAutomation;
using OpenQA.Selenium;

namespace Framework.TestsSuites
{
    [TestClass]
    public class WebTask1TestSuite
    {
        public TestContext TestContext { get; set; }
        private Way2automation _way2automation;

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithCSV()
        {
            var currentTest = Base.Report.CreateTest(TestContext.TestName);
            IWebDriver driver = SeleniumUtils.GenerateDriver("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30");

            _way2automation = new Way2automation(driver, currentTest);
            _way2automation.webTask.CreateUser("TestData.csv");

            SeleniumUtils.ShutDown(driver);
            Base.Report.FinaliseTestWithOutScreenShot(currentTest);
        }

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithJson()
        {
            var currentTest = Base.Report.CreateTest(TestContext.TestName);
            IWebDriver driver = SeleniumUtils.GenerateDriver("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30");

            _way2automation = new Way2automation(driver, currentTest);
            _way2automation.webTask.CreateUser("UserDetails.json", false);

            SeleniumUtils.ShutDown(driver);
            Base.Report.FinaliseTestWithOutScreenShot(currentTest);
        }

        [AssemblyCleanup]
        public static void AfterAll()
        {
            //finalise test
            Base.Report.FlushReport();
        }
    }
}