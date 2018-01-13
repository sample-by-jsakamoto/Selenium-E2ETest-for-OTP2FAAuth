using System;
using Microsoft.Extensions.Configuration;

namespace OTP2FAAuthWebAppDemo.E2ETest.Lib
{
    internal static class AppSettings
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Environment.CurrentDirectory);
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);

            return builder.Build();
        }
    }
}
