
using SpecFlowTemplate.Utils;
using TechTalk.SpecFlow;
using static SpecFlowTemplate.Utils.RestConnection;
using static SpecFlowTemplate.Utils.ConfigurationProperties;
using static SpecFlowTemplate.Utils.Properties;



namespace SpecFlowTemplate.Hooks
{
    [Binding]
    internal class RestHooks
    {
        [BeforeScenario("api")]
        public void InitRestClinet()
        {
            initConnection(GetConfigurationProperty().EnvironmentSpeciicProperty(API_URL));
        }

    }
}