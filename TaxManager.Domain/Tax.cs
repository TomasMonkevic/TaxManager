using System;

namespace TaxManager.Domain
{
    public class Tax
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public TaxType TaxType { get; set; }
        public DateTime From { get; set; }
        public DateTime To
        {
            get {
                return TaxType switch
                {
                    TaxType.Daily => From.AddDays(1),
                    TaxType.Weekly => From.AddDays(7),
                    TaxType.Monthly => From.AddMonths(1),
                    TaxType.Annually => From.AddYears(1),
                    _ => From
                };
            }
        }

        public Municipality Municipality { get; set; }
        public int MunicipalityId { get; set; }
    }
}
