using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace UnitTestProject1
{
    [TestFixture]
    public class CheckMenu
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string headerLocator = "h1";

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckMenuTestMethod()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            int mainMenuElementsNumber = driver.FindElements(By.CssSelector("#box-apps-menu li")).Count;
            for (int i =1; i <= mainMenuElementsNumber; i++)
            {
                driver.FindElement(By.CssSelector("#box-apps-menu li:nth-child(" + i + ")")).Click();
                NUnit.Framework.Assert.True(AreElementsPresent(driver, By.CssSelector(headerLocator)));

                IWebElement mainMenuElement = driver.FindElement(By.CssSelector("#box-apps-menu li:nth-child(" + i + ")"));
                int menuElementsNumber = mainMenuElement.FindElements(By.CssSelector(".docs li")).Count;
                
                for (int j = 1; j <= menuElementsNumber; j++)
                {
                    driver.FindElement(By.CssSelector(".docs li:nth-child(" + j + ")")).Click();
                    NUnit.Framework.Assert.True(AreElementsPresent(driver, By.CssSelector(headerLocator)));
                }

            }
        }

        bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
