using System.Collections.Generic;
using AventStack.ExtentReports;
using Framework.Src.Modules;
using Framework.Src.Tools;
using OpenQA.Selenium;

namespace Framework.WayToAutomation.Pages
{
    public class WebTask : SeleniumUtils
    {
        public WebTask(IWebDriver driver, ExtentTest currentTest) : base(driver, currentTest)
        {
            _curTest = currentTest;
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
            WaitForElement(addUserButton());

            // Retrieves the add user text and takes a screenshot
            string AdduserText = GetText(addUserButton());
           StepPassedScreenShot("Successfully navigated to site" + AdduserText);
            if (CSV)
            {
                var newJSon = dataReader.ConvertCsvFileToJsonObject(fileName);
                fileName = newJSon;
            }

            List<UserModule> user = new DataReaderMethods().ReadFromJson<List<UserModule>>(fileName, CSV);

            foreach (UserModule _user in user)
            {
                string randomNumber = GenerateRandomNumber(100);
                //  Clicks the add user button and waits for form to load
                //  Takes a screenshot once foram loads
                ClickElement(addUserButton());
                WaitForElement(addUserText());

                //Enter the user details into the form
                EnterText(firstNameTextField(), _user.firstName + randomNumber);
                EnterText(lastNameTextField(), _user.lastName + randomNumber);
                EnterText(userNameTextField(), _user.userName + randomNumber);
                EnterText(passwordTextField(), _user.password);
                ClickElement(companyRadioButton(_user.customer));
                SelectTextFromDropdown(roleDropDown(), _user.role);
                EnterText(emailTextField(), _user.email);
                EnterText(mobilephoneTextField(), _user.cell + randomNumber);
                ClickElement(saveButton());

                //Validates that the user is created with a screen shot 
                WaitForElement(userNameInTable(_user.firstName + randomNumber));

                StepPassedScreenShot("Successfully added user" + _user.firstName);
            }

            StepPassedScreenShot("Successfully added users with validations.");
        }
    }
}