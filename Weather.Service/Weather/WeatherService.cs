using Newtonsoft.Json;
using System;
using System.Net.Http;
using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;
using Weather.Domain.Enums;

namespace Weather.Service.Weather
{
    public class WeatherService: IWeatherService
    {
        public OperationResult<WeatherDto> GetWeatherByCityName(string cityName)
        {
            var apiData = GetOpenWeatherMapData(cityName);
            if (!apiData.IsResultOk)
            {
                return OperationResult<WeatherDto>.Fail(apiData.Failure.FailureType, apiData.Failure.Reason);
            }

            return OperationResult<WeatherDto>.Success(GetWeatherDataDto(apiData.Value));
        }

        private OperationResult<OpenWeatherMapDto> GetOpenWeatherMapData(string cityName)
        {
            var url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, "c764d533f587eaba096b5ea481ce3437");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var responseTask = httpClient.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var jsonString = readTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var response = JsonConvert.DeserializeObject<OpenWeatherMapDto>(jsonString);
                        return OperationResult<OpenWeatherMapDto>.Success(response);
                    }
                    else
                    {
                        var errorContent = JsonConvert.DeserializeObject<ApiErrorDto>(jsonString);
                        return OperationResult<OpenWeatherMapDto>.Fail(OperationResultType.InvalidArguments, errorContent.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                return OperationResult<OpenWeatherMapDto>.Fail(OperationResultType.Exception, string.Format("Failed to send request to {0} with message {1}", url, exception.Message));
            }
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
