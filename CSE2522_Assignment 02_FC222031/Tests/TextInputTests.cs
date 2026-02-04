using CSE2522_Assignment_02_FC222031.Pages;

namespace CSE2522_Assignment_02_FC222031.Tests
{
    /// <summary>
    /// Test class for TC001 - Text Input functionality
    /// Contains only test logic and assertions (NO page actions)
    /// </summary>
    [TestFixture]
    public class TextInputTests : BaseTest
    {
        private TextInputPage textInputPage;

        [SetUp]
        public new void SetUp()
        {
            // Call base SetUp to initialize driver
            base.SetUp();

            // Navigate to Text Input page
            NavigateToPage("textinput");

            // Initialize Page Object
            textInputPage = new TextInputPage(driver);
        }

        [Test(Description = "TC001_1 - Text Input - Verification of the page")]
        [TestCase(TestName = "TC001_1")]
        public void TC001_1()
        {
            // Assert: The text input page is displayed
            Assert.That(driver.Url, Does.Contain("textinput"), "Text Input page URL is not correct");
            
            // Assert: A text box is appearing on the page
            Assert.That(textInputPage.IsInputFieldDisplayed(), Is.True, "Input field is not displayed");
            
            // Assert: A button is appearing on the page
            Assert.That(textInputPage.IsUpdateButtonDisplayed(), Is.True, "Update button is not displayed");
        }

        [Test(Description = "TC001_1 - Text Input - Button text changes after input")]
        [TestCase(TestName = "TC001_1_ButtonTextChange")]
        public void TC001_1_ButtonTextChange()
        {
            // Arrange
            string randomText = "MyNewButtonName" + DateTime.Now.Ticks;

            // Act: User enters a random text to the textbox (delegated to Page Object)
            textInputPage.EnterTextIntoInputField(randomText);
            
            // Act: User presses the button (delegated to Page Object)
            textInputPage.ClickUpdateButton();

            // Assert: The button text is changing to the text entered by the user
            string actualButtonText = textInputPage.GetButtonText();
            Assert.That(actualButtonText, Is.EqualTo(randomText),
                $"Button text did not change. Expected: '{randomText}', Actual: '{actualButtonText}'");
        }
    }
}
