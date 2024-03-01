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
                { "browser", "iPhone" },
                { "os_version", "14" },
                { "device", "iPhone 12" },
                { "realMobile", "true" },
                { "browserstack.local", "false" },
                { "build", "browserstack-build-1" },
                { "name", "BStack test" },
                { "browserstack.source", "mstest:sample" }
            };
        driver = ConfigureBrowserStack("safari", browserstackOptions);
    }

    [TestMethod]
    public void SearchBstackDemo()
    {
        RunTest(driver);
    }
}