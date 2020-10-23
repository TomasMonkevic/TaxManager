using TaxManager.Domain;
using System.Linq;

namespace TaxManager.Persistence.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly TaxManagerContext _context;

        public TaxRepository(TaxManagerContext context) {
            _context = context;
        }

        public void Add(Tax tax)
        {
            var taxes = _context.Taxes.ToList();
            var municip = _context.Manucipalities.ToList();
        }
    }
}
