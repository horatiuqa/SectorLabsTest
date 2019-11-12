using Microsoft.VisualStudio.TestTools.UnitTesting;
using SectorLabsTest.Pages;
using SectorLabsTest.Tools;
using System.Threading;

namespace SectorLabsTest.Tests
{
    [TestClass]
    public class VerifyThatExtraFiltersAreCorrectlySet : BaseTest
    {
      
        [TestMethod]
        public void VerifyThatExtraFiltersAreCorrectlySetTest()
        {
            DetailsPage detailspage = new DetailsPage(driver);
            HomePage homepage = new HomePage(driver);
            ResultsPage resultspage = new ResultsPage(driver);
            BasePage basepage = new BasePage(driver);

            basepage.GoToUrl(Constants.Url);

            homepage.SelectDestination(Constants.Destination);
            homepage.SelectCheckInDate();
            homepage.SelectCheckOutDate();
            homepage.SelectGuests(2,1,0);
            homepage.ClickSearchButton();

            Thread.Sleep(2000);
            resultspage.ClickMoreFilters();
            resultspage.IncreaseBedrooms(5);
            int selectedNrOfBedrooms = resultspage.ExtractNrOfBedroomsFromFilter();
            resultspage.SelectPoolFacility();                    
            resultspage.ClickShowStaysButton();

            resultspage.VerifyNumberOfAppliedFilters(2);
            resultspage.VerifyTheNrOfBedrooms(selectedNrOfBedrooms);
            resultspage.OpenFirstSearchResult();
            detailspage.VerifyFacilities(Constants.Pool);

            basepage.QuitDriver();
        }
    }
}


