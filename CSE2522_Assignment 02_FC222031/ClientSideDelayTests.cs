using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031
{
    /// <summary>
    /// Test class for TC003_1 - Client Side Delay functionality
    /// Handles client-side delay and waiting for loading indicators
    /// </summary>
    [TestFixture]
    public class DelayTests : BaseTest
    {
        private ClientSideDelayPage clientSideDelayPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver with SSL bypass
            InitializeDriver();

            // Navigate to Client Side Delay page
            NavigateToPage("clientdelay");

            // Initialize Page Object
            clientSideDelayPage = new ClientSideDelayPage(driver);
        }

        [Test(Description = "TC003_1 - Client Side Delay - Verification of the page")]
        [TestCase(TestName = "TC003_1_VerifyClientSideDelayPageDisplayed")]
        public void TC003_1_VerifyClientSideDelayPageDisplayed()
        {
            // Assert: The page is displayed. Button is appearing to trigger the client side logic.
            Assert.That(driver.Url, Does.Contain("clientdelay"), "Client Side Delay page URL is not correct");
            Assert.That(clientSideDelayPage.IsTriggerButtonDisplayed(), Is.True, 
                "Trigger button is not displayed on the page");
        }

        [Test(Description = "TC003_1 - Client Side Delay - Complete workflow verification")]
        [TestCase(TestName = "TC003_1_VerifyCompleteClientSideDelayWorkflow")]
        public void TC003_1_VerifyCompleteClientSideDelayWorkflow()
        {
            // Act: User clicks on the button
            clientSideDelayPage.ClickTriggerButton();

            // Wait for the loading indicator to disappear and data banner to appear
            clientSideDelayPage.WaitForLoadingIndicatorToDisappear();
            clientSideDelayPage.WaitForDataBannerToAppear();

            // Assert: A banner is appearing saying "Data calculated on the client side."
            Assert.That(clientSideDelayPage.IsDataBannerDisplayed(), Is.True, 
                "Data banner did not appear after loading completed");
            
            string bannerText = clientSideDelayPage.GetDataBannerText();
            Assert.That(bannerText, Does.Contain("Data calculated on the client side"), 
                $"Banner does not contain expected text. Actual text: '{bannerText}'");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            CleanupDriver();
        }
    }
}
