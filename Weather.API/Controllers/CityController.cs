using Microsoft.AspNetCore.Mvc;
using Weather.API.Helpers;
using Weather.Service.Cities;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult Get(string country) => OperationResultResponder.GetServiceResponse(this, _cityService.GetByCountryId(country));
    }
}
