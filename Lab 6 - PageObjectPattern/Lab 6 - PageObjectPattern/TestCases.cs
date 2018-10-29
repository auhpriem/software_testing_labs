using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Lab_6___PageObjectPattern
{
    [TestFixture]
    class TestCases
    {
        // IWebDriver driver;

        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //"https://www.biletik.aero/"
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void cleanup()
        {
            driver.Quit();
        }

        [Test]
        public void SearchWithValidData()
        {
            string FromPoint = "Москва";
            string ToPoint = "Минск";
            string date = "11.11.2018";
            string dateFormat = "2018/11/11";

            InitialPage page = new InitialPage(driver);
            page.goToPage();
            FormObject MainForm = new FormObject(page.MainForm);

            MainForm.Departure.Clear();
            MainForm.Departure.SendKeys(FromPoint);
            Thread.Sleep(1000);
            MainForm.Departure.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            
            MainForm.Arrival.Clear();
            MainForm.Arrival.SendKeys(ToPoint);
            Thread.Sleep(1000);
            MainForm.Arrival.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            
            MainForm.MainDatepikerTo.Clear();
            MainForm.MainDatepikerTo.SendKeys(date);

            //Adults = 2
            PassengersInput passengersInput = new PassengersInput(MainForm.CountPass);
            passengersInput.CountPassWidgetInput.Click();
            passengersInput.PlusAdult();
            passengersInput.PlusAdult();

            MainForm.SubmitButton.Submit();

            if (!driver.Url.Contains("https://www.biletik.aero"))
                driver.Navigate().GoToUrl("https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from="+date+"&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0");

            //Validate result
            page.ValidateFromPoints(dateFormat, FromPoint);
            page.ValidateToPoints(ToPoint);
        
        }
    }
}
