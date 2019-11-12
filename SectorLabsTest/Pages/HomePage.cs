using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Threading;

namespace SectorLabsTest.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)    
        {
           
        }

        private static readonly By _checkInInput = By.CssSelector("#checkin_input");

        private static readonly By _checkOutInput = By.CssSelector("#checkout_input");

        public static readonly By _nextMonthButton = By.CssSelector("._17w2za div:nth-child(2)");

        private static readonly By _searchBar = By.CssSelector("#Koan-magic-carpet-koan-search-bar__input");

        private static readonly By _firstListValue = By.CssSelector("._sbonnk li:nth-child(1)");

        private static readonly By _searchButton = By.CssSelector("._1r868w button");

        private static readonly By _guestsPicker = By.CssSelector("#lp-guestpicker");

        private static readonly By _incrementAdults = By.CssSelector("div[aria-labelledby*='search_bar-adults'] svg[aria-label='add']");

        private static readonly By _incrementChildren = By.CssSelector("div[aria-labelledby*='search_bar-children'] svg[aria-label='add']");

        private static readonly By _incrementInfants = By.CssSelector("div[aria-labelledby*='search_bar-infants'] svg[aria-label='add']");

        private static readonly By _saveButton = By.CssSelector("#filter-panel-save-button");


        public void SelectCheckInDate()
        {
            SelectCalendar(7, "._f4ecdav td[aria-label^='Choose'][aria-label*='");
        }

        public void SelectCalendar(int nrDays, string selector)
        {
            var currentDate = DateTime.Today;
            int today = currentDate.Day;
            int numberOfDays = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            string currentMonth = currentDate.ToString("MMMM", CultureInfo.InvariantCulture);
            if (nrDays == 7)
                driver.FindElement(_checkInInput).Click();
            else
                driver.FindElement(_checkOutInput).Click();

            if (today + nrDays > numberOfDays)
            {
                currentDate = currentDate.AddMonths(1);
                currentMonth = currentDate.ToString("MMMM", CultureInfo.InvariantCulture);
                today = (today + nrDays) - numberOfDays;
                driver.FindElement(_nextMonthButton).Click();
            }
            else
            {
                today += nrDays;
            }

            IWebElement calendar = driver.FindElement(By.CssSelector(selector + currentMonth + " " + today + "']"));
            calendar.Click();
        }

        public void SelectCheckOutDate()
        {
            SelectCalendar(14, "._1ge5ctyw td[aria-label^='Choose'][aria-label*='");
        }

        public string ExtractDestination()
        {
            driver.FindElement(_searchBar).Click();
            string selectedDestination = driver.FindElement(_firstListValue).Text.ToString(); 
            string[] values = selectedDestination.Split(',');
            string city = values[0];
            string[] city2 = city.Split('\n');
            string city3 = city2[1];
            return city3;
        }

        public int ExtractNrOfGuests()
        {
            string text = driver.FindElement(_guestsPicker).Text.ToString();
            string[] values = text.Split(' ');
            int guests = Convert.ToInt32(values[0]);
            return guests;
        }

        public void ClickSearchButton()
        {
            driver.FindElement(_searchButton).Click();
        }

        public string SelectDestination(string destination)
        {
            driver.FindElement(_searchBar).Click();
            driver.FindElement(_searchBar).SendKeys(destination);
            Thread.Sleep(1000);

            string city = ExtractDestination();

            driver.FindElement(_firstListValue).Click();

            return city;
        }


        public void SelectGuests(int adults, int children, int infants)
        {
            driver.FindElement(_guestsPicker).Click();

            int i = 0;
            while (i < adults)
            {
                driver.FindElement(_incrementAdults).Click();
                i++;
            }
            int j = 0;
            while(j < children)
            {
                driver.FindElement(_incrementChildren).Click();
                j++;
            }
            int k = 0;
            while(k < infants)
            {
                driver.FindElement(_incrementInfants).Click();
                k++;
            }
            driver.FindElement(_saveButton).Click();
        }
    }
}
