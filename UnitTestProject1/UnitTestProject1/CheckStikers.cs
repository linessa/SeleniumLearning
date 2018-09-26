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
    public class CheckStikers
    {
        private IWebDriver driver;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void CheckStikersTestMethod()
        {
            driver.Url = "http://localhost:8080/litecart/en/";

            int productsNumber = driver.FindElements(By.CssSelector(".product")).Count;
            for (int i = 0; i < productsNumber; i++)
            {
                IWebElement currentElement = driver.FindElements(By.CssSelector(".product"))[i];
                NUnit.Framework.Assert.True(AreOnlyOneElementPresent(driver, currentElement));
            }
        }

        public bool AreOnlyOneElementPresent(IWebDriver driver, IWebElement currentElement)
        {
            return currentElement.FindElements(By.CssSelector(".sticker")).Count == 1;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
