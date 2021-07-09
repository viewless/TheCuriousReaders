using NUnit.Framework;
using SpecFlowTemplate.Steps.Actions;
using SpecFlowTemplate.UIComponents;
using SpecFlowTemplate.Utils;
using TechTalk.SpecFlow;

namespace TheCuriousReaderTests.Steps
{
    [Binding]
    public class LoginTestsSteps
    {
       
        private readonly BaseUserActions _user;

        public LoginTestsSteps(BaseUserActions user)
        {
            _user = user;
        }

        [Given(@"user is on login page")]
        public void GivenUserIsOnLoginPage()
        {
            _user.OpensPage("Landing");
        }

        [When(@"enter with ""(.*)"" and ""(.*)""")]
        public void WhenEnterWithAnd(string email, string password)
        {
            _user.TypesInto(LandingPage.EMIAL_FIELD, email);
            _user.TypesInto(LandingPage.PASSWORD_FIELD, password);
            _user.ClicksOn(LandingPage.LOGIN_BUTTON);
        }
        
        [Then(@"should successfully enter to home page")]
        public void ThenShouldSuccessfullyEnterToHomePage()
        {
            Assert.That(_user.ReadsTextFrom(HomePage.HOME_PAGE_HEADER).Contains("Books added within the last 2 weeks:"));
        }

        [Then(@"should receive error message ""(.*)""")]
        public void ThenShouldReceiveErrorMessage(string message)
        {
            Assert.That(_user.ReadsTextFrom(LandingPage.ERROR_MESSAGE).Contains(message));
        }

    }
}
