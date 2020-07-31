using System;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using OpenQA.Selenium;

namespace Framework.Src.Tools
{
    public class Reporting
    {

        public Reporting()
        {
            Setup();
        }

        private int ScreenshotCounter = 0;
        public string ReportName { get; set; }
        private string _reportDir;
        private string ReportDirectory { get => _reportDir; set => _reportDir = value; }
        private string LogFile { get => _reportDir + @"\Log.txt"; }
        private AventStack.ExtentReports.ExtentReports _report;



        //Get date time
        public string GetDateTime()
        {
            return DateTime.Now.ToString("hh-mm-ss dd-MM-yyyy");
        }

        public ExtentTest CreateTest(string TestName)
        {
            return _report.CreateTest(TestName);
        }

        //Setup the reporting
        private void Setup()
        {
            //Location for reports
            ReportDirectory = @"..\..\..\Reports\" + ReportName + @"\" + GetDateTime() + @"\";
            System.IO.Directory.CreateDirectory(ReportDirectory);

            //Create html _report
            var htmlReport = new AventStack.ExtentReports.Reporter.ExtentHtmlReporter(ReportDirectory);
            _report = new AventStack.ExtentReports.ExtentReports();
            _report.AttachReporter(htmlReport);
            _report.Flush();
        }


        public string TestFailed(string message, IWebDriver driver, ExtentTest curTest)
        {
            try
            {
                curTest.Fail(message + "</br>",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(
                        TakeScreenshot(false, driver)).Build());
                _report.Flush();
                Console.WriteLine("[FAILURE] - " + message);
                return message;
            }
            catch
            {
                return "Failed to log Test Failure - " + message;
            }
        }



        public string TakeScreenshot_(bool status, IWebDriver _driver)
        {
            string screenshotPath = null;
            if (_driver != null)
            {
                screenshotPath = (screenshotPath == null) ? TakeScreenshot(status, _driver) : screenshotPath;
            }

            return screenshotPath;
        }


        public string FinaliseTestWithOutScreenShot(ExtentTest curTest)
        {
            try
            {
                curTest.Pass("Test Complete" + "</br>");
                return null;
            }
            catch
            {
                return "Failed to log Test Complete";
            }
        }

        public string FlushReport()
        {
             try
            {
                _report.Flush();
                Console.WriteLine("[SUCCESS] - Test Complete");
                return null;
            }
            catch
            {
                return "Failed to log Test Complete";
            }
        }

        public string StepPassed(string message,ExtentTest curTest)
        {
            try
            {
                curTest.Pass(message);
                _report.Flush();
                Console.WriteLine("[SUCCESS] - " + message);
                return null;
            }
            catch
            {
                Console.WriteLine("Failed to log step passed - " + message);
                return "Failed to log step passed";
            }
        }


        public string StepPassedWithScreenshot(string message, IWebDriver driver,ExtentTest curTest)
        {
            try
            {
                curTest.Pass(message + "<br>",
                MediaEntityBuilder.CreateScreenCaptureFromPath(
                TakeScreenshot(true, driver)).Build());
                _report.Flush();
                return null;
            }
            catch (Exception exc)
            {
                 Console.WriteLine("Failed to log step passed - " + exc.Message);
                curTest.Warning(message);
                return message;
            }
        }

        public void Warning(string message,ExtentTest curTest)
        {
            try
            {
                curTest.Warning(message);
                _report.Flush();
            }
            catch
            {
                Console.WriteLine("Failed to log step passed - " + message);
            }
        }


        public void LogUrlRequesteEndpoint(string request, CodeLanguage codeFormat,ExtentTest curTest)
        {
            //Creates a Label Request for the _report
            var formattedLabel = MarkupHelper.CreateLabel("Request:", ExtentColor.Teal);
            curTest.Log(Status.Info, formattedLabel);

            //Creates a code block
            var formattedMessage = MarkupHelper.CreateCodeBlock(request, codeFormat);
            curTest.Log(Status.Info, formattedMessage);
        }

        public void LogResponse(string response, CodeLanguage codeFormat, ExtentTest curTest)
        {
            //Creates a Label Response for the _report
            var formattedLabel = MarkupHelper.CreateLabel("Response:", ExtentColor.Teal);
            curTest.Log(Status.Info, formattedLabel);

            var formattedMessage = MarkupHelper.CreateCodeBlock(response, codeFormat);
            curTest.Log(Status.Info, formattedMessage);
        }

        //Inserts Picture into _report
        public void InsertPicture(string url, ExtentTest curTest)
        {
            curTest.Log(Status.Pass, "<img src=" + url + " width=200px height=200px />");
        }


        public string TakeScreenshot(bool status, IWebDriver _driver)
        {
            ScreenshotCounter++;
            StringBuilder builder = new StringBuilder();
            StringBuilder relativeBuilder = new StringBuilder();
            builder.Append(ReportDirectory);
            relativeBuilder.Append("Screenshots\\");
            System.IO.Directory.CreateDirectory("" + builder + relativeBuilder);
            relativeBuilder.Append(ScreenshotCounter + "_" + ((status) ? @"Passed" : @"Failed"));
            relativeBuilder.Append(".png");

            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("" + builder + relativeBuilder);
            return "./" + relativeBuilder;
        }
    }

}