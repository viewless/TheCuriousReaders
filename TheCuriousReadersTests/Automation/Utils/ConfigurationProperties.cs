using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace SpecFlowTemplate.Utils
{
    internal class ConfigurationProperties
    {
        private readonly IConfiguration _configuration;
        private readonly String _targetEnv;

        private const String TARGET_ENVIRONMENT_KEY = "Environment";
        private const String DEFAULT_ENV = "env:default";
        private const String HOST = "host";
        private const String PAGES = "pages_path";

        private static ConfigurationProperties _configurationProperties;

        private ConfigurationProperties()
        {
            _targetEnv = SetTargetEnvironment();

            Console.WriteLine($"Will be using environment {_targetEnv}");
            Console.WriteLine($"Has agrgs: {Environment.GetCommandLineArgs()}");
            _configuration = BuildConfig();
        }

        private String SetTargetEnvironment()
        {
            String keyValue = Environment.GetEnvironmentVariable(TARGET_ENVIRONMENT_KEY);
            return String.IsNullOrEmpty(keyValue) ? DEFAULT_ENV : keyValue;
        }

        private IConfiguration BuildConfig()
        {
            var configurationBuilder = new ConfigurationBuilder();

            string directoryName = Path.GetDirectoryName(typeof(ConfigurationProperties).Assembly.Location);
            configurationBuilder.AddJsonFile(Path.Combine(directoryName, @"appSettings.json"));

            return configurationBuilder.Build();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ConfigurationProperties GetConfigurationProperty()
        {
            if (_configurationProperties == null)
            {
                _configurationProperties = new ConfigurationProperties();
            }

            return _configurationProperties;
        }

        public string Named(string propertyKey)
        {
            return _configuration.GetValue<string>(propertyKey);
        }

        public string EnvironmentSpeciicProperty(string propertyKey)
        {
            return _configuration.GetValue<string>($"{_targetEnv}:{propertyKey}");
        }

        public string AddressOfPage(string pageName)
        {
            // If needed will provide logic fo different environments
            string host = _configuration.GetValue<string>($"{_targetEnv}:{HOST}");
            string page = _configuration.GetValue<string>($"{_targetEnv}:{PAGES}:{pageName.ToLower()}");

            return $"{host}/{page}";
        }
    }
}