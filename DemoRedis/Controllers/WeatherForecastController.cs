using DemoRedis.Attributes;
using DemoRedis.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IResponseCacheService _responseCacheService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IResponseCacheService responseCacheService)
        {
            _logger = logger;
            _responseCacheService = responseCacheService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Cache(100)]
        public async Task<IActionResult> Get()
        {
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            Console.WriteLine(HttpContext.Request.Path);
            await _responseCacheService.RemoveCacheResponseAsync("/WeatherForecast");
            return Ok();
        }
    }
}