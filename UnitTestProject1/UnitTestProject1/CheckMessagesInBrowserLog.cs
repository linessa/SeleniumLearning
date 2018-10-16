using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject1
{
    [TestFixture]
    public class CheckMessagesInBrowserLog
    {
        private IWebDriver driver;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void CheckMessagesInBrowserLogTestMethod()
        {
            Random rand = new Random();
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";

            int productCount = driver.FindElements(By.CssSelector(".dataTable .row")).Count;

            for (int i = 3; i < productCount; i++)
            {
                driver.Url = driver.FindElements(By.CssSelector(".dataTable .row"))[i].FindElement(By.CssSelector("a")).GetAttribute("href");
                
                NUnit.Framework.Assert.True(driver.Manage().Logs.GetLog("browser").Count == 0);

                driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            }
            
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
