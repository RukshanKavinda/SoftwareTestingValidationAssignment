using OpenQA.Selenium;
using CSE2522_Assignment_02_FC222031.Pages;

namespace CSE2522_Assignment_02_FC222031.Tests
{
    /// <summary>
    /// Test class for TC004 - Alerts functionality
    /// Contains only test logic and assertions (NO page actions)
    /// </summary>
    [TestFixture]
    public class AlertTests : BaseTest
    {
        private AlertPage alertPage;

        [SetUp]
        public new void SetUp()
        {
            // Call base SetUp to initialize driver
            base.SetUp();

            // Navigate to Alerts page
            NavigateToPage("alerts");

            // Initialize Page Object
            alertPage = new AlertPage(driver);
        }

        [Test(Description = "TC004_1 - Alerts - Verification of the page")]
        [TestCase(TestName = "TC004_1")]
        public void TC004_1()
        {
            // Assert: Alerts page is displayed
            Assert.That(driver.Url, Does.Contain("alert"), "Alerts page URL is not correct");
            
            // Assert: Alert button is appearing
            Assert.That(alertPage.IsAlertButtonDisplayed(), Is.True, "Alert button is not displayed");
            
            // Assert: Confirm button is appearing
            Assert.That(alertPage.IsConfirmButtonDisplayed(), Is.True, "Confirm button is not displayed");
            
            // Assert: Prompt button is appearing
            Assert.That(alertPage.IsPromptButtonDisplayed(), Is.True, "Prompt button is not displayed");
        }

        [Test(Description = "TC004_2 - Alerts - Alert button verification")]
        [TestCase(TestName = "TC004_2")]
        public void TC004_2()
        {
            // Act: User clicks on the Alert button (delegated to Page Object)
            alertPage.ClickAlertButton();

            // Wait for alert to appear
            IAlert alert = WaitForAlert();

            // Assert: An alert is popping up
            Assert.That(alert, Is.Not.Null, "Alert did not appear after clicking Alert button");

            // Assert: Alert text contains expected message
            string alertText = alert.Text;
            Assert.That(alertText, Does.Contain("working day").IgnoreCase,
                $"Alert text is not correct. Actual: '{alertText}'");

            // Act: The user accepts the alert
            alert.Accept();

            // Wait to ensure alert is closed
            wait.Until(drv => !alertPage.IsAlertPresent());

            // Assert: The alert is closing
            Assert.That(alertPage.IsAlertPresent(), Is.False, "Alert did not close after accepting");
        }

        [Test(Description = "TC004_3 - Alerts - Confirm button verification")]
        [TestCase(TestName = "TC004_3")]
        public void TC004_3()
        {
            // Act: User clicks on the Confirm button (delegated to Page Object)
            alertPage.ClickConfirmButton();

            // Wait for confirm alert to appear
            IAlert confirmAlert = WaitForAlert();

            // Assert: An alert is popping up with the confirm text
            Assert.That(confirmAlert, Is.Not.Null, "Confirm alert did not appear");
            string confirmText = confirmAlert.Text;
            Assert.That(confirmText, Does.Contain("Do you agree").IgnoreCase,
                $"Confirm alert text is not correct. Actual: '{confirmText}'");

            // Act: The user accepts the alert
            confirmAlert.Accept();

            // Wait for second alert
            IAlert yesAlert = WaitForAlert();

            // Assert: Another alert is appearing with the text "Yes"
            Assert.That(yesAlert, Is.Not.Null, "Second alert did not appear after accepting");
            string yesText = yesAlert.Text;
            Assert.That(yesText, Does.Contain("Yes"),
                $"Second alert should contain 'Yes'. Actual: '{yesText}'");

            yesAlert.Accept();
            wait.Until(drv => !alertPage.IsAlertPresent());

            // Now test declining
            // Act: User clicks on the Confirm button again
            alertPage.ClickConfirmButton();
            IAlert confirmAlert2 = WaitForAlert();

            // Act: The user declines the alert
            confirmAlert2.Dismiss();

            // Wait for third alert
            IAlert noAlert = WaitForAlert();

            // Assert: Another alert is appearing with the text "No"
            Assert.That(noAlert, Is.Not.Null, "Third alert did not appear after declining");
            string noText = noAlert.Text;
            Assert.That(noText, Does.Contain("No"),
                $"Third alert should contain 'No'. Actual: '{noText}'");

            noAlert.Accept();
        }

        [Test(Description = "TC004_4 - Alerts - Prompt button verification")]
        [TestCase(TestName = "TC004_4")]
        public void TC004_4()
        {
            string userInput = "TestInput123";

            // Act: User clicks on the Prompt button (delegated to Page Object)
            alertPage.ClickPromptButton();

            // Wait for prompt alert to appear
            IAlert promptAlert = WaitForAlert();

            // Assert: An alert is popping up with the prompt
            Assert.That(promptAlert, Is.Not.Null, "Prompt alert did not appear");

            // Act: User enters a random text to the prompt and accepts
            promptAlert.SendKeys(userInput);
            promptAlert.Accept();

            // Wait for result alert
            IAlert acceptAlert = WaitForAlert();

            // Assert: Another alert is appearing with the user value
            Assert.That(acceptAlert, Is.Not.Null,
                "Second alert did not appear after entering text and accepting");
            string acceptAlertText = acceptAlert.Text;
            Assert.That(acceptAlertText, Does.Contain(userInput),
                $"Alert should contain user input '{userInput}'. Actual: '{acceptAlertText}'");

            acceptAlert.Accept();
            wait.Until(drv => !alertPage.IsAlertPresent());

            // Now test declining
            // Act: User clicks on the Prompt button again
            alertPage.ClickPromptButton();
            IAlert promptAlert2 = WaitForAlert();

            // Act: User enters text and declines the alert
            promptAlert2.SendKeys("AnotherInput");
            promptAlert2.Dismiss();

            // Wait for result alert
            IAlert dismissAlert = WaitForAlert();

            // Assert: Another alert is appearing with "No answer"
            Assert.That(dismissAlert, Is.Not.Null,
                "Third alert did not appear after declining");
            string dismissAlertText = dismissAlert.Text;
            Assert.That(dismissAlertText, Does.Contain("No answer").IgnoreCase,
                $"Alert should contain 'No answer'. Actual: '{dismissAlertText}'");

            dismissAlert.Accept();
        }
    }
}
