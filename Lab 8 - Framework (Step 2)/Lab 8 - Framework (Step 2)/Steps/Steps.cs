using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_8___Framework_Step_2.Steps
{
    class Steps
    {
        IWebDriver driver;
        protected string CurrentOrderURL;

        public void InitBrowser()
        {
            driver = Driver.DriverInstance.GetInstance();
        }

        public void CloseBrowser()
        {
            Driver.DriverInstance.CloseBrowser();
        }

        public void OpenInitialPage()
        {
            Pages.InitialPage InitialPage = new Pages.InitialPage(driver);
            InitialPage.OpenPage();
        }

        public void DoSearchWithData(string cityNameArrival, string cityNameDeparture, string dateTo, int AdutPassengers, string validURL)
        {
            OpenInitialPage();
            PopulateArrival(cityNameArrival);
            PopulateDeparture(cityNameDeparture);
            PopulateDateTo(dateTo);
            PopulateAdultPassengers(AdutPassengers);
            StartSearch(validURL);
        }

        public void PopulateArrival(string cityName)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateArrival(cityName, true);
        }

        public void PopulateDeparture(string cityName)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateDeparture(cityName, true);
        }

        public void PopulateDateTo(string dateTo)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateDateTo(dateTo);
        }

        public void PopulateAdultPassengers(int count)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateAdultPassengers(count);
        }

        public bool IsCityFromHelperDisplayed(string cityName)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateDeparture(cityName, false);
            return initialPage_MainForm.CheckCityHelper();
        }

        public bool IsCityToHelperDisplayed(string cityName)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.PopulateArrival(cityName, false);
            return initialPage_MainForm.CheckCityHelper();
        }

        public bool IsPassengerInputHelperDisplayed()
        {
            return new Pages.InitialPage_MainForm(driver).CheckPassengerInputHelper();
        }

        public void StartSearch(string UrlIfHFaild)
        {
            Pages.InitialPage_MainForm initialPage_MainForm = new Pages.InitialPage_MainForm(driver);
            initialPage_MainForm.StartTicketSearch();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);
        }

        public void ValidateSearchResult(string UrlIfHFaild, string cityTo, string cityFrom, string date)
        {
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);
            foreach(var el in searchResultPage.GetCityToList())
            {
                Assert.IsTrue(el.Contains(cityTo));
            }
            foreach (var el in searchResultPage.GetCityFromList())
            {
                Assert.IsTrue(el.Contains(cityFrom));
            }
            foreach (var el in searchResultPage.GetDateFromList())
            {
                Assert.IsTrue(el.Contains(date));
            }
        }

        public bool CheckMinTicketPrice(string UrlIfHFaild)
        {
            List<int> ticketPrices = new List<int>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);

            while (!searchResultPage.LinkSortPrice.Displayed) { }
            searchResultPage.LinkSortPrice.Click();

            int PriceFromLink = searchResultPage.GetPriceFromLink();
            while (ticketPrices.Count == 0)
                ticketPrices = searchResultPage.GetTicketPrices();
            return ticketPrices.Min() == PriceFromLink;
        }

        public List<int> GetMinTicketPrices(string UrlIfHFaild)
        {
            List<int> ticketPrices = new List<int>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);

            while (!searchResultPage.LinkSortPrice.Displayed) { }
            searchResultPage.LinkSortPrice.Click();

            while (ticketPrices.Count == 0)
                ticketPrices = searchResultPage.GetTicketPrices();
            return ticketPrices;
        }

        public List<string> GetMinTicketTimes(string UrlIfHFaild)
        {
            List<string> ticketTimes = new List<string>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);
            while (!searchResultPage.LinkSortTime.Displayed) { }
            searchResultPage.LinkSortTime.Click();

            while (ticketTimes.Count == 0)
                ticketTimes = searchResultPage.GetTicketTimes();
            return ticketTimes;
        }

        public bool CheckMinTicketTimeAndPrice(string UrlIfHFaild)
        {
            List<string> ticketTimes = new List<string>();
            List<int> ticketPrices = new List<int>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);

            while (!searchResultPage.LinkSortTime.Displayed) { }
            searchResultPage.LinkSortTime.Click();

            int PriceFromLink = searchResultPage.GetPriceFromLinkByTime();
            while (ticketPrices.Count == 0)
                ticketPrices = searchResultPage.GetTicketPrices();

            return searchResultPage.GetTicketPrices().First() == PriceFromLink;
        }

        public bool CheckMinPriceFasterTicket(string UrlIfHFaild)
        {
            List<Tuple<int, string>> keyValues = new List<Tuple<int, string>>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);

            while (!searchResultPage.LinkSortPriceTime.Displayed) { }
            searchResultPage.LinkSortPriceTime.Click();

            int PriceFromLink = searchResultPage.GetPriceFromLinkSortPriceTime();
            while (keyValues.Count == 0)
                keyValues = searchResultPage.GetTicketPricesByTime();
            return keyValues.OrderBy(p => p.Item2).OrderBy(p => p.Item1).Select(p=>p.Item1).First() == PriceFromLink;
        }

        public List<Tuple<int, string>> GetMinPriceFasterTickets(string UrlIfHFaild)
        {
            List<Tuple<int, string>> keyValues = new List<Tuple<int, string>>();
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);

            while (!searchResultPage.LinkSortPriceTime.Displayed) { }
            searchResultPage.LinkSortPriceTime.Click();

            int PriceFromLink = searchResultPage.GetPriceFromLinkSortPriceTime();
            while (keyValues.Count == 0)
                keyValues = searchResultPage.GetTicketPricesByTime();
            return keyValues;
        }

        public Tuple<int, string> OrderFirstTicket(string UrlIfHFaild)
        {
            Pages.SearchResultPage searchResultPage = new Pages.SearchResultPage(driver, UrlIfHFaild);
            while (!searchResultPage.LinkSortPrice.Displayed) { }
            var firstTicket = searchResultPage.getFirstTicket();
            CurrentOrderURL = firstTicket.Item3;
            return new Tuple<int, string>(firstTicket.Item1, firstTicket.Item2);
        }

        public void OpenOrderPage()
        {
            driver.Navigate().GoToUrl(CurrentOrderURL);
        }

        public void ValidateOrderInputData(string date, string cityFrom, string cityTo, int price, string time)
        {
            Pages.OrderPage orderPage = new Pages.OrderPage(driver);
            Assert.IsTrue(orderPage.ValidateOrderInputData(cityFrom, cityTo, price, time, date));
        }

        public bool IsErrorOnSubmitOrder()
        {
            Pages.OrderPage orderPage = new Pages.OrderPage(driver);
            Actions actions = new Actions(driver);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0, 1500)");
            actions.MoveToElement(orderPage.SubmitOrder).Click().Perform();
            try
            {
                driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckPassengerCount(int passCount)
        {
            Pages.OrderPage orderPage = new Pages.OrderPage(driver);
            return orderPage.CheckPassengerCount(passCount);
        }
    }
}
