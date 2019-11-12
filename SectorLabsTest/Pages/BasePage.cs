using OpenQA.Selenium;

namespace SectorLabsTest.Pages
{
    public class BasePage
    {
        public IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public void QuitDriver()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
