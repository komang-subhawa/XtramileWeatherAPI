using Microsoft.Extensions.DependencyInjection;
using Weather.Service.Cities;
using Weather.Service.Countries;
using Weather.Service.Weather;

namespace Weather.API.Helpers
{
    public class WeatherDependency
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IOpenWeatherMapWorker, OpenWeatherMapWorker>();
        }
    }
}
