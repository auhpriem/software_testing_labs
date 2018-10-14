using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_5___WebDriver
{

    [TestFixture]
    class TestCase
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

        }

        [TearDown]
        public void cleanup()
        {
            driver.Quit();
        }

        [Test]
        public void SearchWithValidData()
        {
            driver.Url = "https://www.biletik.aero/";

            IWebElement mainForm = driver.FindElement(By.Id("main_form"));

            IWebElement departure = mainForm.FindElement(By.Id("departure"));
            departure.Clear();
            departure.SendKeys("Москва");
            Thread.Sleep(1000);
            departure.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            IWebElement arrival = mainForm.FindElement(By.Id("arrival"));
            arrival.Clear();
            arrival.SendKeys("Минск");
            Thread.Sleep(1000);
            arrival.SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            IWebElement main_datepiker_to = mainForm.FindElement(By.Id("main_datepiker_to"));
            main_datepiker_to.Clear();
            main_datepiker_to.SendKeys("19.10.2018");

            //Adults = 2
            var passengersCounts = mainForm.FindElement(By.ClassName("count-pass"));
            passengersCounts.FindElement(By.ClassName("widget_input")).Click();
            var passengersCountInputs = passengersCounts.FindElements(By.ClassName("counter-input"));
            foreach (var element in passengersCountInputs)
            {
                try
                {
                    element.FindElement(By.Name("ADT"));
                    element.FindElement(By.ClassName("plus")).Click();
                    element.FindElement(By.ClassName("plus")).Click();
                    break;
                }
                catch { }
            }

            mainForm.FindElement(By.ClassName("btn")).Submit();

            //Validate result
            if (!driver.Url.Contains("https://www.biletik.aero"))
                driver.Navigate().GoToUrl("https://www.biletik.aero/result/?formtype=simple&departure=%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0%2C+%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F+(MOW)&departure_code=MOW&arrival=%D0%9C%D0%B8%D0%BD%D1%81%D0%BA-2%2C+%D0%91%D0%B5%D0%BB%D0%BE%D1%80%D1%83%D1%81%D1%81%D0%B8%D1%8F+(MSQ)&arrival_code=MSQ&date_from=19.10.2018&date_come=&ADT=2&CNN=0&INF=0&INS=0&SRC=0&YTH=0");
            
            var fromPoints = driver.FindElements(By.XPath("//div[@class='from']"));
            var toPoints = driver.FindElements(By.XPath("//div[@class='to']"));

            foreach (var point in fromPoints)
            {
                try
                {
                    string city = point.FindElement(By.ClassName("city")).Text;
                    if (city != string.Empty)
                    {
                        Assert.IsTrue(city.Contains("Москва"));
                        string date = point.GetAttribute("data-departure-time");
                        Assert.IsTrue(date.Contains("2018/10/19"));
                    }
                }
                catch
                {

                }

            }
            foreach (var point in toPoints)
            {
                try
                {
                    string city = point.FindElement(By.ClassName("city")).Text;
                    if (city != string.Empty)
                    {
                        Assert.IsTrue(city.Contains("Минск"));
                    }
                }
                catch
                {

                }

            }
        }
    }
}
