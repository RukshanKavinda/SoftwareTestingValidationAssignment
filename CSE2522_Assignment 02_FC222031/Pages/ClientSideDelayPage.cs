using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031.Pages
{
    /// <summary>
    /// Page Object for Client Side Delay page
    /// Contains only WebElements and action methods
    /// </summary>
    public class ClientSideDelayPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ClientSideDelayPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // WebElements - Locators only
        private By TriggerButton => By.XPath("//button[contains(@class,'btn') and not(@disabled)]");
        private By LoadingIndicator => By.XPath("//div[@id='spinner' or contains(@class,'spinner')]");
        private By DataBanner => By.XPath("//p[contains(@class,'bg-success') or contains(text(),'Data calculated')]");

        // Page Actions - Methods only (NO assertions)
        public void ClickTriggerButton()
        {
            driver.FindElement(TriggerButton).Click();
        }

        public bool IsTriggerButtonDisplayed()
        {
            try
            {
                return driver.FindElement(TriggerButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsDataBannerDisplayed()
        {
            try
            {
                return driver.FindElement(DataBanner).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetDataBannerText()
        {
            return driver.FindElement(DataBanner).Text;
        }

        public void WaitForLoadingIndicatorToDisappear()
        {
            wait.Until(drv =>
            {
                try
                {
                    IWebElement spinner = drv.FindElement(LoadingIndicator);
                    return !spinner.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });
        }

        public void WaitForDataBannerToAppear()
        {
            wait.Until(drv =>
            {
                try
                {
                    IWebElement banner = drv.FindElement(DataBanner);
                    return banner.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
    }
}
