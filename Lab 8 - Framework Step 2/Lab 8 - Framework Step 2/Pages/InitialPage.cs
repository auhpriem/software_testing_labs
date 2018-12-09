using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8___Framework_Step_2.Pages
{
    class InitialPage
    {

        private const string BASE_URL = "https://www.biletik.aero/";

        private IWebDriver driver;

        public InitialPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        [FindsBy(How = How.Id, Using = "main_form")]
        public IWebElement MainForm;

        [FindsBy(How = How.Id, Using = "departure")]
        public IWebElement Departure;

        [FindsBy(How = How.XPath, Using = "//div[@class='autocomplete-suggestions']")]
        public IList<IWebElement> AutocompleteSuggestions;

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(BASE_URL);
            Console.WriteLine("Initial page opened");
        }
    }
}
