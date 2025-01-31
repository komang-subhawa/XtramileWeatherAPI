using Newtonsoft.Json;
using System;
using System.Net.Http;
using Weather.Domain.DTOs;
using Weather.Domain.DTOs.OpenWeatherMap;
using Weather.Domain.Enums;

namespace Weather.Service.Weather
{
    public class OpenWeatherMapWorker: IOpenWeatherMapWorker
    {
        public OperationResult<OpenWeatherMapDto> GetOpenWeatherMapData(string cityName)
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
    }
}
