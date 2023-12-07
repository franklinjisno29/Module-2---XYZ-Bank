using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.DOM;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class XYZBankHomePage
    {
        IWebDriver driver;

        public XYZBankHomePage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//button[contains(@ng-click,'customer()')]")]
        private IWebElement? CustomerLoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@ng-click,'manager()')]")]
        private IWebElement? ManagerLoginButton { get; set; }

        //Act
        public CustomerPage ClickCustomerLogin()
        {
            CustomerLoginButton?.Click();
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.Id("userSelect")));
            return new CustomerPage(driver);
        }
        public ManagerPage ClickManagerLogin()
        {
            ManagerLoginButton?.Click();
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@ng-click,'addCust()')]")));
            return new ManagerPage(driver);
        }
    }
}
