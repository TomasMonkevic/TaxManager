using System;
using Shouldly;
using TaxManager.Persistence.Repository;
using TaxManager.Service;
using TaxManager.Domain;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using TaxManager.Service.Exceptions;
using System.IO;

namespace TaxManager.UnitTests
{
    public class ImportServiceTests
    {
        private const string TEST_DATA_PATH = "../../../TestData";

        private readonly IMunicipalityRepository _municipalityRepo = Substitute.For<IMunicipalityRepository>();
        private readonly ITaxRepository _taxRepository = Substitute.For<ITaxRepository>();

        private readonly ImportService _importService;

        public ImportServiceTests() 
        {
            _importService = new ImportService(_municipalityRepo, _taxRepository);
        }

        [Fact]
        public void CanImportMunicipalities_WhenEmpty() 
        {
            using (var sr = new StreamReader($"{TEST_DATA_PATH}/correct.json"))
            {
                _importService.ImportMunicipalities(sr);
            }
            _municipalityRepo.ReceivedWithAnyArgs(3).Add(default);
            _taxRepository.ReceivedWithAnyArgs(5).Add(default);

            _municipalityRepo.Received(1).Save();
            _taxRepository.Received(1).Save();
        }

        // [Fact] TODO not yet finished
        // public void CanImportMunicipalities_WhenIncorrectData() 
        // {
        //     using (var sr = new StreamReader($"{TEST_DATA_PATH}/incorrect.json"))
        //     {
        //         _importService.ImportMunicipalities(sr);
        //     }
        //     _municipalityRepo.ReceivedWithAnyArgs(2).Add(default);
        //     _taxRepository.ReceivedWithAnyArgs(5).Add(default);

        //     _municipalityRepo.Received(1).Save();
        //     _taxRepository.Received(1).Save();
        // }
    }
}
