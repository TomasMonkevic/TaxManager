using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxManager.Service;

namespace TaxManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;
        private readonly IImportService _importService;

        public MunicipalityController(IMunicipalityService municipalityService, IImportService importService)
        {
            _importService = importService;
            _municipalityService = municipalityService;
        }

        [HttpPost]
        public IActionResult Create(string municipalityName)
        {
            var isCreated = _municipalityService.Create(municipalityName);
            return isCreated ? (IActionResult) Ok() : BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(string municipalityName)
        {
            var isDeleted = _municipalityService.Delete(municipalityName);
            return isDeleted ? (IActionResult) Ok() : BadRequest();
        }

        [HttpPost]
        [Route("Import")]
        public IActionResult Import(IFormFile file)
        {
            using var stream = new StreamReader(file.OpenReadStream());
            var isSuccessfulImport = _importService.ImportMunicipalities(stream);
            return isSuccessfulImport ? (IActionResult) Ok() : BadRequest();
        }
    }
}
