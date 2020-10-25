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

        public bool Create(string manucipality)
        {
            try
            {
                _municipalityRepo.Add(new Municipality { Name = manucipality });
                _municipalityRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string manucipalityName)
        {
            var municipality = _municipalityRepo.Get(manucipalityName);
            if(municipality == null) 
            {
                return false;
            }

            _municipalityRepo.Remove(municipality);
            _municipalityRepo.Save();
            return true;
        }
    }
}
