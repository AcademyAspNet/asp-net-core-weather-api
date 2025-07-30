using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WeatherAPI.Data.Entities;

namespace WeatherAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            string? connectionString = _configuration.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new MissingFieldException("Failed to get Default connection string");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
