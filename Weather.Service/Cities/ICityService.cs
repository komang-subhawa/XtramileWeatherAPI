using System.Collections.Generic;
using Weather.Domain.DTOs;

namespace Weather.Service.Cities
{
    public interface ICityService
    {
        OperationResult<List<CityDto>> GetByCountryId(string countryId);
        OperationResult<CityDto> GetByCityId(string cityId);
    }
}
