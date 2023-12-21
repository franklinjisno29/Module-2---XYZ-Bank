using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZ_Bank.Helpers;

namespace AirIndia.TestScripts
{
    internal class AddCustomerTests : CoreCodes
    {
        [Test, Category("end-to-end Test")]
        public void AddCustomerTest()
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            test = extent.CreateTest("Add Customer Test");
            XYZBankHomePage xyzbank = new XYZBankHomePage(driver);
            Log.Information("Add Customer Test Started");
            test.Info("Add Customer Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@ng-click,'manager()')]")));
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";       //Excel Implementation
            string? sheetName = "Customer";
            List<CustomerData> customerDataList = ExcelUtils.ReadCustomerData(excelFilePath, sheetName);
            foreach (var customerData in customerDataList)
            {
                try
                {
                    string? firstName = customerData?.FirstName;
                    string? lastName = customerData?.LastName;
                    string? postcode = customerData?.PostCode;
                    string? currency = customerData?.Currency;
                    var managerpage = fluentWait.Until(d => xyzbank.ClickManagerLogin());
                    Log.Information("Manager Login Button Clicked");
                    test.Info("Manager Login Button Clicked");
                    managerpage.FillCustomerDetails(firstName, lastName, postcode, currency);
                    Log.Information("Customer Details Filled & Account Opened");
                    test.Info("Customer Details Filled & Account Opened");
                    IAlert AccountopenAlert = driver.SwitchTo().Alert();
                    string alertmsg = AccountopenAlert.Text;
                    AccountopenAlert.Accept();
                    Assert.That(alertmsg, Does.Contain("Account created successfully"));
                    TakeScreenshot();
                    LogTestResult("Add Customer Test", "Add Customer Test Success");
                }
                catch (AssertionException ex)
                {
                    LogTestResult("Add Customer Test", "Add Customer Test Failed", ex.Message);
                }
            }
        }
    }
}
