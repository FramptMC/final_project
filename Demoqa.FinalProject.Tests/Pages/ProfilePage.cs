using Demoqa.FinalProject.Tests.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demoqa.FinalProject.Tests.Pages
{
    public class ProfilePage: DemoqaPage
    {
        private const string _path = "profile";
        private const string DeleteAccountBtnXPath = @"//button[@id='submit' and text()='Delete Account']";
        private const string SubmitDeleteAccountBtnId = @"closeSmallModal-ok";

        public ProfilePage(IWebDriver webDriver)
            :base(webDriver)
        {
            PageFactory.InitElements(_webDriver, this);
        }

        [FindsBy(How = How.XPath, Using = DeleteAccountBtnXPath)]
        public IWebElement DeleteAccountBtn { get; set; }

        [FindsBy(How = How.Id, Using = SubmitDeleteAccountBtnId)]
        public IWebElement SubmitDeleteAccountBtn { get; set; }

        public void DeleteAccount()
        {
            DeleteAccountBtn = _webDriver.WaitUntilElementExists(By.XPath(DeleteAccountBtnXPath), _timeToWait);
            DeleteAccountBtn.Click();

            SubmitDeleteAccountBtn = _webDriver.WaitUntilElementExists(By.Id(SubmitDeleteAccountBtnId), _timeToWait);
            SubmitDeleteAccountBtn.Click();
        }
    }
}
