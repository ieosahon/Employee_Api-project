using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

        private readonly ILoggerManager _loggerManager;
        private readonly IRepoManager _repository;
        public WeatherForecastController(ILoggerManager loggerManager, IRepoManager repository)
        {
            _loggerManager = loggerManager;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            

            _loggerManager.LogInfo("Here is info message from our values controller.");
            _loggerManager.LogDebug("Here is debug message from our values controller.");
            _loggerManager.LogWarn("Here is warn message from our values controller.");
            _loggerManager.LogError("Here is an error message from our values controller.");
            return new string[] { "value1", "value2" };
        }
    }
}
