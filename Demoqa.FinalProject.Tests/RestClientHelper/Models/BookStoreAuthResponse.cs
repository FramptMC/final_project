using System;
using System.Text.Json.Serialization;

namespace Demoqa.FinalProject.Tests.RestClientHelper.Models
{
    [Serializable]
    public class BookStoreAuthResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
