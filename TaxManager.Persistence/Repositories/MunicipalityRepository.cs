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

        public Municipality Get(string name) {
            return _context.Manucipalities.Include(m => m.Taxes)
                                          .FirstOrDefault(m => m.Name.ToLower() == name.ToLower());
        }
    }
}
