using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8___Framework_Step_2.Pages
{
    class OrderPage
    {
        private IWebDriver driver;

        public OrderPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='row']//div[@class='price']//b")]
        public IList<IWebElement> TicketPrices;

        [FindsBy(How = How.XPath, Using = "//div[@class='row']//div[@class='flight-time']")]
        public IList<IWebElement> TicketTimes;

        [FindsBy(How = How.XPath, Using = "//div[@class='from']")]
        public IList<IWebElement> FromPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='to']")]
        public IList<IWebElement> ToPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='row']")]
        public IList<IWebElement> PageRows;

        [FindsBy(How = How.Id, Using = "order_aj")]
        public IWebElement SubmitOrder;

        [FindsBy(How = How.XPath, Using = "//div[@class='row item_passenger_info']")]
        public IList<IWebElement> PassengerInputs;

        public List<string> GetCityToList()
        {
            List<string> cityList = new List<string>();

            foreach (var point in ToPoints)
            {
                var CityList = point.FindElements(By.ClassName("city"));
                if (CityList.Count == 1)
                {
                    string cityAttribute = CityList.First().Text;
                    if (cityAttribute != string.Empty)
                    {
                        cityList.Add(cityAttribute);
                    }
                }
            }
            return cityList;
        }

        public List<string> GetCityFromList()
        {
            List<string> cityList = new List<string>();
            foreach (var point in FromPoints)
            {
                var CityList = point.FindElements(By.ClassName("city"));
                if (CityList.Count == 1)
                {
                    string cityAttribute = CityList.First().Text;
                    if (cityAttribute != string.Empty)
                    {
                        cityList.Add(cityAttribute);
                    }
                }
            }
            return cityList;
        }

        public bool ValidateTicketDate(string date)
        {
            foreach (var point in PageRows)
            {
                var DataDateDepartureList = point.FindElements(By.XPath("//div[@data-date-departure='" + date + "']"));
                if (DataDateDepartureList != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateOrderInputData(string fromCity, string toCity, int price, string time, string date)
        {
            while (true)
            {
                var CityToList = GetCityToList();
                if (CityToList.Count == 1)
                {
                    if (CityToList.First() != "")
                        break;
                }
            }
            bool isValid = true;
            if (!GetCityToList().First().Contains(toCity))
                isValid = false;
            if (!GetCityFromList().First().Contains(fromCity))
                isValid = false;
            if (!ValidateTicketDate(date))
                isValid = false;
            if (!TicketTimes.First().Text.Contains(time))
                isValid = false;
            if (Convert.ToInt32(TicketPrices.First().Text.Replace(" ", "").Replace("₽", "")) != price)
                isValid = false;

            return isValid;
        }

        public bool CheckPassengerCount(int passCount)
        {
            return passCount == PassengerInputs.Count();
        }
    }
}
