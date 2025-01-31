using Microsoft.AspNetCore.Mvc;
using Weather.API.Helpers;
using Weather.Service.Countries;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult Get() => OperationResultResponder.GetServiceResponse(this, _countryService.GetAll());
    }
}
