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
                { "osVersion", "14" },
                { "deviceName", "iPhone 12" },
                { "realMobile", "true" },
                { "local", "true" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
            };
        local = ConfigureLocalBrowserStack();

        driver = ConfigureBrowserStack("chromium", browserstackOptions);
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
