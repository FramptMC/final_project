using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Demoqa.FinalProject.Tests.RestClientHelper.Models
{
    [Serializable]
    public class BookStoreBooksResponse
    {
        [JsonPropertyName("books")]
        public IEnumerable<Book> Books { get; set; }

        [Serializable]
        public class Book
        {
            [JsonPropertyName("publisher")]
            public string Publisher { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }
        }
    }
}
