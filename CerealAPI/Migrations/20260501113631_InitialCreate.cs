using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cereal_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cereals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Mfr = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    Protein = table.Column<int>(type: "INTEGER", nullable: false),
                    Fat = table.Column<int>(type: "INTEGER", nullable: false),
                    Sodium = table.Column<int>(type: "INTEGER", nullable: false),
                    Fiber = table.Column<float>(type: "REAL", nullable: false),
                    Carbo = table.Column<float>(type: "REAL", nullable: false),
                    Sugars = table.Column<int>(type: "INTEGER", nullable: false),
                    Potass = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitamins = table.Column<int>(type: "INTEGER", nullable: false),
                    Shelf = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    Cups = table.Column<float>(type: "REAL", nullable: false),
                    Rating = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cereals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cereals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
