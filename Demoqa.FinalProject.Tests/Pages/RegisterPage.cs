using Demoqa.FinalProject.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Demoqa.FinalProject.Tests.Pages
{
    public class RegisterPage : DemoqaPage
    {
        private const string _path = "register";

        private const string _firstNameInputId = "firstname";
        private const string _lastNameInputId = "lastname";
        private const string _userNameInputId = "userName";
        private const string _passwordInputId = "password";
        private const string _recaptchaXPath = "//div[@class='recaptcha-checkbox-checkmark']";
        private const string _registerBtnId = "register";

        public RegisterPage(IWebDriver webDriver)
            : base(webDriver)
        {
            PageFactory.InitElements(_webDriver, this);
        }

        [FindsBy(How = How.Id, Using = _firstNameInputId)]
        public IWebElement FirstNameInput { get; set; }

        [FindsBy(How = How.Id, Using = _lastNameInputId)]
        public IWebElement LastNameInput { get; set; }

        [FindsBy(How = How.Id, Using = _userNameInputId)]
        public IWebElement UserNameInput { get; set; }

        [FindsBy(How = How.Id, Using = _passwordInputId)]
        public IWebElement PasswordInput { get; set; }

        [FindsBy(How = How.Id, Using = _registerBtnId)]
        public IWebElement RegisterBtn { get; set; }

        public void RegisterUser(string firstname, string lastname, string username, string password)
        {
            FirstNameInput.SendKeys(firstname);
            LastNameInput.SendKeys(lastname);
            UserNameInput.SendKeys(username);
            PasswordInput.SendKeys(password);

            ClickCaptcha();
            _webDriver.SwitchTo().ParentFrame();
            Thread.Sleep(10000);
            Submit();
        }

        public void Submit()
        {
            RegisterBtn.Click();
        }

        private void ClickCaptcha()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name,'a-')]")));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-border")));
            element.Click();
        }

        public static string GetPath()
        {
            return _path;
        }
    }
}
