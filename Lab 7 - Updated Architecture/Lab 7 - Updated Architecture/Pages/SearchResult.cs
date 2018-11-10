using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7___Updated_Architecture.Pages
{
    class SearchResult
    {
        private IWebDriver driver;

        public SearchResult(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='from']")]
        public IList<IWebElement> FromPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='to']")]
        public IList<IWebElement> ToPoints;

        public List<string> GetCityToList(string city)
        {
            List<string> cityList = new List<string>();

            foreach (var point in ToPoints)
            {
                try
                {
                    string cityAttribute = point.FindElement(By.ClassName("city")).Text;
                    if (cityAttribute != string.Empty)
                    {
                        cityList.Add(cityAttribute);
                    }
                }
                catch { }
            }
            return cityList;
        }
        public List<string> GetCityFromList(string date, string city)
        {
            List<string> cityList = new List<string>();
            foreach (var point in FromPoints)
            {
                try
                {
                    string cityAttribute = point.FindElement(By.ClassName("city")).Text;
                    if (cityAttribute != string.Empty)
                    {
                        cityList.Add(cityAttribute);
                    }
                }
                catch
                {

                }

            }
            return cityList;
        }
        public List<string> GetDateFromList(string date, string city)
        {
            List<string> dateFromList = new List<string>();
            foreach (var point in FromPoints)
            {
                try
                {
                    if (point.FindElement(By.ClassName("city")).Text != string.Empty)
                    {
                        dateFromList.Add(point.GetAttribute("data-departure-time"));
                    }
                }
                catch
                {

                }

            }
            return dateFromList;
        }
    }
}
