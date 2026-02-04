using OpenQA.Selenium;

namespace CSE2522_Assignment_02_FC222031
{
    public class AlertsPage
    {
        private IWebDriver driver;

        public AlertsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Page Elements
        private By AlertButton => By.Id("alertButton");
        private By ConfirmButton => By.Id("confirmButton");
        private By PromptButton => By.Id("promptButton");

        // Page Actions
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

        // Alert Handling Methods
        public string GetAlertText()
        {
            IAlert alert = driver.SwitchTo().Alert();
            return alert.Text;
        }

        public void AcceptAlert()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void DismissAlert()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public void EnterTextInPromptAndAccept(string text)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(text);
            alert.Accept();
        }

        public void EnterTextInPromptAndDismiss(string text)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(text);
            alert.Dismiss();
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
