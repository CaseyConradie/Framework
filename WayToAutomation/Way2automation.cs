using AventStack.ExtentReports;
using Framework.Src.Tools;
using Framework.WayToAutomation.Pages;
using OpenQA.Selenium;

namespace Framework.WayToAutomation
{
    public class Way2automation : SeleniumUtils
    {

        private WebTask webTask;
        
        public Way2automation(IWebDriver driver, ExtentTest currentTest) : base(driver, currentTest)
        {
            webTask = new WebTask(driver,currentTest);
        }

        public void CreateUsers(string fileName, bool CSV = true)
        {
            webTask.CreateUser(fileName, CSV);
        }
    }
}