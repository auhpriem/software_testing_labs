using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Lab_8___Framework_Step_2.Tests
{
    [TestFixture]
    class TestCases
    {
        private Steps.Steps steps = new Steps.Steps();

        [SetUp]
        public void Init()
        {
            steps.InitBrowser();
        }

        [TearDown]
        public void Cleanup()
        {
            steps.CloseBrowser();
        }
        [Test]
        public void CheckInputHelpers()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";

            steps.OpenInitialPage();
            Assert.IsTrue(steps.IsCityFromHelperDisplayed(FromPoint));
            Assert.IsTrue(steps.IsCityToHelperDisplayed(ToPoint));
            Assert.IsTrue(steps.IsPassengerInputHelperDisplayed());
        }
        [Test]
        public void CheckTicketSearch()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";
            string DateToFromat = "2018/12/30";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);
            steps.ValidateSearchResult(SearchPageURLInFailedCase,ToPoint, FromPoint, DateToFromat);
            

        }
        [Test]
        public void CheckMinTicketPrice()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            Assert.IsTrue(steps.CheckMinTicketPrice(SearchPageURLInFailedCase));
        }
        [Test]
        public void CheckMinTicketSort()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            List<int> ticketPrices = steps.GetMinTicketPrices(SearchPageURLInFailedCase);
            List<int> expectedticketPrices = ticketPrices.OrderBy(p => p).ToList();
            Assert.IsTrue(expectedticketPrices.SequenceEqual(ticketPrices));
        }
        [Test]
        public void CheckFasterTicket()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            Assert.IsTrue(steps.CheckMinTicketTimeAndPrice(SearchPageURLInFailedCase));
        }
        [Test]
        public void CheckTicketTimeSorting()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            var ticketTimes = steps.GetMinTicketTimes(SearchPageURLInFailedCase);
            var expectedtickeTimes = ticketTimes.OrderBy(p => p).ToList();
            Assert.IsTrue(expectedtickeTimes.SequenceEqual(ticketTimes));
        }
        [Test]
        public void CheckMinPriceFasterTicket()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            Assert.IsTrue(steps.CheckMinPriceFasterTicket(SearchPageURLInFailedCase));
        }
        [Test]
        public void CheckMinPriceFasterTicketSorting()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            var resultList = steps.GetMinPriceFasterTickets(SearchPageURLInFailedCase);
            var expectedList = resultList.OrderBy(p => p.Item2).OrderBy(p => p.Item1).Select(p => p);
            Assert.IsTrue(expectedList.SequenceEqual(resultList));
        }
        [Test]
        public void CheckTicketOrderWithoutValidData()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";
            string DateToFromat = "2018-12-30";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            var firstTicket = steps.OrderFirstTicket(SearchPageURLInFailedCase);

            steps.OpenOrderPage();

            steps.ValidateOrderInputData(DateToFromat, FromPoint, ToPoint, firstTicket.Item1, firstTicket.Item2);

            Assert.IsTrue(steps.IsErrorOnSubmitOrder());
        }
        [Test]
        public void CheckPersonCountInOrder()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string DateTo = "30.12.2018";
            string DateToFromat = "2018-12-30";

            string SearchPageURLInFailedCase = "https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=" + DateTo + "&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0";
            int AdultPasCount = 2;

            steps.OpenInitialPage();
            steps.PopulateArrival(ToPoint);
            steps.PopulateDeparture(FromPoint);
            steps.PopulateDateTo(DateTo);
            steps.PopulateAdultPassengers(AdultPasCount);

            steps.StartSearch(SearchPageURLInFailedCase);

            var firstTicket = steps.OrderFirstTicket(SearchPageURLInFailedCase);

            steps.OpenOrderPage();

            steps.ValidateOrderInputData(DateToFromat, FromPoint, ToPoint, firstTicket.Item1, firstTicket.Item2);

            Assert.IsTrue(steps.CheckPassengerCount(AdultPasCount));
        }
    }
}
