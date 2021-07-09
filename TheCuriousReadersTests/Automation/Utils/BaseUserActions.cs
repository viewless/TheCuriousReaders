using SpecFlowTemplate.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static SpecFlowTemplate.Utils.ConfigurationProperties;

namespace SpecFlowTemplate.Steps.Actions
{
    public class BaseUserActions
    {
        protected readonly IWebDriver _currentPage;
        private readonly WebDriverWait _wait;

        public BaseUserActions()
        {
            this._currentPage = WebDriverProvider.GetPreparedDriver();

            string explicitWaitTimeOut = GetConfigurationProperty().Named(Properties.DRIVER_EXPLICIT_WAIT);

            if (explicitWaitTimeOut == null)
            {
                double timeout = Double.Parse(explicitWaitTimeOut);
                this._wait = new WebDriverWait(_currentPage, TimeSpan.FromMilliseconds(timeout));
            }

            this._wait = new WebDriverWait(_currentPage, TimeSpan.FromMilliseconds(5000));
        }

        internal void StartsApplication()
        {
            OpensPage("Home");
        }

        internal void OpensPage(string pageName)
        {
            _currentPage.Navigate().GoToUrl(GetConfigurationProperty().AddressOfPage(pageName.ToLower().Replace(" ", "_")));
        }

        internal IWebElement WaitsUntilClickable(By elementLocator)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
        }

        internal IWebElement WaitsUntilVisible(By elementLocator)
        {
            return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
        }

        internal void WaitUntilNotVisible(By elementLocator)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
        }

        internal void WaitUntilNotVisible(By elementLocator, TimeSpan timeout)
        {
            TimeSpan originalTimeout = _wait.Timeout;
            _wait.Timeout = timeout;

            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));

            _wait.Timeout = originalTimeout;
        }

        internal void ClicksOn(By elementLocator)
        {
            WaitsUntilClickable(elementLocator).Click();
        }

        internal string ReadsTextFrom(By elementLocator)
        {
            return WaitsUntilVisible(elementLocator).Text.Trim();
        }

        internal void TypesInto(By elementLocator, string text)
        {
            WaitsUntilClickable(elementLocator).Clear();
            Find(elementLocator).SendKeys(text);
        }

        internal void EnterText(string text)
        {
            _currentPage.SwitchTo().ActiveElement().SendKeys(text);
        }

        internal string ReadsFieldValue(By elementLocator)
        {
            return WaitsUntilVisible(elementLocator).GetAttribute("value").Trim();
        }

        internal void SelectsDropDownItem(By dropdownElementLocator, string optionValue)
        {
            IWebElement element = Find(dropdownElementLocator);
            SelectElement dropDownElement = new SelectElement(element);

            if (!dropDownElement.SelectedOption.GetProperty("value").Equals(optionValue)) { 
                dropDownElement.SelectByValue(optionValue);
            }
        }

        internal void SelectsDropDownItemByText(By dropdownElementLocator, string optionText)
        {
            IWebElement element = Find(dropdownElementLocator);
            SelectElement dropDownElement = new SelectElement(element);

            if (!dropDownElement.SelectedOption.GetProperty("value").Equals(optionText))
            {
                dropDownElement.SelectByText(optionText);
            }
        }

        internal string ReadsSelectedOption(By dropdownElementLocator)
        {
            IWebElement element = Find(dropdownElementLocator);
            SelectElement dropDownElement = new SelectElement(element);

            return dropDownElement.SelectedOption.GetProperty("value");
        }

        internal bool CanSee(By dropdownElementLocator)
        {
            try
            {
                return Find(dropdownElementLocator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        internal void WaitsABit(TimeSpan timeout)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (stopWatch.Elapsed < timeout);
        }

        internal protected IWebElement Find(By elementLocator)
        {
            return _currentPage.FindElement(elementLocator);
        }

        internal protected IList<IWebElement> FindAll(By elementLocator)
        {
            return _currentPage.FindElements(elementLocator);
        }
    }
}
