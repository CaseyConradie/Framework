
using Framework.Src.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Framework.WayToAutomation;
using OpenQA.Selenium;
using AventStack.ExtentReports;

namespace Framework.TestsSuites
{
    [TestClass]
    public class WebTask1TestSuite
    {
        private Way2automation _way2automation;

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithCSV()
        {

            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", "").Trim());
            _way2automation = new Way2automation(SeleniumUtils.GenerateDriver("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30"), currentTest);
            _way2automation.CreateUsers("TestData.csv");
            _way2automation.GetDriver.Close();
            Base.Report.FinaliseTestWithOutScreenShot(currentTest);
        }

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithJson()
        {
            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", "").Trim());
            _way2automation = new Way2automation(SeleniumUtils.GenerateDriver("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30"), currentTest);
            _way2automation.CreateUsers("UserDetails.json", false);
            _way2automation.GetDriver.Close();
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