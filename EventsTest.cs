using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(8)]
internal sealed class EventsTest {
    private static IWebDriver s_driver { get; set; }


    [SetUp]
    public static void SetUp() {
        s_driver = new FirefoxDriver();
    }


    [TearDown]
    protected static void TearDown() {
        s_driver.Quit();
    }


    [Test, Order(2)]
    public static void MemberTriesRegisteringForSameEventTwice() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1600);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".card:last-child form > .btn")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Already registered to that event."));
    }


    [Test, Order(1)]
    public static void MemberRegisterInEvent() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1600);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".card:last-child .btn")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your Registration is complete!"));
        s_driver.FindElement(By.LinkText("Your Events")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("tr:last-child > td:nth-child(1)")).Text, Is.EqualTo("Drawing Test Event"));
    }


    [Test, Order(3)]
    public static void MemberWithdrawalsFromEvent() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1600);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Your Events")).Click();
        s_driver.FindElement(By.CssSelector("tr:last-child .btn")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Registration has been cancelled"));
        CleanupTestData();
    }


    /// <summary>
    /// Discard Event Created By Prior Test
    /// </summary>
    private static void CleanupTestData()
    {
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Events")).Click();
        s_driver.FindElement(By.CssSelector("tr:last-child a:nth-child(2)")).Click();
        s_driver.FindElement(By.CssSelector(".btn-danger")).Click();
    }
}
