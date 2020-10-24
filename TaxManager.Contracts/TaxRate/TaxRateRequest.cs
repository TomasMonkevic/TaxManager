using System;

namespace TaxManager.Contracts
{
    public class TaxRateRequest
    {
        public string Municipality { get; set; }
        public DateTime Day { get; set; }
    }
}
