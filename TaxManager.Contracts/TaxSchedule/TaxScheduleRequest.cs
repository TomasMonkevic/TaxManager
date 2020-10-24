using System;

namespace TaxManager.Contracts
{
    public class TaxScheduleRequest
    {
        public string Municipality { get; set; }
        public double Rate { get; set; }
        public string TaxType { get; set; }
        public DateTime From { get; set; }
    }
}
