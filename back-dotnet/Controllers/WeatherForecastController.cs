using Microsoft.AspNetCore.Mvc;

namespace back_dotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    private Random random = new Random();

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        
        using(var activity = CustomActivitySource.source.StartActivity("MaybeLong")) {
        // une chance sur 3
        if (random.Next(3) == 1)
        {
            activity?.AddEvent(new System.Diagnostics.ActivityEvent("StartOfLong"));
            Task.Delay(3000).Wait();
            activity?.AddEvent(new System.Diagnostics.ActivityEvent("EndOfLong"));
        }
        }
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
