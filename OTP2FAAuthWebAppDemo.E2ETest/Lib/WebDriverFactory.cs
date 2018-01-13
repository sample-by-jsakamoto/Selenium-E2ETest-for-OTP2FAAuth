using System;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace OTP2FAAuthWebAppDemo.E2ETest.Lib
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(IConfiguration testConfig, out DriverService driverService)
        {
            var usingDriver = testConfig["usingDriver"];

            var driverHostName = default(string); // https://github.com/SeleniumHQ/selenium/issues/4988
            var driverOptions = default(DriverOptions);
            var baseDir = Environment.CurrentDirectory;
            switch (usingDriver)
            {
                case "Chrome":
                    driverHostName = "127.0.0.1";
                    driverService = ChromeDriverService.CreateDefaultService(baseDir);
                    driverOptions = new ChromeOptions();
                    break;
                case "Edge":
                    driverHostName = "localhost";
                    driverService = EdgeDriverService.CreateDefaultService(baseDir);
                    driverOptions = new EdgeOptions();
                    break;
                case "Firefox":
                    driverHostName = "127.0.0.1";
                    driverService = FirefoxDriverService.CreateDefaultService(baseDir);
                    driverOptions = new FirefoxOptions();
                    break;
                case "IE":
                    driverHostName = "127.0.0.1";
                    driverService = InternetExplorerDriverService.CreateDefaultService(baseDir);
                    driverOptions = new InternetExplorerOptions();
                    break;
                default:
                    throw new Exception($"Unknown driver \"{usingDriver}\" was specified in configuration.");
            }

            try
            {
                driverService.HideCommandPromptWindow = true;
                driverService.Start();

                var webDriver = new RemoteWebDriver(new Uri($"http://{driverHostName}:{driverService.Port}"), driverOptions);
                try
                {
                    //webDriver.Manage().Window.Maximize();
                    //webDriver.Manage().Window.Position = new Point(x: 426, y: 0);
                    //webDriver.Manage().Window.Size = new Size(width: 854, height: 720);

                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                    return webDriver;
                }
                catch
                {
                    webDriver.Dispose();
                    throw;
                }
            }
            catch
            {
                driverService.Dispose();
                driverService = null;
                throw;
            }
        }
    }
}
