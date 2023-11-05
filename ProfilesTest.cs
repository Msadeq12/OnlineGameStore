// Generated by Selenium IDE
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(4)]
public class ProfilesTest {
  private FirefoxOptions _options;
  private IWebDriver _driver;


  [SetUp]
  public void SetUp() {
    _options = new FirefoxOptions { AcceptInsecureCertificates = true };
    _driver = new FirefoxDriver(_options);
  }


  [TearDown]
  protected void TearDown() {
    _driver.Quit();
  }


  [Test, Order(1)]
  public void memberEntersProfileInformationWithFutureBirthDate() {
    _driver.Navigate().GoToUrl("https://localhost:7132/");
    _driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
    _driver.FindElement(By.Id("login")).Click();
    _driver.FindElement(By.Id("Input_UserName")).Click();
    _driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    _driver.FindElement(By.Id("Input_Password")).Click();
    _driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    _driver.FindElement(By.Id("login-submit")).Click();
    _driver.FindElement(By.Id("manage")).Click();
    _driver.FindElement(By.Id("Profile_FirstName")).Click();
    _driver.FindElement(By.Id("Profile_FirstName")).SendKeys("John");
    _driver.FindElement(By.Id("Profile_LastName")).Click();
    _driver.FindElement(By.Id("Profile_LastName")).SendKeys("Smith");
    _driver.FindElement(By.Id("Profile_Gender")).Click();
    {
      var dropdown = _driver.FindElement(By.Id("Profile_Gender"));
      dropdown.FindElement(By.XPath("//option[. = 'Male']")).Click();
    }
    _driver.FindElement(By.CssSelector("option:nth-child(2)")).Click();
    _driver.FindElement(By.Id("Profile_DOB")).Click();
    _driver.FindElement(By.Id("Profile_DOB")).SendKeys("2023-11-30");
    _driver.FindElement(By.CssSelector(".btn-primary")).Click();
    _driver.FindElement(By.CssSelector(".col")).Click();
    Assert.That(_driver.FindElement(By.CssSelector(".text-danger")).Text, Is.EqualTo("Birth date must be a valid date that\'s in the past."));
  }


  [Test, Order(2)]
  public void memberEntersProfileDetails() {
    _driver.Navigate().GoToUrl("https://localhost:7132/");
    _driver.Manage().Window.Size = new System.Drawing.Size(1612, 991);
    _driver.FindElement(By.Id("login")).Click();
    _driver.FindElement(By.Id("Input_UserName")).Click();
    _driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
    _driver.FindElement(By.Id("Input_Password")).Click();
    _driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    _driver.FindElement(By.Id("login-submit")).Click();
    _driver.FindElement(By.Id("manage")).Click();
    _driver.FindElement(By.Id("Profile_FirstName")).Click();
    _driver.FindElement(By.Id("Profile_FirstName")).SendKeys("John");
    _driver.FindElement(By.Id("Profile_LastName")).Click();
    _driver.FindElement(By.Id("Profile_LastName")).SendKeys("Smith");
    _driver.FindElement(By.Id("Profile_Gender")).Click();
    {
      var dropdown = _driver.FindElement(By.Id("Profile_Gender"));
      dropdown.FindElement(By.XPath("//option[. = 'Male']")).Click();
    }
    _driver.FindElement(By.CssSelector("option:nth-child(2)")).Click();
    _driver.FindElement(By.Id("Profile_DOB")).Click();
    _driver.FindElement(By.Id("Profile_DOB")).SendKeys("1982-09-22");
    _driver.FindElement(By.Id("Profile_RecievePromotions")).Click();
    _driver.FindElement(By.CssSelector(".btn-primary")).Click();
    _driver.FindElement(By.CssSelector(".alert")).Click();
    Assert.That(_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your profile has been updated"));
  }
}
