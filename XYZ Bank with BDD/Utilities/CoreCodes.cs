﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using Serilog;
using OpenQA.Selenium.Support.UI;

namespace XYZ_Bank_with_BDD.Utilities
{
    internal class CoreCodes
    {
        protected void TakeScreenshot(IWebDriver driver)
        {
            ITakesScreenshot its = (ITakesScreenshot)driver;
            Screenshot screenshot = its.GetScreenshot();
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string filePath = currDir + "/Screenshots/ss_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".png";
            screenshot.SaveAsFile(filePath);
            Console.WriteLine("taken screenshot");
        }

        protected void LogTestResult(string testName, string result, string? errorMessage = null)
        {
            Log.Information(result);
            if (errorMessage == null)
            {
                Log.Information(testName + "passed");
            }
            else
            {
                Log.Error($"Test failed for {testName} \n Exception: \n{errorMessage}");
            }
        }
        public static DefaultWait<IWebDriver> Waits(IWebDriver driver)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(30);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(150);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "element not found";
            return fluentWait;
        }
    }
}