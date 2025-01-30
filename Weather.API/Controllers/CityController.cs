using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Weather.Domain.DTOs;
using Weather.Service.Cities;
using Weather.Service.Countries;

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
        public IEnumerable<CityDto> Get()
        {
            return _cityService.GetAll();
        }
    }
}
