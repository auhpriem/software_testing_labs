using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7___Updated_Architecture.Steps
{
    class Steps
    {
        IWebDriver driver;

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

    }
}
