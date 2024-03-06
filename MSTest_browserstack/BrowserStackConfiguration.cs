using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace MSTest_browserstack;

public class BrowserStackConfiguration
{
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

    public static RemoteWebDriver Configure(String browser)
    {
        DriverOptions capability = GetBrowserOption(browser);

        capability.BrowserVersion = "latest";

        capability.AddAdditionalOption("bstack:options", capability);
        RemoteWebDriver driver = new RemoteWebDriver(
          new Uri("http://localhost:4444/wd/hub/"),
          capability
        );

        return driver;
    }

}
