using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using ValTechAUT.AcceptanceTests.Common;
using ValTechAUT.AcceptanceTests.Pages;

namespace ValtechAUT.AcceptanceTests.Steps
{
    [Binding]
    public sealed class TestSteps
    {
        #region Arrange

        [Given(@"I launch the valtech website")]
        public void GivenILaunchTheValtechWebsite()
        {
            TestPage testpg = new TestPage();

            testpg.launchPage();

        }

       
        #endregion

        #region Act
        [When(@"I navigate to the tab menu ""(.*)""")]
        public void WhenINavigateToTheTabMenu(string tabMenu)
        {
            TestPage testpg = new TestPage();

            testpg.NavigateToTabMenu(tabMenu);
        }

        [When(@"I navigate to the tab menu ""(.*)"" page")]
        public void WhenINavigateToTheTabMenuPage(string p0)
        {
            TestPage testpg = new TestPage();

            testpg.NavigateToContactPage();
        }

        #endregion

        #region Assert

        [Then(@"I should see the latest news displayed on the page")]
        public void ThenIShouldSeeTheLatestNewsDisplayedOnThePage()
        {
            TestPage testpg = new TestPage();

            testpg.VerifyLatestNewsDisplayed();

        }

        [Then(@"I should see the page title as ""(.*)""")]
        public void ThenIShouldSeeThePageTitleAs(string verifyTitle)
        {
            TestPage testpg = new TestPage();

            testpg.VerifyPageHeader(verifyTitle);
        }

        [Then(@"I should be able to calculate the number of offices")]
        public void ThenIShouldBeAbleToCalculateTheNumberOfOffices()
        {
             TestPage testpg = new TestPage();

             int getNoOfOffices = testpg.getOfficeCount();

            Console.WriteLine("The number of Valtech contact us office location includes" + getNoOfOffices);
        }

        #endregion
    }
}
