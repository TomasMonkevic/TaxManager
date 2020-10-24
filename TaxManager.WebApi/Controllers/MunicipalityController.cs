using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxManager.Contracts;
using TaxManager.Service;

namespace TaxManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;
        private readonly ILogger<MunicipalityController> _logger;

        public MunicipalityController(IMunicipalityService municipalityService, ILogger<MunicipalityController> logger)
        {
            _municipalityService = municipalityService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(string municipalityName) //TODO create dto
        {
            _municipalityService.Create(municipalityName);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string municipalityName)
        {
            _municipalityService.Delete(municipalityName);
            return Ok();
        }
    }
}
