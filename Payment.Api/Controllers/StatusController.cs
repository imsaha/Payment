using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;

        public StatusController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet, Route("")]
        public IActionResult Get()
        {
            // return application health data
            return Ok();
        }
    }
}
