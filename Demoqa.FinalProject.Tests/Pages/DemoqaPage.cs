using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demoqa.FinalProject.Tests.Pages
{
    public abstract class DemoqaPage
    {
        protected IWebDriver _webDriver;
        protected const int _timeToWait = 10;

        public DemoqaPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
    }
}
