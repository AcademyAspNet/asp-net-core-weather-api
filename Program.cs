using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WeatherAPI.Data;
using WeatherAPI.Data.Entities;
using WeatherAPI.Models.DTO;

namespace WeatherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("Default");

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new MissingFieldException("Failed to get Default connection string");

                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            app.MapGet("/api/v1/countries", ([FromServices] ApplicationDbContext database) =>
            {
                return database.Countries.ToList();
            });

            app.MapGet("/api/v1/countries/{id:int}", ([FromServices] ApplicationDbContext database, [FromRoute] int id) =>
            {
                Country? country = database.Countries.Find(id);

                if (country == null)
                    return Results.NotFound();

                return Results.Ok(country);
            });

            app.MapPost("/api/v1/countries", ([FromServices] ApplicationDbContext database, [FromBody] CountryDto countryDto) =>
            {
                if (string.IsNullOrWhiteSpace(countryDto.Code) || countryDto.Code.Length != 2)
                    return Results.BadRequest("Incorrect country code");

                if (string.IsNullOrWhiteSpace(countryDto.Name) || countryDto.Name.Length < 2 || countryDto.Name.Length > 256)
                    return Results.BadRequest("Incorrect country name");

                Country country = new Country()
                {
                    Code = countryDto.Code,
                    Name = countryDto.Name
                };

                database.Countries.Add(country);

                try
                {
                    database.SaveChanges();
                }
                catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Results.Ok(country);
            });

            app.Run();
        }
    }
}
