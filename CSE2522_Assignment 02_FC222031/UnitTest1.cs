
using OpenQA.Selenium;

namespace CSE2522_Assignment_02_FC222031
{
    /// <summary>
    /// Test class for TC001_1 - Text Input functionality
    /// Verifies text input and button text change behavior
    /// </summary>
    [TestFixture]
    public class TextInputTests : BaseTest
    {
        private TextInputPage textInputPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver with SSL bypass
            InitializeDriver();

            // Navigate to Text Input page
            NavigateToPage("textinput");

            // Initialize Page Object
            textInputPage = new TextInputPage(driver);
        }

        [Test(Description = "TC001_1 - Text Input - Verification of the page")]
        [TestCase(TestName = "TC001_1_VerifyTextInputPageDisplayed")]
        public void TC001_1_VerifyTextInputPageDisplayed()
        {
            // Assert: The text input page is displayed. A text box and a button is appearing on the page.
            Assert.That(driver.Url, Does.Contain("textinput"), "Text Input page URL is not correct");
            Assert.That(textInputPage.IsInputFieldDisplayed(), Is.True, "Input field is not displayed");
            Assert.That(textInputPage.IsUpdateButtonDisplayed(), Is.True, "Update button is not displayed");
        }

        [Test(Description = "TC001_1 - Text Input - Button text changes after input")]
        [TestCase(TestName = "TC001_1_ButtonTextChangesAfterInput")]
        public void TC001_1_ButtonTextChangesAfterInput()
        {
            // Arrange
            string randomText = "MyNewButtonName" + DateTime.Now.Ticks;

            // Act: User enters a random text to the textbox and presses the button
            textInputPage.EnterTextIntoInputField(randomText);
            textInputPage.ClickUpdateButton();

            // Assert: The button text is changing to the text entered by the user
            string actualButtonText = textInputPage.GetButtonText();
            Assert.That(actualButtonText, Is.EqualTo(randomText), 
                $"Button text did not change to the expected value. Expected: '{randomText}', Actual: '{actualButtonText}'");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            CleanupDriver();
        }
    }
}
