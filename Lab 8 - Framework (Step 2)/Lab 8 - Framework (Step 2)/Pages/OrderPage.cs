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
        public List<string> GetCityFromList()
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
        public bool ValidateTicketDate(string date)
        {
            foreach (var point in PageRows)
            {
                try
                {
                    if (point.FindElement(By.XPath("//div[@data-date-departure='" + date + "']")) != null)
                    {
                        return true;
                    }
                }
                catch
                {

                }

            }
            return false;
        }

        public bool ValidateOrderInputData(string fromCity, string toCity, int price, string time, string date)
        {
            while (true)
            {
                try
                {
                    if (GetCityToList().First() != "")
                        break;
                }
                catch { }
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
            if (Convert.ToInt32(TicketPrices.First().Text.Replace(" ","").Replace("₽", "")) != price)
                isValid = false;

            return isValid;
        }
        
        public bool CheckPassengerCount(int passCount)
        {
            return passCount == PassengerInputs.Count();
        }
    }
}
