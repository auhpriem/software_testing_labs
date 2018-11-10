using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Lab_7___Updated_Architecture.Tests
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
            Assert.AreEqual(steps.IsCityFromHelperDisplayed(FromPoint), true);
            Assert.AreEqual(steps.IsCityToHelperDisplayed(ToPoint), true);
            Assert.AreEqual(steps.IsPassengerInputHelperDisplayed(), true);
        }
    }
}
