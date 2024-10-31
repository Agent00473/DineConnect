using DineConnect.PaymentManagementService.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DineConnect.PaymentManagementService.API.Controllers
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
        private DineOutPaymentDbContext _ctxt;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DineOutPaymentDbContext ctxt)
        {
            _logger = logger;
            _ctxt = ctxt;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var count = _ctxt.Refunds.ToList().Count;
            Console.WriteLine(count);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
