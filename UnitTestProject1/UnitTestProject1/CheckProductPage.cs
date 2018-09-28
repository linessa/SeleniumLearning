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
    public class CheckProductPage
    {
        private IWebDriver driver;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
        }

        [Test]
        public void CheckProductPageTestMethod()
        {
            driver.Url = "http://localhost:8080/litecart/en/";

            IWebElement firstProduct = driver.FindElements(By.CssSelector("#box-campaigns .product"))[0];
            string textMainPage = firstProduct.FindElement(By.CssSelector("div .name")).Text;

            IWebElement regularPriceMainPage = firstProduct.FindElement(By.CssSelector(".regular-price"));
            string textRegularPriceMainPage = regularPriceMainPage.Text;
            Color colorRegularPriceMainPage = ParseColor(regularPriceMainPage.GetCssValue("color"));
            string textDecorationRegularPriceMainPage = regularPriceMainPage.GetCssValue("text-decoration");
            Size sizeRegularPriceMainPage = regularPriceMainPage.Size;

            IWebElement campaignPriceMainPage = firstProduct.FindElement(By.CssSelector(".campaign-price"));
            string textCampaignPriceMainPage = campaignPriceMainPage.Text;
            Color colorCampaignPriceMainPage = ParseColor(campaignPriceMainPage.GetCssValue("color"));
            string fontWeightCampaignPriceMainPage = campaignPriceMainPage.GetCssValue("font-weight");
            Size sizeCampaignPriceMainPage = campaignPriceMainPage.Size;

            NUnit.Framework.Assert.True(sizeRegularPriceMainPage.Height < sizeCampaignPriceMainPage.Height);
            NUnit.Framework.Assert.True(sizeRegularPriceMainPage.Width < sizeCampaignPriceMainPage.Width);
            NUnit.Framework.Assert.True(IsGrey(colorRegularPriceMainPage));
            NUnit.Framework.Assert.True(textDecorationRegularPriceMainPage.Contains("line-through"));
            NUnit.Framework.Assert.True(IsRed(colorCampaignPriceMainPage));
            NUnit.Framework.Assert.True(fontWeightCampaignPriceMainPage.Equals("900") || fontWeightCampaignPriceMainPage.Equals("700")); //700 chrome, 900 firefox and ie


            driver.Url = firstProduct.FindElement(By.CssSelector(".link")).GetAttribute("href");
            
            string textProductPage = driver.FindElement(By.CssSelector("h1")).Text;

            IWebElement regularPriceProductPage = driver.FindElement(By.CssSelector(".regular-price"));
            string textRegularPriceProductPage = regularPriceProductPage.Text;
            Color colorRegularPriceProductPage = ParseColor(regularPriceProductPage.GetCssValue("color"));
            string textDecorationRegularPriceProductPage = regularPriceProductPage.GetCssValue("text-decoration");
            Size sizeRegularPriceProductPage = regularPriceProductPage.Size;

            IWebElement campaignPriceProductPage = driver.FindElement(By.CssSelector(".campaign-price"));
            string textCampaignPriceProductPage = campaignPriceProductPage.Text;
            Color colorCampaignPriceProductPage = ParseColor(campaignPriceProductPage.GetCssValue("color"));
            string fontWeightCampaignPriceProductPage = campaignPriceProductPage.GetCssValue("font-weight");
            Size sizeCampaignPriceproductPage = campaignPriceProductPage.Size;

            NUnit.Framework.Assert.True(sizeRegularPriceProductPage.Height < sizeCampaignPriceproductPage.Height);
            NUnit.Framework.Assert.True(sizeRegularPriceProductPage.Width < sizeCampaignPriceproductPage.Width);
            NUnit.Framework.Assert.True(IsGrey(colorRegularPriceProductPage));
            NUnit.Framework.Assert.True(textDecorationRegularPriceProductPage.Contains("line-through"));
            NUnit.Framework.Assert.True(IsRed(colorCampaignPriceProductPage));
            NUnit.Framework.Assert.True(fontWeightCampaignPriceProductPage.Equals("700")); 



            NUnit.Framework.Assert.True(textMainPage.Equals(textProductPage));
            NUnit.Framework.Assert.True(textRegularPriceProductPage.Equals(textRegularPriceMainPage));
            NUnit.Framework.Assert.True(textCampaignPriceProductPage.Equals(textCampaignPriceMainPage));

        }

        public bool IsRed(Color color)
        {
            return color.B == 0 && color.G == 0;
        }
        public bool IsGrey(Color color)
        {
                return color.B == color.G && color.R == color.G;
        }
        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
