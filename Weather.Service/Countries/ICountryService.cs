using System.Collections.Generic;
using Weather.Domain.DTOs;

namespace Weather.Service.Countries
{
    public interface ICountryService
    {
        IEnumerable<CountryDto> GetAll();
    }
}
