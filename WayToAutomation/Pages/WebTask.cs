using System.Collections.Generic;
using AventStack.ExtentReports;
using Framework.Src.Modules;
using Framework.Src.Tools;
using OpenQA.Selenium;

namespace Framework.WayToAutomation.Pages
{
    public class WebTask : SeleniumUtils
    {
        private ExtentTest _curTest;
        private IWebDriver _driver;
        public WebTask(IWebDriver driver, ExtentTest currentTest) : base(driver, currentTest)
        {
            _curTest = currentTest;
            _driver = driver;
        }

        DataReaderMethods dataReader = new DataReaderMethods();

        //By selectors with xpaths
        private By addUserButton() { return By.XPath(@"//button[contains(text(),'Add User')]"); }
        private By addUserText() { return By.XPath(@"//h3[contains(text(),'Add User')]"); }
        private By firstNameTextField() { return By.XPath(@"//input[@name='FirstName']"); }
        private By lastNameTextField() { return By.XPath(@"//input[@name='LastName']"); }
        private By userNameTextField() { return By.XPath(@"//input[@name='UserName']"); }
        private By passwordTextField() { return By.XPath(@"//input[@name='Password']"); }
        private By companyRadioButton(string company) { return By.XPath(@"//label[text()='" + company + "']"); }
        private By roleDropDown() { return By.XPath(@"//select[@name='RoleId']"); }
        private By emailTextField() { return By.XPath(@"//input[@name='Email']"); }
        private By mobilephoneTextField() { return By.XPath(@"//input[@name='Mobilephone']"); }
        private By saveButton() { return By.XPath(@"//button[@ng-click='save(user)']"); }
        private By userNameInTable(string value) { return By.XPath(@"//td[text()='" + value + "']"); }

        public void CreateUser(string fileName, bool CSV = true)
        {
            // Waits for the add user button
            WaitForElement(addUserButton(), _curTest);

            // Retrieves the add user text and takes a screenshot
            string AdduserText = GetText(addUserButton(), _curTest);
            Base.Report.StepPassedWithScreenshot("Successfully navigated to site" + AdduserText, _driver, _curTest);
            if (CSV)
            {
                var newJSon = dataReader.ConvertCsvFileToJsonObject(fileName);
                fileName = newJSon;
            }

            List<UserModule> user = new DataReaderMethods().ReadFromJson<List<UserModule>>(fileName, CSV);

            foreach (UserModule _user in user)
            {
                string randomNumber = GenerateRandomNumber(100, _curTest);
                //  Clicks the add user button and waits for form to load
                //  Takes a screenshot once foram loads
                ClickElement(addUserButton(), _curTest);
                WaitForElement(addUserText(), _curTest);

                //Enter the user details into the form
                EnterText(firstNameTextField(), _user.firstName + randomNumber, _curTest);
                EnterText(lastNameTextField(), _user.lastName + randomNumber, _curTest);
                EnterText(userNameTextField(), _user.userName + randomNumber, _curTest);
                EnterText(passwordTextField(), _user.password, _curTest);
                ClickElement(companyRadioButton(_user.customer), _curTest);
                SelectTextFromDropdown(roleDropDown(), _user.role, _curTest);
                EnterText(emailTextField(), _user.email, _curTest);
                EnterText(mobilephoneTextField(), _user.cell + randomNumber, _curTest);
                ClickElement(saveButton(), _curTest);

                //Validates that the user is created with a screen shot 
                WaitForElement(userNameInTable(_user.firstName + randomNumber), _curTest);

                Base.Report.StepPassed("Successfully added user" + _user.firstName, _curTest);
            }

            Base.Report.StepPassedWithScreenshot("Successfully added users with validations.", _driver, _curTest);
        }
    }
}