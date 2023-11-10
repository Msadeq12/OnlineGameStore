using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(2)]
internal sealed class LoginTest
{
    private static IWebDriver s_driver;


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
    public static void memberLogsInSuccessfully()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("h1")).Text, Is.EqualTo("Our Game Collection"));
    }


    [Test, Order(2)]
    public static void memberIsLockedOutDueToUnsuccessfullLogin()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1S");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test2$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test3$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".text-danger")).Text, Is.EqualTo("This account has been locked out, please try again later in 15 Minutes."));
        // Reduced lockout time for test user to 2 seconds
        Thread.Sleep(2000);
    }
}