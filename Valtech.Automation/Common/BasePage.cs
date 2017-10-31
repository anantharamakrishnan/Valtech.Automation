using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValTechAUT.AcceptanceTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace ValTechAUT.AcceptanceTests.Common
{
    public class BasePage 
     {

        public static readonly TimeSpan NoWait = new TimeSpan(0, 0, 0, 0);

        //public static RemoteWebDriver Driver = null;

         protected Driver Driver = new Driver(BaseDriver.Driver);

        #region XPath

        #endregion

         public void launchPage()
         {
             Driver.Url = ConfigurationManager.AppSettings["ValtechBaseUrl"];

         }
    }
}
