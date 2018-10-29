using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6___PageObjectPattern
{
    class InitialPage
    {
        private IWebDriver driver;

        public InitialPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "main_form")]
        public IWebElement MainForm;

        [FindsBy(How = How.Id, Using = "departure")]
        public IWebElement Departure;

        [FindsBy(How = How.XPath, Using = "//div[@class='from']")]
        public IList<IWebElement> FromPoints;

        [FindsBy(How = How.XPath, Using = "//div[@class='to']")]
        public IList<IWebElement> ToPoints;

        public void ValidateToPoints(string city)
        {
            foreach (var point in ToPoints)
            {
                try
                {
                    string cityAttribute = point.FindElement(By.ClassName("city")).Text;
                    if (cityAttribute != string.Empty)
                    {
                        Assert.IsTrue(cityAttribute.Contains(city));
                    }
                }
                catch   {    }
            }
        }
        public void ValidateFromPoints(string date, string city)
        {
            foreach (var point in FromPoints)
            {
                try
                {
                    string cityAttribute = point.FindElement(By.ClassName("city")).Text;
                    if (cityAttribute != string.Empty)
                    {
                        Assert.IsTrue(cityAttribute.Contains(city));
                        string dateAttribute = point.GetAttribute("data-departure-time");
                        Assert.IsTrue(dateAttribute.Contains(date));
                    }
                }
                catch
                {

                }

            }
        }
        public void goToPage()
        {
            driver.Navigate().GoToUrl("https://www.biletik.aero/");
        }
    }

    class FormObject
    {
        private IWebElement MainForm;

        public FormObject(IWebElement MainForm)
        {
            this.MainForm = MainForm;
            PageFactory.InitElements(MainForm, this);
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
    }

    class PassengersInput
    {
        private IWebElement PassengerInput;

        public PassengersInput(IWebElement PassengerInput)
        {
            this.PassengerInput = PassengerInput;
            PageFactory.InitElements(PassengerInput, this);
        }

        [FindsBy(How = How.ClassName, Using = "widget_input")]
        public IWebElement CountPassWidgetInput;

        [FindsBy(How = How.ClassName, Using = "counter-input")]
        public IList<IWebElement> CountPassInputs;

        public void PlusAdult()
        {
            foreach (var element in CountPassInputs)
            {
                try
                {
                    element.FindElement(By.Name("ADT"));
                    element.FindElement(By.ClassName("plus")).Click();
                    break;
                }
                catch { }
            }
        }
    }
}
