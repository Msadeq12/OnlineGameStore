using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


[TestFixture, Order(8)]
internal sealed class ReportsTest {
  private static IWebDriver s_driver;
  private static IDictionary<string, object> s_vars;
  private readonly static string s_path = Path.GetTempPath();


  [SetUp]
  public static void SetUp() {
    Directory.CreateDirectory(s_path);
    var options = new ChromeOptions();
    options.AddUserProfilePreference("download.default_directory", s_path);
    options.AddUserProfilePreference("download.prompt_for_download", false);
    options.AddUserProfilePreference("disable-popup-blocking", "true");
    s_driver = new ChromeDriver(options);
    s_vars = new Dictionary<string, object>();
  }


  [TearDown]
  protected static void TearDown() {
    s_driver.Quit();
  }


  private static string WaitForWindow(int timeout) {
    try {
      Thread.Sleep(timeout);
    } catch(Exception e) {
      Console.WriteLine("{0} Exception caught.", e);
    }
    var whNow = ((IReadOnlyCollection<object>)s_driver.WindowHandles).ToList();
    var whThen = ((IReadOnlyCollection<object>)s_vars["WindowHandles"]).ToList();
    if (whNow.Count > whThen.Count) {
      return whNow.Except(whThen).First().ToString();
    } else {
      return whNow.First().ToString();
    }
  }


  [Test, Order(1)]
  public static void AdminViewReport()
  {
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1050, 708);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.LinkText("Report")).Click();
    s_vars["WindowHandles"] = s_driver.WindowHandles;
    s_driver.FindElement(By.LinkText("View")).Click();
    s_vars["win1464"] = WaitForWindow(2000);
    s_driver.SwitchTo().Window(s_vars["win1464"].ToString());
    var elements = s_driver.FindElements(By.CssSelector("embed"));
    Assert.True(elements.Count > 0);
  }


  [Test, Order(2)]
  public static void AdminDownloadReport() {
    var filesToDelete = Directory.GetFiles(s_path, "CVGS_*.pdf", SearchOption.TopDirectoryOnly);
    if(filesToDelete != null)
    {
        foreach(var fileToDelete in filesToDelete)
        {
            File.Delete(fileToDelete);
        }
    }
    s_driver.Navigate().GoToUrl("https://localhost:7132/");
    s_driver.Manage().Window.Size = new System.Drawing.Size(1050, 708);
    s_driver.FindElement(By.Id("login")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).Click();
    s_driver.FindElement(By.Id("Input_UserName")).SendKeys("admin");
    s_driver.FindElement(By.Id("Input_Password")).Click();
    s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
    s_driver.FindElement(By.Id("login-submit")).Click();
    s_driver.FindElement(By.LinkText("Report")).Click();
    s_driver.FindElement(By.LinkText("Download")).Click();
    Thread.Sleep(5000);
    var file = Directory.GetFiles(s_path, "CVGS_*.pdf", SearchOption.TopDirectoryOnly);
    Assert.That(file != null);
    File.Delete(file[0]) ;
  }
}
