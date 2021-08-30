using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyConvertor.Api.Resources
{
    public class PostConverstion
    {
        int Value { get; set; }
        string fromCurrency { get; set; }
        string toCurrency { get; set; }
    }
}
