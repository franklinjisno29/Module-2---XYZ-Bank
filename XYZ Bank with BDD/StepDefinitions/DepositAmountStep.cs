using XYZ_Bank_with_BDD.Hooks;
using XYZ_Bank_with_BDD.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Security.Cryptography;
using TechTalk.SpecFlow;
using XYZ_Bank_with_BDD.Utilities;
using SeleniumExtras.WaitHelpers;

namespace XYZ_Bank_with_BDD.StepDefinitions
{
    [Binding]
    internal class DepositAmountStep : CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;

        [Given(@"User will be on Homepage")]
        public void GivenUserWillBeOnHomepage()
        {
            var fluentWait = Waits(driver);
            driver.Url = "https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login";
            driver.Manage().Window.Maximize();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@ng-click,'customer()')]")));
        }

        [When(@"User will click on the Customer Login Button")]
        public void WhenUserWillClickOnTheCustomerLoginButton()
        {
            var fluentWait = Waits(driver);
            IWebElement custLoginButton = driver.FindElement(By.XPath("//button[contains(@ng-click,'customer()')]"));
            custLoginButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("userSelect")));
        }

        [Then(@"Customer Page is loaded in the same page")]
        public void ThenCustomerPageIsLoadedInTheSamePage()
        {
            TakeScreenshot(driver);
            try
            {
                Assert.That(driver.Url, Does.Contain("customer"));
                LogTestResult("Deposit Amount Test", "Customer Page Is Loaded");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Deposit Amount Test", "Customer Page not Loaded", ex.Message);
            }
        }

        [When(@"User selects a '([^']*)' from the list")]
        public void WhenUserSelectsAFromTheList(string cId)
        {
            IWebElement? CustomerSelect = driver?.FindElement(By.Id("userSelect"));
            CustomerSelect?.Click();
            SelectElement selectname = new SelectElement(CustomerSelect);
            selectname.SelectByValue(cId);
        }

        [When(@"Clicks the Login Button")]
        public void WhenClicksTheLoginButton()
        {
            var fluentWait = Waits(driver);
            IWebElement LoginButton = driver.FindElement(By.XPath("//button[contains(@class,'btn-default')]"));
            LoginButton?.Click();
            IWebElement pageLoadedElements = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Name("accountSelect")));

        }

        [Then(@"Account Page is loaded in the same page")]
        public void ThenAccountPageIsLoadedInTheSamePage()
        {
            TakeScreenshot(driver);
            try
            {
                Assert.That(driver.Url, Does.Contain("account"));
                LogTestResult("Deposit Amount Test", "Account Page Is Loaded");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Deposit Amount Test", "Account Page not Loaded", ex.Message);
            }
        }

        [When(@"User select the '([^']*)'")]
        public void WhenUserSelectThe(string accountno)
        {
            IWebElement? SelectAccountButton = driver?.FindElement(By.Name("accountSelect"));
            SelectAccountButton?.Click();
            SelectElement selectname = new SelectElement(SelectAccountButton);
            selectname.SelectByValue(accountno);
        }

        [When(@"Clicks the Deposit select Button")]
        public void WhenClicksTheDepositSelectButton()
        {
            var fluentWait = Waits(driver);
            IWebElement DepositSelectButton = driver.FindElement(By.XPath("//button[contains(@ng-click,'deposit()')]"));
            DepositSelectButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@ng-model='amount']")));

        }

        [When(@"Types the '([^']*)' to Deposit")]
        public void WhenTypesTheToDeposit(string amount)
        {
            IWebElement? DepositText = driver?.FindElement(By.XPath("//input[@ng-model='amount']"));
            DepositText?.Click();
            DepositText?.SendKeys(amount);
        }

        [When(@"Clicks the Deposit Button")]
        public void WhenClicksTheDepositButton()
        {
            var fluentWait = Waits(driver);
            IWebElement DepositButton = driver.FindElement(By.XPath("//button[contains(@class,'btn-default')]"));
            DepositButton?.Click();
            IWebElement pageLoadedElements = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@ng-show='message']")));
        }

        [Then(@"Success Message Appears")]
        public void ThenSuccessMessageAppears()
        {
            IWebElement? msg = driver?.FindElement(By.XPath("//span[@ng-show='message']"));
            string? msgtext = msg?.Text;
            TakeScreenshot(driver);
            try
            {
                Assert.That(msgtext, Does.Contain("Deposit Successful"));
                LogTestResult("Deposit Amount Test", "Deposited Amount");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Deposit Amount Test", "Depositing Amount Failed", ex.Message);
            }
        }
    }
}
