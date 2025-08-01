using WeatherAPI.Data.Entities;
using WeatherAPI.Models.DTO;

namespace WeatherAPI.Services
{
    public interface ICountryService
    {
        Country CreateCountry(CountryDto countryDto);
    }
}
