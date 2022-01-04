using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Demoqa.FinalProject.Tests.Models
{
    [Serializable]
    public class Book
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
