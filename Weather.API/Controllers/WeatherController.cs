using Microsoft.AspNetCore.Mvc;
using Weather.API.Helpers;
using Weather.Service.Weather;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("weathers")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Get(string city) => OperationResultResponder.GetServiceResponse(this, _weatherService.GetWeatherByCityName(city));
    }
}
