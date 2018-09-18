using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
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
        public void TestMethod1()
        {
            driver.Url = "https://www.google.by/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Пошук Google"));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
