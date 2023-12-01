using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(13)]
internal sealed class RatingsTest {
    private static IWebDriver s_driver { get; set; }


    [SetUp]
    public static void SetUp() {
        s_driver = new FirefoxDriver(new FirefoxOptions { AcceptInsecureCertificates = true });
    }


    [TearDown]
    protected static void TearDown() {
        s_driver.Quit();
    }


    [Test, Order(3)]
    public static void MemberEditGameRating() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
        s_driver.FindElement(By.CssSelector(".radio-label:nth-child(2)")).Click();
        s_driver.FindElement(By.CssSelector("button.btn:nth-child(5)")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Rating submitted successfully."));
        {
            // Assert 5 stars has been given (asserting that 5th star is apart of checked html class)
            var fifthStarCheckedElement = s_driver.FindElements(By.CssSelector("div.card:nth-child(4) > div:nth-child(2) > span:nth-child(5).checked"));
            Assert.True(fifthStarCheckedElement.Count == 1);
        }
        Assert.That(s_driver.FindElement(By.CssSelector(".text-muted:nth-child(7)")).Text, Is.EqualTo("Posted by TestMember"));
    }


    [Test, Order(2)]
    public static void MemberAddGameRating() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
        s_driver.FindElement(By.CssSelector(".radio-label:nth-child(6)")).Click();
        s_driver.FindElement(By.CssSelector("button.btn:nth-child(5)")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Rating submitted successfully."));
        {
            // Assert 3 stars has been given (asserting that 3rd star is apart of checked html class and 4th is not)
            var thirdStarCheckedElement = s_driver.FindElements(By.CssSelector("div.card:nth-child(4) > div:nth-child(2) > span:nth-child(3).checked"));
            Assert.True(thirdStarCheckedElement.Count == 1);
            var forthStarCheckedElement = s_driver.FindElements(By.CssSelector("div.card:nth-child(4) > div:nth-child(2) > span:nth-child(4).checked"));
            Assert.True(forthStarCheckedElement.Count == 0);
        }
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Rating submitted successfully."));
        Assert.That(s_driver.FindElement(By.CssSelector(".text-muted:nth-child(7)")).Text, Is.EqualTo("Posted by TestMember"));
    }
    

    [Test, Order(1)]
    public static void EnsureUnauthenticatedUserCannotRateGame() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1620);
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn")).Click();
        s_driver.FindElement(By.CssSelector("button.btn:nth-child(5)")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector("h1")).Text, Is.EqualTo("Log in"));
    }
}
