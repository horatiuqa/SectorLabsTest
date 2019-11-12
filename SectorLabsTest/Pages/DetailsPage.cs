using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;

namespace SectorLabsTest.Pages
{
    public class DetailsPage : BasePage
    {
        public DetailsPage(IWebDriver driver) : base(driver)
        {

        }

        private static readonly By _showAllAmenitiesBtn = By.CssSelector("div[id='amenities']>div button");
        private static readonly By _showAllAmenitiesBtn2 = By.XPath("//div/div/div[4]/div[2]/div/div[1]/div[5]/div/div/section/div/div[3]/a");
        private static readonly By _facilitiesContainer = By.XPath("//h2[div[contains(text(), 'Facilities')]]/following-sibling::div");


        private void ExpandAmenitiesList()
        {
            Actions action = new Actions(driver);
            if (driver.FindElements(_showAllAmenitiesBtn).Count != 0)
            {
            action.MoveToElement(driver.FindElement(_showAllAmenitiesBtn));
            action.Perform();
            Thread.Sleep(500);
            driver.FindElement(_showAllAmenitiesBtn).Click();
            }
            else
            {
                action.MoveToElement(driver.FindElement(_showAllAmenitiesBtn2));
                action.Perform();
                Thread.Sleep(500);
                driver.FindElement(_showAllAmenitiesBtn2).Click();
            }
        }

        public void VerifyFacilities(string facility)
        {
            Actions action = new Actions(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            ExpandAmenitiesList();

            action.MoveToElement(driver.FindElement(_facilitiesContainer));
            action.Perform();
            Assert.IsTrue(driver.FindElement(_facilitiesContainer).Text.Contains(facility), "Facilities not okay");
        }
        
    }
}
