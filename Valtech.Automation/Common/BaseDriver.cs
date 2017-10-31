using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace ValTechAUT.AcceptanceTests.Common
{
    public class BaseDriver
    {
        public static IWebDriver Driver;

        public void LaunchBrowser()
        {
            if (Driver == null)
            {
                switch (ConfigurationManager.AppSettings["Browser"])
                {
                    case "IE":
                        //Implement the drive initiation
                        Console.WriteLine("STARTED ON -- IE");
                        break;

                    case "Firefox":
                        Console.WriteLine("STARTED ON -- FIREFOX");
                        break;

                    case "Chrome":
                        var chromeoptions = new ChromeOptions();
                        Driver = new ChromeDriver(chromeoptions);
                        Console.WriteLine("STARTED ON -- CHROME");
                        break;

                    default:
                        throw new Exception("Driver Not set Properly");
                }
                Driver.Manage().Window.Maximize();
                Driver.Manage().Cookies.DeleteAllCookies();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            }


        }

        public void EndBrowser()
        {
            if (Driver != null)
            {
                Console.WriteLine("Exiting Driver ......");
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }

        public void CleanUpBrowser()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
