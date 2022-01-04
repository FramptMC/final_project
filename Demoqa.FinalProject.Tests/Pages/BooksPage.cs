using Demoqa.FinalProject.Tests.Models;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Demoqa.FinalProject.Tests.Pages
{
    public class BooksPage : DemoqaPage
    {
        private const string _path = "books";

        private const string _searchBarInput = "searchBox";
        private const string _bookRowXPath = @"//div[@class='rt-tbody']/div[@class='rt-tr-group']";
        private const string _bookColXPath = @".//div[contains(@class, 'rt-td')]";

        public BooksPage(IWebDriver webDriver)
            : base(webDriver)
        {
            PageFactory.InitElements(_webDriver, this);
        }

        [FindsBy(How = How.Id, Using = _searchBarInput)]
        public IWebElement SearchBarInput { get; set; }

        public List<Book> SearchByPublisher(string publisher)
        {
            SearchBarInput.SendKeys(publisher);
            Thread.Sleep(10000);

            return GetBooksResult();
        }

        public List<Book> GetBooksResult()
        {
            var rows = _webDriver.FindElements(By.XPath(_bookRowXPath));

            List<Book> result = rows
                .Select(x => x.FindElements(By.XPath(_bookColXPath)))
                .Select(element => new Book
                {
                    Image = element[0].Text,
                    Title = element[1].Text,
                    Author = element[2].Text,
                    Publisher = element[3].Text
                })
                .Where(book => !string.IsNullOrWhiteSpace(book.Publisher))
                .ToList();

            return result;
        }

        public static string GetPath()
        {
            return _path;
        }
    }
}
