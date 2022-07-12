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
    private readonly IHttpClientFactory _clientFactory;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {

        using (var activity = CustomActivitySource.source.StartActivity("MaybeLong"))
        {
            // une chance sur 3
            if (Random.Shared.Next(3) == 1)
            {
                activity?.AddEvent(new System.Diagnostics.ActivityEvent("StartOfLong"));
                await Task.Delay(1000);
                activity?.AddEvent(new System.Diagnostics.ActivityEvent("MidOfLong"));
                await Task.Delay(1000);

            }
        }


        using (var activity = CustomActivitySource.source.StartActivity("QueryBack"))
        {
            var client = _clientFactory.CreateClient("backjava");
            var queries = Enumerable.Range(0, 100).Select(x => client.GetAsync("/long"));
            await Task.WhenAll(queries);
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
