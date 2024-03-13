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
        Thread device1 = new Thread(() => RunTest("chromium", new Dictionary<string, object>
            {
                { "osVersion", "14" },
                { "deviceName", "iPhone 12" },
                { "realMobile", "true" },
                { "local", "false" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
            }));

        Thread device2 = new Thread(() => RunTest("chrome", new Dictionary<string, object>
            {
                { "os", "Windows" },
                { "osVersion", "10" },
                { "browserVersion", "latest" },
                { "local", "false" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
            }));

        Thread device3 = new Thread(() => RunTest("safari", new Dictionary<string, object>
            {
                { "os", "OS X" },
                { "osVersion", "Ventura" },
                { "browserVersion", "16.5" },
                { "local", "false" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
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