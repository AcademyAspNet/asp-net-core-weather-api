using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Data.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(2)]
        public required string Code { get; set; }

        [MaxLength(256)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
