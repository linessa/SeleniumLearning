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

            int productsNumber = driver.FindElements(By.CssSelector(".product.column.shadow.hover-light")).Count;
            for (int i = 0; i < productsNumber; i++)
            {
                IWebElement currentElement = driver.FindElements(By.CssSelector(".product.column.shadow.hover-light"))[i];
                NUnit.Framework.Assert.True(AreOnlyOneElementPresent(driver, currentElement));
            }
        }

        public bool AreOnlyOneElementPresent(IWebDriver driver, IWebElement currentElement)
        {
            bool result = false;
            if (currentElement.FindElements(By.CssSelector(".sticker.sale")).Count == 1 ^ currentElement.FindElements(By.CssSelector(".sticker.new")).Count == 1)
            {
                result = true;
            }
            return result;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
