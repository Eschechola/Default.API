using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Default.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "otanimes");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "otanimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR(8)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Street = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    Complement = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "otanimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    PrimaryPhone = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    SecondaryPhone = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "otanimes");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "otanimes");
        }
    }
}
