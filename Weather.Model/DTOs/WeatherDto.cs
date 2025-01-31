namespace Weather.Domain.DTOs
{
    public class WeatherDto
    {

        public LocationDto Location { get; set; }

        public int Time { get; set; }

        public decimal Wind { get; set; }

        public int Visibility { get; set; }

        public string SkyCondition { get; set; }

        public decimal TemperatureCelcius { get; set; }

        public decimal TemperatureFahrenheit { get; set; }

        public string DewPoint { get; set; }

        public int RelativeHumidity { get; set; }

        public int Pressure { get; set; }
    }
}
