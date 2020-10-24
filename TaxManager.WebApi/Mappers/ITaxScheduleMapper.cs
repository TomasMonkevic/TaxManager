using TaxManager.Contracts;
using TaxManager.Domain;

namespace TaxManager.WebApi.Mappers {

    public interface ITaxScheduleMapper
    {
        Tax ToTax(TaxScheduleRequest request);
    }
}