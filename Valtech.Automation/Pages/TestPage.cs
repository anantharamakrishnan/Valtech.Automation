using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValTechAUT.AcceptanceTests.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.ObjectModel;

namespace ValTechAUT.AcceptanceTests.Pages
{
    public class TestPage : BasePage
    {

        public TestPage()
        {
            //PageFactory.InitElements(Driver, this);
        }

        #region XPath
        private const string latestNewsHeaderXPath = "//div[@class='news-post__listing-header']/header/h2";
        private const string verifyPageDataXPath = "//h2[.='$CHANGE DATA$']";
        private const string tabMenuXPath = "//div[@id='navigationMenuWrapper']//span[.='$CHANGEDATA$']";
        private const string pageHeaderXPath = "//header[@class='page-header']/h1";
        private const string globeIconMenuXPath = "//*[contains(@data-icon,'contact')]";
        private const string allContactUsXPath = "//a[starts-with(@href,'/about/contact-us')]";
        #endregion

        public void NavigateToTabMenu(string tabMenu)
        {
            //This method would be a generic method to switch any tab navigation tab menus
            Driver.FindElement(By.XPath(tabMenuXPath.Replace("$CHANGEDATA$", tabMenu))).Click();

        }

        public void NavigateToContactPage()
        {
            Driver.FindElement(By.XPath(globeIconMenuXPath)).Click();
        }

        public void VerifyLatestNewsDisplayed()
        {

            Assert.IsTrue(Driver.ValidatePage("Latest News"));

        //Alternatively
           string getLNtext = Driver.FindElement(By.XPath(latestNewsHeaderXPath)).Text;
           Assert.AreEqual("Latest News", getLNtext, "Latest News section is not displayed on the website");

            //This is Another alternate approach. Hard coding is done only for the demonstration purpose. Refer next scenario for passing it from scenario
           Assert.IsTrue(Driver.FindElement(By.XPath(verifyPageDataXPath.Replace("$CHANGEDATA$", "Latest News"))).Displayed);
        }


        public void VerifyPageHeader(string verifyTitle)
        {
            string getHeadertext = Driver.FindElement(By.XPath(pageHeaderXPath)).Text;
            Assert.AreEqual(verifyTitle, getHeadertext, "Latest News section is not displayed on the website");
        }



        internal int getOfficeCount()
        {
            ReadOnlyCollection<IWebElement> links = Driver.FindElements(By.XPath(allContactUsXPath));

            return links.Count;
        }
    }
}
