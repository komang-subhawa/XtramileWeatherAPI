using System;
using System.Collections.Generic;
using System.Linq;
using Weather.Domain;
using Weather.Domain.DTOs;

namespace Weather.Service.Countries
{
    public class CountryService: ICountryService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<CountryDto> GetAll()
        {
            List<CountryDto> countries = new List<CountryDto>
            {
                new CountryDto { Id = "USA", Name = "United States" },
                new CountryDto { Id = "GBR", Name = "United Kingdom" },
                new CountryDto { Id = "FRA", Name = "France" },
                new CountryDto { Id = "JPN", Name = "Japan" },
                new CountryDto { Id = "DEU", Name = "Germany" }
            };

            return countries;
        }
    }
}
