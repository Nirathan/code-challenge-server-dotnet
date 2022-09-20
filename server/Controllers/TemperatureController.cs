using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ISensorService sensorService;

        public TemperatureController(ISensorService sensorService)
        {
            this.sensorService = sensorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var sensor = await sensorService.Get(id);
            return Ok(sensor);
        }
    }
}
