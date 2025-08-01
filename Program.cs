using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WeatherAPI.Data;
using WeatherAPI.Data.Entities;
using WeatherAPI.Models.DTO;
using WeatherAPI.Services;
using WeatherAPI.Services.Implementations;

namespace WeatherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("Default");

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new MissingFieldException("Failed to get Default connection string");

                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<ICountryService, CountryService>();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
