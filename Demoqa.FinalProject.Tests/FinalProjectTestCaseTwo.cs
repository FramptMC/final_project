using Demoqa.FinalProject.Tests.Models;
using Demoqa.FinalProject.Tests.Pages;
using Demoqa.FinalProject.Tests.RestClientHelper;
using Demoqa.FinalProject.Tests.RestClientHelper.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Demoqa.FinalProject.Tests
{
    public class FinalProjectTestCaseTwo
    {
        private const string BASE_URL = "https://demoqa.com/";
        private const string _testPublisher = "O'Reilly Media";
        private const string _lastBookTitle = "Understanding ECMAScript 6";
        private RestClient _webClient;
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webClient = new RestClient();
            _webDriver = new ChromeDriver();
        }

        [Test]
        [TestCase(_testPublisher)]
        public void Test(string publisher)
        {
            var booksPage = GetBooksPage();
            var booksByPublisherFromApi = GetBooksFromApi();
            var booksFromUi = booksPage.GetBooksResult();

            var booksFromApiByPublisher = FilterByPublisher(booksByPublisherFromApi, publisher);
            var booksFromUiByPublisher = booksPage.SearchByPublisher(publisher);

            Assert.AreEqual(booksFromApiByPublisher.Count(), booksFromUiByPublisher.Count());
            Assert.AreEqual(booksByPublisherFromApi.Last().Title, _lastBookTitle);
            Assert.AreEqual(booksFromUi.Last().Title, _lastBookTitle);
        }

        private BooksPage GetBooksPage()
        {
            _webDriver.Navigate().GoToUrl(BuildUrl(BooksPage.GetPath()));
            _webDriver.Manage().Window.Maximize();

            return new BooksPage(_webDriver);
        }

        private IEnumerable<BookStoreBooksResponse.Book> GetBooksFromApi()
        {
            IRestResponse<BookStoreBooksResponse> bookStoreBooksResponse = _webClient.GetBooks();

            return bookStoreBooksResponse.Data.Books;
        }

        private IEnumerable<BookStoreBooksResponse.Book> FilterByPublisher(IEnumerable<BookStoreBooksResponse.Book> books, string publisher)
        {
            return books.Where(book => book.Publisher == publisher);
        }

        private static string BuildUrl(string path) => $"{BASE_URL}{path}";

        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
