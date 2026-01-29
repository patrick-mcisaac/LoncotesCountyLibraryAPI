using Library.Models;
using Library.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddNpgsql<LibraryDbContext>(builder.Configuration["LoncotesCountyLibraryDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Materials
app.MapGet("/api/materials", (LibraryDbContext db, int? materialTypeId, int? genreId) =>
{
    IQueryable<Material> materials = db.Material.Where(m => m.OutOfCirculationSince == new DateTime(0001, 01, 01, 0, 0, 0));

    if (materialTypeId != null)
    {
        materials = materials.Where(m => m.MaterialTypeId == materialTypeId);
    }

    if (genreId != null)
    {
        materials = materials.Where(m => m.GenreId == genreId);
    }

    return Results.Ok(materials
    .Select(m =>
    new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        GenreId = m.GenreId,
        OutOfCirculationSince = m.OutOfCirculationSince,
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        },
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            CheckoutDays = m.MaterialType.CheckoutDays
        }
    })
    .ToList());
});



app.Run();


