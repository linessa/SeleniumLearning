using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.pages
{
    class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        internal ProductPage Open(int productNumber)
        {
            driver.Url = driver.FindElements(By.CssSelector("#box-most-popular .product"))[productNumber - 1].FindElement(By.CssSelector(".link")).GetAttribute("href");
            return this;
        }

        internal ProductPage SelectManufacture(string name)
        {
            if (IsElementPresent(driver, "select[name*=Size]"))
            {
                SelectElement selectManufacture = new SelectElement(driver.FindElement(By.CssSelector("select[name*=Size]")));
                selectManufacture.SelectByText(name);
            }
            return this;
        }

        private bool IsElementPresent(IWebDriver driver, string selector)
        {
            return driver.FindElements(By.CssSelector(selector)).Count > 0;
        }

        internal ProductPage AddProduct(int productNumber)
        {
            driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.CssSelector("#cart .quantity")), productNumber.ToString()));
            return this;
        }

    }
}
