using System.Collections.Generic;
using System.Linq;
using Weather.Domain.DTOs;
using Weather.Domain.Enums;

namespace Weather.Service.Cities
{
    public class CityService: ICityService
    {
        public OperationResult<List<CityDto>> GetByCountryId(string countryId)
        {
            var cities = string.IsNullOrEmpty(countryId) ? GetMockData() : GetMockData().Where(c => c.CountryId.ToLower() == countryId.ToLower()).ToList();
            return OperationResult<List<CityDto>>.Success(cities);
        }

        public OperationResult<CityDto> GetByCityId(string cityId)
        {
            var cities = GetMockData();
            var city = cities.SingleOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return OperationResult<CityDto>.Fail(OperationResultType.NoRecord, "City not found");
            }
            else
            {
                return OperationResult<CityDto>.Success(city);
            }
        }

        private List<CityDto> GetMockData()
        {
            List<CityDto> cities = new List<CityDto>
            {
                new CityDto { Id = "NYC", Name = "New York", CountryId = "USA" },
                new CityDto { Id = "LAX", Name = "Los Angeles", CountryId = "USA" },
                new CityDto { Id = "CHI", Name = "Chicago", CountryId = "USA" },
                new CityDto { Id = "MIA", Name = "Miami", CountryId = "USA" },
                new CityDto { Id = "SFO", Name = "San Francisco", CountryId = "USA" },

                new CityDto { Id = "LON", Name = "London", CountryId = "GBR" },
                new CityDto { Id = "MAN", Name = "Manchester", CountryId = "GBR" },
                new CityDto { Id = "BIR", Name = "Birmingham", CountryId = "GBR" },
                new CityDto { Id = "LIV", Name = "Liverpool", CountryId = "GBR" },
                new CityDto { Id = "EDI", Name = "Edinburgh", CountryId = "GBR" },

                new CityDto { Id = "PAR", Name = "Paris", CountryId = "FRA" },
                new CityDto { Id = "MAR", Name = "Marseille", CountryId = "FRA" },
                new CityDto { Id = "LYO", Name = "Lyon", CountryId = "FRA" },
                new CityDto { Id = "NIC", Name = "Nice", CountryId = "FRA" },
                new CityDto { Id = "TOU", Name = "Toulouse", CountryId = "FRA" },

                new CityDto { Id = "TOK", Name = "Tokyo", CountryId = "JPN" },
                new CityDto { Id = "OSA", Name = "Osaka", CountryId = "JPN" },
                new CityDto { Id = "KYO", Name = "Kyoto", CountryId = "JPN" },
                new CityDto { Id = "YOK", Name = "Yokohama", CountryId = "JPN" },
                new CityDto { Id = "NAG", Name = "Nagoya", CountryId = "JPN" },

                new CityDto { Id = "BER", Name = "Berlin", CountryId = "DEU" },
                new CityDto { Id = "MUC", Name = "Munich", CountryId = "DEU" },
                new CityDto { Id = "HAM", Name = "Hamburg", CountryId = "DEU" },
                new CityDto { Id = "FRA", Name = "Frankfurt", CountryId = "DEU" },
                new CityDto { Id = "COL", Name = "Cologne", CountryId = "DEU" }
            };

            return cities;
        }
    }
}
