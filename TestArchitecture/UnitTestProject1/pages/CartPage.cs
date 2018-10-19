using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.pages
{
    class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        internal CartPage Open()
        {
            driver.Url = "http://localhost:8080/litecart/en/checkout";
            return this;
        }

        internal int GetRowsInTable()
        {
            return driver.FindElements(By.CssSelector(".dataTable tr")).Count;
        }

        internal CartPage DeleteProduct(int productNumber, int rowsInTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=remove_cart_item]"))).Click();
            if (productNumber != rowsInTable - 5)
            {
                wait.Until((IWebDriver d) => d.FindElements(By.CssSelector(".dataTable tr")).Count == rowsInTable - productNumber);
            }
            return this;
        }

        internal string GetTextInEmptyCart()
        {
            return wait.Until((IWebDriver d) => d.FindElement(By.CssSelector("#checkout-cart-wrapper em"))).GetAttribute("textContent");
        }
    }
}
