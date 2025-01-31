using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;

namespace Weather.Service.Weather
{
    public interface IOpenWeatherMapWorker
    {
        OperationResult<OpenWeatherMapDto> GetOpenWeatherMapData(string cityName);
    }
}
