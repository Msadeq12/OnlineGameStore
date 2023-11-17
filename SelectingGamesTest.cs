using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(9)]
internal sealed class SelectingGamesTest {
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
  public static void MemberSelectsGameFromTheirAccountHomepage() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 991);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.LinkText("Details")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector("h1")).Text, Is.EqualTo("Game Details"));
  }


  [Test, Order(2)]
  public static void MemberSearchesForAGameAndViewsDetails() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    // ToDo: if scroll related exeptions expand scope.
    s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 1800);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.Name("searchString")).Click();
    s_driver.FindElement(By.Name("searchString")).SendKeys("Unit Test");
    s_driver.FindElement(By.CssSelector("input:nth-child(3)")).Click();
    s_driver.FindElement(By.LinkText("Details")).Click();
    s_driver.FindElement(By.CssSelector("h1")).Click();
    CleanupTestData();
  }


  private static void CleanupTestData()
  {
    s_driver.FindElement(By.Id("logout")).Click();
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.LinkText("Games")).Click();
    s_driver.FindElement(By.CssSelector(".text-white:last-child a:nth-child(2)")).Click();
    s_driver.FindElement(By.CssSelector(".btn-danger")).Click(); //Todo: Delete after member searches for this game *Actually maybe deal with this in context*
  }
}
