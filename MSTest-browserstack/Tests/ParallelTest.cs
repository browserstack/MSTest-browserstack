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
                { "osVersion", "14" },
                { "deviceName", "iPhone 12" },
                { "realMobile", "true" },
                { "local", "false" },
                { "buildName", "browserstack-build-1" },
                { "sessionName", "BStack test" },
                { "source", "mstest:sample" }
            };
        Thread device1 = new Thread(() => RunTest("safari", browserstackOptions));
        Thread device2 = new Thread(() => RunTest("chrome", browserstackOptions));
        Thread device3 = new Thread(() => RunTest("firefox", browserstackOptions));
        Thread device4 = new Thread(() => RunTest("safari", browserstackOptions));
        Thread device5 = new Thread(() => RunTest("edge", browserstackOptions));

        device1.Start();
        device2.Start();
        device3.Start();
        device4.Start();
        device5.Start();

        device1.Join();
        device2.Join();
        device3.Join();
        device4.Join();
        device5.Join();
    }

    private static void RunTest(string browser, Dictionary<string, object> browserstackOptions)
    {
        IWebDriver? driver = BrowserStackConfiguration.ConfigureBrowserStack(browser, browserstackOptions);

        SingleTest.RunTest(driver);
    }
}