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
    public class CheckCountrySort
    {
        private IWebDriver driver;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void CheckCountrySortTestMethod()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";

            ReadOnlyCollection<IWebElement> countryList = driver.FindElements(By.CssSelector(".row"));
            List<string> coutriesNameList = new List<string>();
            for (int i = 0; i < countryList.Count; i++)
            {
                ReadOnlyCollection<IWebElement> countryRow = countryList[i].FindElements(By.CssSelector("td"));
                coutriesNameList.Add(countryRow[4].Text);

                if (int.Parse(countryRow[5].Text) > 0)
                {
                    driver.Url = countryRow[4].FindElement(By.CssSelector("a")).GetAttribute("href");
                    ReadOnlyCollection<IWebElement> zonesList = driver.FindElements(By.CssSelector("#table-zones tr"));
                    List<string> zonesNameList = new List<string>();
                    for (int j = 1; j < zonesList.Count-1; j++)
                    {
                        ReadOnlyCollection<IWebElement> zoneRow = zonesList[j].FindElements(By.CssSelector("td"));
                        zonesNameList.Add(zoneRow[2].Text);
                    }

                    NUnit.Framework.Assert.True(CheckAlphabeticOrderInList(zonesNameList));

                    driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
                    countryList = driver.FindElements(By.CssSelector(".row"));
                }

            }
            NUnit.Framework.Assert.True(CheckAlphabeticOrderInList(coutriesNameList));

        }

        public bool CheckAlphabeticOrderInList(List<string> list)
        {
            List<string> sortedCoutriesNameList = list.OrderBy(o => o).ToList();
            return sortedCoutriesNameList.SequenceEqual(list);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
