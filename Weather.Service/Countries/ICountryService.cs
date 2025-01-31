using System.Collections.Generic;
using Weather.Domain.DTOs;

namespace Weather.Service.Countries
{
    public interface ICountryService
    {
        OperationResult<List<CountryDto>> GetAll();
        OperationResult<CountryDto> GetByCountryId(string countryId);
    }
}
