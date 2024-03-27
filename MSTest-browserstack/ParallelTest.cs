namespace MSTest_browserstack;

[TestClass]
[TestCategory("sample-parallel-test")]
public class ParallelTest
{
    [TestMethod]
    [DataRow("ios", "chromium")]
    [DataRow("windows", "chrome")]
    [DataRow("mac", "safari")]
    public void SearchBstackDemo(string profile, string environment)
    {
        var singleTest = new SingleTest(profile, environment);
        singleTest.Init();
        singleTest.SearchBstackDemo();
        singleTest.Cleanup();
    }
}
