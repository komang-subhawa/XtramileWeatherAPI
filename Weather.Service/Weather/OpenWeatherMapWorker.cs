using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;
using Weather.Domain.Enums;
using Weather.Domain.Settings;

namespace Weather.Service.Weather
{
    public class OpenWeatherMapWorker: IOpenWeatherMapWorker
    {
        private readonly IOptions<OpenWeatherMapSettings> _settings;

        public OpenWeatherMapWorker(IOptions<OpenWeatherMapSettings> settings)
        {
            _settings = settings;
        }

        public OperationResult<OpenWeatherMapDto> GetOpenWeatherMapData(string cityName)
        {
            var url = string.Format(_settings.Value.Url, cityName, _settings.Value.AppKey);
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
    }
}
