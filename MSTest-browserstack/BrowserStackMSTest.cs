using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using BrowserStack;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MSTest_browserstack;

[TestClass]
public class BrowserStackMSTest
{
    protected IWebDriver? driver;
    protected string? profile;
    protected string? environment;
    private Local? browserStackLocal;

    public BrowserStackMSTest(string profile, string environment)
    {
        this.profile = profile;
        this.environment = environment;
    }

    public static DriverOptions GetBrowserOption(String? browser)
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

    [TestInitialize]
    public void Init()
    {
        string? currentDirectory = GoUpLevels(Directory.GetCurrentDirectory(), 3);

        string configFile = profile + ".conf.json";

        if (profile != null && currentDirectory != null)
        {
            string path = Path.Combine(currentDirectory, configFile);

            JObject configuration = JObject.Parse(File.ReadAllText(path));
            if (configuration is null)
                throw new Exception("Configuration not found!");

            // Get Environment specific capabilities
            JObject? capabilitiesJsonArr = configuration.GetValue("environments") as JObject;

            if (capabilitiesJsonArr is null)
                throw new Exception("Environments not found!");

            JObject? capabilities = capabilitiesJsonArr.GetValue(environment) as JObject;

            if (capabilities is null)
                throw new Exception("Capabilities not initialised!");

            // Get Common Capabilities
            JObject? commonCapabilities = configuration.GetValue("capabilities") as JObject;

            // Merge Capabilities
            capabilities.Merge(commonCapabilities);

            // Get username and accesskey
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<BrowserStackMSTest>()
            .Build();

            string? username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            string? accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

            if (username == null || accesskey == null)
            {
                username = config["username"];
                accesskey = config["access_key"];
            }

            if (username == null || accesskey == null)
            {
                throw new Exception("Invalid Credentials");
            }

            dynamic capability = GetBrowserOption(environment);

            capabilities["browserstack.user"] = username;
            capabilities["browserstack.key"] = accesskey;
            foreach (var entry in capabilities)
            {
                if (environment == "edge" || environment == "safari")
                {
                    capability.AddAdditionalCapability(entry.Key, entry.Value);
                }
                else
                {
                    capability.AddAdditionalCapability(entry.Key, entry.Value, true);
                }
            }

            if (profile == "local")
            {
                string? key = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

                if (key == null)
                {
                    key = config["access_key"];
                }

                browserStackLocal = new Local();

                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("key", key)
                };

                browserStackLocal.start(bsLocalArgs);
            }

            driver = new RemoteWebDriver(
            new Uri("https://hub.browserstack.com/wd/hub/"),
            capability
            );
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver?.Quit();
        if (browserStackLocal != null)
        {
            browserStackLocal.stop();
        }
    }

    private static string GoUpLevels(string path, int levels)
    {
        // Combine with ".." for each level to go up
        string newPath = path;
        for (int i = 0; i < levels; i++)
        {
            newPath = Path.Combine(newPath, "..");
        }

        // Get the full path after going up the specified levels
        return Path.GetFullPath(newPath);
    }
}
