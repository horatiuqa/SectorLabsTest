using Microsoft.VisualStudio.TestTools.UnitTesting;
using SectorLabsTest.Pages;
using SectorLabsTest.Tools;

namespace SectorLabsTest.Tests
{
    [TestClass]
    public class VerifyThatResultsMatchSeacrhCriteria : BaseTest
    {
        
        [TestMethod]
        public void VerifyThatResultsMatchSeacrhCriteriaTest()
        {
            ResultsPage resultspage = new ResultsPage(driver);
            HomePage homepage = new HomePage(driver);
            BasePage basepage = new BasePage(driver);

            basepage.GoToUrl(Constants.Url);

            string city = homepage.SelectDestination(Constants.Destination);
            homepage.SelectCheckInDate();
            homepage.SelectCheckOutDate();
            homepage.SelectGuests(2, 1, 0);
           
            int guests = homepage.ExtractNrOfGuests();
            homepage.ClickSearchButton();

            resultspage.VerifyThatTheDestinationIsCorrect(city);
            resultspage.VerifyThatApartmentsCanFitGuests(guests);
            resultspage.VerifyThatTheNumberOfGuestsIsCorrect(guests);
            resultspage.VerifyThatThePeriodIsCorrect();

            basepage.QuitDriver();
        }
    }
}

