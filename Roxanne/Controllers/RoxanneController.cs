using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Roxanne.Contracts;

namespace Roxanne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoxanneController : ControllerBase
    {
        private readonly IAlexaService alexaService;

        public RoxanneController(IAlexaService _alexaService)
        {
            alexaService = _alexaService;
        }
        
        [HttpPost]
        public IActionResult Post(SkillRequest input)
        {
            var response = alexaService.ProcessRequest(input);

            return Ok(response);
        }
    }
}
