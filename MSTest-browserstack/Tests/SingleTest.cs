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
        driver = ConfigureBrowserStack("chromium", browserstackOptions);
    }

    [TestMethod]
    public void SearchBstackDemo()
    {
        RunTest(driver);
    }

    public static void RunTest(IWebDriver? driver)
    {
        if (driver != null)
        {
            try
            {
                driver.Navigate().GoToUrl("https://bstackdemo.com/");
                Assert.AreEqual("StackDemo", driver.Title);
                Console.WriteLine(driver.Title);
                string productOnPageText = driver.FindElement(By.XPath("//*[@id=\"1\"]/p")).Text;

                driver.FindElement(By.XPath("//*[@id=\"1\"]/div[4]")).Click();
                bool cartOpened = driver.FindElement(By.XPath("//*[@class=\"float-cart__content\"]")).Displayed;
                Assert.AreEqual(cartOpened, true);
                string productOnCartText = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div/div[2]/div[2]/div[2]/div/div[3]/p[1]")).Text;
                Assert.AreEqual(productOnCartText, productOnPageText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }
        else
        {
            Console.WriteLine("Driver is not initialized");
            Assert.Fail();
        }
    }
}