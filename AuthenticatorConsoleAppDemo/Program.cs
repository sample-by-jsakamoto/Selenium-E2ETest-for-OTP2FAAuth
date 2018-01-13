using System;
using OtpNet;
using CommandLineSwitchParser;

class Program
{
    class Options
    {
        public string Key { get; set; }
    }

    static int Main(string[] args)
    {
        if (!CommandLineSwitch.TryParse<Options>(ref args, out var options, out var err))
        {
            Console.WriteLine($"ERROR: {err}");
            return -1;
        }
        if (string.IsNullOrEmpty(options.Key))
        {
            Console.WriteLine("ERROR: Secret Key is not specified.");
            ShowUsage();
            return -1;
        }

        var secretKey = default(byte[]);
        try { secretKey = Base32Encoding.ToBytes(options.Key); }
        catch (ArgumentException)
        {
            Console.WriteLine("ERROR: Invalid key format.");
            return -1;
        }

        var totp = new Totp(secretKey);
        var totpCode = totp.ComputeTotp();

        Console.WriteLine(totpCode);
        return 0;
    }

    private static void ShowUsage()
    {
        Console.WriteLine("Usage: -k <totp secret key>");
    }
}
