using Weather.Domain.DTOs;

namespace Weather.Service.Weather
{
    public interface IWeatherService
    {
        public OperationResult<WeatherDto> GetWeatherByCityName(string cityName);
    }
}
