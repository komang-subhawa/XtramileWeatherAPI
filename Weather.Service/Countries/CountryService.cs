using System.Collections.Generic;
using System.Linq;
using Weather.Domain.DTOs;
using Weather.Domain.Enums;

namespace Weather.Service.Countries
{
    public class CountryService: ICountryService
    {
        public OperationResult<List<CountryDto>> GetAll()
        {
            var countries = GetMockData();
            return OperationResult<List<CountryDto>>.Success(countries);
        }

        public OperationResult<CountryDto> GetByCountryId(string countryId)
        {
            var countries = GetMockData();
            var country = countries.SingleOrDefault(c => c.Id == countryId);
            if (country is null)
            {
                return OperationResult<CountryDto>.Fail(OperationResultType.NoRecord, "Country not found");                
            } 
            else
            {
                return OperationResult<CountryDto>.Success(country);
            }
        }

        private List<CountryDto> GetMockData()
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
