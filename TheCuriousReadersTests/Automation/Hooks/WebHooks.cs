using SpecFlowTemplate.Utils;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SpecFlowTemplate.Hooks
{
    [Binding]
    public sealed class WebHooks
    {

        [BeforeScenario("web")]
        public void InitScenario()
        {
            WebDriverProvider.InitDriver();
        }

        [AfterScenario("web")]
        public void TearDownScenario()
        {
            IWebDriver driver = WebDriverProvider.GetPreparedDriver();
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
