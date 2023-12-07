using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;
using XYZ_Bank_with_BDD.Hooks;
using XYZ_Bank_with_BDD.Utilities;

namespace XYZ_Bank_with_BDD.StepDefinitions
{
    [Binding]
    internal class AddCustomerStep : CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;

        [When(@"User will click on the Manager Login Button")]
        public void WhenUserWillClickOnTheManagerLoginButton()
        {
            var fluentWait = Waits(driver);
            IWebElement ManagerLoginButton = driver.FindElement(By.XPath("//button[contains(@ng-click,'manager()')]"));
            ManagerLoginButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@ng-click,'addCust()')]")));
        }

        [Then(@"Manager Page is loaded in the same page")]
        public void ThenManagerPageIsLoadedInTheSamePage()
        {
            TakeScreenshot(driver);
            try
            {
                Assert.That(driver.Url, Does.Contain("manager"));
                LogTestResult("Add Customer Test", "Manager Page Loaded");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Add Customer Test", "Manager Page Not Loaded", ex.Message);
            }
        }

        [When(@"User Clicks on the Add customer Select Button")]
        public void WhenUserClicksOnTheAddCustomerSelectButton()
        {
            var fluentWait = Waits(driver);
            IWebElement AddCustomerButton = driver.FindElement(By.XPath("//button[contains(@ng-click,'addCust()')]"));
            AddCustomerButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'btn-default')]")));
        }

        [When(@"Fills the Customer Details '([^']*)','([^']*)','([^']*)'")]
        public void WhenFillsTheCustomerDetails(string firstname, string lastname, string postcode)
        {
            IWebElement? FirstNameText = driver?.FindElement(By.XPath("//input[contains(@ng-model,'fName')]"));
            FirstNameText?.Click();
            FirstNameText?.SendKeys(firstname);
            IWebElement? LastNameText = driver?.FindElement(By.XPath("//input[contains(@ng-model,'lName')]"));
            LastNameText?.Click();
            LastNameText?.SendKeys(lastname);
            IWebElement? PostCodeText = driver?.FindElement(By.XPath("//input[contains(@ng-model,'postCd')]"));
            PostCodeText?.Click();
            PostCodeText?.SendKeys(postcode);
        }

        [When(@"Clicks the Add Customer Button")]
        public void WhenClicksTheAddCustomerButton()
        {
            IWebElement? AddingButton = driver?.FindElement(By.XPath("//button[contains(@class,'btn-default')]"));
            AddingButton?.Click();
        }

        [Then(@"Alert appears with Message")]
        public void ThenAlertAppearsWithMessage()
        {
            IAlert? AccountopenAlert = driver?.SwitchTo().Alert();
            string? alertmsg = AccountopenAlert?.Text;
            AccountopenAlert?.Accept();
            TakeScreenshot(driver);
            try
            {
                Assert.That(alertmsg, Does.Contain("Customer added successfully"));
                LogTestResult("Add Customer Test", "Customer Added");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Add Customer Test", "Customer Adding Failed", ex.Message);
            }
        }

        [When(@"Clicks the Open Account select Button")]
        public void WhenClicksTheOpenAccountSelectButton()
        {
            var fluentWait = Waits(driver);
            IWebElement OpenAccountButton = driver.FindElement(By.XPath("//button[contains(@ng-click,'openAccount()')]"));
            OpenAccountButton?.Click();
            IWebElement pageLoadedElements = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Process']")));
        }

        [When(@"Selects the customer name,'([^']*)','([^']*)' and '([^']*)'")]
        public void WhenSelectsTheCustomerNameAnd(string firstName, string lastName, string currency)
        {
            IWebElement? CustomerNameSelect = driver?.FindElement(By.Name("userSelect"));
            CustomerNameSelect?.Click();
            SelectElement title = new SelectElement(CustomerNameSelect);
            title.SelectByText(firstName + " " + lastName);
            IWebElement? CurrencySelect = driver?.FindElement(By.Id("currency"));
            CurrencySelect?.Click();
            SelectElement currencyselect = new SelectElement(CurrencySelect);
            currencyselect.SelectByValue(currency);
        }

        [When(@"Clicks the Process Button")]
        public void WhenClicksTheProcessButton()
        {
            IWebElement? ProcessButton = driver?.FindElement(By.XPath("//button[text()='Process']"));
            ProcessButton?.Click();
        }

        [Then(@"Alert appears with a Message")]
        public void ThenAlertAppearsWithAMessage()
        {
            IAlert? AccountopenAlert = driver?.SwitchTo().Alert();
            string? alertmsg = AccountopenAlert?.Text;
            AccountopenAlert?.Accept();
            TakeScreenshot(driver);
            try
            {
                Assert.That(alertmsg, Does.Contain("Account created successfully"));
                LogTestResult("Add Customer Test", "Customer Added & Account Opened");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Add Customer Test", "Customer Added & Account Opening Failed", ex.Message);
            }
        }
    }
}
