using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSE2522_Assignment_02_FC222031
{
    /// <summary>
    /// Test class for TC004 - Alerts functionality
    /// Handles TC004_1 through TC004_4: Browser alerts, confirmation boxes, and prompt inputs
    /// </summary>
    [TestFixture]
    public class AlertsTests : BaseTest
    {
        private AlertsPage alertsPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver with SSL bypass
            InitializeDriver();

            // Navigate to Alerts page (note: the URL is "alerts" not "alert")
            NavigateToPage("alerts");

            // Initialize Page Object
            alertsPage = new AlertsPage(driver);
        }

        [Test(Description = "TC004_1 - Alerts - Verification of the page")]
        [TestCase(TestName = "TC004_1_VerifyAlertsPageDisplayed")]
        public void TC004_1_VerifyAlertsPageDisplayed()
        {
            // Assert: Alerts page is displayed. Alert, confirm and prompt buttons appearing.
            Assert.That(driver.Url, Does.Contain("alert"), "Alerts page URL is not correct");
            Assert.That(alertsPage.IsAlertButtonDisplayed(), Is.True, "Alert button is not displayed");
            Assert.That(alertsPage.IsConfirmButtonDisplayed(), Is.True, "Confirm button is not displayed");
            Assert.That(alertsPage.IsPromptButtonDisplayed(), Is.True, "Prompt button is not displayed");
        }

        [Test(Description = "TC004_2 - Alerts - Alert button verification")]
        [TestCase(TestName = "TC004_2_VerifyAlertButton")]
        public void TC004_2_VerifyAlertButton()
        {
            // Act: User clicks on the Alert button
            alertsPage.ClickAlertButton();

            // Wait for alert to appear using explicit wait
            IAlert alert = WaitForAlert();

            // Assert: An alert is popping up with the text "Today is a working day or less likely a holiday"
            Assert.That(alert, Is.Not.Null, "Alert did not appear after clicking Alert button");
            
            string alertText = alert.Text;
            Assert.That(alertText, Does.Contain("working day").IgnoreCase, 
                $"Alert text is not correct. Expected: 'Today is a working day...', Actual: '{alertText}'");

            // Act: The user accepts the alert
            alert.Accept();

            // Wait to ensure alert is closed
            wait.Until(drv => !alertsPage.IsAlertPresent());

            // Assert: The alert is closing
            Assert.That(alertsPage.IsAlertPresent(), Is.False, "Alert did not close after accepting");
        }

        [Test(Description = "TC004_3 - Alerts - Confirm button verification")]
        [TestCase(TestName = "TC004_3_VerifyConfirmButton")]
        public void TC004_3_VerifyConfirmButton()
        {
            // Act: User clicks on the Confirm button
            alertsPage.ClickConfirmButton();

            // Wait for confirm alert to appear using explicit wait
            IAlert confirmAlert = WaitForAlert();

            // Assert: An alert is popping up with the text "Today is Friday. Do you agree?"
            Assert.That(confirmAlert, Is.Not.Null, "Confirm alert did not appear");
            
            string confirmText = confirmAlert.Text;
            Assert.That(confirmText, Does.Contain("Do you agree").IgnoreCase, 
                $"Confirm alert text is not correct. Actual: '{confirmText}'");

            // Act: The user accepts the alert
            confirmAlert.Accept();

            // Wait for second alert
            IAlert yesAlert = WaitForAlert();

            // Assert: The alert is closing. Another alert is appearing with the text "Yes"
            Assert.That(yesAlert, Is.Not.Null, "Second alert did not appear after accepting");
            
            string yesText = yesAlert.Text;
            Assert.That(yesText, Does.Contain("Yes"), 
                $"Second alert should contain 'Yes'. Actual: '{yesText}'");

            yesAlert.Accept();

            // Wait for alert to close
            wait.Until(drv => !alertsPage.IsAlertPresent());

            // Now test declining
            // Act: User clicks on the Confirm button again
            alertsPage.ClickConfirmButton();

            // Wait for confirm alert
            IAlert confirmAlert2 = WaitForAlert();

            // Act: The user declines the alert
            confirmAlert2.Dismiss();

            // Wait for third alert
            IAlert noAlert = WaitForAlert();

            // Assert: The alert is closing. Another alert is appearing with the text "No"
            Assert.That(noAlert, Is.Not.Null, "Third alert did not appear after declining");
            
            string noText = noAlert.Text;
            Assert.That(noText, Does.Contain("No"), 
                $"Third alert should contain 'No'. Actual: '{noText}'");

            noAlert.Accept();
        }

        [Test(Description = "TC004_4 - Alerts - Prompt button verification")]
        [TestCase(TestName = "TC004_4_VerifyPromptButton")]
        public void TC004_4_VerifyPromptButton()
        {
            string userInput = "TestInput123";

            // Act: User clicks on the Prompt button
            alertsPage.ClickPromptButton();

            // Wait for prompt alert to appear using explicit wait
            IAlert promptAlert = WaitForAlert();

            // Assert: An alert is popping up with the prompt
            Assert.That(promptAlert, Is.Not.Null, "Prompt alert did not appear");

            // Act: User enters a random text to the prompt and accepts the alert
            promptAlert.SendKeys(userInput);
            promptAlert.Accept();

            // Wait for result alert
            IAlert acceptAlert = WaitForAlert();

            // Assert: The alert is closing. Another alert is appearing with the text "user value <user input>"
            Assert.That(acceptAlert, Is.Not.Null, 
                "Second alert did not appear after entering text and accepting");
            
            string acceptAlertText = acceptAlert.Text;
            Assert.That(acceptAlertText, Does.Contain(userInput), 
                $"Alert should contain user input '{userInput}'. Actual: '{acceptAlertText}'");

            acceptAlert.Accept();

            // Wait for alert to close
            wait.Until(drv => !alertsPage.IsAlertPresent());

            // Now test declining
            // Act: User clicks on the Prompt button again
            alertsPage.ClickPromptButton();

            // Wait for prompt alert
            IAlert promptAlert2 = WaitForAlert();

            // Act: User enters a random text to the prompt and declines the alert
            promptAlert2.SendKeys("AnotherInput");
            promptAlert2.Dismiss();

            // Wait for result alert
            IAlert dismissAlert = WaitForAlert();

            // Assert: The alert is closing. Another alert is appearing with the text "user value - No answer"
            Assert.That(dismissAlert, Is.Not.Null, 
                "Third alert did not appear after declining");
            
            string dismissAlertText = dismissAlert.Text;
            Assert.That(dismissAlertText, Does.Contain("No answer").IgnoreCase, 
                $"Alert should contain 'No answer'. Actual: '{dismissAlertText}'");

            dismissAlert.Accept();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            CleanupDriver();
        }
    }
}
