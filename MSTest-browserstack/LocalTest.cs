using System;
using OpenQA.Selenium;

namespace MSTest_browserstack;

[TestClass]
[TestCategory("sample-local-test")]
public class LocalTest : BrowserStackMSTest
{
    public LocalTest() : base("local", "chromium") { }

    public LocalTest(string profile, string environment) : base(profile, environment) { }

    [TestMethod]
    public void SearchBstackDemo()
    {
        if (driver != null)
        {
            driver.Navigate().GoToUrl("http://bs-local.com:45454/");

            String? page_title = driver.Title;
            if (page_title == "BrowserStack Local")
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Local test ran successful\"}}");
            }
        }
    }
}
