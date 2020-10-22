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
        public async Task<IActionResult> Create([FromBody] PersonDto person, [FromServices] IPersonService service)
        {
            var res = await service.Create(person);
            if (res)
            {
                return Ok("Success, " + person.Name + " " + person.Family + " insert in DB");
            }
            else
            {
                return Problem("خطا در ایجاد شخص");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] string id, [FromServices] IPersonService service)
        {
            var res = await service.Get(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] string id,[FromBody] PersonDto person, [FromServices] IPersonService service)
        {
            var res = await service.Update(id,person);
            if (res)
            {
                return Ok("داده باموفقیت ویرایش شد");
            }
            else
            {
                return Problem("ویرایش با خطا مواجه شد");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id, [FromServices] IPersonService service)
        {
            var res = await service.Delete(id);
            if (res)
            {
                return Ok("داده باموفقیت حذف شد");
            }
            else
            {
                return Problem("حذف با خطا مواجه شد");
            }
        }
    }
}
