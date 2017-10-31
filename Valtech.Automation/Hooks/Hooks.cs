using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ValTechAUT.AcceptanceTests.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace ValTechAUT.AcceptanceTests.Hooks
{
    [Binding]
    public class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
   
        #region SpecFlowHooks
        
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Need to Implement Environment Setup
            //Snapshot Directory cleanupsetc
        }

        [BeforeScenario]
        public  void BeforeScenario()
        {
            new BaseDriver().LaunchBrowser();

        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                //Implement the logic to take Screenshots for further references
                new BaseDriver().EndBrowser();
            }
            //new BaseDriver().CleanUpBrowser();

        }

        #endregion
        [AfterTestRun]
        public static void AfterTestRun()
        {
            new BaseDriver().EndBrowser();
        }
    }
}
