using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Page Elements (Locators)
        private By UsernameField => By.Id("username");
        private By PasswordField => By.Id("password");
        private By LoginButton => By.Id("loginButton");
        private By ErrorMessage => By.ClassName("error-message");

        // Page Actions (Methods)
        public void EnterUsername(string username)
        {
            driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(LoginButton).Click();
        }

        public string GetErrorMessage()
        {
            return driver.FindElement(ErrorMessage).Text;
        }

        // Combined action method
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public bool IsLoginButtonDisplayed()
        {
            return driver.FindElement(LoginButton).Displayed;
        }
    }
}