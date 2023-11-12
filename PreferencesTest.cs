using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(4)]
internal sealed class PreferencesTest {
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
  public static void MemberAddInitialPreferences() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 719);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.Id("manage")).Click();
    s_driver.FindElement(By.Id("change-preferences")).Click();
    {
      var elements = s_driver.FindElements(By.CssSelector(".form-group:nth-child(2) .item-container:nth-child(1)"));
      Assert.True(elements.Count == 0);
    }
    {
      var elements = s_driver.FindElements(By.CssSelector(".form-group:nth-child(2) .item-container"));
      Assert.True(elements.Count == 0);
    }
    {
      string value = s_driver.FindElement(By.Id("Input_LanguagesID")).GetAttribute("value");
      Assert.That(value, Is.EqualTo("0"));
    }
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) svg")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) li:nth-child(1)")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) .btn-container svg")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) li:nth-child(1)")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) .btn-container")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) svg")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) li:nth-child(1)")).Click();
    s_driver.FindElement(By.Id("Input_LanguagesID")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("Input_LanguagesID"));
      dropdown.FindElement(By.XPath("//option[. = 'Japanese']")).Click();
    }
    s_driver.FindElement(By.CssSelector("#Input_LanguagesID > option:nth-child(9)")).Click();
    s_driver.FindElement(By.Id("update-preferences-button")).Click();
    s_driver.FindElement(By.CssSelector(".alert")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your preferences have been updated"));
  }


  [Test, Order(2)]
  public static void MemberEditInitialPreferences() {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 1019);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.Id("manage")).Click();
    s_driver.FindElement(By.Id("change-preferences")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) .item-container:nth-child(1) line:nth-child(2)")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) .btn-container")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) .btn-container svg")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(1) li:nth-child(4)")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) .btn-container")).Click();
    s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) .btn-container svg")).Click();
    s_driver.FindElement(By.CssSelector("li:nth-child(8)")).Click();
    s_driver.FindElement(By.Id("Input_LanguagesID")).Click();
    {
      var dropdown = s_driver.FindElement(By.Id("Input_LanguagesID"));
      dropdown.FindElement(By.XPath("//option[. = 'French']")).Click();
    }
    s_driver.FindElement(By.CssSelector("#Input_LanguagesID > option:nth-child(2)")).Click();
    s_driver.FindElement(By.Id("update-preferences-button")).Click();
    Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your preferences have been updated"));
  }
}
