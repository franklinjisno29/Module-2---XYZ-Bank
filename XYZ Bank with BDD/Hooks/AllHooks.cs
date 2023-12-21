using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Serilog;
using TechTalk.SpecFlow;
using XYZ_Bank_with_BDD.Utilities;

namespace XYZ_Bank_with_BDD.Hooks
{
    [Binding]
    public sealed class AllHooks
    {
        public static Dictionary<string, string>? properties;
        public static IWebDriver? driver;
        public static ExtentReports extent;
        static ExtentSparkReporter sparkReporter;
        public static ExtentTest? test;

        [BeforeFeature(Order = 1)]
        public static void ReadConfigSettings()
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

        [BeforeFeature(Order = 2)]
        public static void InitializeBrowser()
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

        [BeforeFeature(Order = 3)]
        public static void LogFileCreation()
        {
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        [AfterFeature]
        public static void CleanUp()
        {
            driver?.Quit();
            extent.Flush();
            Log.CloseAndFlush();
        }
    }
}