using System;
using System.Linq;
using TaxManager.Persistence.Repository;
using TaxManager.Service.Exceptions;

namespace TaxManager.Service
{
    public class TaxService : ITaxService
    {
        private readonly IMunicipalityRepository _municipalityRepo;
        private readonly ITaxRepository _taxRepository;

        public TaxService(IMunicipalityRepository municipalityRepo, ITaxRepository taxRepository) 
        {
            _municipalityRepo = municipalityRepo;
            _taxRepository = taxRepository;
        }

        public double GetTaxRate(string municipalityName, DateTime day) 
        {
            var municipality = _municipalityRepo.Get(municipalityName);
            var tax = municipality?.Taxes?.Where(t => t.From.Date == day.Date || (t.From < day.Date && day.Date < t.To.Date))
                              .OrderBy(t => t.TaxType)
                              .FirstOrDefault() ?? throw new TaxNotAppliedException();
            return tax.Rate;
        }
    }
}