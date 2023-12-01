using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(15)]
public sealed class CartTest {
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
    public static void memberAddsItemsToCart() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1400);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Add to Cart")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".fa")).Text, Is.EqualTo("1"));
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(2) .btn-success")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".fa")).Text, Is.EqualTo("2"));
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn-success")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".fa")).Text, Is.EqualTo("3"));
    }


    [Test, Order(2)]
    public static void memberRemovesItemFromCart() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1700);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        ReAddCartItems();
        s_driver.FindElement(By.CssSelector(".fa")).Click();
        s_driver.FindElement(By.CssSelector(".col-12:nth-child(1) .btn")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".fa")).Text, Is.EqualTo("2"));
    }


    [Test, Order(3)]
    public static void memberProvidesFaultyInformationAtCheckout()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1700);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        ReAddCartItems();
        s_driver.FindElement(By.CssSelector(".fa")).Click();
        s_driver.FindElement(By.Id("CardHolderName")).Click();
        s_driver.FindElement(By.Id("CardNumber")).Click();
        s_driver.FindElement(By.Id("CardNumber")).SendKeys("JJJSHW");
        s_driver.FindElement(By.Id("CVV")).Click();
        s_driver.FindElement(By.Id("CVV")).SendKeys("LLLLLLL");
        s_driver.FindElement(By.Id("ExpiryDate")).Click();
        s_driver.FindElement(By.Id("ExpiryDate")).SendKeys("JJJJ");
        s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
        s_driver.FindElement(By.Id("CardHolderName")).SendKeys("John Smith");
        s_driver.FindElement(By.Id("CardNumber")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(2) > .text-danger")).Text, Is.EqualTo("Invalid Card Number"));
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(3) > .text-danger")).Text, Is.EqualTo("Invalid CVV"));
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(4) > .text-danger")).Text, Is.EqualTo("Invalid Expiry Date. Format MM/YY"));
    }


    [Test, Order(4)]
    public static void memberChecksOutCartItems() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1700);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        ReAddCartItems();
        s_driver.FindElement(By.CssSelector(".fa")).Click();
        s_driver.FindElement(By.Id("CardHolderName")).Click();
        s_driver.FindElement(By.Id("CardHolderName")).SendKeys("John Smith");
        s_driver.FindElement(By.CssSelector(".form-group:nth-child(2)")).Click();
        s_driver.FindElement(By.Id("CardNumber")).Click();
        s_driver.FindElement(By.Id("CardNumber")).SendKeys("6011000180331112");
        s_driver.FindElement(By.Id("CVV")).Click();
        s_driver.FindElement(By.Id("CVV")).SendKeys("333");
        s_driver.FindElement(By.Id("ExpiryDate")).Click();
        s_driver.FindElement(By.Id("ExpiryDate")).SendKeys("11/28");
        s_driver.FindElement(By.CssSelector(".btn-primary")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".card-title")).Text, Is.EqualTo("Order Confirmation"));
        Assert.That(s_driver.FindElement(By.CssSelector("h3:nth-child(2)")).Text, Is.EqualTo("Invoice Details"));
    }


    /// <summary>
    /// Reinitializes cart items upon a new browser session 
    /// </summary>
    private static void ReAddCartItems()
    {
        s_driver.FindElement(By.LinkText("Add to Cart")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(2) .btn-success")).Click();
        s_driver.FindElement(By.CssSelector(".carousel-item:nth-child(3) .btn-success")).Click();
    }
}
