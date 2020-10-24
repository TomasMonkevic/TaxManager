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
        private readonly ITaxManagementService _taxManagementService;
        private readonly ILogger<TaxController> _logger;

        public TaxController(ITaxManagementService taxManagementService, ILogger<TaxController> logger)
        {
            _taxManagementService = taxManagementService;
            _logger = logger;
        }

        [HttpGet]
        public TaxRateResponse Get([FromQuery] TaxRateRequest request)
        {
            var rate = _taxManagementService.GetTaxRate(request.Municipality, request.Day);
            return new TaxRateResponse { Rate = rate };
        }
    }
}
