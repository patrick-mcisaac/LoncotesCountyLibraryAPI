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

app.MapGet("/api/materials/{id}", (LibraryDbContext db, int id) =>
{
    try
    {
        return Results.Ok(db.Material
        .Include(m => m.Genre)
        .Include(m => m.MaterialType)
        .Include(m => m.Checkouts)
        .ThenInclude(c => c.Patron)
        .Select(m => new MaterialDTO
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
            },
            Checkouts = m.Checkouts == null ? null : m.Checkouts.Select(c => new CheckoutDTO
            {
                Id = c.Id,
                PatronId = c.PatronId,
                CheckoutDate = c.CheckoutDate,
                ReturnDate = c.ReturnDate,
                Patron = new PatronDTO
                {
                    Id = c.Patron.Id,
                    FirstName = c.Patron.FirstName,
                    LastName = c.Patron.LastName,
                    Address = c.Patron.Address,
                    Email = c.Patron.Email,
                    IsActive = c.Patron.IsActive
                }
            }).ToList()

        }).Single(m => m.Id == id));
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});


app.MapPost("/api/materials", (LibraryDbContext db, Material material) =>
{
    db.Material.Add(material);
    db.SaveChanges();
    return Results.Created($"/api/materials/{material.Id}", material);
});

app.MapDelete("/api/materials/{id}", (LibraryDbContext db, int id) =>
{
    Material? material = db.Material.SingleOrDefault(m => m.Id == id);

    if (material == null)
    {
        return Results.NotFound();
    }
    db.Material.Remove(material);
    db.SaveChanges();
    return Results.NoContent();

});


// MaterialType

app.MapGet("/api/materialtypes", (LibraryDbContext db) =>
{
    IQueryable<MaterialType> materialType = db.MaterialType.AsQueryable();

    return Results.Ok(materialType.Select(m => new MaterialTypeDTO
    {
        Id = m.Id,
        Name = m.Name,
        CheckoutDays = m.CheckoutDays
    }).ToList());
});

app.Run();


