using System;

namespace TaxManager.Domain
{
    public class Tax
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public TaxType TaxType { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public Municipality Municipality { get; set; }
        public int MunicipalityId { get; set; }
    }
}
