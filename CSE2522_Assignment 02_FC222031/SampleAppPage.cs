using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031
{
    public class SampleAppPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public SampleAppPage(IWebDriver driver)
        {
            this.driver = driver;
            // Initialize explicit wait with 10 seconds timeout
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Page Elements - Using simple XPath selectors
        private By UsernameField => By.XPath("//input[@name='UserName']");
        private By PasswordField => By.XPath("//input[@name='Password']");
        private By LoginButton => By.XPath("//button[@id='login']");
        private By StatusLabel => By.Id("loginstatus");

        // Page Actions
        public void EnterUsername(string username)
        {
            driver.FindElement(UsernameField).Clear();
            driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(PasswordField).Clear();
            driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(LoginButton).Click();
        }

        public string GetStatusMessage()
        {
            // Use explicit wait to wait for the status message to appear after login
            var statusElement = wait.Until(drv => 
            {
                try
                {
                    var element = drv.FindElement(StatusLabel);
                    // Wait until the element has text (not empty)
                    return !string.IsNullOrEmpty(element.Text) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            return statusElement?.Text ?? string.Empty;
        }

        public bool IsUsernameFieldDisplayed()
        {
            try
            {
                return driver.FindElement(UsernameField).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsPasswordFieldDisplayed()
        {
            try
            {
                return driver.FindElement(PasswordField).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsLoginButtonDisplayed()
        {
            try
            {
                return driver.FindElement(LoginButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // Combined action method for login
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }
    }
}
