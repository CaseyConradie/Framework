using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Framework.Src.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Framework.WayToAutomation.Pages
{
    public class DogAPITask
    {

        APIController _apiController = new APIController("https://dog.ceo/api/");

        public void APITask1(ExtentTest currentTest)
        {
            //Endpoint
            string endpoint = "breeds/list/all";
            Base.Report.LogUrlRequesteEndpoint(endpoint, CodeLanguage.Xml, currentTest);

            //Headers (Cookies, etc)
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=d0f92bfbc920328711fe87c781e7851501593900235");

            //Rest call retrieve response
            var response = _apiController.Get(endpoint, headers);

            //Validate status code OK
            Assert.AreEqual("OK", response.StatusCode.ToString());

            //Retrieve breed and validates 
            var retriever = response.Content.Contains("retriever");
            Assert.IsTrue(retriever);

            //Reporting
            Base.Report.LogResponse(response.Content, CodeLanguage.Xml, currentTest);
            Base.Report.StepPassed("Successfully located the value '" + retriever + ".", currentTest);
        }

        public void APITask2(ExtentTest currentTest)
        {
            //Endpoint
            string endpoint = "breed/retriever/list";
            Base.Report.LogUrlRequesteEndpoint(endpoint, CodeLanguage.Xml, currentTest);

            //Headers (Cookies, etc)
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=d0f92bfbc920328711fe87c781e7851501593900235");

            //Rest call retrieve response
            var response = _apiController.Get(endpoint, headers);

            //Validate status code OK
            Assert.AreEqual("OK", response.StatusCode.ToString());
            Base.Report.LogResponse(response.Content, CodeLanguage.Json, currentTest);

            Base.Report.StepPassed("Successfully retrieved sub-breeds.", currentTest);
        }

        public void APITask3(ExtentTest currentTest)
        {
            //Endpoint
            string endpoint = "breed/retriever/golden/images/random";
            Base.Report.LogUrlRequesteEndpoint(endpoint, CodeLanguage.Xml, currentTest);

            //Headers (Cookies, etc)
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=d0f92bfbc920328711fe87c781e7851501593900235");

            //Rest call retrieve response
            var response = _apiController.Get(endpoint, headers);

            //Validate status code OK
            Assert.AreEqual("OK", response.StatusCode.ToString());

            Base.Report.LogResponse(response.Content, CodeLanguage.Json, currentTest);

            //Locating the image url
            JObject json = JObject.Parse(response.Content);
            var imageLocation = json["message"];

            Base.Report.InsertPicture(imageLocation.ToString(), currentTest);
        }
    }

}