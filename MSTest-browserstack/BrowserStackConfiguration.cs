using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using BrowserStack;

namespace MSTest_browserstack;

public class BrowserStackConfiguration
{
    protected static string? username;
    protected static string? accesskey;

    public static DriverOptions GetBrowserOption(String browser)
    {
        return browser switch
        {
            "chrome" => new OpenQA.Selenium.Chrome.ChromeOptions(),
            "firefox" => new OpenQA.Selenium.Firefox.FirefoxOptions(),
            "safari" => new OpenQA.Selenium.Safari.SafariOptions(),
            "edge" => new OpenQA.Selenium.Edge.EdgeOptions(),
            _ => new OpenQA.Selenium.Chrome.ChromeOptions(),
        };
    }

    public static IWebDriver? ConfigureBrowserStack(string browser, Dictionary<string, object> browserstackOptions)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<BrowserStackConfiguration>()
            .Build();

        username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
        accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

        if (username == null || accesskey == null)
        {
            username = config["username"];
            accesskey = config["access_key"];
        }

        if (username == null || accesskey == null)
        {
            Console.WriteLine("Invalid Credentials");
            return null;
        }

        DriverOptions capability = GetBrowserOption(browser);

        if (browserstackOptions != null)
        {
            browserstackOptions["browserstack.user"] = username;
            browserstackOptions["browserstack.key"] = accesskey;
            foreach (var entry in browserstackOptions)
            {
                capability.AddAdditionalCapability(entry.Key, entry.Value);
            }
        }

        IWebDriver driver = new RemoteWebDriver(
            new Uri("https://hub.browserstack.com/wd/hub/"),
            capability
        );

        return driver;
    }

    public static Local? ConfigureLocalBrowserStack()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<BrowserStackConfiguration>()
            .Build();

        string? key = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

        if (key == null)
        {
            key = config["access_key"];
        }

        Local local = new Local();

        List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("key", key)
        };

        try
        {
           local.start(bsLocalArgs);
        }

        catch
        {
            local.stop();
        }

        return local;
    }
}
