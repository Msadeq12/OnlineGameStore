using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


[TestFixture, Order(11)]
public sealed class AddressesTest {
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
  public static void memberEnterFaultyAddresses() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 1212);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        //s_driver.FindElement(By.Id("manage")).Click();
        s_driver.FindElement(By.CssSelector(".btn > span")).Click();
        s_driver.FindElement(By.LinkText("Addresses")).Click();
        //s_driver.FindElement(By.Id("change-addresses")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_Line1")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_Line2")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_PostalCode")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_PostalCode")).SendKeys("kjlkj");
        s_driver.FindElement(By.Id("ShippingAddresses_PostalCode")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_PostalCode")).SendKeys("sgdfs");
        s_driver.FindElement(By.Id("update-addresses-button")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(3) .text-danger")).Text, Is.EqualTo("Please enter address line 1"));
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(6) .text-danger")).Text, Is.EqualTo("Please choose a region"));
        Assert.That(s_driver.FindElement(By.CssSelector(".col-md-6:nth-child(1) > .form-group:nth-child(7) .text-danger")).Text, Is.EqualTo("Please enter a city"));
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(4) .text-danger")).Text, Is.EqualTo("Please enter address line 1"));
        Assert.That(s_driver.FindElement(By.CssSelector(".col-md-6:nth-child(2) > .form-group:nth-child(7) .text-danger")).Text, Is.EqualTo("Please choose a region"));
        Assert.That(s_driver.FindElement(By.CssSelector(".form-group:nth-child(8) .field-validation-error")).Text, Is.EqualTo("Please enter a city"));
    }


    [Test, Order(2)]
    public static void memberAddAddresses() {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1212, 1212);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        //s_driver.FindElement(By.Id("manage")).Click();
        s_driver.FindElement(By.CssSelector(".btn > span")).Click();
        s_driver.FindElement(By.LinkText("Addresses")).Click();
        //s_driver.FindElement(By.Id("change-addresses")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_Line1")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_Line1")).SendKeys("111 Test St");
        s_driver.FindElement(By.Id("MailingAddresses_Line2")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_Line2")).SendKeys("Apt# 13");
        s_driver.FindElement(By.Id("Input_SelectedMailingCountryID")).Click();
        {
            var dropdown = s_driver.FindElement(By.Id("Input_SelectedMailingCountryID"));
            dropdown.FindElement(By.XPath("//option[. = 'Canada']")).Click();
        }
        s_driver.FindElement(By.CssSelector("#Input_SelectedMailingCountryID > option:nth-child(2)")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_RegionsID")).Click();
        {
            var dropdown = s_driver.FindElement(By.Id("MailingAddresses_RegionsID"));
            dropdown.FindElement(By.XPath("//option[. = 'Prince Edward Island']")).Click();
        }
        s_driver.FindElement(By.CssSelector("option:nth-child(11)")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_City")).SendKeys("Test");
        s_driver.FindElement(By.Id("MailingAddresses_PostalCode")).Click();
        s_driver.FindElement(By.Id("MailingAddresses_PostalCode")).SendKeys("N0N 0N0");
        s_driver.FindElement(By.Id("Addresses_DeliveryInstructions")).Click();
        s_driver.FindElement(By.Id("Addresses_DeliveryInstructions")).SendKeys("Knock at the door");
        s_driver.FindElement(By.Id("Addresses_SameAddress")).Click();
        s_driver.FindElement(By.Id("update-addresses-button")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your address information have been updated"));
    }


    [Test, Order(3)]
    public static void memberEditAddresses()
    {
        s_driver.Navigate().GoToUrl("https://localhost:7132/");
        s_driver.Manage().Window.Size = new System.Drawing.Size(1612, 1212);
        s_driver.FindElement(By.Id("login")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).Click();
        s_driver.FindElement(By.Id("Input_UserName")).SendKeys("TestMember");
        s_driver.FindElement(By.Id("Input_Password")).Click();
        s_driver.FindElement(By.Id("Input_Password")).SendKeys("Test1$");
        s_driver.FindElement(By.Id("login-submit")).Click();
        //s_driver.FindElement(By.Id("manage")).Click();
        s_driver.FindElement(By.CssSelector(".btn > span")).Click();
        s_driver.FindElement(By.LinkText("Addresses")).Click();
        //s_driver.FindElement(By.Id("change-addresses")).Click();
        s_driver.FindElement(By.Id("Addresses_SameAddress")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_Line1")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_Line1")).SendKeys("222 Test St");
        s_driver.FindElement(By.Id("ShippingAddresses_Line2")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_Line2")).SendKeys("Apt# 22");
        s_driver.FindElement(By.Id("Input_SelectedShippingCountryID")).Click();
        {
            var dropdown = s_driver.FindElement(By.Id("Input_SelectedShippingCountryID"));
            dropdown.FindElement(By.XPath("//option[. = 'Canada']")).Click();
        }
        s_driver.FindElement(By.CssSelector("#Input_SelectedShippingCountryID > option:nth-child(3)")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_RegionsID")).Click();
        {
            var dropdown = s_driver.FindElement(By.Id("ShippingAddresses_RegionsID"));
            dropdown.FindElement(By.XPath("//option[. = 'Alberta']")).Click();
        }
        s_driver.FindElement(By.CssSelector("#ShippingAddresses_RegionsID > option:nth-child(5)")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_City")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_City")).SendKeys("Test2");
        s_driver.FindElement(By.Id("ShippingAddresses_PostalCode")).Click();
        s_driver.FindElement(By.Id("ShippingAddresses_PostalCode")).SendKeys("32322");
        s_driver.FindElement(By.Id("update-addresses-button")).Click();
        Assert.That(s_driver.FindElement(By.CssSelector(".alert")).Text, Is.EqualTo("Your address information have been updated"));
    } 
}
