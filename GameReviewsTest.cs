using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(11)]
internal sealed class GameReviewsTest
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
    public static void MemberAddsReviewThatGetsRegectedByAdmin()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 1211);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(2) .btn")).Click();
        s_driver.FindElement(By.Id("NewReview_CommentText")).Click();
        s_driver.FindElement(By.Id("NewReview_CommentText")).SendKeys("Test Comment");
        s_driver.FindElement(By.CssSelector("button.btn:nth-child(6)")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Review submitted successfully, will be shown once approved by an admin."));
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Reviews")).Click();
        s_driver.FindElement(By.CssSelector("tr:nth-child(1) .btn-danger")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Review disapproved successfully."));
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(2) .btn")).Click();
        s_driver.FindElement(By.Id("logout")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".card-body > p")).Text, Is.Not.EqualTo("Test Comment"));
    }


    [Test, Order(2)]
    public static void MemberAddsReviewThatGetsApprovedByAdmin()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 1611);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Details")).Click();
        s_driver.FindElement(By.Id("NewReview_CommentText")).Click();
        s_driver.FindElement(By.Id("NewReview_CommentText")).SendKeys("Test Comment");
        s_driver.FindElement(By.CssSelector("button.btn:nth-child(6)")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Review submitted successfully, will be shown once approved by an admin."));
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Reviews")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("td:nth-child(3)")).Text, Is.EqualTo("TestMember"));
        s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Details")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".card-body > p")).Text, Is.EqualTo("Test Comment"));
    }
}