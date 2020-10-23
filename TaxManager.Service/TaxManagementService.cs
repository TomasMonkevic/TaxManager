using TaxManager.Persistence.Repository;

namespace TaxManager.Service
{
    public class TaxManagementService : ITaxManagementService
    {
        private readonly ITaxRepository _taxRepository;

        public TaxManagementService(ITaxRepository taxRepository) {
            _taxRepository = taxRepository;
        }

        public void GetTaxRate() {
            _taxRepository.Add(null);
        }
    }
}
