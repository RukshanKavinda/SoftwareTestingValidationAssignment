using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CSE2522_Assignment_02_FC222031.Pages;

namespace CSE2522_Assignment_02_FC222031
{
    public class Tests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Navigate to the application URL
            driver.Navigate().GoToUrl("https://example.com/login");

            // Initialize Page Object
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void TestValidLogin()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass123";

            // Act
            loginPage.Login(username, password);

            // Assert
            Assert.That(driver.Url, Does.Contain("dashboard"));
        }

        [Test]
        public void TestInvalidLogin()
        {
            // Arrange
            string username = "invalid";
            string password = "wrong";

            // Act
            loginPage.Login(username, password);

            // Assert
            string errorMsg = loginPage.GetErrorMessage();
            Assert.That(errorMsg, Does.Contain("Invalid credentials"));
        }

        [Test]
        public void TestLoginButtonIsDisplayed()
        {
            // Assert
            Assert.That(loginPage.IsLoginButtonDisplayed(), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            // Close and quit the browser
            driver?.Quit();
            driver?.Dispose();
        }
    }

"   :{>LYH.