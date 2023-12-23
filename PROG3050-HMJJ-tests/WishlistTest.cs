using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(14)]
public sealed class WishlistTest {
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
    public static void MemberAddsWishlistItem() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
        s_driver.FindElement(By.LinkText("Add to Wish List")).Click();
        s_driver.FindElement(By.LinkText("Wish List")).Click();
        s_driver.FindElement(By.LinkText("Escape the City")).Click();
        Assert.That(s_driver.FindElement(By.LinkText("Remove from Wish List")).Text, Is.EqualTo("Remove from Wish List"));
    }


    [Test, Order(2)]
    public static void MemberRemovesWishlistItem() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Wish List")).Click();
        s_driver.FindElement(By.LinkText("Escape the City")).Click();
        s_driver.FindElement(By.LinkText("Remove from Wish List")).Click();
        Assert.That(s_driver.FindElement(By.LinkText("Add to Wish List")).Text, Is.EqualTo("Add to Wish List"));
    }
}
