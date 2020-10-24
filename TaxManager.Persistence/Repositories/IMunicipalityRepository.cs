using TaxManager.Domain;

namespace TaxManager.Persistence.Repository
{
    public interface IMunicipalityRepository
    {
        Municipality Get(string name);
    }
}
