using System;
using System.Linq;
using TaxManager.Domain;
using TaxManager.Persistence.Repository;
using TaxManager.Service.Exceptions;
using TaxManager.Service.Utils;

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
            var tax = municipality?.Taxes?.Where(t => DateUtils.IsDateInclusive(t.From, t.To, day))
                              .OrderBy(t => t.TaxType)
                              .FirstOrDefault() ?? throw new TaxNotAppliedException();
            return tax.Rate;
        }

        public bool ScheduleTax(string municipalityName, Tax tax)
        {
            var municipality = _municipalityRepo.Get(municipalityName);
            if (municipality == null || municipality.IsTaxOverlapping(tax)) 
            {
                return false;
            }
            
            tax.MunicipalityId = municipality.Id;
            _taxRepository.Add(tax);
            _taxRepository.Save();
            return true;
        }
    }
}
