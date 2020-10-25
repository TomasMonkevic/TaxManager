using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaxManager.Domain;
using TaxManager.Persistence.Repository;
using TaxManager.Service.Utils;

namespace TaxManager.Service
{
    public class ImportService : IImportService
    {
        private readonly IMunicipalityRepository _municipalityRepo;
        private readonly ITaxRepository _taxRepository;

        public ImportService(IMunicipalityRepository municipalityRepo, ITaxRepository taxRepository) 
        {
            _municipalityRepo = municipalityRepo;
            _taxRepository = taxRepository;
        }

        public void ImportMunicipalities(StreamReader stream) 
        {
            //TODO handle exceptions and refactor
            var municipalities = JsonConvert.DeserializeObject<List<Municipality>>(stream.ReadToEnd());
            foreach (var municipality in municipalities) 
            {
                var m = _municipalityRepo.Get(municipality.Name);
                if (m == null) 
                {
                    _municipalityRepo.Add(municipality);
                    m = new Municipality { Id = municipality.Id };
                }

                foreach (var tax in municipality.Taxes) 
                {
                    if (m.IsTaxOverlapping(tax)) 
                    {
                        continue;
                    }

                    tax.MunicipalityId = m.Id;
                    _taxRepository.Add(tax);
                }
            }
            _municipalityRepo.Save();
            _taxRepository.Save();
        }
    }
}
