using TaxManager.Domain;

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
            _context.Add(tax);
            _context.SaveChanges();
        }
    }
}
