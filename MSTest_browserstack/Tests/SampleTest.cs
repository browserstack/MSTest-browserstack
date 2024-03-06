using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace MSTest_browserstack.Tests
{
    [TestClass]
    public class SampleTest : BrowserStackConfiguration
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
}

