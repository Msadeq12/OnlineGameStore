using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(6)]
internal sealed class ProfilesTest {
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
  public static void MemberEntersProfileInformationWithFutureBirthDate() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    //s_driver.FindElement(By.Id("manage")).Click();
    s_driver.FindElement(By.CssSelector(".btn > span")).Click();
    s_driver.FindElement(By.LinkText("Profile")).Click();
    s_driver.FindElement(By.Id("Profile_FirstName")).Click();
    s_driver.FindElement(By.Id("Profile_FirstName")).SendKeys("John");
    s_driver.FindElement(By.Id("Profile_LastName")).Click();
    s_driver.FindElement(By.Id("Profile_LastName")).SendKeys("Smith");
    s_driver.FindElement(By.Id("Profile_Gender")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("Profile_Gender"));
      dropdown.FindElement(By.XPath("//option[. = 'Male']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(2)")).Click();
    s_driver.FindElement(By.Id("Profile_DOB")).Click();
    s_driver.FindElement(By.Id("Profile_DOB")).SendKeys("2023-11-30");
    s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
    s_driver.FindElement(By.CssSelector(".col")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".text-danger")).Text, Is.EqualTo("Birth date must be a valid date that\'s in the past."));
  }


  [Test, Order(2)]
  public static void MemberEntersProfileDetails() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 991);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    //s_driver.FindElement(By.Id("manage")).Click();
    s_driver.FindElement(By.CssSelector(".btn > span")).Click();
    s_driver.FindElement(By.LinkText("Profile")).Click();
    s_driver.FindElement(By.Id("Profile_FirstName")).Click();
    s_driver.FindElement(By.Id("Profile_FirstName")).SendKeys("John");
    s_driver.FindElement(By.Id("Profile_LastName")).Click();
    s_driver.FindElement(By.Id("Profile_LastName")).SendKeys("Smith");
    s_driver.FindElement(By.Id("Profile_Gender")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("Profile_Gender"));
      dropdown.FindElement(By.XPath("//option[. = 'Male']")).Click();
    }
    s_driver.FindElement(By.CssSelector("option:nth-child(2)")).Click();
    s_driver.FindElement(By.Id("Profile_DOB")).Click();
    s_driver.FindElement(By.Id("Profile_DOB")).SendKeys("1982-09-22");
    s_driver.FindElement(By.Id("Profile_RecievePromotions")).Click();
    s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
   // s_driver.FindElement(By.CssSelector(".alert")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your profile has been updated"));
  }
}
