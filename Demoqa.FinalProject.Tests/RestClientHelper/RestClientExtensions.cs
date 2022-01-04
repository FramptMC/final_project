using Demoqa.FinalProject.Tests.RestClientHelper.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demoqa.FinalProject.Tests.RestClientHelper
{
    public static class RestClientExtensions
    {
        public static IRestResponse<BookStoreAuthResponse> BookStoreAuthorizedWithCredentials(this RestClient restClient, string username, string password)
        {
            RestRequest restRequest = new RestRequest("https://demoqa.com/Account/v1/Authorized");
            restRequest.AddJsonBody(new BookStoreAuthRequest
            {
                UserName = username,
                Password = password
            });

            return restClient.Post<BookStoreAuthResponse>(restRequest);
        }

        public static IRestResponse<BookStoreBooksResponse> GetBooks(this RestClient restClient)
        {
            RestRequest restRequest = new RestRequest("https://demoqa.com/BookStore/v1/Books");

            return restClient.Get<BookStoreBooksResponse>(restRequest);
        }
    }
}
