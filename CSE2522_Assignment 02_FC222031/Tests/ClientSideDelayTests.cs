using CSE2522_Assignment_02_FC222031.Pages;

namespace CSE2522_Assignment_02_FC222031.Tests
{
    /// <summary>
    /// Test class for TC003 - Client Side Delay functionality
    /// Contains only test logic and assertions (NO page actions)
    /// </summary>
    [TestFixture]
    public class ClientSideDelayTests : BaseTest
    {
        private ClientSideDelayPage clientSideDelayPage;

        public override void SetUp()
        {
            // Call base SetUp to initialize driver (creates ONE browser)
            base.SetUp();

            // Navigate to Client Side Delay page
            NavigateToPage("clientdelay");

            // Initialize Page Object
            clientSideDelayPage = new ClientSideDelayPage(driver);
        }

        [Test(Description = "TC003_1 - Client Side Delay - Verification of the page")]
        [TestCase(TestName = "TC003_1")]
        public void TC003_1()
        {
            // Assert: The page is displayed
            Assert.That(driver.Url, Does.Contain("clientdelay"), "Client Side Delay page URL is not correct");
            
            // Assert: Button is appearing to trigger the client side logic
            Assert.That(clientSideDelayPage.IsTriggerButtonDisplayed(), Is.True,
                "Trigger button is not displayed on the page");
        }

        [Test(Description = "TC003_1 - Client Side Delay - Complete workflow")]
        [TestCase(TestName = "TC003_1_Workflow")]
        public void TC003_1_Workflow()
        {
            // Act: User clicks on the button (delegated to Page Object)
            clientSideDelayPage.ClickTriggerButton();

            // Act: Wait for the loading indicator to disappear (delegated to Page Object)
            clientSideDelayPage.WaitForLoadingIndicatorToDisappear();

            // Act: Wait for data banner to appear (delegated to Page Object)
            clientSideDelayPage.WaitForDataBannerToAppear();

            // Assert: A banner is appearing
            Assert.That(clientSideDelayPage.IsDataBannerDisplayed(), Is.True,
                "Data banner did not appear after loading completed");

            // Assert: Banner says "Data calculated on the client side."
            string bannerText = clientSideDelayPage.GetDataBannerText();
            Assert.That(bannerText, Does.Contain("Data calculated on the client side"),
                $"Banner does not contain expected text. Actual text: '{bannerText}'");
        }
    }
}
