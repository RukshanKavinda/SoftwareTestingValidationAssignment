using OpenQA.Selenium;

namespace CSE2522_Assignment_02_FC222031.Pages
{
    /// <summary>
    /// Page Object for Text Input page
    /// Contains only WebElements and action methods
    /// </summary>
    public class TextInputPage
    {
        private IWebDriver driver;

        public TextInputPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // WebElements - Locators only
        private By InputField => By.XPath("//input[@type='text']");
        private By UpdateButton => By.XPath("//button[contains(@class,'btn')]");

        // Page Actions - Methods only (NO assertions)
        public void EnterTextIntoInputField(string text)
        {
            driver.FindElement(InputField).Clear();
            driver.FindElement(InputField).SendKeys(text);
        }

        public void ClickUpdateButton()
        {
            driver.FindElement(UpdateButton).Click();
        }

        public string GetButtonText()
        {
            return driver.FindElement(UpdateButton).Text;
        }

        public bool IsInputFieldDisplayed()
        {
            try
            {
                return driver.FindElement(InputField).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsUpdateButtonDisplayed()
        {
            try
            {
                return driver.FindElement(UpdateButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
