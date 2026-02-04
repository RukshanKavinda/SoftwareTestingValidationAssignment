using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031
{
    /// <summary>
    /// Test class for TC002 - Sample App functionality
    /// Handles TC002_1 through TC002_3: Login UI, successful login, and unsuccessful login
    /// </summary>
    [TestFixture]
    public class SampleAppTests : BaseTest
    {
        private SampleAppPage sampleAppPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver with SSL bypass
            InitializeDriver();

            // Navigate to Sample App page
            NavigateToPage("sampleapp");

            // Initialize Page Object
            sampleAppPage = new SampleAppPage(driver);
        }

        [Test(Description = "TC002_1 - Sample App - Verification of the sample app page")]
        [TestCase(TestName = "TC002_1_VerifySampleAppPageDisplayed")]
        public void TC002_1_VerifySampleAppPageDisplayed()
        {
            // Assert: Sample App page is displayed. User name, Password and Login button are appearing.
            Assert.That(driver.Url, Does.Contain("sampleapp"), "Sample App page URL is not correct");
            Assert.That(sampleAppPage.IsUsernameFieldDisplayed(), Is.True, "Username field is not displayed");
            Assert.That(sampleAppPage.IsPasswordFieldDisplayed(), Is.True, "Password field is not displayed");
            Assert.That(sampleAppPage.IsLoginButtonDisplayed(), Is.True, "Login button is not displayed");
        }

        [Test(Description = "TC002_2 - Sample App - Verification of a successful login")]
        [TestCase(TestName = "TC002_2_VerifySuccessfulLogin")]
        public void TC002_2_VerifySuccessfulLogin()
        {
            // Arrange - The correct password for Sample App is "pwd"
            string username = "TestUser";
            string password = "pwd";

            // Act: User inputs a correct user name and password
            sampleAppPage.Login(username, password);

            // Assert: User welcome message appears (GetStatusMessage has explicit wait)
            string statusMessage = sampleAppPage.GetStatusMessage();
            Assert.That(statusMessage, Does.Contain("Welcome").IgnoreCase, 
                $"Welcome message not displayed. Actual message: '{statusMessage}'");
            Assert.That(statusMessage, Does.Contain(username).IgnoreCase, 
                $"Welcome message does not contain username. Actual message: '{statusMessage}'");
        }

        [Test(Description = "TC002_3 - Sample App - Verification of an unsuccessful login")]
        [TestCase(TestName = "TC002_3_VerifyUnsuccessfulLogin")]
        public void TC002_3_VerifyUnsuccessfulLogin()
        {
            // Arrange - Using incorrect password
            string username = "TestUser";
            string incorrectPassword = "wrongpassword";

            // Act: User inputs a correct user name and an incorrect password
            sampleAppPage.Login(username, incorrectPassword);

            // Assert: Invalid Username/password message appears (GetStatusMessage has explicit wait)
            string statusMessage = sampleAppPage.GetStatusMessage();
            Assert.That(statusMessage, Does.Contain("Invalid").IgnoreCase, 
                $"Invalid credentials message not displayed. Actual message: '{statusMessage}'");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            CleanupDriver();
        }
    }
}
