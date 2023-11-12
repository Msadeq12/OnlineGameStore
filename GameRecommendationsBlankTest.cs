using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(3)]
public sealed class GameRecommendationsBlankTest 
{
      private static IWebDriver s_driver;


      [SetUp]
      public static void SetUp() {
        s_driver = new FirefoxDriver(new FirefoxOptions { AcceptInsecureCertificates = true });
      }


      [TearDown]
      protected static void TearDown() {
        s_driver.Quit();
      }


      [Test, Order(1)]
      public static void memberDoesNotSpecifyPreferencesForGameRecommendations() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("h3")).Text, Is.EqualTo("Please update your Preferences to see the recommended games."));
      }
}
