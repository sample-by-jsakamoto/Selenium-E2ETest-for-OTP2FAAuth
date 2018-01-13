using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OTP2FAAuthWebAppDemo.E2ETest.Lib;
using OtpNet;
using Xunit;

namespace OTP2FAAuthWebAppDemo.E2ETest
{
    public class LoginTest
    {
        [Fact(DisplayName = "Pass to 2FA Authentication")]
        public void PassTo2FAAuthentication()
        {
            Console.Clear();
            var testConfig = AppSettings.GetConfiguration().GetSection("test");
            var targetUrl = new Uri(testConfig["targetURL"]);
            var otpKey = Base32Encoding.ToBytes(testConfig["otpKey"]);

            var webDriver = WebDriverFactory.Create(testConfig, out var driverService);
            using (driverService)
            using (webDriver)
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));

                // Open home page.
                StepTrace.Out("Open Home page.");
                webDriver.Navigate().GoToUrl(targetUrl);
                wait.Until(driver => driver.Title.StartsWith("Home page"));

                // Click "Login" to jump to Login page.
                StepTrace.Out("Click \"Login\" to jump to Login page.");
                webDriver.FindElement(By.Id("link-login")).Click();
                wait.Until(driver => driver.Title.StartsWith("Log in"));

                // Enter e-mail and password at Login page, then jump to 2FA Auth page.
                StepTrace.Out("Enter e-mail address.");
                webDriver.FindElement(By.Id("Input_Email")).SendKeys("foo@example.com");
                StepTrace.Out("Enter password.");
                webDriver.FindElement(By.Id("Input_Password")).SendKeys("P@ssw0rd");
                StepTrace.Out("Click \"Login\" button.");
                webDriver.FindElement(By.Id("btn-login")).Click();
                wait.Until(driver => driver.Title.StartsWith("Two-factor authentication"));

                // Compute 2FA Code, 
                var otp = new Totp(otpKey);
                var twoFactorCode = otp.ComputeTotp();
                StepTrace.Out($"Compute tow factor code. (code is: {twoFactorCode})");

                // and enter it, then jump to Home page with username presented.
                StepTrace.Out("Enter tow factor code.");
                webDriver.FindElement(By.Id("Input_TwoFactorCode")).SendKeys(twoFactorCode);
                StepTrace.Out("Click \"Login\" button.");
                webDriver.FindElement(By.Id("btn-login")).Click();

                StepTrace.Out("Validate that the correct username is displayed.");
                wait.Until(driver => driver.Title.StartsWith("Home page"));
                Assert.Contains("foo@example.com", webDriver.FindElement(By.Id("link-username")).Text);

                StepTrace.Out("2FA Authentication is successful!", ConsoleColor.Yellow);
                webDriver.Quit();
            }
        }

    }

    internal static class StepTrace
    {
        public static void Out(string message, ConsoleColor color = ConsoleColor.Cyan)
        {
            // Thread.Sleep(1000);
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine("- " + message);
            Console.ForegroundColor = prev;
            // Thread.Sleep(1000);
        }
    }
}
