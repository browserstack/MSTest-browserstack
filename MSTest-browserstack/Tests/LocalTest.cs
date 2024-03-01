using System;
using System.Collections.Generic;
using BrowserStack;
using OpenQA.Selenium;

namespace MSTest_browserstack;

[TestClass]
public class LocalTest : BrowserStackConfiguration
{
    protected static IWebDriver? driver;
    protected static Local? local;

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
                { "browserstack.local", "true" },
                { "build", "browserstack-build-1" },
                { "name", "BStack test" },
                { "browserstack.source", "mstest:sample" }
            };
        local = ConfigureLocalBrowserStack();

        driver = ConfigureBrowserStack("safari", browserstackOptions);
    }

    [TestMethod]
    public void SearchBstackDemo()
    {
        if (driver != null && local != null)
        {
            try
            {
                driver.Navigate().GoToUrl("http://bs-local.com:45454/");

                String page_title = driver.Title;
                if (page_title == "BrowserStack Local")
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Local test ran successful\"}}");
                }
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Issues connecting local\"}}");
            }
            finally
            {
                driver.Quit();
                local.stop();
            }
        }

        else
        {
            Console.WriteLine("Driver or Local is not initialized");
            Assert.Fail();
        }
    }
}
