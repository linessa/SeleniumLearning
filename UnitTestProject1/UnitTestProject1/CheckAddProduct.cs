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
using System.IO;
using System.Reflection;

namespace UnitTestProject1
{
    [TestFixture]
    public class CheckAddProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        //string email;
        //string password = "123456";

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckAddProductTestMethod()
        {
            Random rand = new Random();
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElements(By.CssSelector("#box-apps-menu li"))[1].Click();
            driver.FindElements(By.CssSelector(".button"))[1].Click();
            driver.FindElements(By.CssSelector("input[name=status]"))[0].Click();
            string productName = "TestProductName_" + rand.Next(101).ToString();
            driver.FindElement(By.CssSelector("input[name*=name]")).SendKeys(productName);
            driver.FindElement(By.CssSelector("input[name=code]")).SendKeys(rand.Next(10000, 99999).ToString());
            driver.FindElement(By.CssSelector("input[data-name*=Ducks]")).Click();
            SelectElement selectCategory = new SelectElement(driver.FindElement(By.CssSelector("select[name=default_category_id]")));
            selectCategory.SelectByText("Rubber Ducks");
            driver.FindElements(By.CssSelector("input[name*=product_groups]"))[0].Click();
            driver.FindElement(By.CssSelector("input[name=quantity]")).SendKeys(rand.Next(20).ToString());
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string imagePath = Path.Combine(outPutDirectory, "duck.jpg");
            string icon_path = new Uri(imagePath).LocalPath;
            driver.FindElement(By.CssSelector("input[name*=new_images]")).SendKeys(icon_path);
            driver.FindElement(By.CssSelector("input[name=date_valid_from]")).SendKeys("12/12/2017");
            driver.FindElement(By.CssSelector("input[name=date_valid_to]")).SendKeys("12/12/2020");

            driver.FindElements(By.CssSelector(".index li"))[1].Click();
            SelectElement selectManufacture = new SelectElement(driver.FindElement(By.CssSelector("select[name=manufacturer_id]")));
            selectManufacture.SelectByText("ACME Corp.");
            driver.FindElement(By.CssSelector("input[name=keywords]")).SendKeys("test_duck" + rand.Next(101).ToString());
            driver.FindElement(By.CssSelector("input[name*=short_description]")).SendKeys("the_best_test_duck" + rand.Next(101).ToString());
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("the_best_test_duck the_best_test_duck the_best_test_duck" + rand.Next(101).ToString());
            driver.FindElement(By.CssSelector("input[name*=head_title]")).SendKeys("the_best_test_duck_title" + rand.Next(101).ToString());
            driver.FindElement(By.CssSelector("input[name*=meta_description]")).SendKeys("the_best_test_duck_meta_description" + rand.Next(101).ToString());

            driver.FindElements(By.CssSelector(".index li"))[3].Click();

            driver.FindElement(By.CssSelector("input[name=purchase_price]")).SendKeys(rand.Next(10).ToString());
            SelectElement selectPurchasePriceCurrencyCode = new SelectElement(driver.FindElement(By.CssSelector("select[name=purchase_price_currency_code]")));
            selectPurchasePriceCurrencyCode.SelectByText("Euros");
            driver.FindElements(By.CssSelector("input[name*=prices]"))[0].SendKeys(rand.Next(30).ToString());
            driver.FindElements(By.CssSelector("input[name*=prices]"))[2].SendKeys(rand.Next(30).ToString());

            driver.FindElement(By.CssSelector("button[name=save]")).Click();

            NUnit.Framework.Assert.True(CheckProductInCatalog(productName));
        }

        public bool CheckProductInCatalog(string productName)
        {
            bool result = false;
            for (int i = 1; i < driver.FindElements(By.CssSelector(".dataTable tr")).Count; i++)
            {
                string text = driver.FindElements(By.CssSelector(".dataTable tr"))[i].FindElements(By.CssSelector("td"))[2].FindElement(By.CssSelector("a")).GetAttribute("textContent");
                if (productName == text)
                {
                    result = true;
                    return result;
                }
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
