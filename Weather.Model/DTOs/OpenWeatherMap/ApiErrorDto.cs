using Newtonsoft.Json;

namespace Weather.Domain.DTOs.OpenWeatherMap
{
    public class ApiErrorDto
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
