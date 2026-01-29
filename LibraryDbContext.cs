using Microsoft.EntityFrameworkCore;
using Library.Models;

public class LibraryDbContext : DbContext
{
    public DbSet<Checkout> Checkout { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<Material> Material { get; set; }
    public DbSet<MaterialType> MaterialType { get; set; }
    public DbSet<Patron> Patron { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            // Genres
            new Genre {Id=1, Name="Fiction"},
            new Genre {Id=2, Name="Fantasy"},
            new Genre {Id=3, Name="Mystery"},
            new Genre {Id=4, Name="Biography"},
            new Genre {Id=5, Name="SciFi"},
            new Genre {Id=6, Name="Rock"},
            new Genre {Id=7, Name="Pop"},
            new Genre {Id=8, Name="Jazz"},
            new Genre {Id=9, Name="Classical"},
            new Genre {Id=10, Name="News"},
            new Genre {Id=11, Name="Fashion"},
            new Genre {Id=12, Name="Science"},
            new Genre {Id=13, Name="Travel"},
            new Genre {Id=14, Name="Cooking"}
        });

        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
            new Patron {Id=1, FirstName="John", LastName="Smith", Address="123 Main St", Email="john.smith@gmail.com", IsActive=true},
            new Patron {Id=2, FirstName="Bill", LastName="Withers", Address="342 Oh", Email="withers@gmail.com", IsActive=false},
            new Patron {Id=3, FirstName="Sarah", LastName="Johnson", Address="456 Oak Ave", Email="sarah.j@yahoo.com", IsActive=true},
            new Patron {Id=4, FirstName="Michael", LastName="Brown", Address="789 Pine Rd", Email="mbrown@hotmail.com", IsActive=true},
            new Patron {Id=5, FirstName="Emily", LastName="Davis", Address="321 Elm St", Email="emily.davis@gmail.com", IsActive=true},
            new Patron {Id=6, FirstName="Robert", LastName="Wilson", Address="654 Maple Dr", Email="rwilson@outlook.com", IsActive=false},
            new Patron {Id=7, FirstName="Jessica", LastName="Martinez", Address="987 Cedar Ln", Email="jess.martinez@gmail.com", IsActive=true},
            new Patron {Id=8, FirstName="David", LastName="Anderson", Address="147 Birch Ct", Email="d.anderson@yahoo.com", IsActive=true},
            new Patron {Id=9, FirstName="Lisa", LastName="Taylor", Address="258 Spruce Way", Email="lisa.t@gmail.com", IsActive=false},
            new Patron {Id=10, FirstName="James", LastName="Thomas", Address="369 Willow Blvd", Email="jthomas@hotmail.com", IsActive=true}
        });

        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
            new MaterialType {Id=1, Name="Book", CheckoutDays=7},
            new MaterialType {Id=2, Name="CD", CheckoutDays=5},
            new MaterialType {Id=3, Name="Magazine", CheckoutDays=14}
        });

        modelBuilder.Entity<Material>().HasData(new Material[]
        {
           // Books (MaterialTypeId = 1)
            new Material {Id=1, MaterialName="The Great Gatsby", MaterialTypeId=1, GenreId=1, OutOfCirculationSince= new DateTime(2024,1,15)},
            new Material {Id=2, MaterialName="To Kill a Mockingbird", MaterialTypeId=1, GenreId=1, OutOfCirculationSince= new DateTime(2023,6,20)},
            new Material {Id=3, MaterialName="The Hobbit", MaterialTypeId=1, GenreId=2},
            new Material {Id=4, MaterialName="Dune", MaterialTypeId=1, GenreId=5},
            new Material {Id=5, MaterialName="The Da Vinci Code", MaterialTypeId=1, GenreId=3},
            new Material {Id=6, MaterialName="Steve Jobs", MaterialTypeId=1, GenreId=4},

            // CDs (MaterialTypeId = 2)
            new Material {Id=7, MaterialName="Abbey Road", MaterialTypeId=2, GenreId=6},
            new Material {Id=8, MaterialName="Thriller", MaterialTypeId=2, GenreId=7},
            new Material {Id=9, MaterialName="Kind of Blue", MaterialTypeId=2, GenreId=8},
            new Material {Id=10, MaterialName="The Four Seasons", MaterialTypeId=2, GenreId=9},
            new Material {Id=11, MaterialName="Nevermind", MaterialTypeId=2, GenreId=6},

            // Magazines (MaterialTypeId = 3)
            new Material {Id=12, MaterialName="National Geographic - January 2024", MaterialTypeId=3, GenreId=12, OutOfCirculationSince= new DateTime(2024,5,1)},
            new Material {Id=13, MaterialName="Time Magazine - March 2024", MaterialTypeId=3, GenreId=10, OutOfCirculationSince= new DateTime(2024,6,15)},
            new Material {Id=14, MaterialName="Vogue - December 2023", MaterialTypeId=3, GenreId=11, OutOfCirculationSince= new DateTime(2024,2,10)},
            new Material {Id=15, MaterialName="Travel + Leisure - April 2024", MaterialTypeId=3, GenreId=13},
            new Material {Id=16, MaterialName="Bon Appétit - February 2024", MaterialTypeId=3, GenreId=14 }
        });

        modelBuilder.Entity<Checkout>().HasData(new Checkout[]
        {
            // Active checkouts (no ReturnDate set)
            new Checkout {Id=1, MaterialId=3, PatronId=1, CheckoutDate=new DateTime(2026,1,22)},
            new Checkout {Id=2, MaterialId=7, PatronId=3, CheckoutDate=new DateTime(2026,1,24)},
            new Checkout {Id=3, MaterialId=15, PatronId=4, CheckoutDate=new DateTime(2026,1,20)},
            new Checkout {Id=4, MaterialId=4, PatronId=5, CheckoutDate=new DateTime(2026,1,23)},
            new Checkout {Id=5, MaterialId=11, PatronId=7, CheckoutDate=new DateTime(2026,1,25)},
            new Checkout {Id=6, MaterialId=16, PatronId=8, CheckoutDate=new DateTime(2026,1,21)},
            
            // Returned checkouts
            new Checkout {Id=7, MaterialId=3, PatronId=1, CheckoutDate=new DateTime(2026,1,10), ReturnDate=new DateTime(2026,1,16)},
            new Checkout {Id=8, MaterialId=5, PatronId=3, CheckoutDate=new DateTime(2026,1,8), ReturnDate=new DateTime(2026,1,14)},
            new Checkout {Id=9, MaterialId=7, PatronId=4, CheckoutDate=new DateTime(2026,1,5), ReturnDate=new DateTime(2026,1,9)},
            new Checkout {Id=10, MaterialId=9, PatronId=5, CheckoutDate=new DateTime(2025,12,20), ReturnDate=new DateTime(2025,12,24)},
            new Checkout {Id=11, MaterialId=15, PatronId=7, CheckoutDate=new DateTime(2025,12,15), ReturnDate=new DateTime(2025,12,28)},
            new Checkout {Id=12, MaterialId=6, PatronId=8, CheckoutDate=new DateTime(2026,1,1), ReturnDate=new DateTime(2026,1,7)},
            
            // Overdue checkouts (checked out but not returned, past due date)
            new Checkout {Id=13, MaterialId=8, PatronId=10, CheckoutDate=new DateTime(2026,1,15)}, // CD, 5 days = due 1/20
            new Checkout {Id=14, MaterialId=10, PatronId=1, CheckoutDate=new DateTime(2026,1,18)}, // CD, 5 days = due 1/23
        });

    }
}