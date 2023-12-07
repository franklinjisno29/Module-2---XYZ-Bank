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
    internal class CustomerPage
    {
        IWebDriver driver;
        public CustomerPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange
        [FindsBy(How = How.Id, Using = "userSelect")]
        private IWebElement? CustomerSelect { get; set; }

        //Act
        public AccountPage SelectCustomer(string cId)
        {
            CustomerSelect?.Click();
            SelectElement selectname = new SelectElement(CustomerSelect);
            selectname.SelectByValue(cId);
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'btn-default')]")));
            driver.FindElement(By.XPath("//button[contains(@class,'btn-default')]")).Click();
            IWebElement pageLoadedElements = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.Name("accountSelect")));
            return new AccountPage(driver);
        }
    }
}
