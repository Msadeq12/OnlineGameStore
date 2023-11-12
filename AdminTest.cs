using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(7)]
internal sealed class AdminTest {
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
  public static void AdminAddGame() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 991);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.LinkText("Games")).Click();
    s_driver.FindElement(By.LinkText("Add New Game")).Click();
    s_driver.FindElement(By.Id("Title")).Click();
    s_driver.FindElement(By.Id("Title")).SendKeys("Unit Test");
    s_driver.FindElement(By.Id("Description")).Click();
    s_driver.FindElement(By.Id("Description")).SendKeys("Interesting way to see if games are able to have fields populated");
    s_driver.FindElement(By.Id("Price")).Click();
    s_driver.FindElement(By.Id("Price")).SendKeys("25.00");
    s_driver.FindElement(By.Id("GameGenre")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("GameGenre"));
      dropdown.FindElement(By.XPath("//option[. = 'Puzzle']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(8)")).Click();
    s_driver.FindElement(By.Id("GamePlatform")).Click();
    {
        var dropdown = s_driver.FindElement(By.Id("GamePlatform"));
        dropdown.FindElement(By.XPath("//option[. = 'Xbox']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(8)")).Click();
    s_driver.FindElement(By.Id("Publisher")).Click();
    s_driver.FindElement(By.Id("Publisher")).SendKeys("NPC Productions");
    s_driver.FindElement(By.Id("ReleaseYear")).Click();
    s_driver.FindElement(By.Id("ReleaseYear")).SendKeys("2023");
    s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".text-white:last-child > td:nth-child(1)")).Text, Is.EqualTo("Unit Test"));
    s_driver.Close();
    }


    [Test, Order(2)]
    public static void AdminAddEvent()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 991);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Events")).Click();
        s_driver.FindElement(By.LinkText("Add new Events")).Click();
        s_driver.FindElement(By.Id("Name")).Click();
        s_driver.FindElement(By.Id("Name")).SendKeys("Drawing Test Event");
        s_driver.FindElement(By.Id("Location")).Click();
        s_driver.FindElement(By.Id("Location")).SendKeys("Online");
        s_driver.FindElement(By.Id("Description")).Click();
        s_driver.FindElement(By.Id("Description")).SendKeys("Draw pictures of you favourite characters from whatever game");
        s_driver.FindElement(By.Id("Date")).Click();
        s_driver.FindElement(By.Id("Date")).SendKeys("002024-11-05T00:00");
        s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("tr:last-child > td:nth-child(1)")).Text, Is.EqualTo("Drawing Test Event"));
        s_driver.FindElement(By.CssSelector("tr:last-child a:nth-child(2)")).Click();
        s_driver.FindElement(By.CssSelector(".btn-danger")).Click();
    }
}
