using Microsoft.EntityFrameworkCore;
using WeatherAPI.Data.Entities;

namespace WeatherAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
