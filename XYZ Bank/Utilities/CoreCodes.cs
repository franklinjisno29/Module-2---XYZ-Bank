using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Serilog;
using OpenQA.Selenium.Support.UI;

namespace AirIndia.Utilities
{
    internal class CoreCodes
    {
        Dictionary<string, string>? properties;
        public IWebDriver? driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest? test;
        public void ReadConfigSettings()
        {
            string currDir = Directory.GetParent(@"../../../").FullName; //getting the working directory
            properties = new Dictionary<string, string>();               //declaring the dictionary
            string filename = currDir + "/ConfigSetting/config.properties"; //taking the file from working directory
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)                               //for geting the file data even if there are whitespaces
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }
            }
        }
        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss") + ".html");

            extent.AttachReporter(sparkReporter);
            ReadConfigSettings();
            if (properties["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = properties["baseUrl"];
            driver.Manage().Window.Maximize();
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.Quit();
            extent.Flush();
            Log.CloseAndFlush();
        }
        public bool CheckLinkStatus(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "HEAD";
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            //test = extent.CreateTest(testName);
            if(errorMessage == null)
            {
                Log.Information(testName + " passed");
                test.Pass(result);
            }
            else
            {
                Log.Error($"Test failed for {testName} \n Exception: \n{errorMessage}");
                test.Fail(result);
            }
        }
        public void TakeScreenshot()
        {
            ITakesScreenshot its = (ITakesScreenshot)driver;
            Screenshot screenshot = its.GetScreenshot();
            string currDir = Directory.GetParent(@"../../../").FullName;
            string filePath = currDir + "/Screenshots/ss_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".png";
            screenshot.SaveAsFile(filePath);
            Console.WriteLine("taken screenshot");
            test?.AddScreenCaptureFromPath(filePath);
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
