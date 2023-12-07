using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class AccountPage
    {
        IWebDriver driver;
        public AccountPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Name, Using = "accountSelect")]
        private IWebElement? SelectAccountButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@ng-click,'deposit()')]")]
        private IWebElement? DepositSelectButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='amount']")]
        private IWebElement? DepositText { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'btn-default')]")]
        private IWebElement? DepositButton { get; set; }

        public void DepositAmount(string accountno, string amount)
        {
            SelectAccountButton?.Click();
            SelectElement selectname = new SelectElement(SelectAccountButton);
            selectname.SelectByValue(accountno);
            DepositSelectButton?.Click();
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@ng-model='amount']")));
            DepositText?.Click();
            DepositText?.SendKeys(amount);
            DepositButton?.Click();
            IWebElement pageLoadedElements = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@ng-show='message']")));
        }
    }
}
