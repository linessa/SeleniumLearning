using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;


namespace UnitTestProject1
{
    [TestFixture]
    public class CheckLinksInNewWindow
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
        public void CheckLinksInNewWindowTestMethod()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";

            driver.FindElement(By.CssSelector(".button")).Click();
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.CssSelector(".fa-external-link"));
            string mainWindow = driver.CurrentWindowHandle;
            ICollection<string> oldWindows = driver.WindowHandles;

            for (int i = 0; i < links.Count; i++)
            {
                links[i].Click();
                wait.Until(d => AnyWindowOtherThan(oldWindows));

                List<string> handles = new List<string>(driver.WindowHandles);

                for (int j = 0; j < oldWindows.Count; j++)
                {
                    handles.Remove(oldWindows.ToList()[j]);
                };

                driver.SwitchTo().Window(handles.First());
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }

        }

        public bool AnyWindowOtherThan(ICollection<string> oldWindows)
        {
            return driver.WindowHandles.Count > oldWindows.Count ? true : false;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
