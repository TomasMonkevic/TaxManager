using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
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

        public bool ImportMunicipalities(StreamReader stream) 
        {
            try
            {
                foreach (var municipality in ReadMunicipalities(stream)) 
                {
                    ImportMunicipality(municipality);
                }
                SaveChanges();
                return true;
            }
            catch(JsonSerializationException)
            {
                return false;
            }
            catch(JSchemaValidationException)
            {
                return false;
            }
        }

        private IEnumerable<Municipality> ReadMunicipalities(StreamReader stream)
        {
            var validatingReader = new JSchemaValidatingReader(new JsonTextReader(stream))
            {
                Schema = JSchema.Parse(IMPORT_SCHEMA)
            };
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<IEnumerable<Municipality>>(validatingReader);
        }

        private void ImportMunicipality(Municipality municipalityToAdd) 
        {
            var existingMunicipality = _municipalityRepo.Get(municipalityToAdd.Name);
            if (existingMunicipality == null)
            {
                _municipalityRepo.Add(municipalityToAdd);
                existingMunicipality = new Municipality { Id = municipalityToAdd.Id };
            }

            ImportMunicipalityTaxes(existingMunicipality, municipalityToAdd.Taxes);
        }

        private void ImportMunicipalityTaxes(Municipality municipality, IEnumerable<Tax> taxes)
        {
            if (taxes == null)
            {
                return;
            }

            foreach (var tax in taxes)
            {
                if (municipality.IsTaxOverlapping(tax))
                {
                    continue;
                }

                tax.MunicipalityId = municipality.Id;
                _taxRepository.Add(tax);
            }
        }

        private void SaveChanges()
        {
            _municipalityRepo.Save();
            _taxRepository.Save();
        }

        //Would be better to read from file or config.
        private const string IMPORT_SCHEMA = @"{
                                    '$schema': 'http://json-schema.org/draft-04/schema#',
                                    'type': 'array',
                                    'items': {
                                      'type': 'object',
                                      'properties': {
                                        'Name': {
                                          'type': 'string'
                                        },
                                        'Taxes': {
                                          'type': 'array',
                                          'items': {
                                            'type': 'object',
                                            'properties': {
                                              'Rate': {
                                                'type': 'number'
                                              },
                                              'TaxType': {
                                                'type': 'string',
                                                'enum': [
                                                  'Daily',
                                                  'Weekly',
                                                  'Monthly',
                                                  'Annually'
                                                ]
                                              },
                                              'From': {
                                                'type': 'string'
                                              }
                                            },
                                            'required': [
                                              'Rate',
                                              'TaxType',
                                              'From'
                                            ]
                                          }
                                        }
                                      },
                                      'required': [
                                        'Name'
                                      ]
                                    }
                                  }";
    }
}
