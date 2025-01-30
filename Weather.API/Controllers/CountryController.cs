using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Weather.Domain.DTOs;
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
        public IEnumerable<CountryDto> Get()
        {
            return _countryService.GetAll();
        }
    }
}
