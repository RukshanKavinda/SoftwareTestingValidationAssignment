using CSE2522_Assignment_02_FC222031.Pages;

namespace CSE2522_Assignment_02_FC222031.Tests
{
    /// <summary>
    /// Test class for TC002 - Sample App functionality
    /// Contains only test logic and assertions (NO page actions)
    /// </summary>
    [TestFixture]
    public class SampleAppTests : BaseTest
    {
        private SampleAppPage sampleAppPage;

        public override void SetUp()
        {
            // Call base SetUp to initialize driver (creates ONE browser)
            base.SetUp();

            // Navigate to Sample App page
            NavigateToPage("sampleapp");

            // Initialize Page Object
            sampleAppPage = new SampleAppPage(driver);
        }

        [Test(Description = "TC002_1 - Sample App - Verification of the sample app page")]
        [TestCase(TestName = "TC002_1")]
        public void TC002_1()
        {
            // Assert: Sample App page is displayed
            Assert.That(driver.Url, Does.Contain("sampleapp"), "Sample App page URL is not correct");
            
            // Assert: User name field is appearing
            Assert.That(sampleAppPage.IsUsernameFieldDisplayed(), Is.True, "Username field is not displayed");
            
            // Assert: Password field is appearing
            Assert.That(sampleAppPage.IsPasswordFieldDisplayed(), Is.True, "Password field is not displayed");
            
            // Assert: Login button is appearing
            Assert.That(sampleAppPage.IsLoginButtonDisplayed(), Is.True, "Login button is not displayed");
        }

        [Test(Description = "TC002_2 - Sample App - Verification of a successful login")]
        [TestCase(TestName = "TC002_2")]
        public void TC002_2()
        {
            // Arrange
            string username = "TestUser";
            string password = "pwd";

            // Act: Perform login action (delegated to Page Object)
            sampleAppPage.Login(username, password);

            // Assert: User welcome message appears
            string statusMessage = sampleAppPage.GetStatusMessage();
            Assert.That(statusMessage, Does.Contain("Welcome").IgnoreCase,
                $"Welcome message not displayed. Actual message: '{statusMessage}'");
            Assert.That(statusMessage, Does.Contain(username).IgnoreCase,
                $"Welcome message does not contain username. Actual message: '{statusMessage}'");
        }

        [Test(Description = "TC002_3 - Sample App - Verification of an unsuccessful login")]
        [TestCase(TestName = "TC002_3")]
        public void TC002_3()
        {
            // Arrange
            string username = "TestUser";
            string incorrectPassword = "wrongpassword";

            // Act: Perform login action with incorrect password (delegated to Page Object)
            sampleAppPage.Login(username, incorrectPassword);

            // Assert: Invalid Username/password message appears
            string statusMessage = sampleAppPage.GetStatusMessage();
            Assert.That(statusMessage, Does.Contain("Invalid").IgnoreCase,
                $"Invalid credentials message not displayed. Actual message: '{statusMessage}'");
        }
    }
}
