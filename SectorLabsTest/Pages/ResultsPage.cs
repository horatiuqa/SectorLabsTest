using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SectorLabsTest.Pages
{
    public class ResultsPage : BasePage
    {
        public ResultsPage(IWebDriver driver) : base(driver)
        {

        }

        private static readonly By _period = By.CssSelector("._v9zlaz6");   

        private static readonly By _guests = By.CssSelector("._y5zuqbb");   

        private static readonly By _destination = By.CssSelector("label[for='Koan-via-SearchHeader__input'] input");  

        private static readonly By _moreFiltersButton = By.CssSelector("#menuItemButton-dynamicMoreFilters");

        private static readonly By _bedroomsFilterCounter2 = By.CssSelector("div[aria-labelledby*='StepIncrementerRow-title-filterItem-stepper-min_bedrooms']>div>div:nth-child(2)>div>div:nth-child(2)>div");
        private static readonly By _bedroomsFilterCounter = By.CssSelector("[id*='filterItem-stepper-min_bedrooms'] span:nth-child(1)");

        private static readonly By _resultsList = By.CssSelector("._fhph4u [itemprop='itemListElement']");

        private static readonly By _increaseBedroomsCount = By.CssSelector("[aria-label='increase value'][aria-describedby*='bedrooms']");   //varianta modal
        private static readonly By _increaseBedroomsCount2 = By.CssSelector("[aria-labelledby*='StepIncrementerRow-title-filterItem-stepper-min_bedrooms-'] svg[aria-label='add']");

        private static readonly By _showStaysButton = By.CssSelector("._f5frt2 button:nth-child(2)");

        private static readonly By _poolRadioButton = By.XPath("/html/body/div[9]/section/div/div/div[2]/div[5]/div/div/div[4]/label/span[1]/span");


        public void VerifyThatApartmentPinChangesDuringHover()
        {
            IList<IWebElement> pins = driver.FindElements(By.CssSelector("div[class='sticky-inner-wrapper'] > div button > div > div"));

            string color1 = pins[0].GetAttribute("style");

            Actions hover = new Actions(driver);
            hover.MoveToElement(driver.FindElement(_resultsList)).Build().Perform();

            string color2 = pins[0].GetAttribute("style");

            Assert.AreNotSame(color1, color2, "The attribute didn't change");
        }

        public void VerifyNumberOfAppliedFilters(int appliedFilters)
        {
            Thread.Sleep(2000);
            string morefilterslabel = driver.FindElement(_moreFiltersButton).Text;
            string[] values = morefilterslabel.Split(' ');
            int nrOfFilters = Convert.ToInt32(values[3]);

            Assert.IsTrue(nrOfFilters.Equals(appliedFilters), "Incorrect nr of applied filters is displayed");
        }

        public void OpenFirstSearchResult()
        {
            driver.FindElement(_resultsList).Click();
        }

        public void SelectPoolFacility()
        {
            driver.FindElement(_poolRadioButton).Click();
        }

        public void ClickShowStaysButton()
        {
            driver.FindElement(_showStaysButton).Click();
        }
        public void ClickMoreFilters()
        {
            Thread.Sleep(1000);
            driver.FindElement(_moreFiltersButton).Click();
        }

        public void IncreaseBedrooms(int bedroomCount)
        {
           Thread.Sleep(2000);

           int i = 0;
           while(i < bedroomCount)
            {

                if (driver.FindElements(_increaseBedroomsCount).Count !=0)
                {
                    driver.FindElement(_increaseBedroomsCount).Click();
                    i++;
                }
                else
                {
                    driver.FindElement(_increaseBedroomsCount2).Click();
                    i++;
                }
            }
        }

        public int ExtractNrOfBedroomsFromFilter()
        {
            Thread.Sleep(2000);
            if (driver.FindElements(_bedroomsFilterCounter2).Count != 0)
            {
                int selectedNrOfBedrooms = Convert.ToInt32(driver.FindElement(_bedroomsFilterCounter2).Text.ToString());
                return selectedNrOfBedrooms;
            }
            else
            {
                int selectedNrOfBedrooms = Convert.ToInt32(driver.FindElement(_bedroomsFilterCounter).Text.ToString());
                return selectedNrOfBedrooms;
            }

        }
        public void VerifyThatApartmentsCanFitGuests(int guests)
        {
            IList<IWebElement> _listOfCapacities = driver.FindElements(By.XPath("//div[contains(text(),'guests')]"));

            foreach (var apartament in _listOfCapacities)
            {
                string fullcapacity = apartament.Text.ToString();
                string[] values = fullcapacity.Split(' ');
                int capacity = Convert.ToInt32(values[0]);
                Assert.IsTrue(capacity >= guests, "Not all appartments can fit the desired number of guests");
            }
        }

        public void VerifyTheNrOfBedrooms(int selectedNrOfBedrooms)
        {
            IList<IWebElement> _listOfCapacities = driver.FindElements(By.XPath("//div[contains(text(),'guests')]"));

            foreach (var apartment in _listOfCapacities)
            {
                string fullcapacity = apartment.Text.ToString();
                string[] values = fullcapacity.Split(' ');
                int displayedBedrooms = Convert.ToInt32(values[3]);

                Assert.IsTrue(selectedNrOfBedrooms <= displayedBedrooms, "Not all appartments have the desired nr of bedrooms");
            }
        }

        public void VerifyThatTheDestinationIsCorrect(string city)
        {
            string location = driver.FindElement(_destination).GetAttribute("value");

            Assert.IsTrue(location.Contains(city), "The location is incorrect");
        }

        public void VerifyThatThePeriodIsCorrect()
        {
            string perioada = driver.FindElement(_period).Text;
            string[] days = perioada.Split(' ');
            var checkinday = Convert.ToInt32(days[1]);
            var checkoutday = Convert.ToInt32(days[3]);
            int period = checkoutday - checkinday;

            Assert.IsTrue(period == 7, "The time period is incorrectly displayed");
        }

        public void VerifyThatTheNumberOfGuestsIsCorrect(int guests)
        {
            string totalGuests = driver.FindElement(_guests).Text;
            string[] values = totalGuests.Split(' ');
            int nrOfGuests = Convert.ToInt32(values[0]);

            Assert.IsTrue(nrOfGuests == guests, "The displayed nr of guests is incorrect");
        }

    }
}
