using NSubstitute;
using Shouldly;
using TaxManager.Domain;
using TaxManager.Persistence.Repository;
using TaxManager.Service;
using Xunit;

namespace TaxManager.UnitTests
{
    public class MunicipalityServiceTests
    {
        private readonly IMunicipalityRepository _municipalityRepo = Substitute.For<IMunicipalityRepository>();

        private readonly MunicipalityService _municipalityService;

        public MunicipalityServiceTests()
        {
            _municipalityService = new MunicipalityService(_municipalityRepo);
        }

        [Fact]
        public void CanCreate()
        {
            var isCreated = _municipalityService.Create("Vilnius");

            isCreated.ShouldBe(true);
        }

        [Fact]
        public void CanDelete()
        {
            string municipalityToDelete = "Vilnius";
            _municipalityRepo.Get(municipalityToDelete).Returns(new Municipality { Name = municipalityToDelete });

            var isDeleted = _municipalityService.Delete(municipalityToDelete);

            isDeleted.ShouldBe(true);
        }

        [Fact]
        public void Delete_WhenDoesNotExist()
        {
            var isDeleted = _municipalityService.Delete("Vilnius");

            isDeleted.ShouldBe(false);
        }
    }
}
