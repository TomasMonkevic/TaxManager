using TaxManager.Domain;

namespace TaxManager.Persistence.Repository
{
    public interface ITaxRepository
    {
        void Add(Tax tax);
        void Save();
    }
}
