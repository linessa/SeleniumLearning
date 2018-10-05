using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;

namespace UnitTestProject1
{
    [TestFixture]
    public class CheckCart
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckCartTestMethod()
        {
            int numberOfProducts = 3;
            for (int i = 1; i <= numberOfProducts; i++)
            {
                driver.Url = "http://localhost:8080/litecart/en/";
                driver.Url = driver.FindElements(By.CssSelector("#box-most-popular .product"))[i-1].FindElement(By.CssSelector(".link")).GetAttribute("href");
                if (IsElementPresent(driver, "select[name*=Size]"))
                {
                    SelectElement selectManufacture = new SelectElement(driver.FindElement(By.CssSelector("select[name*=Size]")));
                    selectManufacture.SelectByText("Small");
                }
                driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.CssSelector("#cart .quantity")), i.ToString()));
            }
            driver.FindElement(By.CssSelector("#cart .link")).Click();

            int rowsInTable = driver.FindElements(By.CssSelector(".dataTable tr")).Count;

            for (int i = 1; i <= rowsInTable-5; i++)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=remove_cart_item]"))).Click();

                if (i != rowsInTable - 5)
                {
                    wait.Until((IWebDriver d) => d.FindElements(By.CssSelector(".dataTable tr")).Count == rowsInTable - i);
                }
                else
                {
                    string text = wait.Until((IWebDriver d) => d.FindElement(By.CssSelector("#checkout-cart-wrapper em"))).GetAttribute("textContent");
                    NUnit.Framework.Assert.True(text == "There are no items in your cart.");
                }
                
            }
            
        }

        public bool IsElementPresent(IWebDriver driver, string selector)
        {
            return driver.FindElements(By.CssSelector(selector)).Count > 0;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
