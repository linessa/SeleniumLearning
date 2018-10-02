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
    public class CheckUserRegistration
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        string email;
        string password = "123456";

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckUserRegistrationTestMethod()
        {
            Random rand = new Random();
            driver.Url = "http://localhost:8080/litecart/en/";

            driver.Url = driver.FindElements(By.CssSelector("form[name=login_form] tr"))[4].FindElement(By.CssSelector("a")).GetAttribute("href");

            driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys(rand.Next(10000001).ToString());
            driver.FindElement(By.CssSelector("input[name=company]")).SendKeys("Test Company");
            driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys("TestFirstName");
            driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys("TestLastName");
            driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys("TestAddress1");
            driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys("TestAddress2");
            driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys(rand.Next(10000, 99999).ToString());
            driver.FindElement(By.CssSelector("input[name=city]")).SendKeys("New-York");

            
            SelectElement selectCountry = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
            selectCountry.SelectByText("United States");

            wait.Until(d =>
            {
                new SelectElement(d.FindElement(By.CssSelector("select[name=zone_code]"))).SelectByText("Arizona");
                return d;
            });

            email = "test_" + rand.Next(1000000001).ToString() + "@gmail.com";
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys(rand.Next(1000000001).ToString());
            SelectCheckBox(false, driver.FindElement(By.CssSelector("input[name=newsletter]")));
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys(password);

            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            driver.Url = driver.FindElements(By.CssSelector("#box-account li"))[3].FindElement(By.CssSelector("a")).GetAttribute("href");
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name=login]")).Click();

            driver.Url = driver.FindElements(By.CssSelector("#box-account li"))[3].FindElement(By.CssSelector("a")).GetAttribute("href");

        }

        public void SelectCheckBox(Boolean check, IWebElement checkbox)
        {
            if (!check && checkbox.Selected)
            {
                checkbox.Click();
            }
            else if (check && !checkbox.Selected)
            {
                checkbox.Click();
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
