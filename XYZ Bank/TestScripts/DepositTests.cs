using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.TestScripts
{
    internal class DepositTests : CoreCodes
    {
        [Test, Category("end-to-end Test")]
        [TestCase("2", "number:1004","1000")]  //parameterization done for customer select, account select, amount select

        public void DepositTest(string cId, string accountno, string amount)
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            XYZBankHomePage xyzbank = new XYZBankHomePage(driver);
            Log.Information("Deposit Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@ng-click,'customer()')]")));

            try
            {
                    var customerpage = fluentWait.Until(d => xyzbank.ClickCustomerLogin());
                    var accountpage = customerpage.SelectCustomer(cId);
                    accountpage.DepositAmount(accountno,amount);
                    IWebElement msg = driver.FindElement(By.XPath("//span[@ng-show='message']"));
                    string? msgtext = msg.Text;
                    TakeScreenshot();
                    Assert.That(msgtext,Does.Contain("Deposit Successful"));
                    LogTestResult("Deposit Test Test", "Deposit Test Success");
                    test = extent.CreateTest("Deposit Test - Passed");
                }
                catch (AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("Deposit Test", "Deposit Test Failed", ex.Message);
                    test.Fail("Deposit Test Failed");
                }
        }
        
    }
}
