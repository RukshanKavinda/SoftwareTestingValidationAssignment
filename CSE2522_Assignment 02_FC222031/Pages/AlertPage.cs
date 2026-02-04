using OpenQA.Selenium;

namespace CSE2522_Assignment_02_FC222031.Pages
{
    /// <summary>
    /// Page Object for Alerts page
    /// Contains only WebElements and action methods
    /// </summary>
    public class AlertPage
    {
        private IWebDriver driver;

        public AlertPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // WebElements - Locators only
        private By AlertButton => By.Id("alertButton");
        private By ConfirmButton => By.Id("confirmButton");
        private By PromptButton => By.Id("promptButton");

        // Page Actions - Methods only (NO assertions)
        public void ClickAlertButton()
        {
            driver.FindElement(AlertButton).Click();
        }

        public void ClickConfirmButton()
        {
            driver.FindElement(ConfirmButton).Click();
        }

        public void ClickPromptButton()
        {
            driver.FindElement(PromptButton).Click();
        }

        public bool IsAlertButtonDisplayed()
        {
            try
            {
                return driver.FindElement(AlertButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsConfirmButtonDisplayed()
        {
            try
            {
                return driver.FindElement(ConfirmButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsPromptButtonDisplayed()
        {
            try
            {
                return driver.FindElement(PromptButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }
}
