using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventSourcing.ApplicationService.Dtos;
using EventSourcing.ApplicationService.Services;
namespace EventSourcing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task Create([FromBody] PersonDto person, [FromServices] IPersonService service)
        {
            await service.Create(person);
        }
        [HttpGet]
        public async Task<PersonDto> GetById([FromQuery] string id, [FromServices] IPersonService service)
        {
            return await service.Get(id);
        }
        
    }
}
