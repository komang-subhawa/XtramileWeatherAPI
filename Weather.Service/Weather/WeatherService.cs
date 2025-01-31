using System;
using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;

namespace Weather.Service.Weather
{
    public class WeatherService: IWeatherService
    {

        private readonly IOpenWeatherMapWorker _openWeatherMapWorker;

        public WeatherService(IOpenWeatherMapWorker openWeatherMapWorker)
        {
            _openWeatherMapWorker = openWeatherMapWorker;
        }

        public OperationResult<WeatherDto> GetWeatherByCityName(string cityName)
        {
            var apiData = _openWeatherMapWorker.GetOpenWeatherMapData(cityName);
            if (!apiData.IsResultOk)
            {
                return OperationResult<WeatherDto>.Fail(apiData.Failure.FailureType, apiData.Failure.Reason);
            }

            return OperationResult<WeatherDto>.Success(GetWeatherDataDto(apiData.Value));
        }

        private WeatherDto GetWeatherDataDto(OpenWeatherMapDto apiData)
        {
            var location = new LocationDto()
            {
                Latitude = apiData.Coordinate.Latitude,
                Longitude = apiData.Coordinate.Longitude
            };

            var weatherData = new WeatherDto()
            {
                Time = apiData.TimeZone,
                DewPoint = "n/a",
                RelativeHumidity = apiData.Main.Humidity,
                Location = location,
                SkyCondition = apiData.Weather[0].Main,
                TemperatureFahrenheit = apiData.Main.Temperature,
                TemperatureCelcius = ConvertFahrenheitToCelcius(apiData.Main.Temperature),
                Visibility = apiData.Visibility,
                Wind = apiData.Wind.Speed,
                Pressure = apiData.Main.Pressure
            };

            return weatherData;
        }

        private decimal ConvertFahrenheitToCelcius(decimal fahrenheitValue)
        {
            return decimal.Round((fahrenheitValue - 32) * 5 / 9, 2, MidpointRounding.AwayFromZero); ;
        }

    }
}
