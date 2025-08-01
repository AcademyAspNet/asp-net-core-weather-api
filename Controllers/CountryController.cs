using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Data;
using WeatherAPI.Data.Entities;
using WeatherAPI.Models.DTO;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(
            ApplicationDbContext database,
            ICountryService countryService,
            ILogger<CountryController> logger
        )
        {
            _database = database;
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        public IList<Country> GetCountries()
        {
            return _database.Countries.ToList();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCountryById([FromRoute] int id)
        {
            Country? country = _database.Countries.Find(id);

            if (country == null)
                return NotFound();

            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryDto countryDto)
        {
            try
            {
                Country country = _countryService.CreateCountry(countryDto);
                return Ok(country);
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                string errorMessage = exception.InnerException?.Message ?? exception.Message;
                _logger.LogError("Failed to create country: {0}", errorMessage);

                return Problem(
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
