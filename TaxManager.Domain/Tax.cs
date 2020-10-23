using System;

namespace TaxManager.Domain
{
    public class Tax
    {
        public double Rate { get; set; }
        public TaxType TaxType { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; } 
    }
}
