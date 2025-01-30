using Microsoft.Extensions.DependencyInjection;
using Weather.Service.Cities;
using Weather.Service.Countries;

namespace Weather.API.Helpers
{
    public class WeatherDependency
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
        }
    }
}
