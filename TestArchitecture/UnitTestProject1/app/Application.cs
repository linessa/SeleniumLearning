using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestArchitecture.pages;

namespace TestArchitecture
{
    public class Application
    {
        private IWebDriver driver;

        private MainPage mainPage;
        private ProductPage productPage;
        private CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal void AddProductsToCart(int productNumber)
        {
            mainPage.Open();
            for (int i = 1; i <= productNumber; i++)
            {
                mainPage.Open();
                productPage.Open(i);
                productPage.SelectManufacture("Small");
                productPage.AddProduct(i);
            }
        }
        internal void DeleteProductsFromCart()
        {
            cartPage.Open();
            
            int rowsInTable = cartPage.GetRowsInTable();

            for (int i = 1; i <= rowsInTable - 5; i++)
            {
                cartPage.DeleteProduct(i, rowsInTable);
            }
        }

        internal bool IsCartEmpty()
        {
            return cartPage.GetTextInEmptyCart() == "There are no items in your cart.";
        }
    }
}
