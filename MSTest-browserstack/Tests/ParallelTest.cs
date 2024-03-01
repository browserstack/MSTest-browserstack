using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace MSTest_browserstack;

[TestClass]
public class ParallelTest
{
    [TestMethod]
    public void SearchBstackDemo()
    {
        ParallelTests.RunParallelTests();
    }
}

public class ParallelTests
{
    public static void RunParallelTests()
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
        Thread device1 = new Thread(() => RunTest("safari", browserstackOptions));
        Thread device2 = new Thread(() => RunTest("safari", browserstackOptions));
        Thread device3 = new Thread(() => RunTest("safari", browserstackOptions));
        Thread device4 = new Thread(() => RunTest("safari", browserstackOptions));

        device1.Start();
        device2.Start();
        device3.Start();
        device4.Start();

        device1.Join();
        device2.Join();
        device3.Join();
        device4.Join();
    }

    private static void RunTest(string browser, Dictionary<string, object> browserstackOptions)
    {
        IWebDriver? driver = BrowserStackConfiguration.ConfigureBrowserStack(browser, browserstackOptions);

        BrowserStackConfiguration.RunTest(driver);
    }
}