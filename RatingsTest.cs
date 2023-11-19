using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(12)]
public sealed class RatingsTest {
  private static IWebDriver s_driver;


  [SetUp]
  public static void SetUp() {
    s_driver = new FirefoxDriver(new FirefoxOptions { AcceptInsecureCertificates = true });
  }


  [TearDown]
  protected static void TearDown() {
    s_driver.Quit();
  }


  [Test, Order(3)]
  public static void memberEditGameRating() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
    s_driver.FindElement(By.Id("NewRating_Value")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("NewRating_Value"));
      dropdown.FindElement(By.XPath("//option[. = '3']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(3)")).Click();
    s_driver.FindElement(By.CssSelector(".btn:nth-child(4)")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Rating submitted successfully."));
    Assert.That(s_driver.FindElement(By.CssSelector(".card:nth-child(4) p")).Text, Is.EqualTo("3"));
    Assert.That(s_driver.FindElement(By.CssSelector(".text-muted:nth-child(2)")).Text, Is.EqualTo("Posted by TestMember"));
  }


  [Test, Order(2)]
  public static void memberAddGameRating() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
    s_driver.FindElement(By.Id("NewRating_Value")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("NewRating_Value"));
      dropdown.FindElement(By.XPath("//option[. = '5']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(5)")).Click();
    s_driver.FindElement(By.CssSelector(".btn:nth-child(4)")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Rating submitted successfully."));
    Assert.That(s_driver.FindElement(By.CssSelector(".card:nth-child(4) p")).Text, Is.EqualTo("5"));
    Assert.That(s_driver.FindElement(By.CssSelector(".text-muted:nth-child(2)")).Text, Is.EqualTo("Posted by TestMember"));
  }


  [Test, Order(1)]
  public static void ensureUnauthenticatedUserCannotRateGame() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
    s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
    s_driver.FindElement(By.CssSelector(".btn:nth-child(4)")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector("h1")).Text, Is.EqualTo("Log in"));
  }
}
