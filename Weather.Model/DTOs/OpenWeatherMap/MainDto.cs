using Newtonsoft.Json;

namespace Weather.Domain.DTOs.OpenWeatherMap
{
    public class CloudDto
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
