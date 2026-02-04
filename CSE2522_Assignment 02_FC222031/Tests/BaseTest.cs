using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031.Tests
{
    /// <summary>
    /// Base class for all test classes
    /// Contains common SetUp and TearDown logic with thread-safe driver instances for parallel execution
    /// </summary>
    public class BaseTest
    {
        // ThreadLocal ensures each parallel test gets its own isolated driver instance
        // Suppress NUnit analyzer warnings as we dispose in [TearDown]
#pragma warning disable NUnit1032
        private static ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();
        private static ThreadLocal<WebDriverWait> _wait = new ThreadLocal<WebDriverWait>();
#pragma warning restore NUnit1032

        // Protected properties to access the thread-local driver and wait
        protected IWebDriver driver => _driver.Value;
        protected WebDriverWait wait => _wait.Value;

        [SetUp]
        public virtual void SetUp()  // Added "virtual" to allow overriding in child classes
        {
            // Configure ChromeOptions to ignore SSL certificate errors
            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors=yes");
            options.AddArgument("--start-maximized");

            // Initialize WebDriver with SSL bypass for this thread
            _driver.Value = new ChromeDriver(options);
            _driver.Value.Manage().Window.Maximize();
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Initialize WebDriverWait with 20 seconds timeout for this thread
            _wait.Value = new WebDriverWait(_driver.Value, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            // Safely close and quit the browser even if test fails
            try
            {
                // Check if driver is not null before attempting to quit
                if (_driver.Value != null)
                {
                    _driver.Value.Quit();
                    _driver.Value.Dispose();
                    _driver.Value = null; // Set to null after disposal
                }
            }
            catch (Exception ex)
            {
                // Log the exception but don't fail the test
                Console.WriteLine($"Error during TearDown: {ex.Message}");
            }
        }

        // Helper method to navigate to specific pages
        protected void NavigateToPage(string pagePath)
        {
            if (_driver.Value != null)
            {
                _driver.Value.Navigate().GoToUrl($"https://uitestingplayground.com/{pagePath}");
            }
        }

        // Helper method to wait for alerts
        protected IAlert WaitForAlert()
        {
            if (_wait.Value != null)
            {
                return _wait.Value.Until(drv =>
                {
                    try
                    {
                        return drv.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException)
                    {
                        return null;
                    }
                });
            }
            return null;
        }
    }
}
