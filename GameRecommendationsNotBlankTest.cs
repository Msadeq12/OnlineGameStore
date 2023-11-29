using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(5)]
internal sealed class GameRecommendationsNotBlankTest
{
    private static IWebDriver s_driver { get; set; }


    [SetUp]
    public static void SetUp()
    {
        s_driver = new FirefoxDriver(new FirefoxOptions { AcceptInsecureCertificates = true });
    }


    [TearDown]
    protected static void TearDown()
    {
        s_driver.Quit();
    }


    [Test, Order(1)]
    public static void memberSpecifiesPreferencesForGameRecommendations()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 1240);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("div.row:nth-child(4) > div:nth-child(2) > h1:nth-child(1)")).Text, Is.EqualTo("Our Game Recommendations:"));
    }
}
