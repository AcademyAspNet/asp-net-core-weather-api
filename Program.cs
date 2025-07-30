using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Data;
using WeatherAPI.Data.Entities;

namespace WeatherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();

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

            app.Run();
        }
    }
}
