using System;
using TaxManager.Contracts;
using TaxManager.Domain;

namespace TaxManager.WebApi.Mappers {

    public class TaxScheduleMapper : ITaxScheduleMapper
    {
        public Tax ToTax(TaxScheduleRequest request)
        {
            var taxType = ToTaxType(request.TaxType);
            if(taxType == null)
            {
                return null;
            }

            return new Tax {
                Rate = request.Rate,
                TaxType = taxType.Value,
                From = request.From
            };
        }

        private TaxType? ToTaxType(string taxType) 
        {
            if(Enum.TryParse(taxType, true, out TaxType taxT)) {
                return taxT;
            }

            return null;
        }
    }
}