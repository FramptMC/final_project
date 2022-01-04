using Demoqa.FinalProject.Tests.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demoqa.FinalProject.Tests.Pages
{
    public class LoginPage : DemoqaPage
    {
        private const string _path = "login";

        private const string _usernameInputId = "userName";
        private const string _passwordInputId = "password";
        private const string _loginButtonId = "login";
        private const string _invalidUserCredentialsXPath = @"//div[@id='output']//p[@id='name']";
        private const string _invalidUserCredentialsMsg = "Invalid username or password!";

        public LoginPage(IWebDriver webDriver)
            :base(webDriver)
        {
            PageFactory.InitElements(_webDriver, this);
        }

        [FindsBy(How = How.Id, Using = _usernameInputId)]
        public IWebElement UserNameInput { get; set; }

        [FindsBy(How = How.Id, Using = _passwordInputId)]
        public IWebElement PasswordInput { get; set; }
     
        [FindsBy(How = How.Id, Using = _loginButtonId)]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = _invalidUserCredentialsXPath)]
        public IWebElement InvalidUserCredentials { get; set; }

        public static string GetPath() 
        {
            return _path;
        }

        public LoginPage InputLoginCredentials(string username, string password)
        {
            UserNameInput.SendKeys(username);
            PasswordInput.SendKeys(password);

            return this;
        }

        public bool AssertWrongUserCredentials()
        {
            InvalidUserCredentials = _webDriver.WaitUntilElementClickable(By.XPath(_invalidUserCredentialsXPath), _timeToWait);

            return InvalidUserCredentials.Text == _invalidUserCredentialsMsg;
        }

        public void Submit()
        {
            LoginButton.Click();
        }
    }
}
