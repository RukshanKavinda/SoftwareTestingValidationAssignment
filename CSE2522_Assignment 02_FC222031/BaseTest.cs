using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031
{
    /// <summary>
    /// Base class for all test classes containing common setup and teardown logic
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        /// <summary>
        /// Initializes the WebDriver with SSL certificate error handling
        /// </summary>
        protected void InitializeDriver()
        {
            // Configure ChromeOptions to ignore SSL certificate errors
            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors=yes");
            options.AddArgument("--start-maximized");

            // Initialize WebDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Initialize WebDriverWait with 20 seconds timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        /// <summary>
        /// Navigates to a specific page on the uitestingplayground website
        /// </summary>
        /// <param name="pagePath">The path to the specific page (e.g., "textinput", "sampleapp")</param>
        protected void NavigateToPage(string pagePath)
        {
            driver.Navigate().GoToUrl($"https://uitestingplayground.com/{pagePath}");
        }

        /// <summary>
        /// Cleans up resources and closes the browser
        /// </summary>
        protected void CleanupDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        /// <summary>
        /// Waits for an element to be visible
        /// </summary>
        /// <param name="by">The locator for the element</param>
        /// <returns>The visible element</returns>
        protected IWebElement WaitForElementVisible(By by)
        {
            return wait.Until(drv => 
            {
                var element = drv.FindElement(by);
                return element.Displayed ? element : null;
            });
        }

        /// <summary>
        /// Waits for an alert to be present
        /// </summary>
        /// <returns>The alert</returns>
        protected IAlert WaitForAlert()
        {
            return wait.Until(drv => 
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
    }
}
