using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoncotesCountyLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    PatronId = table.Column<int>(type: "integer", nullable: false),
                    CheckoutDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CheckoutDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patron",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patron", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialName = table.Column<string>(type: "text", nullable: false),
                    MaterialTypeId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    OutOfCirculationSince = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Material_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Material_MaterialType_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Fantasy" },
                    { 3, "Mystery" },
                    { 4, "Biography" },
                    { 5, "SciFi" },
                    { 6, "Rock" },
                    { 7, "Pop" },
                    { 8, "Jazz" },
                    { 9, "Classical" },
                    { 10, "News" },
                    { 11, "Fashion" },
                    { 12, "Science" },
                    { 13, "Travel" },
                    { 14, "Cooking" }
                });

            migrationBuilder.InsertData(
                table: "MaterialType",
                columns: new[] { "Id", "CheckoutDays", "Name" },
                values: new object[,]
                {
                    { 1, 7, "Book" },
                    { 2, 5, "CD" },
                    { 3, 14, "Magazine" }
                });

            migrationBuilder.InsertData(
                table: "Patron",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IsActive", "LastName" },
                values: new object[,]
                {
                    { 1, "123 Main St", "john.smith@gmail.com", "John", true, "Smith" },
                    { 2, "342 Oh", "withers@gmail.com", "Bill", false, "Withers" },
                    { 3, "456 Oak Ave", "sarah.j@yahoo.com", "Sarah", true, "Johnson" },
                    { 4, "789 Pine Rd", "mbrown@hotmail.com", "Michael", true, "Brown" },
                    { 5, "321 Elm St", "emily.davis@gmail.com", "Emily", true, "Davis" },
                    { 6, "654 Maple Dr", "rwilson@outlook.com", "Robert", false, "Wilson" },
                    { 7, "987 Cedar Ln", "jess.martinez@gmail.com", "Jessica", true, "Martinez" },
                    { 8, "147 Birch Ct", "d.anderson@yahoo.com", "David", true, "Anderson" },
                    { 9, "258 Spruce Way", "lisa.t@gmail.com", "Lisa", false, "Taylor" },
                    { 10, "369 Willow Blvd", "jthomas@hotmail.com", "James", true, "Thomas" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "Id", "GenreId", "MaterialName", "MaterialTypeId", "OutOfCirculationSince" },
                values: new object[,]
                {
                    { 1, 1, "The Great Gatsby", 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "To Kill a Mockingbird", 1, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, "The Hobbit", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, "Dune", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, "The Da Vinci Code", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, "Steve Jobs", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 6, "Abbey Road", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 7, "Thriller", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 8, "Kind of Blue", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 9, "The Four Seasons", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 6, "Nevermind", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 12, "National Geographic - January 2024", 3, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 10, "Time Magazine - March 2024", 3, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 11, "Vogue - December 2023", 3, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 13, "Travel + Leisure - April 2024", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 14, "Bon Appétit - February 2024", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_GenreId",
                table: "Material",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutDays");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Patron");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "MaterialType");
        }
    }
}
