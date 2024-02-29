using OpenQA.Selenium;
using System.Collections.Generic;

namespace MSTest_browserstack;

[TestClass]
public class SingleTest : BrowserStackConfiguration
{
    protected static IWebDriver? driver;

    [TestInitialize]
    public void TestInitialize()
    {
        System.Collections.Generic.Dictionary<string, object> browserstackOptions =
            new Dictionary<string, object>
            {
                { "osVersion", "14" },
                { "deviceName", "iPhone 12" },
                { "realMobile", "true" },
                { "local", "false" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
            };
        driver = ConfigureBrowserStack("safari", browserstackOptions);
    }

    [TestMethod]
    public void SearchBstackDemo()
    {
        RunTest(driver);
    }
}