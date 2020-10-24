using System.Collections.Generic;
using TaxManager.Domain;

namespace TaxManager.Persistence.Repository
{
    public interface IMunicipalityRepository
    {
        void Add(Municipality municipality);
        void Remove(Municipality municipality);
        Municipality Get(string name);
        IEnumerable<Municipality> GetAll();
        void Save();
    }
}
