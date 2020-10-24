using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TaxManager.Domain;
using TaxManager.Persistence.Repository;

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
            var municipalities = JsonConvert.DeserializeObject<List<Municipality>>(stream.ReadToEnd());
            foreach (var municipality in municipalities) 
            {
                var m = _municipalityRepo.Get(municipality.Name);
                if (m == null) {
                    //I hope that id is created here
                    _municipalityRepo.Add(municipality);
                    m = new Municipality { Id = municipality.Id };
                }

                foreach (var tax in municipality.Taxes) {
                    tax.MunicipalityId = m.Id;
                    //TODO check overlap
                    _taxRepository.Add(tax);
                }
            }
            _municipalityRepo.Save();
            _taxRepository.Save();
        }
    }
}
