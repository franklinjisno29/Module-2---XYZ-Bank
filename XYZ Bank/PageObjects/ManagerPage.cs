using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.Page;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirIndia.PageObjects
{
    internal class ManagerPage
    {
        IWebDriver driver;
        public ManagerPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor 
        }

        [FindsBy(How = How.XPath, Using = "//button[contains(@ng-click,'addCust()')]")]
        private IWebElement? AddCustomerButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@ng-model,'fName')]")]
        private IWebElement? FirstNameText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@ng-model,'lName')]")]
        private IWebElement? LastNameText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@ng-model,'postCd')]")]
        private IWebElement? PostCodeText { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'btn-default')]")]
        private IWebElement? AddingButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@ng-click,'openAccount()')]")]
        private IWebElement? OpenAccountButton { get; set; }

        [FindsBy(How = How.Name, Using = "userSelect")]
        private IWebElement? CustomerNameSelect { get; set; }

        [FindsBy(How = How.Id, Using = "currency")]
        private IWebElement? CurrencySelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Process']")]
        private IWebElement? ProcessButton { get; set; }

        public void FillCustomerDetails(string? firstName, string? lastName, string? postcode, string? currency)
        {
            AddCustomerButton?.Click();
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'btn-default')]")));
            FirstNameText?.Click();
            FirstNameText?.SendKeys(firstName);
            LastNameText?.Click();
            LastNameText?.SendKeys(lastName);
            PostCodeText?.Click();
            PostCodeText?.SendKeys(postcode);
            AddingButton?.Click();
            IAlert customerAddedAlert = driver.SwitchTo().Alert();
            customerAddedAlert.Accept();
            OpenAccountButton?.Click();
            IWebElement pageLoadedElements = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Process']")));
            CustomerNameSelect?.Click();
            SelectElement title = new SelectElement(CustomerNameSelect);
            title.SelectByText(firstName+" "+lastName);
            CurrencySelect?.Click();
            SelectElement currencyselect = new SelectElement(CurrencySelect);
            currencyselect.SelectByValue(currency);
            ProcessButton?.Click();
        }
    }
}
