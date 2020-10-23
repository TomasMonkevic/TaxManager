using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public IActionResult Get()
        {
            _taxManagementService.GetTaxRate();
            return Ok();
        }
    }
}
