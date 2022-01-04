using Demoqa.FinalProject.Tests.Helpers;
using Demoqa.FinalProject.Tests.Pages;
using Demoqa.FinalProject.Tests.RestClientHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SeleniumExtras.WaitHelpers;
using System;
using System.Net;
using System.Threading;

namespace Demoqa.FinalProject.Tests
{
    public class FinalProjectTestCaseOne
    {
        private const string BASE_URL = "https://demoqa.com/";
        private const string _alertMsgAccountIsDeleted = "User Deleted.";
        private const string _alertMsgAccountIsRegistered = "User Register Successfully.";

        private RestClient _webClient;
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webClient = new RestClient();
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        [Test]
        [TestCase("Frampt", "Frampt12@")]
        public void Test(string username, string password)
        {
            TryLoginWithCredentials(username, password);
            DeleteAccount();
            AssertAlertMessage(_alertMsgAccountIsDeleted);
            TryLoginWithWrongCredentials(username, password);
            AssertBookStoreAuthorizedWithWrongCredentials(username, password);
        }

        private void RegisterUser(string username, string password)
        {
            _webDriver.Navigate().GoToUrl(BuildUrl(RegisterPage.GetPath()));
            _webDriver.Manage().Window.Maximize();

            RegisterPage registerPage = new RegisterPage(_webDriver);
            registerPage.RegisterUser("ss", "ss", username, password);
        }

        private void TryLoginWithCredentials(string username, string password)
        {
            _webDriver.Navigate().GoToUrl(BuildUrl(LoginPage.GetPath()));
            _webDriver.Manage().Window.Maximize();

            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.InputLoginCredentials(username, password).Submit();
        }

        private void TryLoginWithWrongCredentials(string username, string password)
        {
            _webDriver.Navigate().GoToUrl(BuildUrl(LoginPage.GetPath()));
            _webDriver.Manage().Window.Maximize();

            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.InputLoginCredentials(username, password).Submit();

            Assert.IsTrue(loginPage.AssertWrongUserCredentials());
        }

        private void DeleteAccount()
        {
            ProfilePage profilePage = new ProfilePage(_webDriver);
            profilePage.DeleteAccount();
        }

        private void AssertAlertMessage(string expectedResultMsg)
        {
            IAlert alert = _webDriver.WaitUntilAlertIsVisible(10);
            string alertResultMsg = alert.Text;

            Assert.IsNotEmpty(alertResultMsg);
            Assert.AreEqual(alertResultMsg, expectedResultMsg);

            alert.Accept();
        }

        private void AssertBookStoreAuthorizedWithWrongCredentials(string username, string password)
        {
            var response = _webClient.BookStoreAuthorizedWithCredentials(username, password);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue(response.Data.Message == "UserName and Password required.");
        }

        private static string BuildUrl(string path) => $"{BASE_URL}{path}";

        [TearDown]
        public void CloseWebDriver()
        {
            //_webDriver.Close();
        }
    }
}