using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


[TestFixture, Order(16)]
public sealed class DownloadGamesTest {
    private static IWebDriver s_driver;
    private readonly static string s_path = Path.GetTempPath();


    [SetUp]
    public static void SetUp() {
        Directory.CreateDirectory(s_path);
        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", s_path);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("disable-popup-blocking", "true");
        s_driver = new ChromeDriver(options);
    }


    [TearDown]
    protected static void TearDown() {
        s_driver.Quit();
    }


    [Test, Order(1)]
    public static void memberDownloadsDigitalGame() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 691);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Library")).Click();
        var gameTitleElementText = s_driver.FindElement(By.CssSelector("div.col-md-6:nth-child(1) > div:nth-child(1) > div:nth-child(1)")).Text;
        var gameTitle = gameTitleElementText.Remove(0, 11);
        Console.WriteLine(gameTitle);
        s_driver.FindElement(By.LinkText("Download")).Click();
        Thread.Sleep(5000);
        var fileName = $"{gameTitle}*.txt";
        var file = Directory.GetFiles(s_path, fileName, SearchOption.TopDirectoryOnly);
        Assert.That(file != null);
        File.Delete(file[0]);
    }


    [Test, Order(2)]
    public static void adminProcessesAPhysicalGameOrder() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1012, 1021);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.LinkText("Orders")).Click();
        s_driver.FindElement(By.LinkText("Mark it as Processed")).Click();
        s_driver.FindElement(By.Id("logout")).Click();
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        s_driver.FindElement(By.CssSelector(".btn > span")).Click();
        s_driver.FindElement(By.LinkText("My Orders")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".col-md-6:nth-child(1) .card-text:nth-child(3)")).Text, Is.EqualTo("Status: Processed"));
    }
}
