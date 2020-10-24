using System.Linq;
using TaxManager.Domain;
using TaxManager.Persistence.Repository;

namespace TaxManager.Service
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepo;

        public MunicipalityService(IMunicipalityRepository municipalityRepo) 
        {
            _municipalityRepo = municipalityRepo;
        }

        public void Create(string manucipality) //TODO return bool?
        {
            var municipalities = _municipalityRepo.GetAll();
            if(municipalities.Any(m => m.Name == manucipality)) 
            {
                return;
            }

            _municipalityRepo.Add(new Municipality { Name = manucipality });
            _municipalityRepo.Save();
        }

        public void Delete(string manucipalityName)
        {
            var municipality = _municipalityRepo.Get(manucipalityName);
            if(municipality == null) 
            {
                return;
            }

            _municipalityRepo.Remove(municipality);
            _municipalityRepo.Save();
        }
    }
}
