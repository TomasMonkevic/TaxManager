using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxManager.Contracts;
using TaxManager.Service;
using TaxManager.WebApi.Mappers;

namespace TaxManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;
        private readonly ITaxScheduleMapper _taxScheduleMapper;
        private readonly ILogger<TaxController> _logger;

        public TaxController(ITaxScheduleMapper taxScheduleMapper, ITaxService taxService, ILogger<TaxController> logger)
        {
            _taxScheduleMapper = taxScheduleMapper;
            _taxService = taxService;
            _logger = logger;
        }

        [HttpGet]
        public TaxRateResponse Get([FromQuery] TaxRateRequest request)
        {
            var rate = _taxService.GetTaxRate(request.Municipality, request.Day);
            return new TaxRateResponse { Rate = rate };
        }

        [HttpPost]
        public IActionResult Post(TaxScheduleRequest request)
        {
            var tax = _taxScheduleMapper.ToTax(request);
            if (tax == null) {
                return BadRequest();
            }
            
            _taxService.ScheduleTax(request.Municipality, tax);
            return Ok();
        }
    }
}
