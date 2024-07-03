namespace BrowserStack
{
    [TestClass]
    [TestCategory("sample-local-test")]
    public class SampleLocalTest : BrowserStackMSTest
    {
        public SampleLocalTest() : base() { }

        [TestMethod]
        public void SearchBstackDemo()
        {
            driver?.Navigate().GoToUrl("http://bs-local.com:45454/");
            StringAssert.Contains("BrowserStack Local", driver?.Title);
        }
    }
}

