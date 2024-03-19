using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace BrowserStack;

[TestClass]
public class BrowserStackMSTest
{
    protected RemoteWebDriver? driver;
    public BrowserStackMSTest() { }

    [TestInitialize]
    public void Init()
    {
        DriverOptions capability = new OpenQA.Selenium.Chrome.ChromeOptions();

        capability.BrowserVersion = "latest";

        capability.AddAdditionalOption("bstack:options", capability);
        driver = new RemoteWebDriver(
          new Uri("http://localhost:4444/wd/hub/"),
          capability
        );
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver?.Quit();
    }
}
