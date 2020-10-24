using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxManager.Contracts;
using TaxManager.Service;

namespace TaxManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;
        private readonly ILogger<TaxController> _logger;

        public TaxController(ITaxService taxManagementService, ILogger<TaxController> logger)
        {
            _taxService = taxManagementService;
            _logger = logger;
        }

        [HttpGet]
        public TaxRateResponse Get([FromQuery] TaxRateRequest request)
        {
            var rate = _taxService.GetTaxRate(request.Municipality, request.Day);
            return new TaxRateResponse { Rate = rate };
        }
    }
}
