using System.Linq;
using TaxManager.Domain;

namespace TaxManager.Service.Utils {

    public static class MunicipalityExtentions 
    {
        public static bool IsTaxOverlapping(this Municipality municipality, Tax tax)
        {
            if(municipality.Taxes == null) 
            {
                return false;
            }

            return municipality.Taxes.Any(t => t.TaxType == tax.TaxType && 
                DateUtils.AreDatesOverlapping(t.From, t.To, tax.From, tax.To));
        }
    }
}