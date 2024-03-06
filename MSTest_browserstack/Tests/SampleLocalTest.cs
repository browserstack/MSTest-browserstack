using OpenQA.Selenium.Remote;

namespace MSTest_browserstack.Tests
{
    [TestClass]
    public class SampleLocalTest : BrowserStackConfiguration
    {
        protected static RemoteWebDriver? driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = Configure("chrome");
        }

        [TestMethod]
        public void SearchBstackDemo()
        {
            if (driver != null)
            {
                try
                {
                    driver.Navigate().GoToUrl("http://bs-local.com:45454/");
                    StringAssert.Contains("BrowserStack Local", driver.Title);
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
}

