using AventStack.ExtentReports;
using Framework.Src.Tools;
using Framework.WayToAutomation.Pages;
using OpenQA.Selenium;

namespace Framework.WayToAutomation
{
    public class Way2automation
    {
        private SeleniumUtils _seleniumUtils;
        private ExtentTest _curTest;
        private IWebDriver _driver;
        private WebTask webTask;
        
        public Way2automation(SeleniumUtils seleniumUtils, ExtentTest currentTest)
        {
            _seleniumUtils = seleniumUtils;
            _curTest = currentTest;
            _driver = _seleniumUtils.GetDriver;
            webTask = new WebTask(seleniumUtils,currentTest);
        }

        public void CreateUsers(string fileName, bool CSV = true)
        {
            webTask.CreateUser(fileName, CSV);
        }
    }
}