using WeatherAPI.Data;
using WeatherAPI.Data.Entities;
using WeatherAPI.Models.DTO;

namespace WeatherAPI.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _database;

        public CountryService(ApplicationDbContext database)
        {
            _database = database;
        }

        public Country CreateCountry(CountryDto countryDto)
        {
            Country country = new Country()
            {
                Code = countryDto.Code!,
                Name = countryDto.Name!
            };

            _database.Countries.Add(country);
            _database.SaveChanges();

            return country;
        }
    }
}
