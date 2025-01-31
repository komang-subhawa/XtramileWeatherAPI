using Newtonsoft.Json;

namespace Weather.Domain.DTOs.OpenWeatherMap
{
    public class CoordinateDto
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
