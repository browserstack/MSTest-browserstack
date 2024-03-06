# MSTest-browserstack
Sample code for running sessions with MStest on Browserstack using SDK. <img src="assets/browserstack.png" width=30 height=25> <img src="assets/MSTest.png" width=50 height=25> 

> To perform tests using Selenium 3, please checkout the selenium 3 branch

> To perform tests using Selenium 4, please checkout the main branch

[MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) Integration with BrowserStack.

## Setup

### Installation Steps

1. Clone the repository.
2. Open the solution `MSTest_browserstack.sln` in Visual Studio.
3. Install dependencies using NuGet Package Manager:
    ```bash
    dotnet restore
    ```
4. To run tests on private websites,
    - set `browserstackLocal`: `true` at `browserstack.yml`
5. Build the solution

### Running Tests

- To run tests, execute the following command:
    ```bash
    dotnet test
    ```

- To run the single test, execute the following command:
    ```bash
    dotnet test --filter ClassName=MSTest_browserstack.Tests.SampleTest
    ```

- To run local tests, execute the following command:
    ```bash
    dotnet test --filter ClassName=MSTest_browserstack.Tests.SampleLocalTest
    ```

Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/automate/parallel-calculator?ref=github)

## Integrate your test suite

This repository uses the BrowserStack SDK to run tests on BrowserStack. Follow the steps below to install the SDK in your test suite and run tests on BrowserStack:

* Create sample browserstack.yml file with the browserstack related capabilities with your [BrowserStack Username and Access Key](https://www.browserstack.com/accounts/profile/details) and place it in your root folder.
* Add nuget library BrowserStack.TestAdapter
```sh
dotnet add BrowserStack.TestAdapter
```
* Build project `dotnet build`

### Notes

* View your test results on the [BrowserStack automate dashboard](https://www.browserstack.com/automate).
* For testing on different browsers, check the [platform configurator](https://www.browserstack.com/automate/c-sharp#setting-os-and-browser).
* You can export the environment variables for the Username and Access Key of your BrowserStack account

  * For Unix-like or Mac machines:
  ```
  export BROWSERSTACK_USERNAME=<browserstack-username> &&
  export BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Cmd:
  ```
  set BROWSERSTACK_USERNAME=<browserstack-username>
  set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Powershell:
  ```
  $env:BROWSERSTACK_USERNAME=<browserstack-username>
  $env:BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

## Additional Resources

* [Documentation for writing automate test scripts in C#](https://www.browserstack.com/automate/c-sharp).
* [Customizing your tests on BrowserStack](https://www.browserstack.com/automate/capabilities).
* [Browsers & mobile devices for selenium testing on BrowserStack](https://www.browserstack.com/list-of-browsers-and-platforms?product=automate).
* [Using REST API to access information about your tests via the command-line interface](https://www.browserstack.com/automate/rest-api).
