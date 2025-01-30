using System.Collections.Generic;
using Weather.Domain.DTOs;

namespace Weather.Service.Cities
{
    public interface ICityService
    {
        IEnumerable<CityDto> GetAll();
    }
}
