using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SectorLabsTest.Tests
{
    public class BaseTest
    {

        public IWebDriver driver = new ChromeDriver();   
        //public void Initialize(string url)
        //{
        //    //driver.Navigate().GoToUrl(url);
        //    driver.Manage().Window.Maximize();
        //}
    }
}
