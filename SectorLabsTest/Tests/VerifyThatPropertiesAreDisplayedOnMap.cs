using Microsoft.VisualStudio.TestTools.UnitTesting;
using SectorLabsTest.Pages;
using SectorLabsTest.Tools;

namespace SectorLabsTest.Tests
{
    [TestClass]
    public class VerifyThatPropertiesAreDisplayedOnMap : BaseTest
    {
        [TestMethod]
        public void VerifyThatPropertiesAreDisplayedOnMapTest()
        {
            HomePage homepage = new HomePage(driver);
            ResultsPage resultspage = new ResultsPage(driver);
            BasePage basepage = new BasePage(driver);

            basepage.GoToUrl(Constants.Url);

            homepage.SelectDestination(Constants.Destination);
            homepage.SelectCheckInDate();
            homepage.SelectCheckOutDate();
            homepage.SelectGuests(2,1,0);
            homepage.ClickSearchButton();

            resultspage.VerifyThatApartmentPinChangesDuringHover();

            basepage.QuitDriver();
        }
    }
}
