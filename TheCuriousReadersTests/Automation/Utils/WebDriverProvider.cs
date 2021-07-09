using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Runtime.CompilerServices;
using static SpecFlowTemplate.Utils.ConfigurationProperties;

namespace SpecFlowTemplate.Utils
{
    class WebDriverProvider
    {
        public const string CHROME_DRIVER = "chrome";

        private const string FIREFOX_DRIVER = "firefox";

        private static IWebDriver _driver;

        public static IWebDriver Driver { get => _driver; set => _driver = value; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IWebDriver GetPreparedDriver() => _driver;

        public static void InitDriver(string desireDriver)
        {
            switch (desireDriver)
            {
                case CHROME_DRIVER: CreateChromeDriver(); break;
                case FIREFOX_DRIVER: CreateFireFoxDriver(); break;
            }

            double implicitTimeout = Double.Parse(GetConfigurationProperty()
                                                    .Named(Properties.DRIVER_IMPLICIT_WAIT));

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitTimeout);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void InitDriver()
        {
            String desireDriver = GetConfigurationProperty().Named(Properties.DRIVER).ToLower();
            InitDriver(desireDriver);
        }

        private static void CreateChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();

            string browserMode = GetConfigurationProperty().Named(Properties.BROWSER_WINDOW_MODE);
            if (browserMode != null && "maximized".Equals(browserMode.ToLower()))
            {
                options.AddArguments("start-maximized");
            }

            _driver = new ChromeDriver(options);
        }

        private static void CreateFireFoxDriver()
        {
            _driver = new FirefoxDriver();
        }
    }
}