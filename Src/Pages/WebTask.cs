using System.Collections.Generic;
using System.Data;
using AventStack.ExtentReports;
using Framework.Src.Modules;
using Framework.Src.Tools;
using OpenQA.Selenium;

namespace Framework.Src.Pages
{
    public class WebTask 
    {
        
        private SeleniumUtils _seleniumUtils;
        private ExtentTest _curTest;
        private IWebDriver _driver;
        public WebTask(SeleniumUtils seleniumUtils, ExtentTest currentTest)
        {
            _seleniumUtils = seleniumUtils;
            _curTest = currentTest;
            _driver = _seleniumUtils.GetDriver;
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

        public void CreateUser(string DataFile)
        {

            // Waits for the add user button
            _seleniumUtils.WaitForElement(addUserButton(), _curTest);

            // Retrieves the add user text and takes a screenshot
            string AdduserText = _seleniumUtils.GetText(addUserButton(),_curTest);
            Base.Report.StepPassedWithScreenshot("Successfully navigated to site" + AdduserText, _driver, _curTest);

            for (int i = 1; i < 3; i++)
            {
                //  Clicks the add user button and waits for form to load
                //  Takes a screenshot once foram loads
                _seleniumUtils.ClickElement(addUserButton(), _curTest);
                _seleniumUtils.WaitForElement(addUserText(), _curTest);

                // Generates a random number between 0 - 100 and adds it to the users creds
                string randomNumber = _seleniumUtils.GenerateRandomNumber(100, _curTest);

                //Reads from CSV
                DataTable CSVData = dataReader.readCSVfile(DataFile);

                string firstName = dataReader.RetrieveDataFromDataTable(CSVData, "FirstName", i - 1) + randomNumber;
                string lastName = dataReader.RetrieveDataFromDataTable(CSVData, "LastName", i - 1) + randomNumber;
                string userName = dataReader.RetrieveDataFromDataTable(CSVData, "UserName", i - 1) + randomNumber;
                string password = dataReader.RetrieveDataFromDataTable(CSVData, "Password", i - 1) + randomNumber;
                string email = dataReader.RetrieveDataFromDataTable(CSVData, "Email", i - 1).ToString();
                string cell = dataReader.RetrieveDataFromDataTable(CSVData, "Cell", i - 1).ToString();
                string role = dataReader.RetrieveDataFromDataTable(CSVData, "Role", i - 1).ToString();
                string customer = dataReader.RetrieveDataFromDataTable(CSVData, "Customer", i - 1).ToString();

                //Enter the user details into the form
                _seleniumUtils.EnterText(firstNameTextField(), firstName, _curTest);
                _seleniumUtils.EnterText(lastNameTextField(), lastName, _curTest);
                _seleniumUtils.EnterText(userNameTextField(), userName, _curTest);
                _seleniumUtils.EnterText(passwordTextField(), password, _curTest);
                _seleniumUtils.ClickElement(companyRadioButton(customer), _curTest);
                _seleniumUtils.SelectTextFromDropdown(roleDropDown(), role, _curTest);
                _seleniumUtils.EnterText(emailTextField(), email, _curTest);
                _seleniumUtils.EnterText(mobilephoneTextField(), cell, _curTest);

                _seleniumUtils.ClickElement(saveButton(), _curTest);

                //Validates that the user is created with a screen shot 
                _seleniumUtils.WaitForElement(userNameInTable(firstName), _curTest);

                Base.Report.StepPassed("Successfully added user" + firstName, _curTest);
            }

            Base.Report.StepPassedWithScreenshot("Successfully added users with validations.", _driver, _curTest);
        }

        public void CreateUserWithJson(string DataFile)
        {
            // Waits for the add user button
            _seleniumUtils.WaitForElement(addUserButton(),_curTest);

            // Retrieves the add user text and takes a screenshot
            string AdduserText = _seleniumUtils.GetText(addUserButton(),_curTest);
            Base.Report.StepPassedWithScreenshot("Successfully navigated to site" + AdduserText, _driver,_curTest);

            for (int i = 0; i < 2; i++)
            {
                string randomNumber = _seleniumUtils.GenerateRandomNumber(100,_curTest);
                List<UserModule> user = new DataReaderMethods().ReadFromJson(DataFile);

                string firstName = user[i].firstName + randomNumber;
                string lastName = user[i].lastName + randomNumber;
                string userName = user[i].userName + randomNumber;
                string role = user[i].role;
                string customer = user[i].customer;
                string password = user[i].password + randomNumber;
                string email = user[i].email;
                string cell = user[i].cell + randomNumber;

                //  Clicks the add user button and waits for form to load
                //  Takes a screenshot once foram loads
                _seleniumUtils.ClickElement(addUserButton(),_curTest);
                _seleniumUtils.WaitForElement(addUserText(),_curTest);

                //Enter the user details into the form
                _seleniumUtils.EnterText(firstNameTextField(), firstName,_curTest);
                _seleniumUtils.EnterText(lastNameTextField(), lastName,_curTest);
                _seleniumUtils.EnterText(userNameTextField(), userName,_curTest);
                _seleniumUtils.EnterText(passwordTextField(), password,_curTest);
                _seleniumUtils.ClickElement(companyRadioButton(customer),_curTest);
                _seleniumUtils.SelectTextFromDropdown(roleDropDown(), role,_curTest);
                _seleniumUtils.EnterText(emailTextField(), email,_curTest);
                _seleniumUtils.EnterText(mobilephoneTextField(), cell,_curTest);
                _seleniumUtils.ClickElement(saveButton(),_curTest);

                //Validates that the user is created with a screen shot 
                _seleniumUtils.WaitForElement(userNameInTable(firstName),_curTest);

                Base.Report.StepPassed("Successfully added user" + user[i].firstName,_curTest);
            }

            Base.Report.StepPassedWithScreenshot("Successfully added users with validations.", _driver,_curTest);
        }
    }
}