using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize] // <--- Requires JWT token
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IActionResult GetWeather()
    {
        var data = new[]
        {
            new { Date = DateTime.Now.ToShortDateString(), Temp = "32°C", Summary = "Sunny" },
            new { Date = DateTime.Now.AddDays(1).ToShortDateString(), Temp = "30°C", Summary = "Cloudy" },
            new { Date = DateTime.Now.AddDays(2).ToShortDateString(), Temp = "28°C", Summary = "Rainy" }
        };

        return Ok(data);
    }
}
