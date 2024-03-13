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
        Thread device1 = new Thread(() => RunTest("safari", new Dictionary<string, object>
            {
                { "os_version", "14" },
                { "device", "iPhone 12" },
                { "realMobile", "true" },
                { "browserstack.local", "false" },
                { "build", "browserstack-build-1" },
                { "name", "BStack test" },
                { "browserstack.source", "mstest:sample" }
            }));

        Thread device2 = new Thread(() => RunTest("chrome", new Dictionary<string, object>
            {
                { "os", "Windows" },
                { "os_version", "10" },
                { "browser_version", "latest" },
                { "browserstack.local", "false" },
                { "build", "browserstack-build-1" },
                { "name", "BStack test" },
                { "browserstack.source", "mstest:sample" }
            }));

        Thread device3 = new Thread(() => RunTest("safari", new Dictionary<string, object>
            {
                { "os", "OS X" },
                { "os_version", "Ventura" },
                { "browser_version", "16.5" },
                { "browserstack.local", "false" },
                { "build", "browserstack-build-1" },
                { "name", "BStack test" },
                { "browserstack.source", "mstest:sample" }
            }));

        device1.Start();
        device2.Start();
        device3.Start();

        device1.Join();
        device2.Join();
        device3.Join();
    }

    private static void RunTest(string browser, Dictionary<string, object> browserstackOptions)
    {
        IWebDriver? driver = BrowserStackConfiguration.ConfigureBrowserStack(browser, browserstackOptions);

        SingleTest.RunTest(driver);
    }
}