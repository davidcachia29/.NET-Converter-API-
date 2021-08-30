using System;
using System.Collections.Generic;

namespace MyConvertor.Core.Models
{
    public class Currency
    {
        public bool success { get; set; }
        public string timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public List<String> rates { get; set; }        
    }
}
