using System;
using Framework.Src.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Src.Pages;
using OpenQA.Selenium;
using System.Reflection;


namespace Framework.TestsSuites
{
    [TestClass]
    public class WebTask1TestSuite
    {
        public TestContext TestContext { get; set; }
        private WebTask webTask;

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithCSV()
        {
            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()","").Trim());
            SeleniumUtils seleniumUtils = new SeleniumUtils("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30"); 
            webTask = new WebTask(seleniumUtils, currentTest);
            webTask.CreateUser("TestData.csv");
            seleniumUtils.GetDriver.Close();
            Base.Report.FinaliseTestWithOutScreenShot(currentTest);
        }

        [TestMethod, TestCategory("UITest")]
        public void AddUserWithJson()
        {
            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()","").Trim());
            SeleniumUtils seleniumUtils = new SeleniumUtils("chrome", "http://www.way2automation.com/angularjs-protractor/webtables/", "84.0.4147.30"); 
            webTask = new WebTask(seleniumUtils, currentTest);
            webTask.CreateUserWithJson("UserDetails.json");
            seleniumUtils.GetDriver.Close();
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