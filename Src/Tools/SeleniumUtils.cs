using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using AventStack.ExtentReports;

namespace Framework.Src.Tools
{
    public abstract class SeleniumUtils : ReportHandler
    {
        public SeleniumUtils(IWebDriver driver, ExtentTest _curTest)
        {
            _driver = driver;
        }

        public IWebDriver GetDriver => _driver;


        /// <summary>
        /// Generates the driver by selecting browser and version
        /// Navigates to the URl and maximises the browser
        /// </summary>
        public static IWebDriver GenerateDriver(string BrowserType, string url, string version = null)
        {
            IWebDriver _drv = (IWebDriver)null;
            switch (BrowserType.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), version);
                    _drv = new ChromeDriver();
                    _drv.Navigate().GoToUrl(url);
                    _drv.Manage().Window.Maximize();
                    break;
                case "firefox":
                case "fire-fox":
                    new DriverManager().SetUpDriver(new FirefoxConfig(), version);
                    _drv = new FirefoxDriver();
                    _drv.Navigate().GoToUrl(url);
                    _drv.Manage().Window.Maximize();
                    break;
                case "internet explore":
                case "ie":
                case "internet-explore":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig(), version);
                    _drv = new InternetExplorerDriver();
                    _drv.Navigate().GoToUrl(url);
                    _drv.Manage().Window.Maximize();
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig(), version);
                    _drv = new EdgeDriver();
                    _drv.Navigate().GoToUrl(url);
                    _drv.Manage().Window.Maximize();
                    break;
                default:
                    new DriverManager().SetUpDriver(new ChromeConfig(), version);
                    _drv = new ChromeDriver();
                    _drv.Navigate().GoToUrl(url);
                    _drv.Manage().Window.Maximize();
                    break;
            };
            return _drv;
        }

        /// <summary>
        /// Shuts down the driver
        /// </summary>
        public static bool ShutDown(IWebDriver _driver)
        {
            try
            {
                _driver.Close();
                _driver.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string GenerateRandomNumber(int EndNumber)
        {
            try
            {
                Random random = new Random();
                int randomNumber = random.Next(0, EndNumber);
                return randomNumber.ToString();
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to generate rand" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public bool WaitForElement(By selector, int timeSpan = 10)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeSpan));
                wait.Until(_driver => _driver.FindElement(selector));
                wait.Until(_driver => _driver.FindElement(selector).Displayed);
                wait.Until(_driver => _driver.FindElement(selector).Enabled);
                return true;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to wait for element" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public bool EnterText(By selector, string text)
        {
            try
            {
                WaitForElement(selector);
                IWebElement element = _driver.FindElement(selector);
                element.Clear();
                element.SendKeys(text);
                return true;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to enter text" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public string GetText(By selector)
        {
            try
            {
                WaitForElement(selector);
                IWebElement element = _driver.FindElement(selector);
                return element.Text;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to get text" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public string GetAttribute(By selector, string attribute)
        {
            try
            {
                WaitForElement(selector);
                IWebElement element = _driver.FindElement(selector);
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to retrieve attribute" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public bool ClickElement(By selector)
        {
            try
            {
                WaitForElement(selector);
                IWebElement element = _driver.FindElement(selector);
                element.Click();
                return true;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to click element" + ex.Message, _driver, _curTest);
                throw;
            }
        }


        public List<string> GetAllValuesFrom(string xpath)
        {
            try
            {
                var elementList = _driver.FindElements(By.XPath(xpath));
                List<string> tempList = new List<string>();

                foreach (IWebElement currentElement in elementList)
                {
                    if (!String.IsNullOrEmpty(currentElement.Text))
                    {
                        tempList.Add(currentElement.Text);
                        continue;
                    }

                    tempList.Add("N/A");
                }

                return tempList;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to get list of texts" + ex.Message, _driver, _curTest);
                throw;
            }
        }

        public bool SelectTextFromDropdown(By selector, string textToSelect)
        {
            try
            {
                WaitForElement(selector);
                IWebElement element = _driver.FindElement(selector);
                SelectElement select = new SelectElement(element);
                select.SelectByText(textToSelect);
                return true;
            }
            catch (Exception ex)
            {
                Base.Report.TestFailed("Failed to select from dropdown" + ex.Message, _driver, _curTest);
                throw;
            }
        }

    }
}