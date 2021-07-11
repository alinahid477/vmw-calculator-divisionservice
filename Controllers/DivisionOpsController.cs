using System;
using System.Collections.Generic;
using System.Linq;
using divisionservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace divisionservice.Controllers
{
    [ApiController]
    [Route("[controller]")] //--> this yields eg: http://localhost:5002/divisionops/. For home eg: http://localhost:5002/ use [Route("/")]
    public class DivisionOpsController: ControllerBase
    {
        private readonly ILogger<DivisionOpsController> _logger;
        private readonly IDivisionService _divisionService;
        
        public DivisionOpsController(IDivisionService divisionService, ILogger<DivisionOpsController> logger)
        {
            _divisionService = divisionService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Hello from Devision Service. How can I help?");
        }

        [HttpGet("healthcheck")]
        public ActionResult<string> healthcheck()
        {
            return Ok("Devision Service is running. Thank you!!");
        }

        [HttpPost("divide")]
        public ActionResult<string> divide([FromForm] double a, [FromForm] double b)
        {
            string validated = this._divisionService.validate(a,b);
            if(validated.Length > 0) {
                return StatusCode(500,validated);
            }

            return Ok(this._divisionService.divide(a,b));
        }
    }
}