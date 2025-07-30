using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Models.DTO
{
    public class CountryDto
    {
        [Required(ErrorMessage = "Country code is required")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be {1} characters long")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Minimum country name length is {1} characters")]
        public string? Name { get; set; }
    }
}
