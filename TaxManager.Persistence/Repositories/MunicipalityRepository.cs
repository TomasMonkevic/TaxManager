using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaxManager.Domain;

namespace TaxManager.Persistence.Repository
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly TaxManagerContext _context;

        public MunicipalityRepository(TaxManagerContext context) {
            _context = context;
        }

        public void Add(Municipality municipality)
        {
            _context.Manucipalities.Add(municipality);
        }

        public Municipality Get(string name) {
            return _context.Manucipalities.Include(m => m.Taxes)
                                          .FirstOrDefault(m => m.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<Municipality> GetAll()
        {
            return _context.Manucipalities.ToList();
        }

        public void Remove(Municipality municipality)
        {
            _context.Manucipalities.Remove(municipality);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
