using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8___Framework_Step_2.Pages
{
    class SearchResultPage
    {
        private IWebDriver driver;

        public SearchResultPage(IWebDriver driver, string BASE_URL)
        {
            this.driver = driver;
            if (!driver.Url.Contains("https://www.biletik.aero"))
            {
                driver.Navigate().GoToUrl(BASE_URL);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='from']")]
        public IList<IWebElement> FromPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='to']")]
        public IList<IWebElement> ToPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='row']//a[@class='btn btn-primary']//b")]
        public IList<IWebElement> TicketPrices;

        [FindsBy(How = How.XPath, Using = "//div[@class='row']//a[@class='btn btn-primary']")]
        public IList<IWebElement> TicketOrderLinks;

        [FindsBy(How = How.XPath, Using = "//div[@class='row']//div[@class='flight-time']")]
        public IList<IWebElement> TicketTimes;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_price']")]
        public IWebElement LinkSortPrice;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_price']//span")]
        public IWebElement MinTicketPrice;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_time']")]
        public IWebElement LinkSortTime;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_time']//span")]
        public IWebElement MinTicketPriceByTime;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_priceTime']")]
        public IWebElement LinkSortPriceTime;

        [FindsBy(How = How.XPath, Using = "//a[@class='link_sort_priceTime']//span")]
        public IWebElement MinSortTimePriceTicketPrice;

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

        public List<string> GetDateFromList()
        {
            List<string> dateFromList = new List<string>();
            foreach (var point in FromPoints)
            {
                var CityList = point.FindElements(By.ClassName("city"));
                if (CityList.Count == 1)
                {
                    if (CityList.First().Text != string.Empty)
                    {
                        dateFromList.Add(point.GetAttribute("data-departure-time"));
                    }
                }
            }
            return dateFromList;
        }

        public int GetPriceFromLink()
        {
            string mtp = MinTicketPrice.Text.Replace(" ", "");
            return Convert.ToInt32(mtp);
        }
        public int GetPriceFromLinkByTime()
        {
            string mtp = MinTicketPriceByTime.Text.Replace(" ", "");
            return Convert.ToInt32(mtp);
        }
        public int GetPriceFromLinkSortPriceTime()
        {
            string mtp = MinSortTimePriceTicketPrice.Text.Replace(" ", "");
            return Convert.ToInt32(mtp);
        }

        public List<int> GetTicketPrices()
        {
            List<int> ticketPrices = new List<int>();
            foreach (var el in TicketPrices)
            {
                string mtp = el.Text.Replace(" ", "");
                ticketPrices.Add(Convert.ToInt32(mtp));
            }
            return ticketPrices;
        }

        public List<string> GetTicketTimes()
        {
            List<string> ticketTimes = new List<string>();
            foreach (var el in TicketTimes)
            {
                if (el.Text != string.Empty)
                {
                    ticketTimes.Add(el.Text);
                }
            }
            return ticketTimes;
        }

        public List<Tuple<int, string>> GetTicketPricesByTime()
        {
            List<Tuple<int, string>> valuePairs = new List<Tuple<int, string>>();
            List<string> ticketTimes = new List<string>();
            List<int> ticketPrices = new List<int>();

            while (ticketTimes.Count == 0)
                ticketTimes = GetTicketTimes();
            while (ticketPrices.Count == 0)
                ticketPrices = GetTicketPrices();

            for (int i = 0; i < ticketPrices.Count; i++)
            {
                valuePairs.Add(new Tuple<int, string>(ticketPrices[i], ticketTimes[i]));
            }
            return valuePairs;
        }

        public Tuple<int, string, string> getFirstTicket()
        {
            Tuple<int, string, string> returnData = new Tuple<int, string, string>(0, "", null);

            IList<IWebElement> PageRows = driver.FindElements(By.XPath("//div[@class='row']"));
            foreach (var el in PageRows)
            {
                var FlightTimeList = el.FindElements(By.ClassName("flight-time"));
                if (FlightTimeList.Count == 1)
                {
                    if (FlightTimeList.First().Text != "")
                    {
                        returnData = new Tuple<int, string, string>
                                (
                                Convert.ToInt32(el.FindElement(By.XPath("//a[@class='btn btn-primary']//b")).Text.Replace(" ", "")),
                                FlightTimeList.First().Text,
                                TicketOrderLinks[3].GetAttribute("href")
                                );
                        return returnData;
                    }
                }
            }
            return returnData;
        }
    }
}
