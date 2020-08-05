using AventStack.ExtentReports;
using Framework.Src.Tools;
using Framework.WayToAutomation.Pages;
using OpenQA.Selenium;

namespace Framework.WayToAutomation
{
    public class Way2automation : SeleniumUtils
    {

        public WebTask webTask {get; set;}
        
        public Way2automation(IWebDriver driver, ExtentTest currentTest) : base(driver, currentTest)
        {
            webTask = new WebTask(driver,currentTest);
        }
    }
}