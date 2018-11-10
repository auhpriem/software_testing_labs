using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_7___Updated_Architecture.Pages
{
    class InitialPage_MainForm
    {
        private IWebElement MainForm;
        private IWebDriver driver;

        public InitialPage_MainForm(IWebDriver driver)
        {
            this.driver = driver;
            Pages.InitialPage initialPage = new Pages.InitialPage(this.driver);
            this.MainForm = initialPage.MainForm;
            PageFactory.InitElements(this.MainForm, this);
        }

        [FindsBy(How = How.Id, Using = "departure")]
        public IWebElement Departure;

        [FindsBy(How = How.ClassName, Using = "btn")]
        public IWebElement SubmitButton;

        [FindsBy(How = How.Id, Using = "arrival")]
        public IWebElement Arrival;

        [FindsBy(How = How.Id, Using = "main_datepiker_to")]
        public IWebElement MainDatepikerTo;

        [FindsBy(How = How.ClassName, Using = "count-pass")]
        public IWebElement CountPass;

        [FindsBy(How = How.XPath, Using = "//div[@class='to']")]
        public IList<IWebElement> ToPoints;

        [FindsBy(How = How.XPath, Using = "//input[@class='widget_input counter_input']")]
        public IWebElement CountPassWidgetInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='passan-wrp']")]
        public IWebElement CountPassWrapInput;

        public bool CheckCityHelper()
        {
            List<string> AutocompleteSuggestions = new List<string>();
            foreach (var el in new Pages.InitialPage(this.driver).AutocompleteSuggestions)
                AutocompleteSuggestions.Add(el.GetAttribute("style"));
            return AutocompleteSuggestions.Where(el => !el.Contains("display: none;")).Count() > 0 ? true : false;
        }
        public void PopulateDeparture(string departure, bool withEnterPressing)
        {
            Departure.Clear();
            Departure.SendKeys(departure);
            Thread.Sleep(1000);
            if (withEnterPressing)
            {
                Departure.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
            }
        }
        public void PopulateArrival(string arrival, bool withEnterPressing)
        {
            Arrival.Clear();
            Arrival.SendKeys(arrival);
            Thread.Sleep(1000);
            if (withEnterPressing)
            {
                Departure.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
            }
        }
        public bool CheckPassengerInputHelper()
        {
            CountPassWidgetInput.Click();
            return CountPassWrapInput.GetAttribute("style").Contains("display: block;");
        }
    }
}
